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

        private void button2_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                button2.Text = "Stop modulation";
                timer1.Start();
                flag = true;
            }
            else
            {
                button2.Text = "Run modulation";
                timer1.Stop();
                flag = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            try
            {
                if (numericUpDown1.Value == 0)
                    throw new ArgumentException();
                p = new Particle[(int)numericUpDown1.Value - 1];
            }
            catch
            {
                MessageBox.Show("Check input data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            for (int i = 0; i < p.Length; i++)
                p[i] = new Particle();
            pc = new ParticleWithCoord();


            int midle_speed = Convert.ToInt32(Math.Sqrt(3 * 0.013 * trackBar1.Value));

            for (int j = 0; j < p.Length; j++)
            {
                p[j].SetMaxSpeed = midle_speed + 1;
                p[j].X = rnd.Next(pictureBox1.Width);
                p[j].Y = rnd.Next(pictureBox1.Height);
                p[j].X_Speed = rnd.Next(-midle_speed, midle_speed);
                p[j].Y_Speed = rnd.Next(-midle_speed, midle_speed);

                drawPoint(Convert.ToInt32(p[j].X), Convert.ToInt32(p[j].Y), pictureBox1);

            }
            pc.SetMaxSpeed = midle_speed + 1;
            pc.X = rnd.Next(700);
            pc.Y = rnd.Next(350);
            pc.X_Speed = rnd.Next(-midle_speed, midle_speed);
            pc.Y_Speed = rnd.Next(-midle_speed, midle_speed);

            drawPoint(Convert.ToInt32(pc.X), Convert.ToInt32(pc.Y), pictureBox1);
            button2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            for (int i = 0; i < p.Length; i++)
            {
                p[i].Move(pictureBox1.Size);
                p[i].repulsionPower(p);

            }
            pc.Move(pictureBox1.Size);
            pc.repulsionPower(p);

            for (int j = 0; j < p.Length; j++)
                drawPoint(Convert.ToInt32(p[j].X), Convert.ToInt32(p[j].Y), pictureBox1);
            drawPoint(Convert.ToInt32(pc.X), Convert.ToInt32(pc.Y), pictureBox1);

            if (radioButton1.Checked)
            {
                //=================
                label3.Text = "Time modulation:\n" + pc.ActionTime + " ms";
                //==================
                if (pc.ActionTime >= numericUpDown2.Value)
                {
                    flag = false;
                    timer1.Stop();
                    pc.ActionTime = 0;
                    button2.Text = "Run modulation";
                }
            }
            else if (radioButton2.Checked)
            {
                label3.Text = "Iteration: " + ((int)pc.ActionTime / 100).ToString();
                if (pc.ActionTime / 100 >= numericUpDown2.Value)
                {
                    flag = false;
                    timer1.Stop();
                    pc.ActionTime = 0;
                    button2.Text = "Run modulation";
                }
            }
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

        //private void Timer2_Tick(object sender, EventArgs e)
        //{
        //    pictureBox1.Refresh();
        //    Particle[] pr = new Particle[2];
        //    pr[0] = p1;
        //    pr[1] = p2;
        //    p1.Move(pictureBox1.Size);
        //    p2.Move(pictureBox1.Size);
        //    p1.repulsionPower(pr);

        //    drawPoint(Convert.ToInt32(p1.X), Convert.ToInt32(p1.Y), pictureBox1);
        //    drawPoint(Convert.ToInt32(p2.X), Convert.ToInt32(p2.Y), pictureBox1);
        //}
    }
}
