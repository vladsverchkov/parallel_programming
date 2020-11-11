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
        List<Particle> molecules;
        int speed;
        CancellationTokenSource cancelToken;
        CancellationToken ct;
        public FormMain()
        {
            InitializeComponent();
            this.molecules = new List<Particle>();
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
