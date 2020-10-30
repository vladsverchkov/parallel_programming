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
        List<Molecule> molecules;
        int speed;
        CancellationTokenSource cancelToken;
        CancellationToken ct;
        public FormMain()
        {
            InitializeComponent();
            this.molecules = new List<Molecule>();
        }


        private void refreshButton_Click(object sender, EventArgs e)
        {
            
        }

        private void temperatureInput_ValueChanged(object sender, EventArgs e)
        {
           
        }
    }
}
