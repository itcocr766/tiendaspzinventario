using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Modelo;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using System.Drawing.Printing;
using System.Globalization;

namespace POS.Cierres
{
    public partial class Cierre : Form
    {

        string numero;
        int fondo = 0;
        int contador = 0;
        bool impri;
         public string inicialff = "";
        public  string finalff = "";
        public string inicialpp = "";
        public string finalpp = "";
        int finalf = 0;
        int inicialf=0;
        int inicialp = 0;
        int finalp = 0;
        int iniciala = 0;
        int finala = 0;
        double efectivof,efectivop,efectivoa;
        double tarjetaf,tarjetap,tarjetaa;
        double sumaf,sumap, sumaabono,sumatoria;
        int abonofinal = 0;
        public string abonoiniciall = "";
        public  string abonofinall = "";
         string formato;
        StreamWriter facturawr;
        StreamReader streamToPrint;
        Font printFont;
        public Cierre()
        {
            InitializeComponent();
        }

        private void Cierre_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(dateTimePicker1.Value.ToString());
            
            
        }

        public void cargarabonos()
        {

            try
            {

                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    if (abonofinal > 0)
                    {
                        mysql.cadenasql = "SELECT * FROM `abonos` where Codigo not between '0' and '" + abonofinal + "'";
                    }
                    else
                    {
                        mysql.cadenasql = "SELECT * FROM `abonos`";
                    }

                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView3.DataSource = dtDatos;

                    mysql.Dispose();

                }






            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
        public void cargarFactura()
        {
            
            try
            {
               
                using (var mysql= new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    if (finalf>0)
                    {
                        mysql.cadenasql = "SELECT * FROM `factura` where Tipo='Factura' and Numero not between '0' and '"+finalf+"'";
                    }
                    else
                    {
                        mysql.cadenasql = "SELECT * FROM `factura`";
                    }

                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;

                    mysql.Dispose();

                }

              



                 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
               
            }
           

        }

        public void cargarsale()
        {

            try
            {

                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    if (finalp > 0)
                    {
                        mysql.cadenasql = "SELECT * FROM `sales` where Numero not between '0' and '" + finalp + "'";
                    }
                    else
                    {
                        mysql.cadenasql = "SELECT * FROM `sales`";
                    }

                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView2.DataSource = dtDatos;

                    mysql.Dispose();

                }






            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
               
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) &&
                    !string.IsNullOrEmpty(textBox4.Text))
                {
                    textBox13.Text = (double.Parse(textBox1.Text) + double.Parse(textBox10.Text) + double.Parse(textBox12.Text)).ToString();
                    textBox14.Text = (double.Parse(textBox8.Text) + double.Parse(textBox9.Text) + double.Parse(textBox11.Text)).ToString();

                }
                else
                {

                    MessageBox.Show("No ha ingresado valores suficientes para realizar este proceso", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
               

            }
            catch (Exception er)
            {
                MessageBox.Show("Hubo un error a conectar a la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sumatoria = 0;
            fondo = 0;
            try
            {
                if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text) &&
                    !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrEmpty(textBox13.Text) && !string.IsNullOrEmpty(textBox14.Text))
                {
                    sumatoria += (double.Parse(textBox2.Text) + double.Parse(textBox3.Text) + double.Parse(textBox4.Text)+double.Parse(textBox5.Text));
                    textBox7.Text = ((double.Parse(textBox13.Text) + double.Parse(textBox14.Text))).ToString();
                    textBox6.Text = sumatoria.ToString();
                }
                else
                {

                   DialogResult result= MessageBox.Show("No ha ingresado un fondo de caja." +
                       "Desea que el programa calcule el cierre con un monto de cien mil colones como fondo?\n" +
                       "Presione 'No' para salir y digitar un fondo de caja.","Faltan datos",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text) &&
                             !string.IsNullOrEmpty(textBox13.Text) && !string.IsNullOrEmpty(textBox14.Text))
                        {
                            
                            fondo = 100000;
                            sumatoria += (double.Parse(textBox2.Text) + double.Parse(textBox3.Text) + double.Parse(textBox4.Text) + fondo);
                            textBox7.Text = ((double.Parse(textBox13.Text) + double.Parse(textBox14.Text))).ToString();
                            textBox5.Text = "100000";
                            textBox6.Text = sumatoria.ToString();
                        }
                        else if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) ||
                            string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox13.Text) || string.IsNullOrEmpty(textBox14.Text))
                        {
                            MessageBox.Show("No ha ingresado valores suficientes para realizar el cierre", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }
                     

                    }
                   
                }
                

            }
            catch (Exception errf)
            {

                MessageBox.Show("Hubo un error a conectar a la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cajeros()
        {
            contador = 0;
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    
                    mysql.cadenasql = "SELECT DISTINCT CodigoCajero FROM factura";
                    mysql.comando = new MySqlCommand(mysql.cadenasql,mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    mysql.lector = mysql.comando.ExecuteReader();
                    while (mysql.lector.Read())
                    {

                        contador++;
                    }
                    mysql.Dispose();

                }



            }
            catch (Exception ex)
            {

                MessageBox.Show("Hubo un error a conectar a la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            guardarcierre();
            if (impri)
            {
                formatodeactura();
                imprimir();
                button6.Enabled = true;
                limpieza();
            }
           
        }

        private void guardarcierre()
        {
            impri = false;
            numero = "";
            try
            {
                if (!string.IsNullOrEmpty(textBox2.Text)&& !string.IsNullOrEmpty(textBox3.Text)&&
                    !string.IsNullOrEmpty(textBox1.Text)&& !string.IsNullOrEmpty(textBox8.Text)&&
                    !string.IsNullOrEmpty(textBox4.Text)&& !string.IsNullOrEmpty(textBox10.Text)&& !string.IsNullOrEmpty(textBox9.Text)&&
                    !string.IsNullOrEmpty(textBox12.Text)&& !string.IsNullOrEmpty(textBox11.Text)&&
                    !string.IsNullOrEmpty(textBox13.Text)&& !string.IsNullOrEmpty(textBox14.Text)&&
                    !string.IsNullOrEmpty(textBox7.Text)&& !string.IsNullOrEmpty(textBox5.Text)&&
                    !string.IsNullOrEmpty(textBox6.Text))
                {
                    if (dataGridView1.Rows.Count > 0)
                    {
                        inicialf = Int32.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                        finalf = Int32.Parse(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value.ToString());
                    }
                    else
                    {
                        inicialf = 0;
                        finalf = 0;
                    }

                    if (dataGridView2.Rows.Count > 0)
                    {
                        inicialp = Int32.Parse(dataGridView2.Rows[0].Cells[0].Value.ToString());
                        finalp = Int32.Parse(dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Value.ToString());
                    }
                    else
                    {
                        inicialp = 0;
                        finalp = 0;
                    }

                    if (dataGridView3.Rows.Count > 0)
                    {
                        iniciala = Int32.Parse(dataGridView3.Rows[0].Cells[0].Value.ToString());
                        finala = Int32.Parse(dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[0].Value.ToString());
                    }
                    else
                    {
                        iniciala = 0;
                        finala = 0;
                    }
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();

                        mysql.cadenasql = "INSERT INTO `cierre`(`Fecha`, `FacturaInicialf`, `FacturaFinalf`,`FacturaInicialp`,`FacturaFinalp`,`abonoinicial`, `abonofinal`, `VentasContado`, `Abonos`,`Fondo`,`VentasTarjeta`) VALUES (" +
                            "'" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "'," +
                            "'" + inicialf + "'," +
                            "'" + finalf + "'," +
                            "'" + inicialp + "'," +
                            "'" + finalp + "'," +
                            "'" + iniciala + "'," +
                            "'" + finala + "'," +
                            "'" + double.Parse(textBox13.Text).ToString("0.00000000",CultureInfo.InvariantCulture) + "'," +
                            "'" + double.Parse(textBox4.Text).ToString("0.00000000", CultureInfo.InvariantCulture) + "'" +
                            ",'"+ double.Parse(textBox5.Text).ToString("0.00000000", CultureInfo.InvariantCulture) + "'" +
                            ",'"+ double.Parse(textBox14.Text).ToString("0.00000000", CultureInfo.InvariantCulture) + "')";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();

                        mysql.Dispose();
                        impri = true;
                    }


                    using (var mysql2 = new Mysql())
                    {
                        mysql2.conexion();
                        mysql2.cadenasql = "select Max(Numero) from cierre";
                        mysql2.comando = new MySqlCommand(mysql2.cadenasql, mysql2.con);
                        mysql2.comando.ExecuteNonQuery();
                        using (var lee2 = mysql2.comando.ExecuteReader())
                        {
                            while (lee2.Read())
                            {
                                numero = lee2["Max(Numero)"].ToString();
                            }
                        }
                        mysql2.Dispose();
                    }



                }
                else
                {
                    MessageBox.Show("Faltan datos","Faltan datos",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

                }
                
            }
            catch (Exception evo)
            {
                MessageBox.Show(evo.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        #region metodos impresion
        public void formatodeactura()
        {

            formato = "";

            try
            {

                if (dataGridView1.Rows.Count > 0)
                {
                    inicialff = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    finalff = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value.ToString();
                }
                else
                {
                    inicialff = "0";
                    finalff = "0";
                }

                if (dataGridView2.Rows.Count > 0)
                {
                    inicialpp = dataGridView2.Rows[0].Cells[0].Value.ToString();
                    finalpp = dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Value.ToString();
                }
                else
                {
                    inicialpp = "0";
                    finalpp = "0";
                }

                if (dataGridView3.Rows.Count > 0)
                {
                    abonoiniciall = dataGridView3.Rows[0].Cells[0].Value.ToString();
                    abonofinall = dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[0].Value.ToString();
                }
                else
                {
                    abonoiniciall = "0";
                    abonofinall = "0";
                }


                formato = "TIENDA                       OUTLET\n" +
                              "CAJA                           ADMINISTRADOR\n" +
                              "NUMERO CIERRE            " + numero + "\n" +
                              "FECHA EMISIÓN         " + DateTime.Now.ToShortDateString() + "\n" +
                              "FACTURA INICIAL F      " + inicialff + "\n" +
                              "FACTURA FINAL F        " + finalff + "\n" +
                              "FACTURA INICIAL P      " + inicialpp + "\n" +
                              "FACTURA FINAL P        " + finalpp + "\n" +
                              "ABONO INICIAL          " + abonoiniciall + "\n" +
                              "ABONO FINAL            " + abonofinall + "\n" +
                              "VENTAS                       MONTO\n" +
                              "----------------------------------\n" +
                              "VENTAS CONTADO      ₡" + textBox13.Text + "\n" +
                              "ABONOS                       ₡" + textBox4.Text + "\n" +
                              "VENTAS TARJETA          ₡" +textBox14.Text + "\n" +
                              "ENTRADA CAJA             ₡" + textBox5.Text + "\n" +
                              "SALIDA CAJA                 ₡0\n" +
                              "TOTAL CAJA                  ₡0\n" +
                              "FONDO CAJA                ₡0\n" +
                              "POR ENTREGAR            ₡" + textBox6.Text + "\n\n" +
                              "NOTAS\n\n" +
                              "________________________________________\n\n" +
                              "________________________________________\n\n" +
                              "FIRMA CAJERO           FIRMA ADMIN\n\n" +
                              "________________       _________________\n\n" +
                              "Impreso el " + DateTime.Now.ToString();


                facturawr = new StreamWriter("Cierre.txt");
                facturawr.WriteLine(formato);
                facturawr.Flush();
                facturawr.Close();

                //}







            }
            catch (Exception err_0016)
            {
                Mensaje.Error(err_0016, "329");



            }

        }

        public void limpieza()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox8.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            textBox12.Text = "";
            textBox11.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox7.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
        }

        public void imprimir()
        {

            try
            {



                streamToPrint = new StreamReader
                    ("Cierre.txt");


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

        private void button6_Click(object sender, EventArgs e)
        {
            verificarcierre();

           
            cargarFactura();
            cargarsale();
            cargarabonos();
            button6.Enabled = false;
 
        }

        public  void verificarcierre()
        {
            int conta = 0;
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    
                    mysql.cadenasql = "select count(*),max(FacturaFinalf),max(FacturaFinalp),max(abonofinal) from cierre";
                    mysql.comando = new MySqlCommand(mysql.cadenasql,mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (MySqlDataReader lee=mysql.comando.ExecuteReader())
                    {
                       
                            while (lee.Read())
                            {
                            if (Int32.Parse(lee["count(*)"].ToString())>0)
                            {

                                conta += 1;
                            }
                                
                            }
                           
                        
                        if (conta > 0)
                        {
                            finalf = Int32.Parse(lee["max(FacturaFinalf)"].ToString());
                            finalp = Int32.Parse(lee["max(FacturaFinalp)"].ToString());
                            abonofinal = Int32.Parse(lee["max(abonofinal)"].ToString());
                        }
                        else
                        {
                            finalp = 0;
                            finalf = 0;
                            abonofinal = 0;
                        }

                        mysql.Dispose();
                    }
                   
                    mysql.Dispose();

                }

              



            }
            catch (Exception huy)
            {
                MessageBox.Show(huy.ToString());
            }
        }

        private void cambiarLaClaveDeAccesoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cambiarlaclave ccc = new cambiarlaclave();
            ccc.Show(this);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            verificarcierre();


            cargarFactura();
            cargarsale();
            cargarabonos();
            button6.Enabled = false;
            button5.Enabled = false;
            sumarefes();
            sumarpes();
            sumarabonos();
            realizartotales();
            sumatoriatotal();
        }

        public void realizartotales()
        {

            try
            {
                if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) &&
                    !string.IsNullOrEmpty(textBox4.Text))
                {
                    textBox13.Text =  (double.Parse(textBox1.Text) + double.Parse(textBox10.Text) + double.Parse(textBox12.Text)).ToString();
                    textBox14.Text = (double.Parse(textBox8.Text) + double.Parse(textBox9.Text) + double.Parse(textBox11.Text)).ToString();

                }
                else
                {

                    MessageBox.Show("No ha ingresado valores suficientes para realizar este proceso", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }


            }
            catch (Exception er)
            {
                MessageBox.Show("Hubo un error a conectar a la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      


        public void sumatoriatotal()
        {
            sumatoria = 0;
            fondo = 0;
            try
            {
                if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text) &&
                    !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrEmpty(textBox13.Text) && !string.IsNullOrEmpty(textBox14.Text))
                {
                    sumatoria += (double.Parse(textBox2.Text) + double.Parse(textBox3.Text) + double.Parse(textBox4.Text) + double.Parse(textBox5.Text));
                    textBox7.Text = (double.Parse(textBox13.Text) + double.Parse(textBox14.Text)).ToString();
                    textBox6.Text = (sumatoria).ToString();
                }
                else
                {

                    DialogResult result = MessageBox.Show("No ha ingresado un fondo de caja." +
                        "Desea que el programa calcule el cierre con un monto de cien mil colones como fondo?\n" +
                        "Presione 'No' para salir y digitar un fondo de caja.", "Faltan datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text) &&
                             !string.IsNullOrEmpty(textBox13.Text) && !string.IsNullOrEmpty(textBox14.Text))
                        {

                            fondo = 100000;
                            sumatoria += (double.Parse(textBox2.Text) + double.Parse(textBox3.Text) + double.Parse(textBox4.Text) + fondo);
                            textBox7.Text = (double.Parse(textBox13.Text) + double.Parse(textBox14.Text)).ToString();
                            textBox5.Text = "100000";
                            textBox6.Text = sumatoria.ToString();
                            button6.Enabled = false;
                            button5.Enabled = true;
                        }
                        else if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) ||
                            string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox13.Text) || string.IsNullOrEmpty(textBox14.Text))
                        {
                            MessageBox.Show("No ha ingresado valores suficientes para realizar el cierre", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }


                    }

                }


            }
            catch (Exception errf)
            {

                MessageBox.Show("Hubo un error a conectar a la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public void sumarefes()
        {

            sumaf = 0;
            tarjetaf = 0;
            efectivof = 0;
            try
            {
                for (int h = 0; h < dataGridView1.Rows.Count; h++)
                {

                    sumaf += double.Parse(dataGridView1.Rows[h].Cells[3].Value.ToString());
                    if (dataGridView1.Rows[h].Cells[5].Value.ToString() == "Efectivo")
                    {

                        efectivof += double.Parse(dataGridView1.Rows[h].Cells[3].Value.ToString());
                    }
                    else if (dataGridView1.Rows[h].Cells[5].Value.ToString() == "Tarjeta")
                    {
                        tarjetaf += double.Parse(dataGridView1.Rows[h].Cells[3].Value.ToString());

                    }
                }
                textBox2.Text = sumaf.ToString();
                textBox1.Text = efectivof.ToString();
                textBox8.Text = tarjetaf.ToString();

            }
            catch (Exception trend)
            {

                MessageBox.Show("", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void sumarabonos()
        {

            sumaabono = 0;
            efectivoa = 0;
            tarjetaa = 0;

            try
            {
                for (int h = 0; h < dataGridView3.Rows.Count; h++)
                {

                    sumaabono += double.Parse(dataGridView3.Rows[h].Cells[3].Value.ToString());
                    if (dataGridView3.Rows[h].Cells[7].Value.ToString() == "Efectivo")
                    {

                        efectivoa += double.Parse(dataGridView3.Rows[h].Cells[3].Value.ToString());
                    }
                    else if (dataGridView3.Rows[h].Cells[7].Value.ToString() == "Tarjeta")
                    {
                        tarjetaa += double.Parse(dataGridView3.Rows[h].Cells[3].Value.ToString());
                    }
                }
                textBox4.Text = sumaabono.ToString();
                textBox12.Text = efectivoa.ToString();
                textBox11.Text = tarjetaa.ToString();

            }
            catch (Exception trend)
            {
                MessageBox.Show(trend.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public void sumarpes()
        {
            sumap = 0;
            efectivop = 0;
            tarjetap = 0;
            try
            {
                for (int hh = 0; hh < dataGridView2.Rows.Count; hh++)
                {

                    sumap += double.Parse(dataGridView2.Rows[hh].Cells[3].Value.ToString());
                    if (dataGridView2.Rows[hh].Cells[5].Value.ToString() == "Efectivo")
                    {

                        efectivop += double.Parse(dataGridView2.Rows[hh].Cells[3].Value.ToString());
                    }
                    else if (dataGridView2.Rows[hh].Cells[5].Value.ToString() == "Tarjeta")
                    {

                        tarjetap += double.Parse(dataGridView2.Rows[hh].Cells[3].Value.ToString());
                    }
                }
                textBox3.Text = sumap.ToString();
                textBox10.Text = efectivop.ToString();
                textBox9.Text = tarjetap.ToString();
            }
            catch (Exception trend)
            {

                MessageBox.Show("", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            historico his = new historico();
            his.Show(this);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            guardarcierre();
            if (impri)
            {
                formatodeactura();
                imprimir();
                button6.Enabled = true;
                limpieza();
            }
        }

        public void cargarfacs()
        {
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    mysql.cadenasql = "select * from abonos";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView3.DataSource = dtDatos;
                    mysql.Dispose();

                }
            }
            catch (Exception exxx)
            {
                MessageBox.Show(exxx.ToString());
            }
        }

        private void textBox15_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
         

         
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            sumaabono = 0;
            efectivoa = 0;
            tarjetaa = 0;
        
            try
            {
                for (int h = 0; h < dataGridView3.Rows.Count; h++)
                {

                    sumaabono += double.Parse(dataGridView3.Rows[h].Cells[3].Value.ToString());
                    if (dataGridView3.Rows[h].Cells[7].Value.ToString()=="Efectivo")
                    {

                        efectivoa+= double.Parse(dataGridView3.Rows[h].Cells[3].Value.ToString());
                    }
                    else if (dataGridView3.Rows[h].Cells[7].Value.ToString() == "Tarjeta")
                    {
                        tarjetaa+= double.Parse(dataGridView3.Rows[h].Cells[3].Value.ToString());
                    }
                }
                textBox4.Text = sumaabono.ToString();
                textBox12.Text =efectivoa.ToString();
                textBox11.Text = tarjetaa.ToString();

            }
            catch (Exception trend)
            {
                MessageBox.Show("", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sumap = 0;
            efectivop = 0;
            tarjetap = 0;
            try
            {
                for (int hh = 0; hh < dataGridView2.Rows.Count; hh++)
                {

                    sumap += double.Parse(dataGridView2.Rows[hh].Cells[3].Value.ToString());
                    if (dataGridView2.Rows[hh].Cells[5].Value.ToString() == "Efectivo")
                    {

                        efectivop+= double.Parse(dataGridView2.Rows[hh].Cells[3].Value.ToString());
                    }
                    else if (dataGridView2.Rows[hh].Cells[5].Value.ToString() == "Tarjeta")
                    {

                        tarjetap+= double.Parse(dataGridView2.Rows[hh].Cells[3].Value.ToString()); 
                    }
                }
                textBox3.Text =  sumap.ToString();
                textBox10.Text = efectivop.ToString();
                textBox9.Text = tarjetap.ToString();
            }
            catch (Exception trend)
            {

                MessageBox.Show("", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sumaf = 0;
            tarjetaf = 0;
            efectivof = 0;
            try
            {
                for (int h = 0; h < dataGridView1.Rows.Count; h++)
                {

                    sumaf += double.Parse(dataGridView1.Rows[h].Cells[3].Value.ToString());
                    if (dataGridView1.Rows[h].Cells[5].Value.ToString() == "Efectivo")
                    {

                        efectivof += double.Parse(dataGridView1.Rows[h].Cells[3].Value.ToString());
                    }
                    else if (dataGridView1.Rows[h].Cells[5].Value.ToString() == "Tarjeta")
                    {
                        tarjetaf += double.Parse(dataGridView1.Rows[h].Cells[3].Value.ToString());

                    }
                }
                textBox2.Text =  sumaf.ToString();
                textBox1.Text= efectivof.ToString();
                textBox8.Text = tarjetaf.ToString();
                
            }
            catch (Exception trend)
            {

                MessageBox.Show("","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
