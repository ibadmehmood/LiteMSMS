using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mobile_Shop_Management_System
{
    public partial class frmPurchaseReportViewer : Form
    {
        System.Data.SQLite.SQLiteConnection con;
        SQLiteDataAdapter adapt;
        String from, to;

        public frmPurchaseReportViewer(String from, String to)
        {
            this.from = from;
            this.to = to;

            InitializeComponent();
            con = new SQLiteConnection("Data Source=database.db;Version=3;New=false;Compress=True;");
        }

        private void frmSaleReportViewer_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

            con.Open();
            // DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(from) & !string.IsNullOrEmpty(to))
            {

                // MessageBox.Show("not null");
                adapt = new SQLiteDataAdapter("SELECT * from tblPurchase where date(purchase_date) between date('" + from + "') and date('" + to + "')", con);
            }
            else
            {
                //MessageBox.Show("null");
                adapt = new SQLiteDataAdapter("SELECT * from tblPurchase", con);
            }

            //adapt.Fill(dt);
            PurchaseReport dataset = new PurchaseReport();

            //  adapt.Fill(dataset);
            adapt.Fill(dataset, dataset.Tables[0].TableName);
            ReportParameter reportParameter = new ReportParameter("date", DateTime.Now.ToString());
            var reportDataSource1 = new ReportDataSource("SaleReport", dataset.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.SetParameters(reportParameter);
            reportViewer1.ProcessingMode = ProcessingMode.Local;

            reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();

            con.Close();

        }
    }
}
