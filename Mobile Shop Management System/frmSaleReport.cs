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
    public partial class frmSaleReport : Form
    {
        System.Data.SQLite.SQLiteConnection con;
        SQLiteDataAdapter adapt;
        String from, to;
        frmMain frmMain;

        long totalBalance;
        public frmSaleReport(frmMain frmMain)
        {
            this.frmMain = frmMain;
            InitializeComponent();
            con = new SQLiteConnection("Data Source=database.db;Version=3;New=false;Compress=True;");
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            from = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            to = dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");
            con.Open();
            DataTable dt = new DataTable();
            MessageBox.Show(from);
           // adapt = new SQLiteDataAdapter("SELECT id as ID, type as AccountTitle , invoiceid as InvoiceID ,accountid as AccountID ,payment as Payment ,receipt as Receipt  from tblAccountTransaction where date(date) between date('" + from + "') and date('" + to + "')", con);
            adapt = new SQLiteDataAdapter("SELECT item.date as Date,item.sale_itemid as InvNo  ,description.imie as Imie,description.description ,sale_price as Rate from tblSaleInvoiceItem as item inner join tblPurchase as description  on item.purchase_itemid=description.id where date(date) between date('" + from + "') and date('" + to + "')", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }

        public void RefreshGridView()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT item.date as Date,item.sale_itemid as InvNo  ,description.imie as Imie,description.description ,sale_price as Rate from tblSaleInvoiceItem as item inner join tblPurchase as description  on item.purchase_itemid=description.id", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            con.Close();
        }

        private void frmCashBook_Load(object sender, EventArgs e)
        {
            RefreshGridView();
            countTotals();
          
        }

        public void countTotals()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT SUM(payment) as Payment ,SUM(receipt) as Receipt  from tblAccountTransaction", con);
            adapt.Fill(dt);
            con.Close();

           // totalPaymentTextBox.Text = dt.Rows[0]["Payment"].ToString();
            //totalReceiptTextBox.Text = dt.Rows[0]["Receipt"].ToString();

            //totalBalance = Convert.ToInt64(dt.Rows[0]["Receipt"].ToString()) - Convert.ToInt64(dt.Rows[0]["Payment"].ToString());
           // totalBalanceTextBox.Text = totalBalance.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSaleReportViewer frmSaleReportViewer = new frmSaleReportViewer(from,to);
            frmSaleReportViewer.MdiParent = frmMain;
            frmSaleReportViewer.WindowState = FormWindowState.Maximized;
            
            frmSaleReportViewer.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
