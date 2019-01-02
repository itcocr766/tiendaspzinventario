using MySql.Data.MySqlClient;
using POS.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.abonos
{
    public partial class Busqueda : Form
    {
        string formato = "";
        string diasvencer = "";
        string fechavenc = "";
        StreamReader streamToPrint;//para leer el xml
        StreamWriter facturawr;
        Font printFont;
        string saldoact = "";
        public Busqueda()
        {
            InitializeComponent();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from abonos where Cedula like '" + textBox1.Text + "%'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();

                }


            }
            catch (Exception euju)
            {

                Mensaje.Error(euju, "43");


            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from abonos where NumFactura like '" + textBox2.Text + "%'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();

                }


            }
            catch (Exception euju)
            {

                Mensaje.Error(euju, "70");


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                formatodeactura();
                imprimir();
                this.Visible = false;
            }
        }

        #region impresion
        public void imprimir()
        {

            try
            {



                streamToPrint = new StreamReader
                    ("Abonohistorico.txt");


                try
                {
                    printFont = new Font("Segoe UI", 10);
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler
                       (this.printDocument1_PrintPage);
                    pd.Print();
                }
                finally
                {
                    streamToPrint.Close();
                }
            }
            catch (Exception err_004)
            {
                Mensaje.Error(err_004, "128");

            }
        }


        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;


            float xPos = 0;

            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;





            string line = null;



            try
            {

                linesPerPage = e.MarginBounds.Height /
                   printFont.GetHeight(e.Graphics);


                while (count < linesPerPage &&
                   ((line = streamToPrint.ReadLine()) != null))
                {
                    yPos = (topMargin - 100) + (count *
                       printFont.GetHeight(e.Graphics));




                    e.Graphics.DrawString(line, printFont, Brushes.Black,
                    //leftMargin - 5, yPos, new StringFormat());
                    leftMargin - 80, yPos, new StringFormat());
                    count++;
                }


                if (line != null)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;

            }
            catch (Exception err_005)
            {
                Mensaje.Error(err_005, "186");

            }


        }

        #endregion

        public void formatodeactura()
        {
            string otrosabonos = "";
            string inicial = "";
            string nombre = "";
            formato = "";

            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion2();
                    mysql.cadenasql = "select NumFactura,Abono,Fecha from abonos where Cedula='" + dataGridView1.CurrentRow.Cells[1].Value + "' and NumFactura='" + dataGridView1.CurrentRow.Cells[2].Value + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (var reader = mysql.comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            otrosabonos += "\n" + "#" + reader["NumFactura"].ToString() + "\t                                    " + "₡" + reader["Abono"].ToString() + "\n" + reader["Fecha"].ToString();

                        }

                    }




                    mysql.cadenasql = "select Inicial,Saldo from saldos where NumFactura='" + dataGridView1.CurrentRow.Cells[2].Value + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (var reader = mysql.comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            inicial = reader["Inicial"].ToString();
                            saldoact = reader["Saldo"].ToString();
                        }

                    }

                    mysql.cadenasql = "select Nombre from clientes where Cedula='" + dataGridView1.CurrentRow.Cells[1].Value + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (var reader = mysql.comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nombre = reader["Nombre"].ToString();

                        }

                    }

                    mysql.cadenasql = "SELECT Fecha FROM saldos WHERE NumFactura='" + dataGridView1.CurrentRow.Cells[2].Value + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                    {
                        while (lee.Read())
                        {
                            fechavenc = lee["Fecha"].ToString();

                        }
                    }


                    DateTime feg = DateTime.Parse(fechavenc);

                    //mysql.cadenasql = "SELECT DATE_ADD('date("+ fechavenc + ")',INTERVAL 90 DAY) AS fe";
                    mysql.cadenasql = "SELECT DATE_ADD('" + feg.ToString("yyyy-MM-dd hh:mm:ss tt") + "',INTERVAL 90 DAY)";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                    {
                        while (lee.Read())
                        {
                            diasvencer = lee["DATE_ADD('" + feg.ToString("yyyy-MM-dd hh:mm:ss tt") + "',INTERVAL 90 DAY)"].ToString();

                        }
                    }
                    mysql.Dispose();


                    formato = ConfigurationManager.AppSettings["cadena2"] +
                        "\n------------------------------------------------" +
                   "\nFECHA DEL APARTADO:\n" + fechavenc +
                   "\nFECHA DE VENCIMIENTO:\n" + DateTime.Parse(diasvencer) + "\n" +
                  "\nCAJERO: " + dataGridView1.CurrentRow.Cells[5].Value +
                   "\nCLIENTE: " + nombre +
                   "\nSALDO INICIAL: " + inicial +
                   "\nSALDO ACTUAL: " + saldoact +
                   "\n------------------------------------------------" +
                   "\n\nABONOS HASTA EL MOMENTO:" +
                   "\n\nNo.Factura              Monto abonado" +
                   otrosabonos +
                   "\n------------------------------------------------";


                    facturawr = new StreamWriter("Abonohistorico.txt");
                    facturawr.WriteLine(formato);
                    facturawr.Flush();
                    facturawr.Close();

                }







            }

            catch (Exception err_0016)
            {
                Mensaje.Error(err_0016, "329");



            }

        }



    }
}
