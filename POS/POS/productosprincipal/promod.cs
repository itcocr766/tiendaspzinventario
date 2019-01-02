using MySql.Data.MySqlClient;
using POS.Modelo;
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
    public partial class promod : Form
    {
        public promod()
        {
            InitializeComponent();
        }

        private void promod_Load(object sender, EventArgs e)
        {
            cargar();
            impuesto.SelectedIndex = 0;
        }

        public void cargar()
        {
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select Barcode from items";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();


                }


            }
            catch (Exception hju)
            {
                Mensaje.Error(hju, "42");


            }

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select Barcode from items where Barcode like '" + barcode.Text + "%'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();

                }


            }
            catch (Exception euju)
            {

                Mensaje.Error(euju, "71");


            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

           
            try
            {
                if (dataGridView1.Rows.Count>0)
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "select * from items where Barcode='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.lector = mysql.comando.ExecuteReader();
                        while (mysql.lector.Read())
                        {
                            descripcion.Text = mysql.lector["Descripcion"].ToString();

                            impuesto.Text = mysql.lector["Impuesto"].ToString();
                            textBox1.Text = mysql.lector["OnHand"].ToString();
                            textBox2.Text = mysql.lector["Precio"].ToString();
                            textBox3.Text = mysql.lector["Costo"].ToString();
                            textBox4.Text = mysql.lector["Marca"].ToString();
                            textBox5.Text = mysql.lector["Talla"].ToString();
                            textBox6.Text = mysql.lector["Familia"].ToString();
                            textBox7.Text = mysql.lector["Estilo"].ToString();
                            textBox8.Text = mysql.lector["Genero"].ToString();

                        }
                        mysql.Dispose();
                        button1.Enabled = true;
                        descripcion.Focus();
                    }
                }

               


            }
            catch (Exception excep)
            {

                Mensaje.Error(excep, "102");
            }


        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode==Keys.Up ||e.KeyCode==Keys.Down)
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "select * from items where Barcode='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.lector = mysql.comando.ExecuteReader();
                        while (mysql.lector.Read())
                        {
                            descripcion.Text = mysql.lector["Descripcion"].ToString();
                           
                            impuesto.Text = mysql.lector["Impuesto"].ToString();
                           

                        }
                        mysql.Dispose();
                    }

                }
              


            }
            catch (Exception excep)
            {

                Mensaje.Error(excep, "102");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(descripcion.Text))
                {
                    errorProvider1.SetError(descripcion, "Campo obligatorio");
                }
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    errorProvider1.SetError(textBox2, "Campo obligatorio");
                }
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    errorProvider1.SetError(textBox1, "Campo obligatorio");
                }
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    errorProvider1.SetError(textBox3, "Campo obligatorio");
                }
             
                if (string.IsNullOrEmpty(textBox4.Text))
                {
                    errorProvider1.SetError(textBox4, "Campo obligatorio");
                }
                if (string.IsNullOrEmpty(textBox5.Text))
                {
                    errorProvider1.SetError(textBox5, "Campo obligatorio");
                }
                if (string.IsNullOrEmpty(textBox6.Text))
                {
                    errorProvider1.SetError(textBox6, "Campo obligatorio");
                }
                if (string.IsNullOrEmpty(textBox7.Text))
                {
                    errorProvider1.SetError(textBox7, "Campo obligatorio");
                }
                if (string.IsNullOrEmpty(textBox8.Text))
                {
                    errorProvider1.SetError(textBox8, "Campo obligatorio");
                }
             


                
                    if (!string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString()) 
                    && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox1.Text) &&
                    !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text) &&
                    !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrEmpty(textBox6.Text) &&
                    !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrEmpty(textBox8.Text) &&
                    !string.IsNullOrEmpty(descripcion.Text))
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "update items set Descripcion='"+ descripcion.Text.Trim()+ "'" +
                            ",Impuesto='"+ impuesto.Text.Trim() + "',OnHand='"+textBox1.Text+"'" +
                            ",Precio='"+textBox2.Text+"',Costo='"+textBox3.Text+"'," +
                            "Marca='"+textBox4.Text+"',Talla='"+textBox5.Text+"',Familia='"+textBox6.Text+"',Estilo='"+textBox7.Text+"'," +
                            "Genero='"+textBox8.Text+"' where Barcode='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        mysql.Dispose();
                        MessageBox.Show("Solicitud procesada correctamente","Acción realizada",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        button1.Enabled = false;
                        barcode.Focus();
                        limpiar();
                    }

                }



            }
            catch (Exception excep)
            {

                Mensaje.Error(excep, "102");
            }
        }

        public void limpiar()
        {
            textBox2.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            descripcion.Text = "";
            errorProvider1.Clear();
        }

        private void descripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)
                  || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)
               || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar)
               || char.IsWhiteSpace(e.KeyChar) ||e.KeyChar=='.')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar)
               || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar)
              || char.IsWhiteSpace(e.KeyChar) ||e.KeyChar=='.')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)
              || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)
              || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)
              || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)
              || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)
              || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
