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
    public partial class frmPaymentReceipt : Form
    {
        System.Data.SQLite.SQLiteConnection con;
        SQLiteDataAdapter adapt;
        long userid;


        public frmPaymentReceipt()
        {
            InitializeComponent();
            con = new SQLiteConnection("Data Source=database.db;Version=3;New=false;Compress=True;");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmCashBook_Load(object sender, EventArgs e)
        {
            receiptButton.Checked = true;
            paymentButton.CheckedChanged+=paymentButton_CheckedChanged;
            receiptButton.CheckedChanged += paymentButton_CheckedChanged;
            accountnumberTextBox.KeyDown += accountnumberTextBox_KeyDown;
            generateSuggestions();
            RefreshGridView();

            
        }

        private void paymentButton_CheckedChanged(object sender, EventArgs e)
        {
            
        }
        public void RefreshGridView()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("select user.id as ID,user.name as Name,user.phone as Phone ,sum(transaction1.userbalance) as Balance from tblAccount as user inner join tblAccountTransaction as transaction1 on user.id=transaction1.accountid GROUP BY transaction1.accountid", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
          
            con.Close();
        }

        public void generateSuggestions()
        {
            AutoCompleteStringCollection phoneCollection = new AutoCompleteStringCollection();

            con.Open();
            SQLiteCommand cmnd1 = con.CreateCommand();
            cmnd1.CommandType = CommandType.Text;
            cmnd1.CommandText = "SELECT * FROM tblAccount";
            SQLiteDataReader Reader;
            Reader = cmnd1.ExecuteReader();

          
                while (Reader.Read())
                {
                    phoneCollection.Add(Reader["phone"].ToString());


                }

            
           
            Reader.Close();

            accountnumberTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            accountnumberTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            accountnumberTextBox.AutoCompleteCustomSource = phoneCollection;
            con.Close();
        }

        private void accountnumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down
                if (accountnumberTextBox.TextLength > 1)
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    adapt = new SQLiteDataAdapter("select * from tblAccount where phone='" + accountnumberTextBox.Text.ToString() + "'", con);
                    adapt.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        nameTextBox.Text = dt.Rows[0]["name"].ToString();
                       
                        userid = Convert.ToInt64(dt.Rows[0]["id"]);
                        GetUserBalance();

                        //purchaseitemid = Convert.ToInt64(dt.Rows[0]["id"]);
                    }


                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
           
            
                
            /*
                SQLiteCommand cmd1 = new SQLiteCommand("update tblAccount set balance=@balance where phone=@phone", con);
                con.Open();
                if (paymentButton.Checked == true){
                    cmd1.Parameters.AddWithValue("@balance", Convert.ToInt64(balanceTextBox.Text.ToString()) + Convert.ToInt64(amountTextBox.Text.ToString()));
                }

                else
                {
                    cmd1.Parameters.AddWithValue("@balance", Convert.ToInt64(balanceTextBox.Text.ToString()) - Convert.ToInt64(amountTextBox.Text.ToString()));
                }

            
                cmd1.Parameters.AddWithValue("@phone", Convert.ToInt64(accountnumberTextBox.Text.ToString()));
                cmd1.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Balance Updated!");
                RefreshGridView();
                

                SQLiteCommand cmd2 = new SQLiteCommand("insert into tblAccountTransaction(type,invoiceid,accountid,payment,receipt,date) values(@type,@invoiceid,@accountid,@payment,@receipt,@date)", con);
                con.Open();
                cmd2.Parameters.AddWithValue("@type", "payment");
                cmd2.Parameters.AddWithValue("@invoiceid", null);
                cmd2.Parameters.AddWithValue("date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd2.Parameters.AddWithValue("@accountid",userid );
                if (paymentButton.Checked == true)
                {
                    cmd2.Parameters.AddWithValue("@payment", Convert.ToInt64(amountTextBox.Text.ToString()));
                    cmd2.Parameters.AddWithValue("@receipt",null);
                   
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@receipt",Convert.ToInt64(amountTextBox.Text.ToString()));
                    cmd2.Parameters.AddWithValue("@payment", null);
                }
                
               

                cmd2.ExecuteNonQuery();
                con.Close();
                clearAll();
             */


           

                SQLiteCommand cmd6 = new SQLiteCommand("insert into tblAccountTransaction(type,invoiceid,accountid,payment,receipt,date,userbalance) values(@type,@invoiceid,@accountid,@payment,@receipt,@date,@balance)", con);
                con.Open();
               
                cmd6.Parameters.AddWithValue("@invoiceid",null);
                cmd6.Parameters.AddWithValue("@accountid", userid);
                if (paymentButton.Checked == true)
                {
                    cmd6.Parameters.AddWithValue("@type", "payment");
                    cmd6.Parameters.AddWithValue("@payment", Convert.ToInt64(amountTextBox.Text.ToString()));
                    cmd6.Parameters.AddWithValue("@receipt", null);
                    cmd6.Parameters.AddWithValue("@balance", Convert.ToInt64(amountTextBox.Text.ToString()));

                }
                else
                {
                    cmd6.Parameters.AddWithValue("@type", "receipt");
                    cmd6.Parameters.AddWithValue("@receipt", Convert.ToInt64(amountTextBox.Text.ToString()));
                    cmd6.Parameters.AddWithValue("@payment", null);
                    cmd6.Parameters.AddWithValue("@balance", -Convert.ToInt64(amountTextBox.Text.ToString()));
                }
               // cmd6.Parameters.AddWithValue("@payment", Convert.ToInt64(amountTextBox.Text.ToString()));
                cmd6.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                //cmd6.Parameters.AddWithValue("@receipt", Convert.ToInt64(amountTextBox.Text.ToString()));
                
                cmd6.ExecuteNonQuery();
                con.Close();

                
                RefreshGridView();
                MessageBox.Show("Balance Updated!");
                

        }
        public void clearAll()
        {
            accountnumberTextBox.Text = "";
            balanceTextBox.Text = "";
            nameTextBox.Text = "";
            amountTextBox.Text = "";
            
        }

        public void GetUserBalance()
        {
            DataTable dt10 = new DataTable();
            adapt = new SQLiteDataAdapter("select sum(userbalance) as userbalance from tblAccountTransaction where accountid=" + userid + "", con);
            con.Open();
            adapt.Fill(dt10);
            if (dt10.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt10.Rows[0]["userbalance"].ToString()))
                {

                    
                    balanceTextBox.Text = dt10.Rows[0]["userbalance"].ToString();
                   
                }

                else
                {
                    
                    balanceTextBox.Text ="0";
                }

            }
            con.Close();

        }
    }
}
