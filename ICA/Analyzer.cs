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
    public partial class Analyzer : Form
    {
        Bitmap image;
        Action<double[]> patchReceiver;
        int length;
        Rectangle imageArea;

        public Analyzer(Action<double[]> patchReceiver = null)
        {
            InitializeComponent();

            this.patchReceiver = patchReceiver;
        }

        public void LoadImage(string src)
        {
            image = new Bitmap(src);

            pictureBox1.Image = image;
            this.ClientSize = image.Size;
            imageArea = new Rectangle(length / 2, length / 2, image.Width - length, image.Height - length);
        }

        public void SetLength(int length)
        {
            this.length = length;
            if (image != null)
                imageArea = new Rectangle(length / 2, length / 2, image.Width - length, image.Height - length);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (image != null && patchReceiver != null
                && imageArea.Contains(e.X, e.Y))
            {
                var patch = new double[length * length];

                for (int y = 0; y < length; y++)
                    for (int x = 0; x < length; x++)
                        patch[y * length + x] = image.GetPixel(e.X + x - length / 2, e.Y + y - length / 2).GetBrightness();

                patchReceiver(patch);
            }
        }

        private void Analyzer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }
    }
}
