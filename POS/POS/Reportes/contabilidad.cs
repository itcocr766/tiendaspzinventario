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

namespace POS.Reportes
{
    public partial class contabilidad : Form
    {
        public contabilidad()
        {
            InitializeComponent();
        }

        private void contabilidad_Load(object sender, EventArgs e)
        {

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.reportViewer1.Clear();

            using (var mysql = new Mysql())
            {
                mysql.conexion();
                mysql.cadenasql = "SELECT factura.Fecha,detalles.NumeroFactura,(detalles.Precio*detalles.Cantidad) AS BrutoGravado,ROUND((((detalles.Precio*detalles.Cantidad)/1.13)-detalles.Descuento),2) AS Brutosingravar,ROUND(((((detalles.Precio*detalles.Cantidad)/1.13)-detalles.Descuento)*13)/100,2) AS Impuesto FROM factura,detalles where factura.Numero=detalles.NumeroFactura AND detalles.Impuesto='(G)' AND factura.Fecha BETWEEN '"+ dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '"+dateTimePicker2.Value.ToString("yyyy-MM-dd")+"'";

                MySqlDataAdapter adapt = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                adapt.Fill(DataSet1.bgravado);

                mysql.cadenasql = "SELECT factura.Fecha,detalles.NumeroFactura,0 AS BrutoGravado,ROUND((((detalles.Precio*detalles.Cantidad))-detalles.Descuento),2) AS Brutosingravar,0 AS Impuesto FROM factura,detalles where factura.Numero=detalles.NumeroFactura AND detalles.Impuesto='(E)' AND factura.Fecha BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";

                MySqlDataAdapter adapt2 = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                adapt2.Fill(DataSet1.bsingrav);

                mysql.cadenasql = "SELECT factura.Fecha, abonos.NumFactura as Nfac, abonos.Abono AS BrutoGravado, ROUND(abonos.Abono / 1.13) AS Brutosingravar, ROUND(((abonos.Abono / 1.13) * 13) / 100, 2) AS Impuesto FROM factura, abonos WHERE factura.Numero = abonos.NumFactura AND abonos.Fecha BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
                MySqlDataAdapter adapt3 = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                adapt3.Fill(DataSet1.abon);

                mysql.Dispose();
            }

            this.reportViewer1.RefreshReport();
           
        }

        private void bsingravBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void abonBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
