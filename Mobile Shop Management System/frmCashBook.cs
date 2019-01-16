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
    public partial class frmCashBook : Form
    {
        System.Data.SQLite.SQLiteConnection con;
        SQLiteDataAdapter adapt;
        String from, to;

        long totalBalance;
        public frmCashBook()
        {
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
            adapt = new SQLiteDataAdapter("SELECT id as ID, type as AccountTitle , invoiceid as InvoiceID ,accountid as AccountID ,payment as Payment ,receipt as Receipt  from tblAccountTransaction where date(date) between date('"+from +"') and date('"+to+"')" , con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
           
            con.Close();
            countTotals(from, to);
        }

        public void RefreshGridView()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT id as ID, type as AccountTitle , invoiceid as InvoiceID ,accountid as AccountID ,payment as Payment ,receipt as Receipt  from tblAccountTransaction", con);
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
            from = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            to = dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");
        }


        public void countTotals()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT SUM(payment) as Payment ,SUM(receipt) as Receipt  from tblAccountTransaction", con);
            adapt.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                long receipt=0, payment = 0;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Payment"].ToString()))
                {
                    totalPaymentTextBox.Text = dt.Rows[0]["Payment"].ToString();
                    payment = Convert.ToInt64(dt.Rows[0]["Payment"].ToString());
                }
                else
                {
                    totalPaymentTextBox.Text = "0" ;
                }
               

                if (!string.IsNullOrEmpty(dt.Rows[0]["Receipt"].ToString()))
                {
                    totalReceiptTextBox.Text = dt.Rows[0]["Receipt"].ToString();
                    receipt = Convert.ToInt64(dt.Rows[0]["Receipt"].ToString());

                }
                else
                {
                    totalReceiptTextBox.Text = "0";
                }
                
                
                totalBalance = receipt - payment;
                totalBalanceTextBox.Text = totalBalance.ToString();
            }
            
        }

        public void countTotals(string from,string to)
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT SUM(payment) as Payment ,SUM(receipt) as Receipt  from tblAccountTransaction where date(date) between date('"+from+"')"+ "and date('"+to+"')", con);
            adapt.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                long receipt = 0, payment = 0;
                
                if (!string.IsNullOrEmpty(dt.Rows[0]["Payment"].ToString()))
                {
                    totalPaymentTextBox.Text = dt.Rows[0]["Payment"].ToString();
                    payment = Convert.ToInt64(dt.Rows[0]["Payment"].ToString());
                }
                else
                {
                    totalPaymentTextBox.Text = "0";
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["Receipt"].ToString()))
                {
                    totalReceiptTextBox.Text = dt.Rows[0]["Receipt"].ToString();
                    receipt = Convert.ToInt64(dt.Rows[0]["Receipt"].ToString());

                }
                else
                {
                    totalReceiptTextBox.Text = "0";
                }



                totalBalance = receipt - payment;
                totalBalanceTextBox.Text = totalBalance.ToString();
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
                
        }
    }
}
