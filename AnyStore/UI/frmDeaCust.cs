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
    public partial class frmDeaCust : Form
    {
        public frmDeaCust()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        DeaCustBLL dc = new DeaCustBLL();
        DeaCustDAL dcDal = new DeaCustDAL();

        userDAL udal = new userDAL();

        private void clear()
        {
            txtAddress.Text = "";
            txtContact.Text = "";
            txtDeaCustID.Text = "";
            txtEmail.Text = "";
            txtName.Text = "";
            txtSearch.Text = "";
            cmdDeaCust.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //getiing data from UI
            dc.type = cmdDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;

            //getting username of logged in user
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            dc.added_by = usr.id;

            //Inserting data into database
            bool success = dcDal.Insert(dc);
            //if data is successfully inserted then the value of success will be true lese it will be false
            if (success == true)
            {
                //data sucessfully inserted
                MessageBox.Show("Dealer/Customer successfully created");
                clear();
            }
            else
            {
                //failed to insert data
                MessageBox.Show("failed to add new Dealer/Customer");
            }
            //refrehing data grid view
            DataTable dt = dcDal.Select();
            dgvDeaCust.DataSource = dt;
        }

        private void frmDeaCust_Load(object sender, EventArgs e)
        {
            //refrehing data grid view
            DataTable dt = dcDal.Select();
            dgvDeaCust.DataSource = dt;
        }

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get index of particular row
            int rowIndex = e.RowIndex;

            txtDeaCustID.Text = dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString();
            cmdDeaCust.Text = dgvDeaCust.Rows[rowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get values from user ui
            dc.id = Convert.ToInt32(txtDeaCustID.Text);
            dc.type = cmdDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;

            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            dc.added_by = usr.id;

            //updating data into database
            bool success = dcDal.Update(dc);
            //if data is updated successfully then the value of success will be true else false
            if (success == true)
            {
                //data updated successfully
                MessageBox.Show("Dealer/Customer successfully updated");
                clear();
            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to update Dealer/Customer");
            }
            //refrehing data grid view
            DataTable dt = dcDal.Select();
            dgvDeaCust.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //getting user from form
            dc.id = Convert.ToInt32(txtDeaCustID.Text);

            bool success = dcDal.Delete(dc);
            //if data is deleted then the value of success will be true else false
            if (success == true)
            {
                //data updated successfully
                MessageBox.Show("Dealer/Customer successfully deleted");
                clear();
            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to delete Dealer/Customer");
            }
            //refrehing data grid view
            DataTable dt = dcDal.Select();
            dgvDeaCust.DataSource = dt;
        }
    }
}
