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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gaform ga = new Gaform();
            ga.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Decoder ds = new Decoder();
            ds.Show();
        }
    }
}
