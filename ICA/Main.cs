using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ICA
{
    public partial class Main : Form
    {
        string baseTitle = "ICA";
        
        Filters filtersWindow;
        Analyzer analyzerWindow;
        ICA ica;

        private int length
        {
            get { return int.Parse(textboxSize.Text); }
        }

        public Main()
        {
            InitializeComponent();
            filtersWindow = new Filters();
            analyzerWindow = new Analyzer(Analyze);
        }

        void ShowFilters()
        {
            if (!filtersWindow.Visible)
                filtersWindow.Show();

            filtersWindow.ShowData(ica);
        }

        void InitializeICA()
        {
            ica = new ICA(length);

            ShowFilters();
        }

        void SetMessage(string msg)
        {
            this.Text = baseTitle + " - " + msg;
        }

        void ClearMessage()
        {
            this.Text = baseTitle;
        }

        double[] ParseRate()
        {
            var r = new List<double>();

            foreach (var line in textboxRate.Text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Match m;
                if ((m = Regex.Match(line, @"([0-9]*\.[0-9]*(?<!^\.))\s*x\s*([0-9]+)")).Success)
                    r.AddRange(Enumerable.Repeat(double.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value)));
                else if ((m = Regex.Match(line, @"[0-9]*\.[0-9]*(?<!^\.)")).Success)
                    r.Add(double.Parse(line));
            }

            return r.ToArray();
        }

        void Learn()
        {
            var d = length * length;
            var dataCount = int.Parse(textboxPatches.Text);
            var data = new double[dataCount * d];
            var imgSamples = int.Parse(textboxSamples.Text);
            var whiten = checkboxWhiten.Checked;
            var rate = ParseRate();
            
            ica.LoadSamples(textboxDirectory.Text, dataCount, imgSamples);
            //ica.LoadTestSamples(dataCount);
            double j = 0;
            for (int i = 0; i < rate.Length; i++)
            {
                j = ica.Learn(dataCount, whiten, rate[i]);
                this.Invoke((Action)(() => SetMessage((100.0 * i / rate.Length).ToString() + " % " + j.ToString())));
            }

            this.Invoke((Action)(() => ClearMessage()));
        }

        private void Encode(string src, int cutoff)
        {
            var bmp = new Bitmap(src);
            var reconst = new Bitmap(bmp.Width, bmp.Height);

            for (int i = 0; i < bmp.Width / length; i++)
                for (int j = 0; j < bmp.Height / length; j++)
                {
                    double[] p = new double[length * length];
                    for (int y = 0; y < length; y++)
                        for (int x = 0; x < length; x++)
                            p[y * length + x] = bmp.GetPixel(i * length + x, j * length + (length - 1 - y)).GetBrightness();

                    double[] s = ica.Encode(p);

                    var factors = s.Select((a, m) => new Tuple<double, int>(a, m)).OrderByDescending(t => Math.Abs(t.Item1));
                    var rec = factors.Take(cutoff).Select(t => ica.W.Skip(t.Item2 * length * length).Take(length * length).Select(x => t.Item1 * x)).Aggregate((u, v) => u.Zip(v, (a, b) => a + b)).ToArray();
                    for (int y = 0; y < length; y++)
                        for (int x = 0; x < length; x++)
                        {
                            var c = (int)(rec[y * length + x] * 255);
                            if (c < 0) c = 0;
                            else if (c > 255) c = 255;
                            reconst.SetPixel(i * length + x, j * length + (length - 1 - y), Color.FromArgb(c, c, c));
                        }
                }

            picturePatch.Image = reconst;
        }

        private void Analyze(double[] patch)
        {
            ShowPatch(patch);

            var u = ica.Encode(patch);

            filtersWindow.ShowData(ica, u);
        }

        private void ShowPatch(double[] patch)
        {
            int width = ica.Length;
            int height = ica.Length;
            var bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    var i = (int)(patch[y * width + x] * 255);
                    bmp.SetPixel(x, y, Color.FromArgb(i, i, i));
                }

            picturePatch.Image = bmp;
        }

        private void buttonInit_Click(object sender, EventArgs e)
        {
            InitializeICA();
        }

        private async void buttonRun_Click(object sender, EventArgs e)
        {
            if (ica.W == null)
                InitializeICA();

            var cancel = new CancellationTokenSource();
            buttonRun.Enabled = false;

            var task = Task.Run((Action)(() => {
                while (!cancel.IsCancellationRequested)
                {
                    Thread.Sleep(200);
                    this.Invoke((Action)ShowFilters);
                }
            }), cancel.Token);
            
            await Task.Run((Action)Learn);

            cancel.Cancel();
            buttonRun.Enabled = true;
        }

        private void buttonSample_Click(object sender, EventArgs e)
        {
            if (!filtersWindow.Visible)
                filtersWindow.Show();

            ica.LoadSamples(textboxDirectory.Text, ica.Size, int.Parse(textboxSamples.Text));
            //ica.LoadTestSamples(ica.Size);
            
            filtersWindow.ShowPatches(ica, 0);
        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            ica.SaveFitersAsText(textboxOutput.Text);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (ica == null)
                InitializeICA();

            ica.LoadFiltersText(textboxOutput.Text);
            ica.Normalize();
            ShowFilters();
        }

        private void buttonAnalyze_Click(object sender, EventArgs e)
        {
            if (!analyzerWindow.Visible)
                analyzerWindow.Show();

            analyzerWindow.SetLength(ica.Length);
            analyzerWindow.LoadImage(textboxImage.Text);
        }
    }
}
