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
    public partial class frmPurchaseInvoice : Form
    {
        System.Data.SQLite.SQLiteConnection con;
        SQLiteDataAdapter adapt;
        SQLiteCommand cmd;
        long lastId;
        long currentid;
        long purchaseitemid;
        long userid;
        long userbalance;
        long lastprice;
        DataGridViewButtonColumn DelColumn;
        bool indicator = false;

        PrintDocument pdoc = null;
        long total;
        int ticketNo;
        DateTime TicketDate;
        String Source, Destination, DrawnBy;

        public frmPurchaseInvoice()
        {
            InitializeComponent();
            textBox4.Text = DateTime.Now.ToString("dd-MM-yyyy h:mm:ss tt");
            con = new SQLiteConnection("Data Source=database.db;Version=3;New=false;Compress=True;");
            getInvoiceDetail();

        }
        public void getInvoiceDetail()
        {
            //get last id 
            string sql = @"SELECT seq FROM  sqlite_sequence where name='" + "tblPurchaseInvoice'";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            con.Open();
            Object b = command.ExecuteScalar();
            if (b == null)
            {
                currentid = 1;
                textBox3.Text = "1";
            }
            else
            {
                lastId = Convert.ToInt64(b);
                currentid = lastId + 1;
                textBox3.Text = currentid.ToString();

            }

            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmSaleInvoice_Load(object sender, EventArgs e)
        {

            if (cashButton.Enabled)
            {
                disableAll();

            }
            else
            {
                enableAll();
            }

           showemptygrid();
            DelColumn = new DataGridViewButtonColumn();
            DelColumn.Text = "X";
            DelColumn.Name = "Action";
            DelColumn.DataPropertyName = "Delete";
            DelColumn.UseColumnTextForButtonValue = true;

            DelColumn.Visible = false;


            dataGridView1.Columns.Add(DelColumn);

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

                        //purchaseitemid = Convert.ToInt64(dt.Rows[0]["id"]);
                    }
                    GetUserBalance();



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

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;


        }

        void textBox1_KeyDown(object sender, KeyEventArgs e)
        {


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
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(descriptionTextBox.Text) && !string.IsNullOrWhiteSpace(imieTextBox.Text)
                && !string.IsNullOrWhiteSpace(priceTextBox.Text) && !string.IsNullOrWhiteSpace(cnicTextBox.Text) )
            {
                if (insertPurchase())
                {

                if (itemNotExist())
                {
                    if (indicator)
                    {

                        insertSaleInvoiceItem();



                    }
                    else
                    {
                        isFirstEntry();

                        insertSaleInvoiceItem();



                    }
                    refreshGridView();
                    countTotal();
                }
                else
                {
                    MessageBox.Show("Item Purchased Before !");

                }
                imieTextBox.Text = "";
                descriptionTextBox.Text = "";
                priceTextBox.Text = "";







            }
                else
                {

                    //item purchased before

                    MessageBox.Show("Item Purchased Before");
                }
            }


        }

        public void insertSaleInvoice()
        {
            SQLiteCommand cmd = new SQLiteCommand("insert into tblPurchaseInvoice(date) values(@date)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("dd-MM-yyyy h:mm:ss tt"));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void insertSaleInvoiceItem()
        {
            SQLiteCommand cmd = new SQLiteCommand("insert into tblPurchaseInvoiceItem(purchase_itemid,sale_itemid,sale_price,date) values(@purchase_itemid,@sale_itemid,@sale_price,@date)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@purchase_itemid", purchaseitemid);
            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@sale_itemid", currentid);
            cmd.Parameters.AddWithValue("@sale_price", priceTextBox.Text.ToString());

            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void refreshGridView()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT tblPurchase.id as ID,tblPurchase.imie as IMIE, tblPurchaseInvoiceItem.sale_price as price,tblPurchase.description as Description FROM tblPurchase INNER JOIN tblPurchaseInvoiceItem ON  tblPurchaseInvoiceItem.purchase_itemid=tblPurchase.id where tblPurchaseInvoiceItem.sale_itemid=" + currentid, con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = false;
            dataGridView1.Columns[4].ReadOnly = false;

            DelColumn.DisplayIndex = 4;
            DelColumn.Visible = true;



            con.Close();
        }
        public void addToInvoice()
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "price")
            {
                //your code goes here

                // MessageBox.Show("cell edited");

                long price = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["price"].Value.ToString());
                long id = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());


                if (price == 0)
                {
                    MessageBox.Show("Price Cant be Null");
                    dataGridView1.Rows[e.RowIndex].Cells["price"].Value = lastprice;
                }
                else
                {

                    SQLiteCommand cmd = new SQLiteCommand("update tblPurchaseInvoiceItem set sale_price=@price where purchase_itemid=@id and sale_itemid=@sale_itemid", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@sale_itemid", currentid);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    SQLiteCommand cmd1 = new SQLiteCommand("update tblPurchase set purchase_price=@price where id=@id", con);
                    con.Open();
                    cmd1.Parameters.AddWithValue("@price", price);
                    cmd1.Parameters.AddWithValue("@id", id);
                   
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    if (total == 0)
                    {


                    }
                    else
                    {
                        // MessageBox.Show(total.ToString() + "3");
                        total = total - lastprice;
                        total = total + price;
                        //MessageBox.Show(total.ToString() + "4");

                    }
                    //MessageBox.Show(total.ToString() + "5");
                    resetPrice();
                }



            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Description")
            {
                string description = dataGridView1.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                long id = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                SQLiteCommand cmd1 = new SQLiteCommand("update tblPurchase set description=@description where id=@id", con);
                con.Open();
                cmd1.Parameters.AddWithValue("@description", description);
                cmd1.Parameters.AddWithValue("@id", id);

                cmd1.ExecuteNonQuery();
                con.Close();
            }

        }

        public void isFirstEntry()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT * from tblPurchaseInvoice where id=" + currentid, con);
            adapt.Fill(dt);
            con.Close();
            if (dt.Rows.Count == 0)
            {
                indicator = true;
                insertSaleInvoice();
            }



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Action")
            {



                if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString()))
                {
                    long id = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());

                    long price = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["price"].Value.ToString());
                    SQLiteCommand cmd = new SQLiteCommand("delete from tblSaleInvoiceItem where purchase_itemid=@id and sale_itemid=@sale_itemid", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@sale_itemid", currentid);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    refreshGridView();
                    if (total == 0)
                    {
                        total = total - price;
                    }
                    else
                    {
                        total = 0;
                    }
                    
                    resetPrice();
                    
                }
              

               
            }


            else if (dataGridView1.Columns[e.ColumnIndex].Name == "price")
            {

                long price = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["price"].Value.ToString());
                lastprice = price;
               // MessageBox.Show(price.ToString());
            }
             */

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Action")
            {



                if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString()))
                {
                    long id = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());

                    long price = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["price"].Value.ToString());
                    SQLiteCommand cmd = new SQLiteCommand("delete from tblPurchaseInvoiceItem where purchase_itemid=@id and sale_itemid=@sale_itemid", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@sale_itemid", currentid);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    SQLiteCommand cmd1 = new SQLiteCommand("delete from tblPurchase where id=@id", con);
                    con.Open();
                    cmd1.Parameters.AddWithValue("@id", id);
                  
                    cmd1.ExecuteNonQuery();
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

                    if (accountButton.Checked)
                    {
                        GetUserBalance();
                    }

                }


            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "price")
            {

                long prices = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["price"].Value.ToString());
                lastprice = prices;
                //MessageBox.Show(total.ToString() + "2");
            }

        }

        public bool itemNotExist()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT * from tblPurchaseInvoiceItem where purchase_itemid=" + purchaseitemid, con);
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
            descriptionTextBox.Text = "";
            priceTextBox.Text = "";
        }

        public void countTotal()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SQLiteDataAdapter("SELECT SUM(sale_price) as totalprice from tblPurchaseInvoiceItem where sale_itemid=" + currentid, con);
            adapt.Fill(dt);
            total = Convert.ToInt64(dt.Rows[0]["totalprice"].ToString());

            totalpriceTextBox.Text = total.ToString();
            receivedTextBox.Text = total.ToString();
            balanceTextBox.Text = "0";
            returnbalanceTextBox.Text = "0";
            MessageBox.Show(total.ToString());
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
            long balance = received - Convert.ToInt64(totalpriceTextBox.Text.ToString());
            balanceTextBox.Text = balance.ToString();

            returnbalanceTextBox.Text = "0";

            if (Convert.ToInt64(balanceTextBox.Text.ToString()) < 0)
            {

               // returnbalanceTextBox.Enabled = true;


            }
            else
            {
               // returnbalanceTextBox.Enabled = false;
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
            balanceTextBox.Text = (Convert.ToInt64(receivedTextBox.Text.ToString()) - total).ToString();
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
                SQLiteCommand cmd = new SQLiteCommand("update tblPurchaseInvoice set total=@total ,return=@return,received=@received,balance=@balance,status=@status where id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@received", receivedTextBox.Text.ToString());
                cmd.Parameters.AddWithValue("@balance", Convert.ToInt64(receivedTextBox.Text.ToString()) - total);
                cmd.Parameters.AddWithValue("@status", "saved");
                cmd.Parameters.AddWithValue("@return", returnbalanceTextBox.Text.ToString());
                cmd.Parameters.AddWithValue("@id", currentid);
                cmd.ExecuteNonQuery();
                con.Close();


                if (Convert.ToInt64(balanceTextBox.Text.ToString()) < 0)
                {

                    long currentbalance = (total - Convert.ToInt64(receivedTextBox.Text.ToString())) + Convert.ToInt64(returnbalanceTextBox.Text.ToString());



                    //user balance code 

                    con.Open();
                    DataTable dt5 = new DataTable();
                    adapt = new SQLiteDataAdapter("select * from tblAccountTransaction where invoiceid=" + currentid, con);
                    adapt.Fill(dt5);
                    con.Close();
                    if (dt5.Rows.Count != 0)
                    {
                        SQLiteCommand cmd2 = new SQLiteCommand("update tblAccountTransaction set userbalance=@balance,type=@type,invoiceid=@invoiceid,accountid=@accountid,payment=@payment,receipt=@receipt,date=@date where invoiceid=@invoiceid", con);
                        con.Open();
                        cmd2.Parameters.AddWithValue("@type", "purchase");
                        cmd2.Parameters.AddWithValue("@invoiceid", currentid);
                        cmd2.Parameters.AddWithValue("@accountid", null);
                        cmd2.Parameters.AddWithValue("@payment", Convert.ToInt64(receivedTextBox.Text.ToString()) - Convert.ToInt64(returnbalanceTextBox.Text.ToString()));
                        cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd2.Parameters.AddWithValue("@receipt", null);
                        cmd2.Parameters.AddWithValue("@balance", (Convert.ToInt64(returnbalanceTextBox.Text.ToString()) + Convert.ToInt64(balanceTextBox.Text.ToString())));
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {

                        SQLiteCommand cmd6 = new SQLiteCommand("insert into tblAccountTransaction(type,invoiceid,accountid,payment,receipt,date,userbalance) values(@type,@invoiceid,@accountid,@payment,@receipt,@date,@balance)", con);
                        con.Open();
                        cmd6.Parameters.AddWithValue("@type", "purchase");
                        cmd6.Parameters.AddWithValue("@invoiceid", currentid);
                        cmd6.Parameters.AddWithValue("@accountid", null);
                        cmd6.Parameters.AddWithValue("@payment", Convert.ToInt64(receivedTextBox.Text.ToString()) - Convert.ToInt64(returnbalanceTextBox.Text.ToString()));
                        cmd6.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd6.Parameters.AddWithValue("@receipt", null);
                        cmd6.Parameters.AddWithValue("@balance", (Convert.ToInt64(returnbalanceTextBox.Text.ToString()) + Convert.ToInt64(balanceTextBox.Text.ToString())));
                        cmd6.ExecuteNonQuery();
                        con.Close();

                    }



                }
                else
                {



                    //balance update 

                    con.Open();
                    DataTable dt5 = new DataTable();
                    adapt = new SQLiteDataAdapter("select * from tblAccountTransaction where invoiceid=" + currentid, con);
                    adapt.Fill(dt5);
                    con.Close();
                    if (dt5.Rows.Count != 0)
                    {
                        SQLiteCommand cmd2 = new SQLiteCommand("update tblAccountTransaction set userbalance=@balance,type=@type,invoiceid=@invoiceid,accountid=@accountid,payment=@payment,receipt=@receipt,date=@date where invoiceid=@invoiceid", con);
                        con.Open();
                        cmd2.Parameters.AddWithValue("@type", "purchase");
                        cmd2.Parameters.AddWithValue("@invoiceid", currentid);
                        cmd2.Parameters.AddWithValue("@accountid", null);
                        cmd2.Parameters.AddWithValue("@payment", receivedTextBox.Text.ToString());
                        cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd2.Parameters.AddWithValue("@receipt",null );
                        cmd2.Parameters.AddWithValue("@balance", (Convert.ToInt64(returnbalanceTextBox.Text.ToString()) + Convert.ToInt64(balanceTextBox.Text.ToString())));
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {

                        SQLiteCommand cmd2 = new SQLiteCommand("insert into tblAccountTransaction(type,invoiceid,accountid,payment,receipt,date,userbalance) values(@type,@invoiceid,@accountid,@payment,@receipt,@date,@balance)", con);
                        con.Open();
                        cmd2.Parameters.AddWithValue("@type", "purchase");
                        cmd2.Parameters.AddWithValue("@invoiceid", currentid);
                        cmd2.Parameters.AddWithValue("@accountid", null);
                        cmd2.Parameters.AddWithValue("@payment", receivedTextBox.Text.ToString());
                        cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd2.Parameters.AddWithValue("@receipt", null);
                        cmd2.Parameters.AddWithValue("@balance", (Convert.ToInt64(returnbalanceTextBox.Text.ToString()) + Convert.ToInt64(balanceTextBox.Text.ToString())));

                        cmd2.ExecuteNonQuery();
                        con.Close();

                    }
                }




            }
            else
            {
                MessageBox.Show(total + "recieved " + receivedTextBox.Text.ToString() + "return " + returnbalanceTextBox.Text.ToString() + "invoiceid " + currentid + "userid " + userid + "balance " + balanceTextBox.Text.ToString());
                SQLiteCommand cmd = new SQLiteCommand("update tblPurchaseInvoice set total=@total ,return=@return,received=@received,balance=@balance,status=@status,account_id=@account_id where id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@received", Convert.ToInt64(receivedTextBox.Text.ToString()));
                cmd.Parameters.AddWithValue("@balance", Convert.ToInt64(receivedTextBox.Text.ToString()) - total);
                cmd.Parameters.AddWithValue("@status", "saved");
                cmd.Parameters.AddWithValue("@return", Convert.ToInt64(returnbalanceTextBox.Text.ToString()));
                cmd.Parameters.AddWithValue("@id", currentid);
                cmd.Parameters.AddWithValue("@account_id", userid);
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



                    //if account type then update trasaction 

                    con.Open();
                    DataTable dt5 = new DataTable();
                    adapt = new SQLiteDataAdapter("select * from tblAccountTransaction where invoiceid=" + currentid, con);
                    adapt.Fill(dt5);
                    con.Close();
                    if (dt5.Rows.Count != 0)
                    {
                        SQLiteCommand cmd2 = new SQLiteCommand("update tblAccountTransaction set userbalance=@balance,type=@type,invoiceid=@invoiceid,accountid=@accountid,payment=@payment,receipt=@receipt,date=@date where invoiceid=@invoiceid", con);
                        con.Open();
                        cmd2.Parameters.AddWithValue("@type", "purchase");
                        cmd2.Parameters.AddWithValue("@invoiceid", currentid);
                        cmd2.Parameters.AddWithValue("@accountid", userid);
                        cmd2.Parameters.AddWithValue("@payment", Convert.ToInt64(receivedTextBox.Text.ToString()) - Convert.ToInt64(returnbalanceTextBox.Text.ToString()));
                        cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd2.Parameters.AddWithValue("@receipt",null );
                        cmd2.Parameters.AddWithValue("@balance", (Convert.ToInt64(returnbalanceTextBox.Text.ToString()) + Convert.ToInt64(balanceTextBox.Text.ToString())));
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {

                        SQLiteCommand cmd2 = new SQLiteCommand("insert into tblAccountTransaction(type,invoiceid,accountid,payment,receipt,date,userbalance) values(@type,@invoiceid,@accountid,@payment,@receipt,@date,@balance)", con);
                        con.Open();
                        cmd2.Parameters.AddWithValue("@type", "purchase");
                        cmd2.Parameters.AddWithValue("@invoiceid", currentid);
                        cmd2.Parameters.AddWithValue("@accountid", userid);
                        cmd2.Parameters.AddWithValue("@payment", Convert.ToInt64(receivedTextBox.Text.ToString()) - Convert.ToInt64(returnbalanceTextBox.Text.ToString()));
                        cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd2.Parameters.AddWithValue("@receipt", null);
                        cmd2.Parameters.AddWithValue("@balance", (Convert.ToInt64(returnbalanceTextBox.Text.ToString()) + Convert.ToInt64(balanceTextBox.Text.ToString())));
                        cmd2.ExecuteNonQuery();
                        con.Close();


                    }


                }
                else
                {
                    SQLiteCommand cmd1 = new SQLiteCommand("update tblAccount set balance=@balance where id=@userid", con);
                    con.Open();
                    cmd1.Parameters.AddWithValue("@balance", userbalance + (total - Convert.ToInt64(receivedTextBox.Text.ToString())));

                    cmd1.Parameters.AddWithValue("@userid", userid);
                    cmd1.ExecuteNonQuery();
                    con.Close();




                    //if account type then update trasaction 

                    con.Open();
                    DataTable dt5 = new DataTable();
                    adapt = new SQLiteDataAdapter("select * from tblAccountTransaction where invoiceid=" + currentid, con);
                    adapt.Fill(dt5);
                    con.Close();
                    if (dt5.Rows.Count != 0)
                    {
                        SQLiteCommand cmd2 = new SQLiteCommand("update tblAccountTransaction set userbalance=@balance,type=@type,invoiceid=@invoiceid,accountid=@accountid,payment=@payment,receipt=@receipt,date=@date where invoiceid=@invoiceid", con);
                        con.Open();
                        cmd2.Parameters.AddWithValue("@type", "purchase");
                        cmd2.Parameters.AddWithValue("@invoiceid", currentid);
                        cmd2.Parameters.AddWithValue("@accountid", userid);
                        cmd2.Parameters.AddWithValue("@payment", receivedTextBox.Text.ToString());
                        cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd2.Parameters.AddWithValue("@receipt",null );
                        cmd2.Parameters.AddWithValue("@balance", (Convert.ToInt64(returnbalanceTextBox.Text.ToString()) + Convert.ToInt64(balanceTextBox.Text.ToString())));

                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {

                        SQLiteCommand cmd2 = new SQLiteCommand("insert into tblAccountTransaction(type,invoiceid,accountid,payment,receipt,date,userbalance) values(@type,@invoiceid,@accountid,@payment,@receipt,@date,@balance)", con);
                        con.Open();
                        cmd2.Parameters.AddWithValue("@type", "purchase");
                        cmd2.Parameters.AddWithValue("@invoiceid", currentid);
                        cmd2.Parameters.AddWithValue("@accountid", userid);
                        cmd2.Parameters.AddWithValue("@payment", receivedTextBox.Text.ToString());
                        cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd2.Parameters.AddWithValue("@receipt", null);
                        cmd2.Parameters.AddWithValue("@balance", (Convert.ToInt64(returnbalanceTextBox.Text.ToString()) + Convert.ToInt64(balanceTextBox.Text.ToString())));
                        cmd2.ExecuteNonQuery();
                        con.Close();


                    }
                  
                }
                GetUserBalance();

            }

            MessageBox.Show("Saved");



        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        public void updateAllBalance()
        {
            SQLiteCommand cmd = new SQLiteCommand("update tblPurchaseInvoice set total=@total ,return=@return,received=@received,balance=@balance,status=@status where id=@id", con);
            con.Open();
            cmd.Parameters.AddWithValue("@total", total);
            cmd.Parameters.AddWithValue("@received", receivedTextBox.Text.ToString());
            cmd.Parameters.AddWithValue("@balance", Convert.ToInt64(receivedTextBox.Text.ToString()) - total);
            cmd.Parameters.AddWithValue("@status", "saved");
            cmd.Parameters.AddWithValue("@return", returnbalanceTextBox.Text.ToString());
            cmd.Parameters.AddWithValue("@id", currentid);
            cmd.ExecuteNonQuery();
            con.Close();
        }



        public void GetUserBalance()
        {
            DataTable dt10 = new DataTable();
            adapt = new SQLiteDataAdapter("select sum(userbalance) as userbalance from tblAccountTransaction where accountid=" + userid + "", con);
            adapt.Fill(dt10);
            if (dt10.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt10.Rows[0]["userbalance"].ToString()))
                {

                    userbalance = Convert.ToInt64(dt10.Rows[0]["userbalance"].ToString());
                    accountbalanceTextBox.Text = dt10.Rows[0]["userbalance"].ToString();
                }

                else
                {
                    userbalance = 0;
                    accountbalanceTextBox.Text = userbalance.ToString();
                }

            }

        }

        public bool insertPurchase()
        {
         
           
            DataTable dt10 = new DataTable();
            adapt = new SQLiteDataAdapter("select id from tblPurchase where imie='" + imieTextBox.Text.ToString()+ "'", con);
            adapt.Fill(dt10);
            if (dt10.Rows.Count > 0)
            {
                purchaseitemid = Convert.ToInt64(dt10.Rows[0]["id"].ToString());
                return false;
            }
            else
            {
                cmd = new SQLiteCommand("insert into tblPurchase(imie,description,purchase_price,cnic,purchase_date,purchase_time) values(@imie,@description,@purchase_price,@cnic,@purchase_date,@purchase_time)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@imie", imieTextBox.Text.ToString());
                cmd.Parameters.AddWithValue("@description", descriptionTextBox.Text.ToString());
                cmd.Parameters.AddWithValue("@purchase_price", priceTextBox.Text.ToString());
                cmd.Parameters.AddWithValue("@cnic", cnicTextBox.Text);
                cmd.Parameters.AddWithValue("@purchase_date", DateTime.Today.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@purchase_time", DateTime.Now.ToString("h:mm:ss tt"));
                cmd.ExecuteNonQuery();

                con.Close();

               


                string sql = @"SELECT seq FROM  sqlite_sequence where name='" + "tblPurchase'";
                SQLiteCommand command = new SQLiteCommand(sql, con);
                con.Open();
                Object b = command.ExecuteScalar();
                if (b == null)
                {
                    purchaseitemid = 1;
                }
                else
                {
                   purchaseitemid = Convert.ToInt64(b);
                   

                }

                con.Close();
                return true;
            }
             
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


    }


}
