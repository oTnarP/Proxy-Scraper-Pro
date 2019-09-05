using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VerifyUsers;

namespace Proxy_Scraper_Pro
{
    public partial class Login : Form
    {
        private bool mouseDown;
        private Point lastLocation;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.ActiveControl = btnClose;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                MessageBox.Show("Please, Enter your Username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (txtPass.Text == "")
            {
                MessageBox.Show("Please, Enter your Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {
                try
                {
                    Registration rg = new Registration();
                    rg.CheckInfo(txtUser, txtPass);

                    if (rg.Report == "Active")
                    {
                        var mf = new Main();
                        mf.Show();
                        this.Hide();
                    }

                    else if (rg.Report == "Deactive")
                    {
                        MessageBox.Show("Your license is expired!!", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    else
                    {
                        MessageBox.Show("Username or Password is incorrect.", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch
                {
                    MessageBox.Show("Please check your connection!", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            this.ActiveControl = btnClose;
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void linkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.softkini.com/contact");
        }
    }
}
