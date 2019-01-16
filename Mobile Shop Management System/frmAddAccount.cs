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
    public partial class frmAddAccount : Form
    {


        SQLiteConnection con;

        SQLiteCommand cmd;
        SQLiteDataAdapter adapt;
        SQLiteDataAdapter adapt1;
        long lastId = 0;

        long previous = 0;
        long first = 0;
        long next = 0;

        public frmAddAccount()
        {
            InitializeComponent();
            con = new SQLiteConnection("Data Source=database.db;Version=3;New=false;Compress=True;");
            DisplayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(accountidTextBox.Text) == lastId)
            {

                if (string.IsNullOrWhiteSpace(mobilenumberTextBox.Text) | string.IsNullOrWhiteSpace(fullnameTextBox.Text))
                {
                    MessageBox.Show("Please Fill the form Correctly!");
                }
                else
                {
                    if (EverythingOK())
                    {
                        cmd = new SQLiteCommand("insert into tblAccount(name,phone,created_at,balance,cnic) values(@name,@phone,@created_at,@balance,@cnic)", con);
                        con.Open();
                        cmd.Parameters.AddWithValue("@name", fullnameTextBox.Text);
                        cmd.Parameters.AddWithValue("@phone", mobilenumberTextBox.Text);
                        cmd.Parameters.AddWithValue("@created_at", DateTime.Now.ToString("h:mm:ss tt"));
                        cmd.Parameters.AddWithValue("@balance", "0");
                        cmd.Parameters.AddWithValue("@cnic", cnicTextBox.Text.ToString());
                      
                        cmd.ExecuteNonQuery();
                        con.Close();
                        previous++;
                        MessageBox.Show("Record Inserted Successfully");
                        clearAll();
                        DisplayData();
                    }
                    else
                    {
                        MessageBox.Show("Mobile Number Matched ! Can't Update");

                    }


                }



            }
            else
            {
                if (EverythingOK())
                {
                    SQLiteCommand cmd = new SQLiteCommand("update tblAccount set phone=@phone ,name=@name,cnic=@cnic where id=@id", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@phone", mobilenumberTextBox.Text);
                    cmd.Parameters.AddWithValue("@name", fullnameTextBox.Text);
                    cmd.Parameters.AddWithValue("@cnic", cnicTextBox.Text);
                    
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt64(accountidTextBox.Text));
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                else
                {
                    MessageBox.Show("Mobile Number Matched ! Can't Update");

                }


            }


        }

        //Display Data in DataGridView  
        private void DisplayData()
        {
            //get last id 
            string sql = @"SELECT id FROM  tblAccount ORDER BY id DESC LIMIT 1";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            con.Open();
            Object b = command.ExecuteScalar();

            if (b == null)
            {
                lastId = 1;
                previous = lastId;
                next = 1;
                
                accountidTextBox.Text = lastId.ToString();
                first = 1;
            }
            else
            {
                lastId = Convert.ToInt64(b);

                previous = lastId;
                next = lastId + 1;
                lastId = lastId + 1;

                accountidTextBox.Text = lastId.ToString();

                //get first id
                string sql1 = @"SELECT id FROM  tblAccount ORDER BY id ASC LIMIT 1";
                SQLiteCommand command1 = new SQLiteCommand(sql1, con);
                long firstId = Convert.ToInt64(command1.ExecuteScalar());
                first = firstId;

            }








            /*
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("select * from tblPurchase", con);
            rowid = con.LastInsertRowId;
            textBox5.Text = rowid.ToString();
            adapt.Fill(dt);
             */
            //  MessageBox.Show(dt.Rows[0]["description"].ToString());
            // dataGridView1.DataSource = dt;
            // dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            // dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            con.Close();
        }

        /*
        private long getID()
        {
            con.Open();
            SQLiteCommand cmd =new SQLiteCommand();
            string sql = @"select last_insert_rowid()";
            long lastId = (long)cmd.ExecuteScalar(sql);
            return lastId;
        }
        */


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {






            for (; previous >= first; previous--)
            {
                next = previous + 1;
                if (getTblPurchasePreviousRow(previous))
                {
                    if (previous == first)
                    {
                        MessageBox.Show("You have Reached First Record !");
                        next = previous + 1;
                    }
                    else
                    {
                        previous = previous - 1;

                    }

                    break;
                }





            }




            /*
            clearAll();
            previous = previous - 1;
            if (getTblPurchaseRow(previous))
            {
            }
           */
        }

        public void clearAll()
        {
            fullnameTextBox.Text = "";
            mobilenumberTextBox.Text = "";
            cnicTextBox.Text = "";
           
            accountidTextBox.Text = "";
        }

        public Boolean getTblPurchasePreviousRow(long id)
        {


            
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("select * from tblAccount where id=" + id, con);
            con.Open();
            adapt.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                con.Close();
                return false;
            }
            else
            {
                clearAll();

                accountidTextBox.Text = dt.Rows[0]["id"].ToString();
                fullnameTextBox.Text = dt.Rows[0]["name"].ToString();
                mobilenumberTextBox.Text = dt.Rows[0]["phone"].ToString();
                
                cnicTextBox.Text = dt.Rows[0]["cnic"].ToString();
                con.Close();
                return true;
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (next == lastId)
            {

                clearAll();
                accountidTextBox.Text = next.ToString();
                previous = next - 1;
                MessageBox.Show("You have Reached Recent Most Record!");
            }
            else
            {
                for (; next < lastId; next++)
                {
                    previous = next - 1;
                    if (getTblPurchasePreviousRow(next))
                    {
                        if (next == lastId)
                        {

                            previous = next - 1;
                        }
                        else
                        {
                            next = next + 1;

                        }
                        break;
                    }


                }
            }



        }





        public Boolean getTblPurchaseNextRow(long id)
        {
            
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("select * from tblAccount where id=" + id, con);
            con.Open();
            adapt.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                con.Close();
                return false;
            }
            else
            {
                clearAll();

                accountidTextBox.Text = dt.Rows[0]["id"].ToString();
                fullnameTextBox.Text = dt.Rows[0]["name"].ToString();
                con.Close();
                return true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clearAll();
            DisplayData();
        }

        public bool EverythingOK()
        {

            
            /*
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("select * from tblPurchase where id=" + Convert.ToInt64(itemcodeTextBox.Text), con);
            adapt.Fill(dt);
           String deviceimie= dt.Rows[0]["imie"].ToString();
            */
            DataTable dt1 = new DataTable();

            adapt1 = new SQLiteDataAdapter("select * from tblAccount where phone='" + mobilenumberTextBox.Text.ToString() + "' and id!=" + accountidTextBox.Text.ToString(), con);
            con.Open();
            adapt1.Fill(dt1);

            if (dt1.Rows.Count >= 1)
            {
                con.Close();
                MessageBox.Show("false" + dt1.Rows.Count.ToString());
                return false;

            }
            else
            {
                con.Close();
                MessageBox.Show("true" + dt1.Rows.Count.ToString());
                
                return true;
            }







        }


    }
}
