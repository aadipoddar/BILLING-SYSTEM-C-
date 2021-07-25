using AnyStore.BLL;
using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pboxClose_Click(object sender, EventArgs e)
        {
            //code to close the form
            this.Close();
        }

        private void lblLogin_Click(object sender, EventArgs e)
        {
            l.username = txtUserName.Text.Trim();
            l.password = txtUserName.Text.Trim();
            l.user_type = cmbUserType.Text.Trim();

            //checking login credentials
            bool success = dal.loginCheck(l);
            if(success == true)
            {
                //login successfull
                MessageBox.Show("Login Successfull");

                //need to open rspective form based on user type
                switch(l.user_type)
                {
                    case "Admin":
                        {
                            //dsiplay admin dashboard
                            frmAdminDashboard admin = new frmAdminDashboard();
                            admin.Show();
                            this.Hide();
                        }
                        break;
                    case "User":
                        {
                            //display user dashboard
                            frmUserDashboard user = new frmUserDashboard();
                            user.Show();
                            this.Hide();
                        }
                        break;
                    default:
                        {
                            //display error message
                            MessageBox.Show("Invalid user type");
                        }
                        break;
                }
            }
            else
            {
                //login failed
                MessageBox.Show("Login Failed");
            }
        }
    }
}
