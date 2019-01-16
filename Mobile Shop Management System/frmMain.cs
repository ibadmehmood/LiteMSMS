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
    public partial class frmMain : Form

    {
        System.Data.SQLite.SQLiteConnection con;
        SQLiteDataAdapter adapt;
        public frmMain()
        {
            
            InitializeComponent();
            
            con = new SQLiteConnection("Data Source=database.db;Version=3;New=false;Compress=True;");
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBackup backupForm = new frmBackup();
            backupForm.MdiParent = this;
            backupForm.Show();
        }

        private void purchaseInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddMobile frmAddMobile = new frmAddMobile();
            frmAddMobile.MdiParent = this;
            frmAddMobile.Show();
        }

        private void saleInvoiceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmSaleInvoice frmSaleInvoice = new frmSaleInvoice();
            frmSaleInvoice.MdiParent = this;
            frmSaleInvoice.Show();
        }

        private void managementToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addEditAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddAccount frmAddAccount = new frmAddAccount();
            frmAddAccount.MdiParent = this;
            frmAddAccount.Show();
        }

        private void cashBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPaymentReceipt frmCashBook = new frmPaymentReceipt();
            frmCashBook.MdiParent = this;
            frmCashBook.Show();
        }

        private void cashBookToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void cashBookToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmCashBook frmCashBook = new frmCashBook();
            frmCashBook.MdiParent = this;
            frmCashBook.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {

            con.Open();
           // DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT id, name,phone,created_at,balance,cnic from tblAccount", con);
            //adapt.Fill(dt);
            DataSet1 dataset = new DataSet1();

          //  adapt.Fill(dataset);
            adapt.Fill(dataset,dataset.Tables[0].TableName);
            
            MessageBox.Show(dataset.DataSetName.ToString());


           // ReportDataSource reportDataSource = new ReportDataSource();
            // Must match the DataSource in the RDLC
           // reportDataSource.Name = "DataSet1";
           // reportDataSource.Value = dataset.Tables[0];



            MessageBox.Show( dataset.Tables[0].Rows.Count.ToString());
            //

           // LocalReport localReport = reportViewer1.LocalReport;

           // localReport.ReportPath = AppDomain.CurrentDomain.BaseDirectory + "invoice.rdlc";

            

            var reportDataSource1 = new ReportDataSource("tblAccount", dataset.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            // this.reportViewer1.LocalReport.ReportEmbeddedResource = 
          //  reportViewer1.LocalReport.ReportPath = AppDomain.CurrentDomain.BaseDirectory + "invoice.rdlc";
            reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
            
           // reportViewer1.LocalReport.DataSources.Add(reportDataSource);


           // reportViewer1.LocalReport.Refresh();
           // reportViewer1.RefreshReport();
            con.Close();

        }

        private void itemCostQuickSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSaleReport frmSaleReport = new frmSaleReport(this);
            frmSaleReport.MdiParent = this;
            frmSaleReport.Show();
        }

        private void saleProfitReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSaleProfitReport frmSaleProfitReport = new frmSaleProfitReport(this);
            frmSaleProfitReport.MdiParent = this;
            frmSaleProfitReport.Show();
        }

        private void purchaseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPurchaseReport frmPurchaseReport = new frmPurchaseReport(this);
            frmPurchaseReport.MdiParent = this;
            frmPurchaseReport.Show();
        }

        private void itemCostQuickSearchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCostSearch frmPurchaseReport = new frmCostSearch();
            frmPurchaseReport.MdiParent = this;
            frmPurchaseReport.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 frmPurchaseReport = new AboutBox1();
            frmPurchaseReport.MdiParent = this;
            frmPurchaseReport.Show();
        }

        private void saleInvoiceReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmSaleReturn frmPurchaseReport = new frmSaleReturn();
            frmPurchaseReport.MdiParent = this;
            frmPurchaseReport.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPurchaseInvoice frmPurchaseReport = new frmPurchaseInvoice();
            frmPurchaseReport.MdiParent = this;
            frmPurchaseReport.Show();
        }

        private void purchaseInvoiceReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPurchaseReturn frmPurchaseReport = new frmPurchaseReturn();
            
            frmPurchaseReport.MdiParent = this;
            frmPurchaseReport.Show();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void addEditUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUser frmAddUser = new frmAddUser();
            frmAddUser.MdiParent = this;
            frmAddUser.Show();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRestore frmAddUser = new FrmRestore();
            frmAddUser.MdiParent = this;
            frmAddUser.Show();
        }

        private void userAccountReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserAccountReport frmAddUser = new frmUserAccountReport();
            frmAddUser.MdiParent = this;
            frmAddUser.Show();
        }
    }
}
