using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Configuracion
{
    public partial class configuracion : Form
    {
        string cadena = "";
        string cadena2 = "";
        string apicomp = "";
        string idcomp = "";
        string endpo = "";
        string cierreimp = "";
        string sucur = "";
        string subs = "";
        string autoincrement="";
        public configuracion()
        {
            InitializeComponent();
        }

        private void configuracion_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
        public void guardar()
        {

            autoincrement = textBox5.Text.Trim();
            Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conf.AppSettings.Settings.Remove("autoincrement");
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
            conf.AppSettings.Settings.Add("autoincrement", autoincrement);
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void button1_Click(object sender, EventArgs e)
        {

            guardar();

            cadena = "server=" + textBox1.Text.Trim() + ";" + "port=" + textBox2.Text.Trim() + ";username=" + textBox3.Text.Trim() +
                 ";password=" + textBox4.Text.Trim() + ";SslMode = none;database=" + textBox5.Text.Trim();

            Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conf.AppSettings.Settings.Remove("cadena");
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
            conf.AppSettings.Settings.Add("cadena", cadena);
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");

            MessageBox.Show("Configuracion exitosa. Verifique la cadena de conexion  " + ConfigurationManager.AppSettings["cadena"]);

            //server = 127.0.0.1; port = 3306; username = root; password =; SslMode = none; database = posnew
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cadena2 = "               " + textBox6.Text.Trim() + "\n               " +  textBox7.Text.Trim() + "\n          " +  textBox8.Text.Trim() + "\n          " +
                 textBox9.Text.Trim()  + "\n               " + textBox10.Text.Trim();

            Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conf.AppSettings.Settings.Remove("cadena2");
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
            conf.AppSettings.Settings.Add("cadena2", cadena2);
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");

            MessageBox.Show("Configuración exitosa. Verifique la cadena de conexión  " + ConfigurationManager.AppSettings["cadena2"]);
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            companyapi();
            idcompi();
            endopo();
            sucurs();
            cierreimpresion();
            substr();
            MessageBox.Show("Configuración exitosa. Verifique la cadena de conexión \n" + ConfigurationManager.AppSettings["apicomp"] + "\n" +
                ConfigurationManager.AppSettings["idcomp"] + "\n" + ConfigurationManager.AppSettings["endpo"] + "\n" + ConfigurationManager.AppSettings["sucur"] + "\n" +
                ConfigurationManager.AppSettings["subs"] + "\n" + ConfigurationManager.AppSettings["cierreimp"]);
        }

        public void substr()
        {
            subs = textBox13.Text;
            Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conf.AppSettings.Settings.Remove("subs");
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
            conf.AppSettings.Settings.Add("subs", subs);
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");

        }

        public void cierreimpresion()
        {

            Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conf.AppSettings.Settings.Remove("cierreimp");
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
            conf.AppSettings.Settings.Add("cierreimp", cierreimp);
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");

        }

        public void sucurs()
        {
            sucur = textBox11.Text;
            Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conf.AppSettings.Settings.Remove("sucur");
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
            conf.AppSettings.Settings.Add("sucur", sucur);
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }


        public void companyapi()
        {
            apicomp = comboBox1.Text;
            Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conf.AppSettings.Settings.Remove("apicomp");
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
            conf.AppSettings.Settings.Add("apicomp", apicomp);
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
         
        }
        public void idcompi()
        {
            idcomp = textBox12.Text;
            Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conf.AppSettings.Settings.Remove("idcomp");
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
            conf.AppSettings.Settings.Add("idcomp", idcomp);
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }
        public void endopo()
        {
            endpo = comboBox2.Text;
            Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conf.AppSettings.Settings.Remove("endpo");
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
            conf.AppSettings.Settings.Add("endpo", endpo);
            conf.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "b6fc970b-5bfe-4af2-8f9f-baa7be749237")
            {
                label11.Text = "Latinos";
                textBox12.Text = "2";
                comboBox2.SelectedIndex = 0;
                textBox1.Text = "104.43.243.208";
                textBox3.Text = "pos";
                textBox4.Text = "123";
                textBox5.Text = "latinos";
                textBox6.Text = "Inversiones Caliana S.A";
                textBox7.Text = "3101036705";
                textBox8.Text = "@LATINOSSTORE";
                textBox9.Text = "PEREZ ZELEDON";
                textBox10.Text = "27711250";
                textBox11.Text = "001";
                textBox13.Text = "2";
                cierreimp = "LATINOS";
            }
            else if (comboBox1.Text == "efc4eab6-e521-4da6-a1eb-c73ca8ae937b")
            {
                label11.Text = "Tu pie";
                textBox12.Text = "9";
                comboBox2.SelectedIndex = 0;
                textBox1.Text = "104.43.243.208";
                textBox3.Text = "pos";
                textBox4.Text = "123";
                textBox5.Text = "tupie";
                textBox6.Text = "FERMO DEL SUR S.A.";
                textBox7.Text = "3101190675";
                textBox8.Text = "PEREZ ZELEDON";
                textBox9.Text = "SAN ISIDRO DEL GENERAL";
                textBox10.Text = "TU  PIE";
                textBox11.Text = "003";
                textBox13.Text = "2";
                cierreimp = "TU PIE";
            }

            else if (comboBox1.Text == "45e7a866-381b-4a1a-81c7-47cdfee2f620")
            {
                label11.Text = "outlet";
                textBox12.Text = "5";
                comboBox2.SelectedIndex = 0;
                textBox1.Text = "104.43.243.208";
                textBox3.Text = "pos";
                textBox4.Text = "123";
                textBox5.Text = "generaleno1";
                textBox6.Text = "MODA MULTIMARCAS S.A.";
                textBox7.Text = "3101766015";
                textBox8.Text = "PEREZ ZELEDON";
                textBox9.Text = "SAN ISIDRO DEL GENERAL";
                textBox10.Text = "OUTLET";
                textBox11.Text = "002";
                textBox13.Text = "2";
                cierreimp = "OUTLET";
            }
            else if (comboBox1.Text == "db2aaa1b-81b9-4b97-b08e-c01a59a087b3")
            {
                label11.Text = "La canasta1";
                textBox12.Text = "6";
                comboBox2.SelectedIndex = 0;
                textBox1.Text = "104.43.243.208";
                textBox3.Text = "pos";
                textBox4.Text = "123";
                textBox5.Text = "canasta";
                textBox6.Text = "FERMO DEL SUR S.A.";
                textBox7.Text = "3101190675";
                textBox8.Text = "PEREZ ZELEDON";
                textBox9.Text = "SAN ISIDRO DEL GENERAL";
                textBox10.Text = "LA CANASTA";
                textBox11.Text = "001";
                textBox13.Text = "2";
                cierreimp = "LA CANASTA 1";
            }
            else if (comboBox1.Text == "2650bd1c-39aa-497a-864b-1795056b19ad")
            {
                label11.Text = "La canasta2";
                textBox12.Text = "7";
                comboBox2.SelectedIndex = 0;
                textBox1.Text = "104.43.243.208";
                textBox3.Text = "pos";
                textBox4.Text = "123";
                textBox5.Text = "canasta22";
                textBox6.Text = "FERMO DEL SUR S.A.";
                textBox7.Text = "3101190675";
                textBox8.Text = "PEREZ ZELEDON";
                textBox9.Text = "SAN ISIDRO DEL GENERAL";
                textBox10.Text = "CANASTA2";
                textBox11.Text = "002";
                textBox13.Text = "2";
                cierreimp = "LA CANASTA 2";
            }
            else if (comboBox1.Text == "fcf0cbed-5531-4f91-ad44-d4386f59fab9")
            {
                label11.Text = "Sucursal latinos";
                textBox12.Text = "21";
                comboBox2.SelectedIndex = 0;
                textBox1.Text = "104.43.243.208";
                textBox3.Text = "pos";
                textBox4.Text = "123";
                textBox5.Text = "latinossuc";
                textBox6.Text = "Inversiones Caliana S.A";
                textBox7.Text = "3101036705";
                textBox8.Text = "PEREZ ZELEDON";
                textBox9.Text = "SAN ISIDRO DEL GENERAL";
                textBox10.Text = "La canasta";
                textBox11.Text = "002";
                textBox13.Text = "3";
                cierreimp = "LA CANASTA";

            }
            else if (comboBox1.Text == "7dde08c9-fb28-48a4-b42f-96a45ba9a1b8")
            {
                label11.Text = "Desafio";
                textBox12.Text = "3";
                comboBox2.SelectedIndex = 0;
                textBox1.Text = "104.43.243.208";
                textBox3.Text = "pos";
                textBox4.Text = "123";
                textBox5.Text = "desafio";
                textBox6.Text = "MODA MULTIMARCAS S.A.";
                textBox7.Text = "3101766015";
                textBox8.Text = "PEREZ ZELEDON";
                textBox9.Text = "SAN ISIDRO DEL GENERAL";
                textBox10.Text = "DESAFIO";
                textBox11.Text = "001";
                textBox13.Text = "2";
                cierreimp = "DESAFIO";
            }
            else
            {
                label11.Text = "Pruebas";
                textBox12.Text = "1";
                comboBox2.SelectedIndex = 1;
                textBox1.Text = "127.0.0.1";
                textBox3.Text = "root";
                textBox4.Text = "Rivipe19866";
                textBox5.Text = "generaleno1";
                textBox11.Text = "001";
                textBox13.Text = "2";
                cierreimp = "pruebas";
            }
        }
    }
}
