using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mobile_Shop_Management_System
{
    public partial class frmSaleReturn : Form
    {
        System.Data.SQLite.SQLiteConnection con;
        SQLiteDataAdapter adapt;
        long lastId;
        long currentid;
        long purchaseitemid;
        long userid,invoiceid;
        String username,phone;
        long userbalance;
        long lastprice;
        DataGridViewButtonColumn DelColumn;
        bool indicator = false;

        PrintDocument pdoc = null;
        long total;
        int ticketNo;
        DateTime TicketDate;
        String Source, Destination, DrawnBy;
        bool deleteColumn = false;
        public frmSaleReturn()
        {
            InitializeComponent();
            dateTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy h:mm:ss tt");
            con = new SQLiteConnection("Data Source=database.db;Version=3;New=false;Compress=True;");
            getInvoiceDetail();

        }
        public void getInvoiceDetail()
        {
            //get last id 
            string sql = @"SELECT id FROM  tblSaleInvoice ORDER BY id DESC LIMIT 1";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            con.Open();
            Object b = command.ExecuteScalar();
            if (b == null)
            {
                currentid = 1;
                invoiceTextBox.Text = "1";
            }
            else
            {
                lastId = Convert.ToInt64(b);
                currentid = lastId + 1;
                invoiceTextBox.Text = currentid.ToString();

            }

            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmSaleInvoice_Load(object sender, EventArgs e)
        {
            invoiceTextBox.KeyDown += invoiceTextBox_KeyDown;
            generateSuggestionsforInvNo();

            accountButton.Enabled = false;
            cashButton.Enabled = false;
            disableAll();

            // showemptygrid();

            DelColumn = new DataGridViewButtonColumn();
            DelColumn.Text = "X";
            DelColumn.Name = "Action";
            DelColumn.DataPropertyName = "Delete";
            DelColumn.UseColumnTextForButtonValue = true;

            showemptygrid();
            imieTextBox.KeyDown += textBox1_KeyDown;
            accountnumberTextBox.KeyDown += accountnumberTextBox_KeyDown;
            AutoCompleteStringCollection imieCollection = new AutoCompleteStringCollection();

            con.Open();
            SQLiteCommand cmnd = con.CreateCommand();
            cmnd.CommandType = CommandType.Text;
            cmnd.CommandText = "SELECT * FROM tblPurchase";
            SQLiteDataReader dReader;
            dReader = cmnd.ExecuteReader();

            if (dReader.Read())
            {
                while (dReader.Read())
                    imieCollection.Add(dReader["imie"].ToString());
            }
            else
            {
                MessageBox.Show("Data not found");
            }
            dReader.Close();

            imieTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            imieTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            imieTextBox.AutoCompleteCustomSource = imieCollection;
            con.Close();

            generateSuggestions();
            
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
                        userid = Convert.ToInt64(dt.Rows[0]["id"].ToString());
                        MessageBox.Show(userid.ToString());
                        userbalance = Convert.ToInt64(dt.Rows[0]["balance"].ToString());
                        accountbalanceTextBox.Text = dt.Rows[0]["balance"].ToString();
                        //purchaseitemid = Convert.ToInt64(dt.Rows[0]["id"]);
                    }


                }
            } 
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

        void textBox1_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down
                if (imieTextBox.TextLength > 0)
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    adapt = new SQLiteDataAdapter("select * from tblPurchase where imie=" + Convert.ToInt64(imieTextBox.Text.ToString()) + "", con);
                    adapt.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Item Not Found!");
                        invoiceTextBox.Text = "";
                        dateTextBox.Text = "";

                    }
                    else
                    {
                        con.Open();
                        DataTable dt2 = new DataTable();
                        adapt = new SQLiteDataAdapter("select * from tblSaleInvoiceItem where purchase_itemid=" + Convert.ToInt64(dt.Rows[0]["id"].ToString()) + "", con);
                        adapt.Fill(dt2);
                        con.Close();

                        if (dt2.Rows.Count == 0)
                        {
                            MessageBox.Show("Item Not Found!");
                            invoiceTextBox.Text = "";
                            dateTextBox.Text = "";

                        }

                        else
                        {
                            con.Open();
                            DataTable dt3 = new DataTable();
                            adapt = new SQLiteDataAdapter("select * from tblSaleInvoice where id=" + Convert.ToInt64(dt2.Rows[0]["sale_itemid"].ToString()) + "", con);
                            adapt.Fill(dt3);
                            con.Close();

                            if (dt3.Rows.Count == 0)
                            {
                                MessageBox.Show("Item Not Found!");
                                invoiceTextBox.Text = "";
                                dateTextBox.Text = "";

                            }

                            else
                            {
                                invoiceTextBox.Text = dt3.Rows[0]["id"].ToString();
                                dateTextBox.Text = dt3.Rows[0]["date"].ToString();
                                invoiceid = Convert.ToInt64(dt3.Rows[0]["id"].ToString());
                                totalpriceTextBox.Text = dt3.Rows[0]["total"].ToString();
                                total = Convert.ToInt64(dt3.Rows[0]["total"].ToString());
                                receivedTextBox.Text = dt3.Rows[0]["received"].ToString();
                                returnbalanceTextBox.Text = dt3.Rows[0]["return"].ToString();
                                if (string.IsNullOrEmpty(dt3.Rows[0]["account_id"].ToString()))
                                {
                                    MessageBox.Show("Cashed");
                                    accountButton.Checked = false;
                                    cashButton.Checked = true;
                                    accountButton.Enabled = false;
                                    cashButton.Enabled = false;
                                    disableAll();
                                    fillDataGridView();
                                }
                                else
                                {
                                    userid = Convert.ToInt64(dt3.Rows[0]["account_id"].ToString());

                                    con.Open();
                                    DataTable dt1 = new DataTable();
                                    adapt = new SQLiteDataAdapter("select * from tblAccount where id=" + userid + "", con);
                                    adapt.Fill(dt1);
                                    con.Close();

                                    if (dt1.Rows.Count > 0)
                                    {
                                        //userbalance = Convert.ToInt64(dt1.Rows[0]["balance"].ToString());
                                       // accountbalanceTextBox.Text = dt1.Rows[0]["balance"].ToString();
                                        accountnumberTextBox.Text = dt1.Rows[0]["phone"].ToString();
                                        nameTextBox.Text = dt1.Rows[0]["name"].ToString();
                                        accountButton.Checked = true;
                                        cashButton.Checked = false;
                                        accountButton.Enabled = false;
                                        cashButton.Enabled = false;
                                        disableAll();
                                        fillDataGridView();
                                        MessageBox.Show(userid.ToString());

                                    }

                                    DataTable dt10 = new DataTable();
                                    adapt = new SQLiteDataAdapter("select sum(userbalance) as userbalance from tblAccountTransaction where accountid=" + userid + "", con);
                                    adapt.Fill(dt10);
                                    if(dt10.Rows.Count!=0){
                                        userbalance = Convert.ToInt64(dt10.Rows[0]["userbalance"].ToString());
                                        accountbalanceTextBox.Text = dt10.Rows[0]["userbalance"].ToString();
                                    }
                                   
                                    MessageBox.Show("Account");
                                }
                            }

                            
                        }

                        
                    }



                }
            }

            /*
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down
                if (imieTextBox.TextLength > 10)
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    adapt = new SQLiteDataAdapter("select * from tblPurchase where imie='" + imieTextBox.Text.ToString() + "'", con);
                    adapt.Fill(dt);
                    con.Close();

                    descriptionTextBox.Text = dt.Rows[0]["description"].ToString();
                    purchaseitemid = Convert.ToInt64(dt.Rows[0]["id"]);
                }
            }
             */

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(imieTextBox.Text))
            {
                con.Open();
                DataTable dt1 = new DataTable();
                adapt = new SQLiteDataAdapter("select purchase.id as id ,sale.sale_price as price,item.received as received, item.return as return from tblPurchase as purchase  inner join tblSaleInvoiceItem as sale on purchase.id=sale.purchase_itemid inner join tblSaleInvoice as item on sale.sale_itemid=item.id where purchase.imie='"+imieTextBox.Text.ToString()+"'", con);
                adapt.Fill(dt1);
                con.Close();

                if (dt1.Rows.Count > 0)
                {
                    long id = Convert.ToInt64(dt1.Rows[0]["id"].ToString());

                    long price = Convert.ToInt64(dt1.Rows[0]["price"].ToString());
                    long received = Convert.ToInt64(dt1.Rows[0]["received"].ToString());
                    SQLiteCommand cmd = new SQLiteCommand("delete from tblSaleInvoiceItem where purchase_itemid=@id and sale_itemid=@sale_itemid", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@sale_itemid", invoiceid);
                    cmd.ExecuteNonQuery();
                    con.Close();


                    total = total - price;

                    received = received - price;
                    receivedTextBox.Text = received.ToString();
                    returnbalanceTextBox.Text = Convert.ToInt64(dt1.Rows[0]["return"].ToString()).ToString();

                    /*8
                    SQLiteCommand cmd1 = new SQLiteCommand("update tblSaleInvoice set total=@total,received=@received,balance=@balance,return=@return  where id=@id ", con);
                    con.Open();

                    cmd1.Parameters.AddWithValue("@total", total);
                    cmd1.Parameters.AddWithValue("@received", Convert.ToInt64(receivedTextBox.ToString()));
                    cmd1.Parameters.AddWithValue("@balance", Convert.ToInt64(balanceTextBox.ToString()));
                    cmd1.Parameters.AddWithValue("@return", Convert.ToInt64(returnbalanceTextBox.ToString()));
                    cmd1.Parameters.AddWithValue("@id", invoiceid);
                   
                  
                    
                   
                    cmd1.ExecuteNonQuery();
                    con.Close();
                     */
                    refreshGridView();
                    resetPrice();

                }
                else
                {
                    MessageBox.Show("Item Not Found!");
                    imieTextBox.Text = "";
                }
                




            }


        }

        public void refreshGridView()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT tblPurchase.id as ID,tblPurchase.imie as IMIE, tblSaleInvoiceItem.sale_price as price,tblPurchase.description as Description FROM tblPurchase INNER JOIN tblSaleInvoiceItem ON  tblSaleInvoiceItem.purchase_itemid=tblPurchase.id where tblSaleInvoiceItem.sale_itemid=" + invoiceid, con);
            adapt.Fill(dt);
           
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = false;
            dataGridView1.Columns[4].ReadOnly = true;

            DelColumn.DisplayIndex = 4;
            DelColumn.Visible = true;



            con.Close();
        }
        public void addToInvoice()
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Price")
            {
                //your code goes here

                // MessageBox.Show("cell edited");

                long price = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["Price"].Value.ToString());
                long id = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());


                if (price == 0)
                {
                    MessageBox.Show("Price Cant be Null");
                    dataGridView1.Rows[e.RowIndex].Cells["Price"].Value = lastprice;
                }
                else
                {

                    SQLiteCommand cmd = new SQLiteCommand("update tblSaleInvoiceItem set sale_price=@price where purchase_itemid=@id and sale_itemid=@sale_itemid", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@sale_itemid", invoiceid);
                    cmd.ExecuteNonQuery();
                    con.Close();

                   

                    if (total == 0)
                    {


                    }
                    else
                    {
                        total = total - lastprice;
                       total = total + price;

                    }
                    resetPrice();
                    GetUserBalance();
                    
                }



            }

        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Action")
            {



                if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString()))
                {
                    long id = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());

                    long price = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["price"].Value.ToString());
                    SQLiteCommand cmd = new SQLiteCommand("delete from tblSaleInvoiceItem where purchase_itemid=@id and sale_itemid=@sale_itemid", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@sale_itemid", invoiceid);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    
                   
                    total = total - price;

                    
                   // receivedTextBox.Text = (Convert.ToInt64(receivedTextBox.Text.ToString())-price).ToString();

                    /*8
                    SQLiteCommand cmd1 = new SQLiteCommand("update tblSaleInvoice set total=@total,received=@received,balance=@balance,return=@return  where id=@id ", con);
                    con.Open();

                    cmd1.Parameters.AddWithValue("@total", total);
                    cmd1.Parameters.AddWithValue("@received", Convert.ToInt64(receivedTextBox.ToString()));
                    cmd1.Parameters.AddWithValue("@balance", Convert.ToInt64(balanceTextBox.ToString()));
                    cmd1.Parameters.AddWithValue("@return", Convert.ToInt64(returnbalanceTextBox.ToString()));
                    cmd1.Parameters.AddWithValue("@id", invoiceid);
                   
                  
                    
                   
                    cmd1.ExecuteNonQuery();
                    con.Close();
                     */
                    refreshGridView();
                    resetPrice();

                }

            }


            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Price")
            {

                long price = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["Price"].Value.ToString());
                lastprice = price;
                // MessageBox.Show(price.ToString());
            }

        }

        public bool itemNotExist()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT * from tblSaleInvoiceItem where purchase_itemid=" + purchaseitemid, con);
            adapt.Fill(dt);
            con.Close();
            if (dt.Rows.Count == 0)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        public void clearAll()
        {
            imieTextBox.Text = "";
            
        }

        public void countTotal()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT SUM(sale_price) as totalprice from tblSaleInvoiceItem where sale_itemid=" + currentid, con);
            adapt.Fill(dt);
            total = Convert.ToInt64(dt.Rows[0]["totalprice"].ToString());

            totalpriceTextBox.Text = total.ToString();
            receivedTextBox.Text = total.ToString();
            balanceTextBox.Text = "0";
            returnbalanceTextBox.Text = "0";
            if (Convert.ToInt64(balanceTextBox.Text.ToString()) < 0)
            {
                returnbalanceTextBox.Enabled = true;


            }
            else
            {
                returnbalanceTextBox.Enabled = false;
            }

            con.Close();
        }

        public void getBalance()
        {
            long received = Convert.ToInt64(receivedTextBox.Text.ToString());
            long balance = Convert.ToInt64(totalpriceTextBox.Text.ToString()) - received;
            balanceTextBox.Text = balance.ToString();

            returnbalanceTextBox.Text = "0";

            if (Convert.ToInt64(balanceTextBox.Text.ToString()) < 0)
            {

                returnbalanceTextBox.Enabled = true;


            }
            else
            {
                returnbalanceTextBox.Enabled = false;
            }
        }

        private void receivedTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(receivedTextBox.Text.ToString()))
            {
                getBalance();
            }

        }

        public void print()
        {
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15);


            PaperSize psize = new PaperSize("Custom", 100, 200);
            //ps.DefaultPageSettings.PaperSize = psize;

            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 820;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;

            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                PrintPreviewDialog pp = new PrintPreviewDialog();
                pp.Document = pdoc;
                result = pp.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pdoc.Print();
                }
            }

        }
        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 50;
            int startY = 55;
            int Offset = 40;
            graphics.DrawString("Welcome to Mobile Shop", new Font("Courier New", 14),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Invoice No:" + currentid,
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Invoice Date :" + DateTime.Now.ToString("dd-MM-yyyy h:mm:ss tt"),
                     new Font("Courier New", 12),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;

            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT tblPurchase.id as ID,tblPurchase.imie as IMIE, tblSaleInvoiceItem.sale_price as price,tblPurchase.description as Description FROM tblPurchase INNER JOIN tblSaleInvoiceItem ON  tblSaleInvoiceItem.purchase_itemid=tblPurchase.id where tblSaleInvoiceItem.sale_itemid=" + currentid, con);
            adapt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    graphics.DrawString(dt.Rows[i]["ID"].ToString() + " " + dt.Rows[i]["IMIE"].ToString() + " " + dt.Rows[i]["description"].ToString() + " " + dt.Rows[i]["price"].ToString(), new Font("Courier New", 10),
                             new SolidBrush(Color.Black), startX, startY + Offset);

                    Offset = Offset + 20;

                }

            }
            con.Close();



            String Grosstotal = "Total Amount to Pay = " + totalpriceTextBox.Text.ToString();

            Offset = Offset + 20;
            underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(Grosstotal, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String DrawnBy = "Ibad Ullah";
            graphics.DrawString("Conductor - " + DrawnBy, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            print();
        }

        public void getRecieptDetails()
        {

        }

        public void resetPrice()
        {
            totalpriceTextBox.Text = total.ToString();
            balanceTextBox.Text = (total - Convert.ToInt64(receivedTextBox.Text.ToString())).ToString();
            
            updateAllBalance();

        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.FormattedValue.GetType() != dataGridView1.CurrentCell.ValueType.UnderlyingSystemType)
                MessageBox.Show("Input type is wrong");
        }

        public void disableAll()
        {
            accountbalanceTextBox.Enabled = false;
            accountnumberTextBox.Enabled = false;
            nameTextBox.Enabled = false;
        }

        public void enableAll()
        {

            accountnumberTextBox.Enabled = true;
            nameTextBox.Enabled = true;
        }

        private void cashButton_CheckedChanged(object sender, EventArgs e)
        {
            if (cashButton.Checked)
            {
                disableAll();
            }
            else
            {
                enableAll();
            }
        }

        private void accountButton_CheckedChanged(object sender, EventArgs e)
        {
            if (accountButton.Checked)
            {
                enableAll();
            }
            else
            {
                disableAll();
            }
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

            if (Reader.Read())
            {
                while (Reader.Read())
                {
                    phoneCollection.Add(Reader["phone"].ToString());


                }

            }
            else
            {
                MessageBox.Show("Data not found");
            }
            Reader.Close();

            accountnumberTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            accountnumberTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            accountnumberTextBox.AutoCompleteCustomSource = phoneCollection;
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cashButton.Checked)
            {
                SQLiteCommand cmd = new SQLiteCommand("update tblSaleInvoice set total=@total ,return=@return,received=@received,balance=@balance,status=@status where id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@received", receivedTextBox.Text.ToString());
                cmd.Parameters.AddWithValue("@balance", total - Convert.ToInt64(receivedTextBox.Text.ToString()));
                cmd.Parameters.AddWithValue("@status", "saved");
                cmd.Parameters.AddWithValue("@return", returnbalanceTextBox.Text.ToString());
                cmd.Parameters.AddWithValue("@id", invoiceid);
                cmd.ExecuteNonQuery();
                con.Close();


                if (Convert.ToInt64(balanceTextBox.Text.ToString()) < 0)
                {
                    //insert user balance
                    long currentbalance = (total - Convert.ToInt64(receivedTextBox.Text.ToString())) + Convert.ToInt64(returnbalanceTextBox.Text.ToString());
                    con.Open();
                    DataTable dt5 = new DataTable();
                    adapt = new SQLiteDataAdapter("select * from tblAccountTransaction where invoiceid=" + invoiceid, con);
                    adapt.Fill(dt5);
                    con.Close();
                    if (dt5.Rows.Count != 0)
                    {
                        SQLiteCommand cmd2 = new SQLiteCommand("update tblAccountTransaction set userbalance=@balance,type=@type,invoiceid=@invoiceid,accountid=@accountid,payment=@payment,receipt=@receipt,date=@date where invoiceid=@invoiceid", con);
                        con.Open();
                        cmd2.Parameters.AddWithValue("@type", "sale");
                        cmd2.Parameters.AddWithValue("@invoiceid",invoiceid);
                        cmd2.Parameters.AddWithValue("@accountid", null);
                        cmd2.Parameters.AddWithValue("@payment", null);
                        cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd2.Parameters.AddWithValue("@receipt", Convert.ToInt64(receivedTextBox.Text.ToString()) - Convert.ToInt64(returnbalanceTextBox.Text.ToString()));
                        cmd2.Parameters.AddWithValue("@balance", (Convert.ToInt64(returnbalanceTextBox.Text.ToString()) + Convert.ToInt64(balanceTextBox.Text.ToString())));
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {

                        SQLiteCommand cmd2 = new SQLiteCommand("insert into tblAccountTransaction(type,invoiceid,accountid,payment,receipt,date) values(@type,@invoiceid,@accountid,@payment,@receipt,@date)", con);
                        con.Open();
                        cmd2.Parameters.AddWithValue("@type", "sale");
                        cmd2.Parameters.AddWithValue("@invoiceid", invoiceid);
                        cmd2.Parameters.AddWithValue("@accountid", null);
                        cmd2.Parameters.AddWithValue("@payment", null);
                        cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd2.Parameters.AddWithValue("@receipt", Convert.ToInt64(receivedTextBox.Text.ToString()) - Convert.ToInt64(returnbalanceTextBox.Text.ToString()));

                        cmd2.ExecuteNonQuery();
                        con.Close();

                    }
                   


                }
                else
                {

                    SQLiteCommand cmd2 = new SQLiteCommand("update tblAccountTransaction set userbalance=@balance,type=@type,invoiceid=@invoiceid,accountid=@accountid,payment=@payment,receipt=@receipt,date=@date where invoiceid=@invoiceid", con);
                    con.Open();
                    cmd2.Parameters.AddWithValue("@type", "sale");
                    cmd2.Parameters.AddWithValue("@invoiceid", invoiceid);
                    cmd2.Parameters.AddWithValue("@accountid", null);
                    cmd2.Parameters.AddWithValue("@payment", null);
                    cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd2.Parameters.AddWithValue("@receipt", receivedTextBox.Text.ToString());
                   // cmd2.Parameters.AddWithValue("@receipt", Convert.ToInt64(receivedTextBox.Text.ToString()) - Convert.ToInt64(returnbalanceTextBox.Text.ToString()));
                    cmd2.Parameters.AddWithValue("@balance", (Convert.ToInt64(returnbalanceTextBox.Text.ToString()) + Convert.ToInt64(balanceTextBox.Text.ToString())));
                    cmd2.ExecuteNonQuery();
                    con.Close();


              
                }




            }
            else
            {

                SQLiteCommand cmd = new SQLiteCommand("update tblSaleInvoice set total=@total ,return=@return,received=@received,balance=@balance,status=@status where id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@received", receivedTextBox.Text.ToString());
                cmd.Parameters.AddWithValue("@balance", total - Convert.ToInt64(receivedTextBox.Text.ToString()));
                cmd.Parameters.AddWithValue("@status", "saved");
                cmd.Parameters.AddWithValue("@return", returnbalanceTextBox.Text.ToString());
                cmd.Parameters.AddWithValue("@id", invoiceid);
                cmd.ExecuteNonQuery();
                con.Close();

                if (Convert.ToInt64(balanceTextBox.Text.ToString()) < 0)
                {

                    long currentbalance = (total - Convert.ToInt64(receivedTextBox.Text.ToString())) + Convert.ToInt64(returnbalanceTextBox.Text.ToString());


                    SQLiteCommand cmd1 = new SQLiteCommand("update tblAccount set balance=@balance where id=@userid", con);
                    con.Open();
                    cmd1.Parameters.AddWithValue("@balance", userbalance + currentbalance);

                    cmd1.Parameters.AddWithValue("@userid", userid);


                    cmd1.ExecuteNonQuery();
                    con.Close();


                    SQLiteCommand cmd2 = new SQLiteCommand("update tblAccountTransaction set userbalance=@balance,type=@type,invoiceid=@invoiceid,accountid=@accountid,payment=@payment,receipt=@receipt,date=@date where invoiceid=@invoiceid", con);
                    con.Open();
                    cmd2.Parameters.AddWithValue("@type", "sale");
                    cmd2.Parameters.AddWithValue("@invoiceid", invoiceid);
                    cmd2.Parameters.AddWithValue("@accountid", userid);
                    cmd2.Parameters.AddWithValue("@payment", null);
                    cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd2.Parameters.AddWithValue("@receipt", Convert.ToInt64(receivedTextBox.Text.ToString()) - Convert.ToInt64(returnbalanceTextBox.Text.ToString()));
                    cmd2.Parameters.AddWithValue("@balance", (Convert.ToInt64(returnbalanceTextBox.Text.ToString()) + Convert.ToInt64(balanceTextBox.Text.ToString())));
                    cmd2.ExecuteNonQuery();
                    con.Close();


                }
                else
                {
                    SQLiteCommand cmd1 = new SQLiteCommand("update tblAccount set balance=@balance where id=@userid", con);
                    con.Open();
                    cmd1.Parameters.AddWithValue("@balance", userbalance + (total - Convert.ToInt64(receivedTextBox.Text.ToString())));

                    cmd1.Parameters.AddWithValue("@userid", userid);
                    cmd1.ExecuteNonQuery();
                    con.Close();

                 


                    SQLiteCommand cmd2 = new SQLiteCommand("update tblAccountTransaction set userbalance=@balance,type=@type,invoiceid=@invoiceid,accountid=@accountid,payment=@payment,receipt=@receipt,date=@date where invoiceid=@invoiceid", con);
                    con.Open();
                    cmd2.Parameters.AddWithValue("@type", "sale");
                    cmd2.Parameters.AddWithValue("@invoiceid", invoiceid);
                    cmd2.Parameters.AddWithValue("@accountid", userid);
                    cmd2.Parameters.AddWithValue("@payment", null);
                    cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd2.Parameters.AddWithValue("@receipt", receivedTextBox.Text.ToString());
                   // cmd2.Parameters.AddWithValue("@receipt", Convert.ToInt64(receivedTextBox.Text.ToString()) - Convert.ToInt64(returnbalanceTextBox.Text.ToString()));
                    cmd2.Parameters.AddWithValue("@balance", (Convert.ToInt64(returnbalanceTextBox.Text.ToString()) + Convert.ToInt64(balanceTextBox.Text.ToString())));
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }

                //update user balance view 
                DataTable dt10 = new DataTable();
                adapt = new SQLiteDataAdapter("select sum(userbalance) as userbalance from tblAccountTransaction where accountid=" + userid + "", con);
                adapt.Fill(dt10);
                if (dt10.Rows.Count != 0)
                {
                    userbalance = Convert.ToInt64(dt10.Rows[0]["userbalance"].ToString());
                    accountbalanceTextBox.Text = dt10.Rows[0]["userbalance"].ToString();
                }


            }

            MessageBox.Show("Saved");



        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


        public void generateSuggestionsforInvNo()
        {
            AutoCompleteStringCollection phoneCollection = new AutoCompleteStringCollection();

            con.Open();
            SQLiteCommand cmnd1 = con.CreateCommand();
            cmnd1.CommandType = CommandType.Text;
            cmnd1.CommandText = "SELECT * FROM tblSaleInvoice";
            SQLiteDataReader Reader;
            Reader = cmnd1.ExecuteReader();

            if (Reader.Read())
            {
                while (Reader.Read())
                {
                    phoneCollection.Add(Reader["id"].ToString());


                }

            }
            else
            {
                MessageBox.Show("Data not found");
            }
            Reader.Close();

            accountnumberTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            accountnumberTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            accountnumberTextBox.AutoCompleteCustomSource = phoneCollection;
            con.Close();
        }

        public void fillInvoiceDetails()
        {

            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("" + currentid, con);
            adapt.Fill(dt);
            con.Close();
        }

        private void invoiceTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down
                if (invoiceTextBox.TextLength > 0)
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    adapt = new SQLiteDataAdapter("select * from tblSaleInvoice where id=" + Convert.ToInt64(invoiceTextBox.Text.ToString()) + "", con);
                    adapt.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Item Not Found!");
                        invoiceTextBox.Text = "";
                        dateTextBox.Text = "";
                      
                    }
                    else
                    {
                        invoiceTextBox.Text = dt.Rows[0]["id"].ToString();
                        dateTextBox.Text = dt.Rows[0]["date"].ToString();
                        invoiceid = Convert.ToInt64(dt.Rows[0]["id"].ToString());
                        totalpriceTextBox.Text = dt.Rows[0]["total"].ToString();
                        total = Convert.ToInt64(dt.Rows[0]["total"].ToString());
                        receivedTextBox.Text = dt.Rows[0]["received"].ToString();
                        returnbalanceTextBox.Text = dt.Rows[0]["return"].ToString();

                        if (string.IsNullOrEmpty(dt.Rows[0]["account_id"].ToString()))
                        {
                            MessageBox.Show("Cashed");
                            accountButton.Checked = false;
                            cashButton.Checked = true;
                            accountButton.Enabled = false;
                            cashButton.Enabled = false;
                            disableAll();
                            fillDataGridView();
                        }
                        else
                        {
                            userid = Convert.ToInt64(dt.Rows[0]["account_id"].ToString());

                            con.Open();
                            DataTable dt1 = new DataTable();
                            adapt = new SQLiteDataAdapter("select * from tblAccount where id=" + userid + "", con);
                            adapt.Fill(dt1);
                            con.Close();

                            if (dt1.Rows.Count > 0)
                            {
                                userbalance = Convert.ToInt64(dt1.Rows[0]["balance"].ToString());
                                accountbalanceTextBox.Text = dt1.Rows[0]["balance"].ToString();
                                accountnumberTextBox.Text = dt1.Rows[0]["phone"].ToString();
                                nameTextBox.Text = dt1.Rows[0]["name"].ToString();
                                accountButton.Checked = true;
                                cashButton.Checked = false;
                                accountButton.Enabled = false;
                                cashButton.Enabled = false;
                                disableAll();
                                fillDataGridView();
                                MessageBox.Show(userid.ToString());

                            }

                            DataTable dt10 = new DataTable();
                            adapt = new SQLiteDataAdapter("select sum(userbalance) as userbalance from tblAccountTransaction where accountid=" + userid + "", con);
                            adapt.Fill(dt10);
                            if (dt10.Rows.Count != 0)
                            {
                                userbalance = Convert.ToInt64(dt10.Rows[0]["userbalance"].ToString());
                                accountbalanceTextBox.Text = dt10.Rows[0]["userbalance"].ToString();
                            }
                            

                            MessageBox.Show("Account");
                        }
                    }



                }
            }
        }

        public void fillDataGridView()
        {
            con.Open();
            SQLiteCommand cmnd1 = con.CreateCommand();
            cmnd1.CommandType = CommandType.Text;
            cmnd1.CommandText = "SELECT tblPurchase.id as ID,tblPurchase.imie as IMIE, tblSaleInvoiceItem.sale_price as price,tblPurchase.description as Description FROM tblPurchase INNER JOIN tblSaleInvoiceItem ON  tblSaleInvoiceItem.purchase_itemid=tblPurchase.id where tblSaleInvoiceItem.sale_itemid=" + invoiceid;
            SQLiteDataReader Reader;
            Reader = cmnd1.ExecuteReader();

            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("IMIE", typeof(string));
            table.Columns.Add("Price", typeof(string));
            table.Columns.Add("Description", typeof(string));

          
            DelColumn.Visible = true;

            if (deleteColumn)
            {
                dataGridView1.Columns.Remove(DelColumn);
            }

          
                
                while (Reader.Read())
                {
                    
                    

                    DataRow dr = table.NewRow();
                    dr["ID"] = Reader["ID"].ToString();
                    dr["IMIE"] = Reader["IMIE"].ToString();

                    dr["Price"] = Reader["price"].ToString(); ;
                    dr["Description"] = Reader["Description"].ToString();
                    table.Rows.Add(dr);

                }
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
                dataGridView1.Columns.Add(DelColumn);
                deleteColumn = true;
                
               
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
         
            Reader.Close();

        
            con.Close();
        }

        public void updateAllBalance()
        {
            SQLiteCommand cmd = new SQLiteCommand("update tblSaleInvoice set total=@total ,return=@return,received=@received,balance=@balance,status=@status where id=@id", con);
            con.Open();
            cmd.Parameters.AddWithValue("@total", total);
            cmd.Parameters.AddWithValue("@received", receivedTextBox.Text.ToString());
            cmd.Parameters.AddWithValue("@balance", total - Convert.ToInt64(receivedTextBox.Text.ToString()));
            cmd.Parameters.AddWithValue("@status", "saved");
            cmd.Parameters.AddWithValue("@return", returnbalanceTextBox.Text.ToString());
            cmd.Parameters.AddWithValue("@id", invoiceid);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void returnbalanceTextBox_TextChanged(object sender, EventArgs e)
        {

        }


        public void GetUserBalance()
        {
            //update user balance view 
            DataTable dt10 = new DataTable();
            adapt = new SQLiteDataAdapter("select sum(userbalance) as userbalance from tblAccountTransaction where accountid=" + userid + "", con);
            adapt.Fill(dt10);
            if (dt10.Rows.Count != 0)
            {
                userbalance = Convert.ToInt64(dt10.Rows[0]["userbalance"].ToString());
                accountbalanceTextBox.Text = dt10.Rows[0]["userbalance"].ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }


}
