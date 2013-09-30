using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ICA
{
    public class ICA
    {
        /// <summary>
        /// length of side of captured patch square
        /// </summary>
        public int Length { get; set; }
        public int Size
        {
            get { return Length * Length; }
        }
        /// <summary>
        /// filers (concatenated filer arrays)
        /// </summary>
        public double[] W { get; set; }

        private double[] samples;


        [DllImport("ICA.dll", EntryPoint = "Initialize")]
        private static extern void initialize(int dim, double[] w);
        // w : row-major (row = filter), data : column-major (column = image)
        [DllImport("ICA.dll", EntryPoint = "Learn")]
        private static extern double learn(int dim, double[] w, double[] data, int count, bool whiten, double rate);

        public IEnumerable<double> Filter(int index)
        {
            return W.Skip(index * Size).Take(Size);
        }

        public IEnumerable<double> Sample(int index)
        {
            return samples.Skip(index * Size).Take(Size);
        }

        public ICA(int length)
        {
            Length = length;
            W = new double[Size * Size];

            initialize(Size, W);
        }

        public void Normalize()
        {
            for (int i = 0; i < Size; i++)
            {
                double n = Math.Sqrt(Filter(i).Select(x => x * x).Sum());
                for (int j = 0; j < Size; j++)
                    W[i * Size + j] = W[i * Size + j] / n;
            }
        }

        // Each image stored in row-major format (beginning at top side)
        public void LoadSamples(string dir, int count, int samplesPerImage)
        {
            samples = new double[count * Size];
            string[] files = Directory.GetFiles(dir);
            var rand = new Random();

            for (int i = 0, c = 0; i * samplesPerImage < count; i++)
            {
                Bitmap bmp = new Bitmap(files[i]);
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                int w = bmpData.Width;
                int h = bmpData.Height;

                unsafe
                {
                    byte* p = (byte*)(void*)bmpData.Scan0;

                    for (int j = 0; j < samplesPerImage && c < count; j++)
                    {
                        int x0 = rand.Next(0, w - Length);
                        int y0 = rand.Next(0, h - Length);
                        for (int dy = 0; dy < Length; dy++)
                            for (int dx = 0; dx < Length; dx++)
                            {
                                int k = (y0 + dy) * bmpData.Stride + (x0 + dx) * 3;
                                samples[c * Size + dy * Length + dx] = (0.299 * p[k + 2] + 0.587 * p[k + 1] + 0.114 * p[k]) / 127.5 - 1.0;
                            }
                        c++;
                    }
                }

                bmp.UnlockBits(bmpData);
            }
        }

        public void LoadTestSamples(int count)
        {
            samples = new double[count * Size];
            var rand = new Random();

            for (int i = 0; i < count; i++)
            {
                int y = rand.Next(0, Length - 1);
                for (int x = 0; x < Length; x++)
                {
                    samples[i * Size + y * Length + x] = Math.Sqrt(rand.NextDouble());
                    samples[i * Size + (y + 1) * Length + x] = -Math.Sqrt(rand.NextDouble());
                }
            }
        }
        
        public double Learn(int count, bool whiten, double rate)
        {
            if (samples == null)
                throw new Exception("Samples not loaded.");
            else if (count * Size > samples.Length)
                throw new Exception("Too many samples required.");

            return learn(Size, W, samples, count, whiten, rate);
        }

        public double[] Encode(double[] data)
        {
            var r = new double[Size];

            for (int i = 0; i < Size; i++)
                r[i] = Filter(i).Zip(data, (a, b) => a * b).Sum();

            return r;
        }

        public static void SaveImage(double[] data, int width, int height, string path)
        {
            double max = data.Max();
            double min = data.Min();
            var bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    var v = (int)((data[y * width + x] - min) / (max - min) * 256);
                    bmp.SetPixel(x, y, Color.FromArgb(v, v, v));
                }

            bmp.Save(path);
        }

        public void LoadFiltersText(string file)
        {
            var a = new List<List<double>>();

            using (var reader = new System.IO.StreamReader(file))
            {
                string l;
                while ((l = reader.ReadLine()) != null)
                {
                    a.Add(new List<double>());
                    foreach (var s in l.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries))
                        a.Last().Add(double.Parse(s));
                }
            }
            if (!a.All(x => x.Count == Size))
                throw new Exception("Ill-formed data file.");

            this.W = a.SelectMany(x => x).ToArray();
        }

        public void SaveFitersAsText(string file)
        {
            using (var writer = new StreamWriter(file))
                for (int i = 0; i < Size; i++)
                    writer.WriteLine(string.Join(" ", Filter(i)));
        }
    }
}
