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
    public partial class frmLogin : Form
    {
        SQLiteDataAdapter adapt;
        SQLiteConnection con;
        public frmLogin()
        {

            InitializeComponent();
            con = new SQLiteConnection("Data Source=database.db;Version=3;New=false;Compress=True;");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.ToString();
            string password = passwordTextBox.Text.ToString();
            SQLiteCommand cmd = new SQLiteCommand("select * from tblUser where username=@username and password=@password", con);
            con.Open();
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password",password );
           

            SQLiteDataReader reader=cmd.ExecuteReader();
           

            if (reader.Read())
            {
                reader.Close();
                con.Close();
              
                //MessageBox.Show("true");
                if (!isOpen("Mobile Shop Management System"))
                {
                    
                    this.Hide();
                    frmMain frmLogin = new frmMain();
                    // frmLogin.Closed += (s, args) => this.Close(); 
                    frmLogin.Show();
                }
               

            }
            else
            {
                reader.Close();
                con.Close();
                MessageBox.Show("Incorrect Username or Password !");
               
            }
            
        }
        private bool isOpen(string name)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == name)
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }

            if (IsOpen == false)
            {
                return IsOpen;
            }
            return IsOpen;
        }
    }
}
