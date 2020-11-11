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

namespace PP_Lab_3
{
    public partial class FormMain : Form
    {
       
        Particle p1 = new Particle();
        Particle p2 = new Particle();
        bool flag = false;
        System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();

        Particle[] p;
        ParticleWithCoord pc;
        double[] xCoor;
        double[] yCoor;
        public FormMain()
        {
            InitializeComponent();        
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            label3.Text = "";
            button2.Enabled = false;
            pictureBox1.Visible = true;
            trackBar1.Value = 300;
        }

        private void temperatureInput_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label4.Text = trackBar1.Value.ToString();
        }

        public void drawPoint(int x, int y, PictureBox pictureBox1)
        {

            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            SolidBrush brush = new SolidBrush(Color.Cyan);
            Point dPoint = new Point(x, (pictureBox1.Height - y));
            dPoint.X = dPoint.X - 2;
            dPoint.Y = dPoint.Y - 2;
            Rectangle rect = new Rectangle(dPoint, new Size(4, 4));
            g.FillRectangle(brush, rect);
            g.Dispose();
        }
    }
}
