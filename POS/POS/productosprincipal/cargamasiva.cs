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
                        dataGridView1.DataSource = null;
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
            //insertes();
            crearreporte();
        }

        private void cargamasiva_Load(object sender, EventArgs e)
        {

        }

        public void insertes(string bar,string des,string imp,string pre,string on,string cos,string mar,string tal,
            string fam, string est,string gen)
        {
            try
            {

                using (var mysql = new Mysql())
                {
                    mysql.conexion();


                   
                        mysql.cadenasql = "INSERT INTO `items`(`Barcode`, `Descripcion`, `Impuesto`, `Precio`, `OnHand`, `Costo`, `Marca`, `Talla`,`Familia`,`Estilo`,`Genero`) " +
                            "VALUES (@bar,@des,@imp,@pre,@on,@cos,@mar,@tal,@fam,@est,@gen)";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.Parameters.AddWithValue("@bar", bar);
                        mysql.comando.Parameters.AddWithValue("@des", des);
                        mysql.comando.Parameters.AddWithValue("@imp", imp);
                        mysql.comando.Parameters.AddWithValue("@pre", pre);
                        mysql.comando.Parameters.AddWithValue("@on", on);
                        mysql.comando.Parameters.AddWithValue("@cos", cos);
                        mysql.comando.Parameters.AddWithValue("@mar", mar);
                        mysql.comando.Parameters.AddWithValue("@tal", tal);
                        mysql.comando.Parameters.AddWithValue("@fam", fam);
                        mysql.comando.Parameters.AddWithValue("@est", est);
                        mysql.comando.Parameters.AddWithValue("@gen", gen);
                        mysql.comando.ExecuteNonQuery();
                  
                    mysql.Dispose();

                }

            }
            catch (MySqlException m)
            {
                MessageBox.Show("Existen datos incorrectos en el archivo.\n" +
                    "Por favor asegurese que no existan valores repetidos, " +
                    "que ya existen en la base de datos,que no hayan valores nulos o sin ningún valor ó caractéres especiales", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void updates(string bar, string des, string imp, string pre, string on, string cos, string mar, string tal,
            string fam, string est, string gen)
        {
            try
            {

                using (var mysql = new Mysql())
                {
                    mysql.conexion();

                    mysql.cadenasql = "UPDATE `items` SET `Barcode`=@bar," +
                        "`Descripcion`=@des,`Impuesto`=@imp,`Precio`=@pre," +
                        "`OnHand`=OnHand+@on,`Costo`=@cos,`Marca`=@mar,`Talla`=@tal," +
                        "`Familia`=@fam,`Estilo`=@est,`Genero`=@gen WHERE Barcode=@bar";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.Parameters.AddWithValue("@bar", bar);
                    mysql.comando.Parameters.AddWithValue("@des", des);
                    mysql.comando.Parameters.AddWithValue("@imp", imp);
                    mysql.comando.Parameters.AddWithValue("@pre", pre);
                    mysql.comando.Parameters.AddWithValue("@on", on);
                    mysql.comando.Parameters.AddWithValue("@cos", cos);
                    mysql.comando.Parameters.AddWithValue("@mar", mar);
                    mysql.comando.Parameters.AddWithValue("@tal", tal);
                    mysql.comando.Parameters.AddWithValue("@fam", fam);
                    mysql.comando.Parameters.AddWithValue("@est", est);
                    mysql.comando.Parameters.AddWithValue("@gen", gen);
                    mysql.comando.ExecuteNonQuery();

                    mysql.Dispose();

                }


            }
            catch (MySqlException m)
            {
                MessageBox.Show("Existen datos incorrectos en el archivo.\n" +
                    "Por favor asegurese que no existan valores repetidos, " +
                    "que ya existen en la base de datos,que no hayan valores nulos o sin ningún valor ó caractéres especiales", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void crearreporte()
        {
            int y = 1;
            try
            {


                Workbook workbook = new Workbook();

                Worksheet sheet = workbook.Worksheets[0];
                for (int x = 0; x < dataGridView1.Rows.Count; x++)
                {
                    using (var mysql=new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "select count(*),Barcode,Descripcion,Precio,OnHand from items where Barcode=@co";
                        mysql.comando = new MySqlCommand(mysql.cadenasql,mysql.con);
                        mysql.comando.Parameters.AddWithValue("@co", dataGridView1.Rows[x].Cells[0].Value.ToString());
                        mysql.comando.ExecuteNonQuery();


                        using (MySqlDataReader lee=mysql.comando.ExecuteReader())
                        {
                            while (lee.Read())
                            {
                                if (Int64.Parse(lee["count(*)"].ToString())>0)
                                {
                                    if (x==0)
                                    {
                                        updates(dataGridView1.Rows[x].Cells[0].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[1].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[2].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[3].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[4].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[5].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[6].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[7].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[8].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[9].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[10].Value.ToString());
                                        sheet.Range["A" + y].Text = "Código";
                                        sheet.Range["B" + y].Text = "Descripción";
                                        sheet.Range["C" + y].Text = "Precio";
                                        sheet.Range["D" +y].Text = "Cantidad";
                                        sheet.Range["E" + y].Text = "Cantidad Total";
                                        sheet.Range["F" + y].Text = "Suma precio Total";
                                        y++;
                                        sheet.Range["A" + y].Text = lee["Barcode"].ToString();
                                        sheet.Range["B" + y].Text = lee["Descripcion"].ToString();
                                        sheet.Range["C" + y].NumberValue = double.Parse(lee["Precio"].ToString());
                                        sheet.Range["D" + y].NumberValue = Int64.Parse(lee["OnHand"].ToString());
                                        sheet.Range["E" + y].NumberValue = (double.Parse(lee["OnHand"].ToString()) + double.Parse(dataGridView1.Rows[x].Cells[4].Value.ToString()));
                                        sheet.Range["F" + y].NumberValue = ((double.Parse(lee["Precio"].ToString()) * (double.Parse(lee["OnHand"].ToString()) + double.Parse(dataGridView1.Rows[x].Cells[4].Value.ToString()))));
                                        y++;

                                    }
                                    else
                                    {
                                        updates(dataGridView1.Rows[x].Cells[0].Value.ToString(),
                                      dataGridView1.Rows[x].Cells[1].Value.ToString(),
                                      dataGridView1.Rows[x].Cells[2].Value.ToString(),
                                      dataGridView1.Rows[x].Cells[3].Value.ToString(),
                                      dataGridView1.Rows[x].Cells[4].Value.ToString(),
                                      dataGridView1.Rows[x].Cells[5].Value.ToString(),
                                      dataGridView1.Rows[x].Cells[6].Value.ToString(),
                                      dataGridView1.Rows[x].Cells[7].Value.ToString(),
                                      dataGridView1.Rows[x].Cells[8].Value.ToString(),
                                      dataGridView1.Rows[x].Cells[9].Value.ToString(),
                                      dataGridView1.Rows[x].Cells[10].Value.ToString());
                                        sheet.Range["A" + y].Text = lee["Barcode"].ToString();
                                        sheet.Range["B" + y].Text = lee["Descripcion"].ToString();
                                        sheet.Range["C" + y].NumberValue = double.Parse(lee["Precio"].ToString());
                                        sheet.Range["D" + y].NumberValue = Int64.Parse(lee["OnHand"].ToString());
                                        sheet.Range["E" + y].NumberValue = (double.Parse(lee["OnHand"].ToString()) + double.Parse(dataGridView1.Rows[x].Cells[4].Value.ToString()));
                                        sheet.Range["F" + y].NumberValue = ((double.Parse(lee["Precio"].ToString()) * (double.Parse(lee["OnHand"].ToString()) + double.Parse(dataGridView1.Rows[x].Cells[4].Value.ToString()))));
                                        y++;
                                    }
                                  
                            
                                }
                                else
                                {

                                    if (x == 0)
                                    {
                                        insertes(dataGridView1.Rows[x].Cells[0].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[1].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[2].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[3].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[4].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[5].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[6].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[7].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[8].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[9].Value.ToString(),
                                 dataGridView1.Rows[x].Cells[10].Value.ToString());
                                        sheet.Range["A" + y].Text = "Código";
                                        sheet.Range["B" + y].Text = "Descripción";
                                        sheet.Range["C" + y].Text = "Precio";
                                        sheet.Range["D" + y].Text = "Cantidad";
                                        sheet.Range["E" + y].Text = "Cantidad Total";
                                        sheet.Range["F" + y].Text = "Suma precio Total";
                                        y++;
                                        sheet.Range["A" + y].Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
                                        sheet.Range["B" + y].Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
                                        sheet.Range["C" + y].NumberValue = double.Parse(dataGridView1.Rows[x].Cells[3].Value.ToString());
                                        sheet.Range["D" + y].NumberValue = Int64.Parse(dataGridView1.Rows[x].Cells[4].Value.ToString());
                                        sheet.Range["E" + y].NumberValue = Int64.Parse(dataGridView1.Rows[x].Cells[4].Value.ToString());
                                        sheet.Range["F" + y].NumberValue = ((double.Parse(dataGridView1.Rows[x].Cells[3].Value.ToString()) * (double.Parse(dataGridView1.Rows[x].Cells[4].Value.ToString()))));
                                        y++;
                                    }
                                    else
                                    {
                                        insertes(dataGridView1.Rows[x].Cells[0].Value.ToString(),
                                     dataGridView1.Rows[x].Cells[1].Value.ToString(),
                                     dataGridView1.Rows[x].Cells[2].Value.ToString(),
                                     dataGridView1.Rows[x].Cells[3].Value.ToString(),
                                     dataGridView1.Rows[x].Cells[4].Value.ToString(),
                                     dataGridView1.Rows[x].Cells[5].Value.ToString(),
                                     dataGridView1.Rows[x].Cells[6].Value.ToString(),
                                     dataGridView1.Rows[x].Cells[7].Value.ToString(),
                                     dataGridView1.Rows[x].Cells[8].Value.ToString(),
                                     dataGridView1.Rows[x].Cells[9].Value.ToString(),
                                     dataGridView1.Rows[x].Cells[10].Value.ToString());
                                        sheet.Range["A" + y].Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
                                        sheet.Range["B" + y].Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
                                        sheet.Range["C" + y].NumberValue = double.Parse(dataGridView1.Rows[x].Cells[3].Value.ToString());
                                        sheet.Range["D" + y].NumberValue = Int64.Parse(dataGridView1.Rows[x].Cells[4].Value.ToString());
                                        sheet.Range["E" + y].NumberValue = Int64.Parse(dataGridView1.Rows[x].Cells[4].Value.ToString());
                                        sheet.Range["F" + y].NumberValue = ((double.Parse(dataGridView1.Rows[x].Cells[3].Value.ToString()) * (double.Parse(dataGridView1.Rows[x].Cells[4].Value.ToString()))));
                                        y++;
                                    }

                                 
                                }
                         
                            }
                        }
                            mysql.Dispose();

                    }

                       


                }

                workbook.SaveToFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/reporteinventario.xls");
                dataGridView1.DataSource = null;
            }
            catch (Exception xls)
            {
                MessageBox.Show(xls.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
           

        }



    }
}
