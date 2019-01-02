using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using POS.Modelo;
namespace POS.productosprincipal
{
    public partial class creaitem : Form
    {
       
        public creaitem()
        {
            InitializeComponent();
        }

        private void creaitem_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            promod prm = new promod();
            prm.Show(this);
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            insertar();
        }


        public void insertar()
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox2.Text)&& 
                  
                    !string.IsNullOrEmpty(richTextBox1.Text) && !string.IsNullOrEmpty(comboBox1.Text))
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql =
                            "insert into items(Barcode,Descripcion,Impuesto)values('"
                            + textBox2.Text.Trim() + "','" + richTextBox1.Text.Trim() + "','" +
                            comboBox1.Text.ToUpper().Trim() + "')";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        mysql.Dispose();
                        MessageBox.Show("La solicitud se proceso correctamente", "Acción realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
                else
                {

                    MessageBox.Show("Falta información","Faltan datos",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }


                textBox2.Focus();
            }
            catch (Exception exec)
            {

                Mensaje.Error(exec,"67");

            }
           
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                comboBox1.Focus();

            }
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                richTextBox1.Focus();

            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();

            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                richTextBox1.Focus();

            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();

            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            try
            {
                if (string.IsNullOrEmpty(richTextBox1.Text))
                {
                    errorProvider1.SetError(richTextBox1, "Campo obligatorio");
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
                if (string.IsNullOrEmpty(textBox9.Text))
                {
                    errorProvider1.SetError(textBox9, "Campo obligatorio");
                }

                if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox1.Text) &&
                    !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text) &&
                    !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrEmpty(textBox6.Text) &&
                    !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrEmpty(textBox8.Text) &&
                    !string.IsNullOrEmpty(textBox9.Text)&& !string.IsNullOrEmpty(richTextBox1.Text))
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql =
                            "INSERT INTO `items`(`Barcode`, `Descripcion`, `Impuesto`, `Precio`, `OnHand`, `Costo`, `Marca`, `Talla`, `Familia`, `Estilo`, `Genero`) VALUES (@ba,@de,@im,@pr,@on,@co,@ma,@ta,@fa,@es,@ge)";

                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.Parameters.AddWithValue("@ba", textBox2.Text);
                        mysql.comando.Parameters.AddWithValue("@de", richTextBox1.Text);
                        mysql.comando.Parameters.AddWithValue("@im", comboBox1.Text);
                        mysql.comando.Parameters.AddWithValue("@pr", textBox1.Text);
                        mysql.comando.Parameters.AddWithValue("@on", textBox3.Text);
                        mysql.comando.Parameters.AddWithValue("@co", textBox4.Text);
                        mysql.comando.Parameters.AddWithValue("@ma", textBox5.Text);
                        mysql.comando.Parameters.AddWithValue("@ta", textBox6.Text);
                        mysql.comando.Parameters.AddWithValue("@fa", textBox7.Text);
                        mysql.comando.Parameters.AddWithValue("@es", textBox8.Text);
                        mysql.comando.Parameters.AddWithValue("@ge", textBox9.Text);
                        mysql.comando.ExecuteNonQuery();
                        mysql.Dispose();
                        MessageBox.Show("La solicitud se proceso correctamente", "Acción realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiar();
                    }


                }
         


                textBox2.Focus();
            }
            catch (MySqlException my)
            {
                MessageBox.Show("Este producto ya existe.Intente con otro código", "No se aceptan códigos repetidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception exec)
            {

                Mensaje.Error(exec, "67");

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
            textBox9.Text = "";
            richTextBox1.Text = "";
            errorProvider1.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) ||char.IsControl(e.KeyChar) ||e.KeyChar=='.')
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
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)
                ||char.IsWhiteSpace(e.KeyChar))
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
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar))
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
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == '.')
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
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
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
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
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
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
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
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
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
