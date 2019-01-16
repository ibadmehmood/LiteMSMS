using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mobile_Shop_Management_System
{
    public partial class frmSplashScreen : Form
    {
        public frmSplashScreen()
        {
            InitializeComponent();
        }

        private void frmSplashScreen_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.1;
            timer2.Start();
            timer1.Start();
            timer1.Tick += timer1_Tick_1;
            timer2.Tick += timer2_Tick;
        }

       

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (timer1.Interval == 1000)
            {
                this.Hide();
                Timer timer = (Timer)sender;
                timer.Stop();

                if (!isOpen("Login"))
                {
                    frmLogin frmLogin = new frmLogin();
                    // frmLogin.Closed += (s, args) => this.Close(); 
                    frmLogin.Show();
                }
               
                
                
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           

            if (timer2.Interval == 100)
            {
                this.Opacity = 1;
            }
            else if (timer2.Interval == 800)
            {
                this.Opacity = 0.5;
                Timer timer = (Timer)sender;
                timer.Stop();
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
