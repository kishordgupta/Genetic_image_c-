using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Decoder : Form
    {
        Bitmap b = null;
        Bitmap b2 = null;
        int size = 10;
        int Bsize = Config.SIZE;
        string result;
        string h = "h";
        string w = "w";
        public Decoder()
        {
            Bsize = Config.SIZE;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                 //   b = (Bitmap)Image.FromFile(file);// resizeImage(new Bitmap(file),new Size(1024, 1024));
                    b= ResizeImage(new Bitmap(file), new Size(Bsize, Bsize));
                    pictureBox1.Image = b;// new Bitmap(file);
                 
                    button1.Enabled = true;
                }
                catch (System.IO.IOException)
                {
                }
            }
        }
        public static Bitmap ResizeImage(Image image, Size sizea)
        { 
            return (new Bitmap(image, sizea));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            System.Drawing.Imaging.BitmapData objectsData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
             System.Drawing.Imaging.ImageLockMode.ReadOnly, b.PixelFormat);

            AForge.Imaging.UnmanagedImage grayImage = AForge.Imaging.Filters.Grayscale.CommonAlgorithms.BT709.Apply(new AForge.Imaging.UnmanagedImage(objectsData));
            AForge.Imaging.Filters.Threshold th = new AForge.Imaging.Filters.Threshold(128);
            th.ApplyInPlace(grayImage);

            // unlock image
            b.UnlockBits(objectsData);
            pictureBox1.Image = grayImage.ToManagedImage(); ;
            b = grayImage.ToManagedImage();
            int count=0;
            b2 = ResizeImage(b, new Size(size, size));
            String s = "";
            pictureBox2.Image = b2;
            for (int i = 0; i < Bsize; i++)
            {
                int k = 0;
                int t = 0;
                for (int j = 0; j < Bsize; j++)
                {
                    if (b.GetPixel(i, j).R > 100 && b.GetPixel(i, j).G > 100 &&
                             b.GetPixel(i, j).B > 100)
                    {
                        s = s + 0;
                    }
                    else
                    {
                        s = s + 1;
                        count++;
                        k++;
                    }
                    if (b.GetPixel(j, i).R > 100 && b.GetPixel(j, i).G > 100 &&
                            b.GetPixel(j, i).B > 100)
                    {
                       
                    }
                    else
                    {
                        t++;
                    }
                   
                }
                richTextBox2.Text = richTextBox2.Text + k + "";
                richTextBox1.Text = richTextBox1.Text+ t + "\n";
                h = h+ "," + k;
                w = w + "," + t;
                s = s + "\n";
                 result = string.Join("", h);
                result =count+"\n"+ h + "\n" + w;
            }
            label1.Text = "Total 1 is " + count;
         //   System.IO.File.WriteAllText( "test.txt", s);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Images|*.png;*.bmp;*.jpg";

            if (dialog.ShowDialog() == DialogResult.OK)
            {

                if (b2 != null)
                {
                    b2.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    System.IO.File.WriteAllText(dialog.FileName.Replace('.',' ')+".txt", result);
                }
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Config.SIZE = (int)numericUpDown2.Value;
            Bsize = Config.SIZE;
        }
    }
}
