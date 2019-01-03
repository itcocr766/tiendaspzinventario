using MySql.Data.MySqlClient;
using POS.Modelo;
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

namespace POS.productosprincipal
{
    public partial class cargamasiva : Form
    {
        public cargamasiva()
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
                        if (dataGridView1.Rows.Count > 0&&dataGridView1.ColumnCount==11)
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
                MessageBox.Show(r.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Hand);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            try
            {

                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    for (int index = 0; index < dataGridView1.Rows.Count; index++)
                    {
                        mysql.cadenasql = "INSERT INTO `items`(`Barcode`, `Descripcion`, `Impuesto`, `Precio`, `OnHand`, `Costo`, `Marca`, `Talla`,`Familia`,`Estilo`,`Genero`) " +
                            "VALUES (@bar,@des,@imp,'@pre,@on,@cos,@mar,@tal,@fam,@est,@gen)";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.Parameters.AddWithValue("@bar", dataGridView1.Rows[index].Cells[0].Value);
                        mysql.comando.Parameters.AddWithValue("@des", dataGridView1.Rows[index].Cells[1].Value);
                        mysql.comando.Parameters.AddWithValue("@imp", dataGridView1.Rows[index].Cells[2].Value);
                        mysql.comando.Parameters.AddWithValue("@pre", dataGridView1.Rows[index].Cells[3].Value);
                        mysql.comando.Parameters.AddWithValue("@on", dataGridView1.Rows[index].Cells[4].Value);
                        mysql.comando.Parameters.AddWithValue("@cos", dataGridView1.Rows[index].Cells[5].Value);
                        mysql.comando.Parameters.AddWithValue("@mar", dataGridView1.Rows[index].Cells[6].Value);
                        mysql.comando.Parameters.AddWithValue("@tal", dataGridView1.Rows[index].Cells[7].Value);
                        mysql.comando.Parameters.AddWithValue("@fam", dataGridView1.Rows[index].Cells[8].Value);
                        mysql.comando.Parameters.AddWithValue("@est", dataGridView1.Rows[index].Cells[9].Value);
                        mysql.comando.Parameters.AddWithValue("@gen", dataGridView1.Rows[index].Cells[10].Value);
                        mysql.comando.ExecuteNonQuery();
                    }
                    mysql.Dispose();

                }



                MessageBox.Show("Se ingresaron: " + dataGridView1.Rows.Count + " registros a la base de datos");
                button2.Enabled = false;

                dataGridView1.DataSource = null;
            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
