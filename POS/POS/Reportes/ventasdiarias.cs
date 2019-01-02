using MySql.Data.MySqlClient;
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
using Microsoft.Reporting.WinForms;
namespace POS.Reportes
{
    public partial class ventasdiarias : Form
    {
        public ventasdiarias()
        {
            InitializeComponent();
        }

        private void ventasdiarias_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reportViewer1.Clear();
            using (var mysql = new Mysql())
            {
                mysql.conexion();
                mysql.cadenasql = "SELECT Fecha,SUM(CASE WHEN T.Consulta = 1 THEN T.Total ELSE 0 END) AS F,SUM(CASE WHEN T.Consulta = 2 THEN T.Total ELSE 0 END) AS P,SUM(CASE WHEN T.Consulta = 3 THEN T.Total ELSE 0 END) AS Abonos,SUM(T.Total) AS Total FROM (SELECT 1 AS Consulta, Date(f.Fecha) as Fecha, SUM(f.Total) as Total FROM factura f WHERE f.Tipo='Factura' AND f.Fecha BETWEEN '"+ dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '"+ dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' GROUP BY Date(f.Fecha) UNION ALL SELECT 2, Date(s.Fecha), SUM(s.Total) FROM sales s WHERE s.Fecha BETWEEN '"+ dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '"+ dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' GROUP BY Date(s.Fecha) UNION ALL SELECT 3, Date(a.Fecha), SUM(a.Abono) FROM abonos a WHERE a.Fecha BETWEEN '"+ dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") +"' GROUP BY Date(a.Fecha)) T GROUP BY T.Fecha ORDER BY T.Fecha";

                MySqlDataAdapter adapt = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                adapt.Fill(DataSet1.ventdia);
                mysql.Dispose();
            }

            this.reportViewer1.RefreshReport();
        }

        private void ventdiaBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
