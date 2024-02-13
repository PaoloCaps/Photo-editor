using Microsoft.VisualBasic.ApplicationServices;

namespace Grayscale
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.AllowDrop = true;
            pictureBox1.DragDrop += pictureBox1_DragDrop;
            pictureBox1.DragEnter += pictureBox1_DragEnter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);

                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color pixelColorBefore = bmp.GetPixel(x, y);

                        // Get the default RGB values of the pixel
                        int defaultR = pixelColorBefore.R;
                        int defaultG = pixelColorBefore.G;
                        int defaultB = pixelColorBefore.B;

                        // Calculate grayscale intensity using weighted averages of the RGB components,
                        // reflecting the perceived brightness of each color channel
                        int avg = (int)(0.2989 * defaultR + 0.5870 * defaultG + 0.1140 * defaultB);

                        // Create a new grayscale color using the calculated intensity
                        Color setPixelColor = Color.FromArgb(avg, avg, avg);

                        // Set the pixel in the bitmap to the grayscale color
                        bmp.SetPixel(x, y, setPixelColor);
                    }
                }

                

                // Display the modified image in pictureBox2
                pictureBox2.Image = (Image)bmp.Clone();
            }
        }


        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var fileNames = data as string[];
                if (fileNames.Length > 0)
                    pictureBox1.Image = Image.FromFile(fileNames[0]);
            }
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}
