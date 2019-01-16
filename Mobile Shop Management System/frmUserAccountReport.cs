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
    public partial class frmUserAccountReport : Form
    {
        System.Data.SQLite.SQLiteConnection con;
        SQLiteDataAdapter adapt;
        Int64 userid;
        string date;
        
        public frmUserAccountReport()
        {
            InitializeComponent();
            con = new SQLiteConnection("Data Source=database.db;Version=3;New=false;Compress=True;");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (accountnumberTextBox != null )
            {
                string from = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") ;
                string to = dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");
                GetUserBalance(userid, from, to);
              // MessageBox.Show(from);
               //MessageBox.Show(userid.ToString());
               RefreshGridView(accountnumberTextBox.Text.ToString(),from, to);
               
              

               
            }
            else
            {
                MessageBox.Show("Account Number");
            }
            
        }

        private void frmUserAccountReport_Load(object sender, EventArgs e)
        {
            accountnumberTextBox.KeyDown += accountnumberTextBox_KeyDown;
            generateSuggestions();
            RefreshGridView();
        }


        public void RefreshGridView()
        {
            Int64 balance=0;
            string description="";
            DataTable table = new DataTable();
            table.Columns.Add("Phone/ID", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Description",typeof(string));
          //  table.Columns.Add("type", typeof(string));
           // table.Columns.Add("invoiceid", typeof(Int64));
            
           // table.Columns.Add("userbalance", typeof(Int64));
            table.Columns.Add("Date", typeof(string));
           // table.Columns.Add("Balance", typeof(Int64));
            SQLiteCommand cmd = new SQLiteCommand("select users.phone ,users.name,transactions.type,transactions.invoiceid,transactions.userbalance, transactions.date from tblAccountTransaction as transactions inner join tblAccount as users on transactions.accountid=users.id", con);
            con.Open();
           // cmd.Parameters.AddWithValue("@username", username);
            //cmd.Parameters.AddWithValue("@password", password);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
               // MessageBox.Show(reader.GetInt64(0).ToString());
                //show only user record
              
                   
                    DataRow dr = table.NewRow();
                    dr["Phone/ID"] = reader.GetString(0).ToString();
                    dr["Name"] = reader.GetString(1).ToString();
                  //  dr["type"] = reader.GetString(2).ToString();
                   
                      //  
                   // else
                      // 
                    
                    //dr["userid"] = reader.GetInt64(3);
                    balance=reader.GetInt64(4);
                   // dr["userbalance"] = balance;

                    if(balance>0){
                        
                             if (!reader.IsDBNull(3))
                                 {
                                //  dr["invoiceid"] = reader.GetInt64(3);

                                  description = "Rs. " + balance + " was debit from your account against invoice no. " + reader.GetInt64(3);
                                  }
                              else
                                 {
                                     description = "Rs. " + balance + " was debit from your account ";
                                 }
                    }
                    else{
                        if (!reader.IsDBNull(3))
                        {
                            

                            description = "Rs. " + balance + " was credit into your account against invoice no. " + reader.GetInt64(3);
                        }
                        else
                        {
                            description = "Rs. " + balance + " was credit into your account. ";
                        }
                    }
                    dr["Description"] = description;
                    
                    dr["Date"] = reader.GetString(5).ToString();
                   // dr["Balance"] = reader.GetInt64(6);
                    table.Rows.Add(dr);
                }
                
            
            dataGridView1.DataSource = table;
           // dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
           // dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
           // dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
           // dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
          //dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            reader.Close();
            con.Close();

           
        }

        public void RefreshGridView(string accountnumber,string from,string to)
        {
            Int64 balance = 0;
            string description = "";
            DataTable table = new DataTable();
            table.Columns.Add("Phone/ID", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Description", typeof(string));
            //  table.Columns.Add("type", typeof(string));
            // table.Columns.Add("invoiceid", typeof(Int64));

            // table.Columns.Add("userbalance", typeof(Int64));
            table.Columns.Add("Date", typeof(string));
            // table.Columns.Add("Balance", typeof(Int64));
            SQLiteCommand cmd = new SQLiteCommand("select users.phone ,users.name,transactions.type,transactions.invoiceid,transactions.userbalance, transactions.date from tblAccountTransaction as transactions inner join tblAccount as users on transactions.accountid=users.id where users.phone='"+accountnumber+"' and date(transactions.date) between date('"+from+"') and date('"+to+"')", con);
            con.Open();
            // cmd.Parameters.AddWithValue("@username", username);
            //cmd.Parameters.AddWithValue("@password", password);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                // MessageBox.Show(reader.GetInt64(0).ToString());
                //show only user record


                DataRow dr = table.NewRow();
                dr["Phone/ID"] = reader.GetString(0).ToString();
                dr["Name"] = reader.GetString(1).ToString();
                //  dr["type"] = reader.GetString(2).ToString();

                //  
                // else
                // 

                //dr["userid"] = reader.GetInt64(3);
                balance = reader.GetInt64(4);
                // dr["userbalance"] = balance;

                if (balance > 0)
                {

                    if (!reader.IsDBNull(3))
                    {
                        //  dr["invoiceid"] = reader.GetInt64(3);

                        description = "Rs. " + balance + " was debit from your account against invoice no. " + reader.GetInt64(3);
                    }
                    else
                    {
                        description = "Rs. " + balance + " was debit from your account ";
                    }
                }
                else
                {
                    if (!reader.IsDBNull(3))
                    {


                        description = "Rs. " + balance + " was credit into your account against invoice no. " + reader.GetInt64(3);
                    }
                    else
                    {
                        description = "Rs. " + balance + " was credit into your account. ";
                    }
                }
                dr["Description"] = description;

                dr["Date"] = reader.GetString(5).ToString();
                // dr["Balance"] = reader.GetInt64(6);
                table.Rows.Add(dr);
            }


            dataGridView1.DataSource = table;
            // dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            reader.Close();
            con.Close();


        }

        private void showemptygrid()
        {

            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("IMIE", typeof(string));
            table.Columns.Add("Price", typeof(string));
            table.Columns.Add("Description", typeof(string));

            for (int i = 0; i < 10; i++)  // add 20 empty rows
            {
                DataRow dr = table.NewRow();
                dr["ID"] = "";
                dr["IMIE"] = "";

                dr["Price"] = "";
                dr["Description"] = "";
                table.Rows.Add(dr);
            }
            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


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
                    totalbalanceTextBox.Text = dt10.Rows[0]["userbalance"].ToString();

                }

                else
                {

                    balanceTextBox.Text = "0";
                }

            }
            con.Close();

        }

        public void GetUserBalance(Int64 id,string from ,string to)
        {
            DataTable dt10 = new DataTable();
            adapt = new SQLiteDataAdapter("select sum(userbalance) as userbalance from tblAccountTransaction where accountid="+userid+" and date(date) between date('" + from + "') and date('" + to + "')", con);
            con.Open();
            adapt.Fill(dt10);
            if (dt10.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt10.Rows[0]["userbalance"].ToString()))
                {


                   totalbalanceTextBox.Text = dt10.Rows[0]["userbalance"].ToString();


                }

                else
                {

                    totalbalanceTextBox.Text = "0";
                }

            }
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

        
    }
}
