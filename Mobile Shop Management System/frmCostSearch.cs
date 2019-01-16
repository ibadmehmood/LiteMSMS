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
    public partial class frmCostSearch : Form
    {
        System.Data.SQLite.SQLiteConnection con;
        System.Data.SQLite.SQLiteDataAdapter adapt;
        public frmCostSearch()
        {
            InitializeComponent();
            con = new SQLiteConnection("Data Source=database.db;Version=3;New=false;Compress=True;");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCostSearch_Load(object sender, EventArgs e)
        {
            

          

            imieTextBox.KeyDown += textBox1_KeyDown;
            descriptionTextBox.KeyDown += descriptionTextBox_KeyDown;
          
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

        private void descriptionTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down
                if (descriptionTextBox.TextLength > 2)
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    adapt = new SQLiteDataAdapter("select * from tblPurchase where description='" + descriptionTextBox.Text.ToString() + "'", con);
                    adapt.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Item Not Found!");
                        descriptionTextBox.Text = "";
                        priceTextBox.Text = "";
                        imieTextBox.Text = "";
                    }
                    else
                    {
                        descriptionTextBox.Text = dt.Rows[0]["description"].ToString();
                        priceTextBox.Text = dt.Rows[0]["purchase_price"].ToString();
                        imieTextBox.Text = dt.Rows[0]["imie"].ToString();
                    }

                    

                }
            }
        }

        public void generateSuggestions()
        {
            AutoCompleteStringCollection phoneCollection = new AutoCompleteStringCollection();

            con.Open();
            SQLiteCommand cmnd1 = con.CreateCommand();
            cmnd1.CommandType = CommandType.Text;
            cmnd1.CommandText = "SELECT * FROM tblPurchase";
            SQLiteDataReader Reader;
            Reader = cmnd1.ExecuteReader();

            if (Reader.Read())
            {
                while (Reader.Read())
                {
                    phoneCollection.Add(Reader["description"].ToString());


                }

            }
            else
            {
                MessageBox.Show("Data not found");
            }
            Reader.Close();

            descriptionTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            descriptionTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            descriptionTextBox.AutoCompleteCustomSource = phoneCollection;
            con.Close();
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
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Item Not Found!");
                        descriptionTextBox.Text = "";
                        priceTextBox.Text = "";
                        imieTextBox.Text = "";
                    }
                    else
                    {
                        imieTextBox.Text = dt.Rows[0]["imie"].ToString();
                        descriptionTextBox.Text = dt.Rows[0]["description"].ToString();
                        priceTextBox.Text = dt.Rows[0]["purchase_price"].ToString();
                    }
                }
            }
        }
    }
}
