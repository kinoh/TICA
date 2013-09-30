using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICA
{
    public partial class Filters : Form
    {
        public Filters()
        {
            InitializeComponent();
        }

        public void ShowData(ICA ica, double[] activity = null)
        {
            int l = ica.Length;
            var bmp = new Bitmap((l + 1) * l - 1, (l + 1) * l - 1);

            for (int i = 0; i < ica.Size; i++)
            {
                double[] patch = ica.Filter(i).ToArray();
                double min = patch.Min();
                double max = patch.Max();
                if (min == max)
                    continue;
                int x0 = (i % l) * (l + 1);
                int y0 = (i / l) * (l + 1);
                double a = double.NaN;
                if (activity != null)
                {
                    a = Math.Abs(activity[i]);
                    if (a > 1) a = 1;
                }

                for (int y = 0; y < l; y++)
                    for (int x = 0; x < l; x++)
                    {
                        var v = (int)((patch[y * l + x] - min) / (max - min) * 255);
                        Color c = (double.IsNaN(a))
                            ? Color.FromArgb(v, v, v)
                            : Color.FromArgb((int)(255 * a), v, (int)(255 * (1 - a)));
                        bmp.SetPixel(x0 + x, y0 + y, c);
                    }
            }

            pictureBox1.Image = bmp;
        }

        public void ShowPatches(ICA ica, int offset)
        {
            int l = ica.Length;
            var bmp = new Bitmap((l + 1) * l - 1, (l + 1) * l - 1);

            for (int i = 0; i < ica.Size; i++)
            {
                double[] patch = ica.Sample(offset + i).ToArray();
                int x0 = (i % l) * (l + 1);
                int y0 = (i / l) * (l + 1);

                for (int y = 0; y < l; y++)
                    for (int x = 0; x < l; x++)
                    {
                        int val = (int)(127.5 * patch[y * l + x] + 127.5);
                        bmp.SetPixel(x0 + x, y0 + y, Color.FromArgb(val, val, val));
                    }
            }

            pictureBox1.Image = bmp;
        }

        public void SaveData(string path)
        {
            pictureBox1.Image.Save(path);
        }

        private void Filters_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }
    }
}
