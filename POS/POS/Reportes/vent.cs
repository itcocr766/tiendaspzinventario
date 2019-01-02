using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using POS.Modelo;
namespace POS.Reportes
{
    public partial class vent : Form
    {
        public vent()
        {
            InitializeComponent();
        }

        private void vent_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reportViewer1.Clear();
            using (var mysql = new Mysql())
            {
                mysql.conexion();
                mysql.cadenasql = "select Z.nombre,(sum(Z.factura)+sum(Z.sale)+sum(Z.abono)) as Total from (SELECT  v.Nombre,SUM(f.Total) as factura,0 as sale,0 as abono FROM vendedores v INNER JOIN  factura f ON v.Codigo=f.CodigoVendedor WHERE   f.Tipo='Factura'   AND f.Fecha     BETWEEN '"+ dateTimePicker1.Value.ToString("yyy-MM-dd") + "' AND '"+dateTimePicker2.Value.ToString("yyyy-MM-dd")+"' GROUP BY v.Nombre union all SELECT v.Nombre,0 as factura,SUM(sales.Total)as sale,0 as abono FROM vendedores v     INNER JOIN sales     ON v.Codigo=sales.CodigoVendedor WHERE sales.Fecha BETWEEN '"+ dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '"+ dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' GROUP BY v.Nombre union all  SELECT v.Nombre, 0 as factura ,0 as sale ,SUM(a.Abono) as abono FROM vendedores v    INNER JOIN  saldos  c  ON v.Codigo=c.CodigoVendedor INNER JOIN abonos a  ON c.NumFactura=a.NumFactura where     a.Fecha     BETWEEN '"+ dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '"+ dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' GROUP BY v.Nombre )Z  group by Z.Nombre";

                MySqlDataAdapter adapt = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                adapt.Fill(DataSet1.dt);
                mysql.Dispose();
            }

            this.reportViewer1.RefreshReport();
        }

        private void dtBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
