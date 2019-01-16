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
    public partial class frmAddMobile : Form


    {


        SQLiteConnection con;
       
        SQLiteCommand cmd;
        SQLiteDataAdapter adapt;
        SQLiteDataAdapter adapt1;
        long lastId=0;
       
        long previous = 0;
        long first = 0;
        long next = 0;
      
        public frmAddMobile()
        {
            InitializeComponent();
            con = new SQLiteConnection("Data Source=database.db;Version=3;New=false;Compress=True;");
            DisplayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(itemcodeTextBox.Text) == lastId)
            {

                if (imieTextBox.Text.ToString().Length < 16 | string.IsNullOrWhiteSpace(descriptionTextBox.Text) | string.IsNullOrWhiteSpace(purchasePriceTextBox.Text))
                    {
                        MessageBox.Show("Please Fill the form Correctly!");
                    }
                    else
                    {
                        if (EverythingOK())
                        {
                        cmd = new SQLiteCommand("insert into tblPurchase(imie,description,purchase_price,cnic,purchase_date,purchase_time) values(@imie,@description,@purchase_price,@cnic,@purchase_date,@purchase_time)", con);
                        con.Open();
                        cmd.Parameters.AddWithValue("@imie", imieTextBox.Text);
                        cmd.Parameters.AddWithValue("@description", descriptionTextBox.Text);
                        cmd.Parameters.AddWithValue("@purchase_price", purchasePriceTextBox.Text);
                        cmd.Parameters.AddWithValue("@cnic", cnicTextBox.Text);
                        cmd.Parameters.AddWithValue("@purchase_date", DateTime.Today.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@purchase_time", DateTime.Now.ToString("h:mm:ss tt"));
                        cmd.ExecuteNonQuery();
                        con.Close();
                        previous++;
                        MessageBox.Show("Record Inserted Successfully");
                        clearAll();
                        DisplayData();
                    }
                        else
                        {
                            MessageBox.Show("Imie Matched ! Can't Update");

                        }
                   

                }
              
                

            }
            else
            {
                if (EverythingOK())
                {
                   SQLiteCommand cmd = new SQLiteCommand("update tblPurchase set imie=@imie ,description=@description,purchase_price=@purchase_price,cnic=@cnic where id=@id", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@imie", imieTextBox.Text);
                    cmd.Parameters.AddWithValue("@description", descriptionTextBox.Text);
                    cmd.Parameters.AddWithValue("@purchase_price", purchasePriceTextBox.Text);
                    cmd.Parameters.AddWithValue("@cnic", cnicTextBox.Text);
                    cmd.Parameters.AddWithValue("@id",Convert.ToInt64(itemcodeTextBox.Text));
                    cmd.ExecuteNonQuery();
                   
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Imie Matched ! Can't Update");

                }

               
            }
            
           
        }

        //Display Data in DataGridView  
        private void DisplayData()
        {
            //get last id 
            string sql = @"SELECT id FROM  tblPurchase ORDER BY id DESC LIMIT 1";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            con.Open();
            Object b = command.ExecuteScalar();

            if (b == null)
            {
                lastId = 1;
                itemcodeTextBox.Text = "1";
            }
            else
            {
                lastId = Convert.ToInt64(b);

                previous = lastId;
                next = lastId + 1;
                lastId = lastId + 1;

                itemcodeTextBox.Text = lastId.ToString();

                //get first id
                string sql1 = @"SELECT id FROM  tblPurchase ORDER BY id ASC LIMIT 1";
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
                            next = previous +1;
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
            imieTextBox.Text = "";
            descriptionTextBox.Text = "";
            cnicTextBox.Text = "";
            purchasePriceTextBox.Text = "";
            itemcodeTextBox.Text = "";
        }

        public Boolean getTblPurchasePreviousRow(long id)
        {

            
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("select * from tblPurchase where id=" + id, con);
            adapt.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                con.Close();
                return false;
            }
            else
            {
                clearAll();
                
                itemcodeTextBox.Text = dt.Rows[0]["id"].ToString();
                imieTextBox.Text = dt.Rows[0]["imie"].ToString();
                descriptionTextBox.Text = dt.Rows[0]["description"].ToString();
                purchasePriceTextBox.Text = dt.Rows[0]["purchase_price"].ToString();
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
                itemcodeTextBox.Text = next.ToString();
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
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("select * from tblPurchase where id=" + id, con);
            adapt.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                con.Close();
                return false;
            }
            else
            {
                clearAll();

                itemcodeTextBox.Text = dt.Rows[0]["id"].ToString();
                imieTextBox.Text = dt.Rows[0]["imie"].ToString();
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

            con.Open();
            /*
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("select * from tblPurchase where id=" + Convert.ToInt64(itemcodeTextBox.Text), con);
            adapt.Fill(dt);
           String deviceimie= dt.Rows[0]["imie"].ToString();
            */
           DataTable dt1 = new DataTable();
           
           adapt1 = new SQLiteDataAdapter("select * from tblPurchase where imie='"+imieTextBox.Text.ToString()+"' and id!="+itemcodeTextBox.Text.ToString(), con);
            
           adapt1.Fill(dt1);
       
               if (dt1.Rows.Count >=1)
               {
                   con.Close();
                   MessageBox.Show("false" + dt1.Rows.Count.ToString());
                   return false;

               }
               else
               {

                   MessageBox.Show("true" + dt1.Rows.Count.ToString());
                   con.Close();
                   return true;
               }

           
          
           
            
            
            
        }

       
    }
}
