using POS.clientesprincipal;
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

namespace POS.clientes
{
    public partial class clienform : Form
    {
        Form1 f1;
       public bool bandera=false;
      
        public clienform(Form1 f)
        {
            InitializeComponent();f1 = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                guardarcliente();
               
                if (bandera)
                {
                    bandera = false;
                    f1.textBox9.Text = textBox3.Text;
                    f1.textBox10.Text = textBox2.Text;
                    this.Visible = false;
                    f1.Visible = true;

                }
             
           
              
            }
            else
            {

                Errores.war();
            }
        }

        private void guardarcliente()
        {
            try
            {

                using (var mysql=new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "insert into clientes(Cedula,Nombre,Telefono,Correo,Direccion)values('" + Int32.Parse(textBox5.Text.Trim()) + "','" + textBox1.Text.ToUpper().Trim() + "','" + textBox3.Text.ToUpper().Trim() + "','" + textBox2.Text.ToUpper().Trim() + "','" + textBox4.Text.ToUpper().Trim() + "')";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    mysql.Dispose();
                    Errores.inf();
                }



                 

                    
                

            }
            catch (Exception guardarcliente)
            {


                Errores.err();
               
            }
        }

        private void clienform_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F7)
            {
                button1.PerformClick();
            }
        }

        private void clienform_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox5_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();

            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();

            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
