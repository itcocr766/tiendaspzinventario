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
using MySql.Data.MySqlClient;
using POS.Control;
using POS.Modelo;
using POS.clientes;
using POS.clientesprincipal;
using System.Globalization;

namespace POS.abonos
{
    public partial class Abonos : Form
    {
        StreamReader streamToPrint;//para leer el xml
        Font printFont;
        string formato;
        Form1 for1;
        StreamWriter facturawr;
        public static string cedu = "";


        public Abonos(Form1 f)
        {
            InitializeComponent();
            for1 = f;
           

        }

        private void Abonos_Load(object sender, EventArgs e)
        {

            try
            {
               

                comboBox1.DataSource = cargarClientes();
                comboBox1.DisplayMember = "CompName";
                comboBox1.ValueMember = "Cedula";

                comboBox4.DataSource = cargarnombrescondes();
                comboBox4.DisplayMember = "Nombre";
                comboBox4.ValueMember = "Nombre";

                comboBox1.Text = for1.comboBox3.Text;



                textBox3.Text = for1.textBox4.Text;
                comboBox1.Focus();



            }
            catch (Exception err_abonos01)
            {

                Mensaje.Error(err_abonos01, "48");
            }




        }

        public void dispararaprocedures()
        {
            try
            {

                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "borrarcancelados";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.CommandType = CommandType.StoredProcedure;
                    mysql.comando.ExecuteNonQuery();


                    mysql.cadenasql = "iniciarcancelados";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.CommandType = CommandType.StoredProcedure;
                    mysql.comando.ExecuteNonQuery();


                    mysql.cadenasql = "crearcancelado";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.CommandType = CommandType.StoredProcedure;
                    mysql.comando.ExecuteNonQuery();


                    mysql.cadenasql = "eliminarnocancelados";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.CommandType = CommandType.StoredProcedure;
                    mysql.comando.ExecuteNonQuery();


                    mysql.Dispose();

                }
                   
             
               
            }
            catch (Exception err_abonos02)
            {
                Mensaje.Error(err_abonos02, "91");
               
                   
            }
           

        }


        #region metodos impresion

        public void imprimir()
        {

            try
            {



                streamToPrint = new StreamReader
                    ("Abono.txt");


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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.Dispose();
                Application.Exit();

            }
            catch (Exception err)
            {
                Mensaje.Error(err, "203");
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {



        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer2_Tick_2(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

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
                    mysql.cadenasql = "select NumFactura,Abono,Fecha from abonos where Cedula='" + comboBox1.Text + "' and NumFactura='" + comboBox2.Text + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (var reader = mysql.comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            otrosabonos += "\n" + "#" + reader["NumFactura"].ToString() + "\t                                    " + "₡" + reader["Abono"].ToString() + "\n" + reader["Fecha"].ToString();

                        }

                    }




                    mysql.cadenasql = "select Inicial from saldos where NumFactura='" + comboBox2.Text + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (var reader = mysql.comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            inicial = reader["Inicial"].ToString();

                        }

                    }

                    mysql.cadenasql = "select Nombre from clientes where Cedula='" + comboBox1.Text + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (var reader = mysql.comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nombre = reader["Nombre"].ToString();

                        }

                    }



                    mysql.Dispose();


                    formato = ConfigurationManager.AppSettings["cadena2"] +
                        "\n------------------------------------------------" +
                   "\nFECHA:     " + DateTime.Now.ToString() +
                  "\nCAJERO: " + textBox3.Text +
                   "\nCLIENTE: " + nombre +
                   "\nSALDO INICIAL: " + inicial +
                   "\nSALDO ANTERIOR: " + textBox2.Text +
                   "\nSALDO ACTUAL: " + string.Format("{0:N2}", (double.Parse(textBox2.Text) - double.Parse(textBox1.Text.Trim()))) +
                   "\n------------------------------------------------" +
                   "\n\nABONOS HASTA EL MOMENTO:" +
                   "\n\nNo.Factura              Monto abonado" +
                   otrosabonos +
                   "\n------------------------------------------------";


                    facturawr = new StreamWriter("Abono.txt");
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





        #endregion



        public void agregarPayment()
        {
            try
            {
                using (var mysql=new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "update abonos set flag=0";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();

                    mysql.cadenasql = "insert into abonos(Cedula,NumFactura,Abono,flag,CodigoCajero,Fecha,TipoPago)values('" + comboBox1.Text + "','" + comboBox2.Text + "','" +
                           double.Parse(textBox1.Text.Trim()).ToString("0.00000000",CultureInfo.InvariantCulture) + "','1','" + textBox3.Text + "','" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " "
                        + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "','"+comboBox3.Text+"')";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();



                    mysql.Dispose();

                }
                   

            }
            catch (Exception err_mysql19)
            {

                Mensaje.Error(err_mysql19, "371");
               
                   
            }

          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrEmpty(comboBox3.Text))&&
                    (double.Parse(textBox1.Text.Trim()) >= 1000 && (double.Parse(textBox1.Text.Trim()) <= double.Parse(textBox2.Text.Trim()))))
                {
                    agregarPayment();
                    formatodeactura();
                    for (int imp = 0; imp < Int32.Parse(numericUpDown1.Value.ToString()); imp++)
                    {
                        imprimir();

                    }

                    button1.Visible = false;
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    label2.Visible = false;
                    label5.Visible = false;
                    comboBox2.Visible = false;
                    label4.Visible = false;
                    label6.Visible = false;
                    label1.Text = "Seleccione un cliente y presione Enter";
                    dispararaprocedures();
                  
                    comboBox1.Enabled = true;
                    this.Visible = false;
                }
                else
                {

                    MessageBox.Show("Asegúrese de tener los siguientes datos:\n" +
                        "1.El monto debe ser mayor o igual a 1000 y debe ser menor o igual al saldo pendiente.\n" +
                        "2.Especifíque el tipo de pago.");
                }

            }
            catch (Exception err_mysql11)
            {
                Mensaje.Error(err_mysql11, "407");
             
            }
       
         
        }

     

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
            try
            {
               
                if (e.KeyCode == Keys.Enter)
                {
                    while (dataGridView1.Rows.Count >= 1)
                    {

                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                    }

                    comboBox2.Visible = true;
                    comboBox2.Text = "";
                    comboBox2.Items.Clear();
                    label4.Visible = true;
                    buscarapscliente();

                    if (comboBox2.Items.Count > 0)
                    {
                        comboBox2.SelectedIndex = 0;
                        comboBox2.Focus();
                    }

                    comboBox1.Enabled = false;
                }
               

            }
            catch (Exception ers)
            {
                Mensaje.Error(ers,"470");
            
            }

           
        }

        public void buscarapscliente()
        {
           
            try
            {

                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "select * from saldos where CodigoCliente='" + comboBox1.Text + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    mysql.lector = mysql.comando.ExecuteReader();

                    while (mysql.lector.Read())
                    {
                        comboBox2.Items.Add(mysql.lector["NumFactura"].ToString());
                        dataGridView1.Rows.Add(mysql.lector["NumFactura"].ToString(),
                            mysql.lector["Saldo"].ToString(), mysql.lector["Inicial"].ToString());

                    }




                    mysql.Dispose();


                }

                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "select Nombre from clientes where Cedula='" + comboBox1.Text + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    mysql.lector = mysql.comando.ExecuteReader();
                    if (mysql.lector.Read())
                    {
                        textBox4.Text = mysql.lector["Nombre"].ToString();

                    }

                    mysql.Dispose();

                }


            }
            catch (Exception jiu)
            {
                MessageBox.Show(jiu.ToString());


            }


        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (comboBox2.Text != "" && e.KeyCode == Keys.Enter)
                {
                 
                    label2.Visible = true;
                    textBox1.Visible = true;
                    label5.Visible = true;
                    textBox2.Visible = true;
                    button1.Visible = true;
                    label6.Visible = true;
                    label1.Text = "Digite el monto que desea abonar";
                    saldo();
                    textBox1.Focus();
                }

            }
            catch (Exception eftyu)
            {
                Mensaje.Error(eftyu, "537");
              

            }

           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public DataTable cargarClientes()
        {
            DataTable dt  = new DataTable();
            try
            {
                using (var mysql=new Mysql())
                {
                    mysql.conexion();

                    mysql.cadenasql = "SELECT * FROM clientes";
                    MySqlCommand cmd = new MySqlCommand(mysql.cadenasql, mysql.con);
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    adap.Fill(dt);
                    mysql.Dispose();

                }
                    

            }
            catch (Exception eds)
            {
                Mensaje.Error(eds, "568");
            


            }
           
            return dt;
        }


        public void saldo()
        {
            try
            {
                using (var mysql=new Mysql())
                {

                    mysql.conexion();

                    mysql.cadenasql = "select Saldo from saldos where NumFactura='" + comboBox2.Text + "' and CodigoCliente='" + comboBox1.Text + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    mysql.lector = mysql.comando.ExecuteReader();
                    if (mysql.lector.Read())
                    {
                        textBox2.Text = mysql.lector["Saldo"].ToString();

                    }


                    mysql.Dispose();
                }

                 

            }
            catch (Exception err_saldo)
            {

                Mensaje.Error(err_saldo, "609");

           
                    

            }

           

           
        

        }

        private void button2_Click(object sender, EventArgs e)
        {
        
        }

        private void Abonos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F10)
            {

                button1.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                if (comboBox3.Text == "Efectivo")
                {
                    comboBox3.Text = "Tarjeta";
                }
                else
                {

                    comboBox3.Text = "Efectivo";
                }

            }
            else if (e.KeyCode==Keys.F1)
            {
                limpieza();
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //if (textBox2.Text.Trim()=="0")
            //{

            //    cancelados ca = new cancelados();
            //    ca.Show();

            //    ca.TopMost = true;
            //}
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            climod cf = new climod(this);
            cf.Show(this);
        }

        private void Abonos_Shown(object sender, EventArgs e)
        {
            this.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            limpieza();

        }

        public void limpieza()
        {

            button1.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            label2.Visible = false;
            label5.Visible = false;
            comboBox2.Visible = false;
            label4.Visible = false;
            label6.Visible = false;
            label1.Text = "Seleccione un cliente y presione Enter";
         
            comboBox1.Enabled = true;
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox4.Text = "";
            comboBox3.Text = "";
            while (dataGridView1.Rows.Count >= 1)
            {

                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            }
            comboBox1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                button1.Focus();
            }
           
        }

        public DataTable cargarnombrescondes()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();

                    mysql.cadenasql = "SELECT * FROM clientes";
                    MySqlCommand cmd = new MySqlCommand(mysql.cadenasql, mysql.con);
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    adap.Fill(dt);
                    mysql.Dispose();

                }


            }
            catch (Exception eds)
            {
                Mensaje.Error(eds, "794");



            }

            return dt;
        }

        private void comboBox4_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable dtt = new DataTable();
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dataGridView2.DataSource = null;
                    using (var mysql = new Mysql())
                    {

                        mysql.conexion();

                        mysql.cadenasql = "select Cedula from clientes where Nombre='" + comboBox4.Text + "'";
                        MySqlDataAdapter adapt = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                        adapt.Fill(dtt);
                        dataGridView2.DataSource = dtt;
                        mysql.Dispose();
                    }

                }
            }
            catch (Exception efd)
            {
                Mensaje.Error(efd, "797");
            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            DataTable dd = new DataTable();
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {

                    dataGridView3.DataSource = null;

                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "select NumFactura from saldos where CodigoCliente='" + dataGridView2.CurrentRow.Cells[0].Value + "' AND datediff(curdate(),Fecha)<=90";
                        MySqlDataAdapter adapt = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                        adapt.Fill(dd);
                        dataGridView3.DataSource = dd;
                        mysql.Dispose();


                    }



                }

            }
            catch (Exception jiu)
            {
                MessageBox.Show(jiu.ToString());


            }
        }
    }
}
