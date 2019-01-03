using Spire.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Anular
{
    public partial class anularvarias : Form
    {
        public anularvarias()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                button1.Enabled = false;



                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = @"C:\";
                ofd.Filter = "Excel Worksheets|*.xlsx";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = ofd.FileName;
                    string fileName;
                    fileName = textBox1.Text;
                    Workbook workbook = new Workbook();
                    workbook.LoadFromFile(fileName);
                    Worksheet sheet = workbook.Worksheets[0];
                    dataGridView1.DataSource = sheet.ExportDataTable();
                    if (dataGridView1.Rows.Count > 0)
                    {
                        button2.Enabled = true;


                    }
                    else
                    {
                        MessageBox.Show("Ingrese los datos del archivo. Asegurese que la ruta sea la correcta", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        button1.Enabled = true;
                        button2.Enabled = false;

                    }

                }



            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
