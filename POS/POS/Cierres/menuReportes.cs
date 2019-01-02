using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Reportes;
namespace POS.Cierres
{
    public partial class menuReportes : Form
    {
        public menuReportes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            contabilidad cnt = new contabilidad();
            cnt.Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vent vpv = new vent();
            vpv.Show(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ventasdiarias vd = new ventasdiarias();
            vd.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }
    }
}
