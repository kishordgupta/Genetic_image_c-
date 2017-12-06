using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Gaform : Form
    {
        static int size = Config.SIZE;
        int maincount = 0;
        int[] h = new int[size];
        int[] w = new int[size];
        internal static string getindividual(int i)
        {
            return population[i].getchromosome();
        }

        Bitmap b;

        public static Indivdual[] population = new Indivdual[24];
        public Gaform()
        {
            size = Config.SIZE;
            InitializeComponent();
            chart1.AddDataSeries("Test", Color.Red, AForge.Controls.Chart.SeriesType.ConnectedDots, 5);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] h = new int[size];
            int[] w = new int[size];
            Config.baseh = new int[size];
            Config.basew= new int[size];
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    b = ResizeImage(new Bitmap(file), new Size(size, size));
                    string[] lines = System.IO.File.ReadAllLines(openFileDialog1.FileName.Replace('.',' ')+".txt");
                    label1.Text = lines[0];
                    maincount = Int32.Parse(lines[0]);
                    Config.basecount = maincount;
                    pictureBox1a.Image = b;// new Bitmap(file);
                    string[] hlines= lines[1].Split(',');
                    string[] wlines = lines[2].Split(',');
                    for (int i = 1; i <= size; i++)
                    {
                        Config.baseh[i-1] = Int32.Parse(hlines[i]);
                        Config.basew[i-1] = Int32.Parse(wlines[i]);
                       
                    }
                  
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
            int count = 0;
            byte[,] sample =new byte[size,size];

            for (int i = 0; i < size; i++)
            {
              
                for (int j = 0; j < size; j++)
                {
                    if (b.GetPixel(i, j).R > 100 && b.GetPixel(i, j).G > 100 &&
                             b.GetPixel(i, j).B > 100)
                    {
                        sample[i, j] =0;
                    }
                    else
                    {
                        sample[i,j] = 1;
                        count++;
                    }
                
                }
               
            }
            Config.baseIndivdual = (byte[,])sample.Clone();
            Config.basediif =  count- maincount;
            label1.Text = "Total 1 is " + count;
            initia();
        }

        void imagshow()
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            richTextBox4.Text = "";
            richTextBox5.Text = "";
            richTextBox6.Text = "";
         
            for (int i = 0; i < population.Length; i++)
            {

              
                richTextBox1.Text = richTextBox1.Text + i  +"\n";
                richTextBox2.Text = richTextBox2.Text +  population[i].totalfitness + "\n";
                richTextBox3.Text = richTextBox3.Text + population[i].fitnessvalueh + "\n";
                richTextBox4.Text = richTextBox4.Text + population[i].fitnessvaluew + "\n";
                richTextBox5.Text = richTextBox5.Text + population[i].fitnessvalueb + "\n";
                richTextBox6.Text = richTextBox6.Text + population[i].rcs + "\n";
                population[i].generateimage();//.Save(i + ".png");
            }
      
            pictureBox1.Image = population[0].pic;
            pictureBox2.Image = population[1].pic;
            pictureBox3.Image = population[2].pic;
            pictureBox4.Image = population[3].pic;
            pictureBox5.Image = population[4].pic;
            pictureBox6.Image = population[5].pic;
            pictureBox7.Image = population[6].pic;
            pictureBox8.Image = population[7].pic;
            pictureBox9.Image = population[9 - 1].pic;
            pictureBox10.Image = population[10 - 1].pic;
            pictureBox11.Image = population[11 - 1].pic;
            pictureBox12.Image = population[12 - 1].pic;
            pictureBox13.Image = population[13 - 1].pic;
            pictureBox14.Image = population[14 - 1].pic;
            pictureBox15.Image = population[15 - 1].pic;
            pictureBox16.Image = population[16 - 1].pic;
            pictureBox17.Image = population[17 - 1].pic;
            pictureBox18.Image = population[18 - 1].pic;
            pictureBox19.Image = population[19 - 1].pic;
            pictureBox20.Image = population[20 - 1].pic;
            pictureBox21.Image = population[21 - 1].pic;
            pictureBox22.Image = population[22 - 1].pic;
            pictureBox23.Image = population[23 - 1].pic;
            pictureBox24.Image = population[24 - 1].pic;

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label5.Text = MainGA.na.ToString();
            imagshow();
            if (a.genartationtime == 0)
            {
                button4.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
            }
            // fill data series
           
            // add new data series to the chart
          
            // set X range to display
          chart1.RangeX = new AForge.Range(0, 99);
            // update the chart
          chart1.UpdateDataSeries("Test", MainGA.testValues);
            //  chart1.AddDataSeries
        }

        public void initia()
        {
            for (int i = 0;i<population.Length; i++)
            {
              //  Console.Write(i+" ");
                Indivdual id = new Indivdual();
                id.generategenes();
         
                population[i] = id;
            }
      
        }
        MainGA a = new MainGA();
        private void button4_Click(object sender, EventArgs e)
        {
            a.genartationtime = numericUpDown3.Value;

            Console.Write("clicked");
            button4.Enabled = false;
            numericUpDown2.Enabled = false;
             numericUpDown3.Enabled = false;
            a.GA();
          //  ThreadStart childref = new ThreadStart(threads);
          //  Console.WriteLine("In Main: Creating the Child thread");
          //  Thread childThread = new Thread(childref);
         //   childThread.Start();
        //    threads();
        }
     
            public void threads() {

            while (true)
            {
                if (a.genartationtime == 0)
                {
                  
                    button4.Enabled = true;
                    numericUpDown2.Enabled = true;
                    numericUpDown3.Enabled = true;
                    label1.Text = (numericUpDown3.Value - a.genartationtime).ToString();
                    break;
                }
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            a.mutationrate = (int)numericUpDown1.Value;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Config.SIZE = (int)numericUpDown2.Value; size = Config.SIZE;
            if (size != 20)
                Config.TS = 10;
            else
                Config.TS = 1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Config.refimage = checkBox1.Checked;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            a.genartationtime = numericUpDown3.Value;

        }
    }
}
