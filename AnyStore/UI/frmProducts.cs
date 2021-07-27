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
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        categoriesDAL cdal = new categoriesDAL();
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        userDAL udal = new userDAL();

        private void clear()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            txtProductID.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
            cmbCategory.Text = "";
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            //refrehing data grid view
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;

            //displaying categories in combo box
            DataTable categoriesDt = cdal.Select();
            cmbCategory.DataSource = categoriesDt;
            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";

            clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;

            String loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            p.added_by = usr.id;

            //Inserting data into database
            bool success = pdal.Insert(p);
            //if data is successfully inserted then the value of success will be true lese it will be false
            if (success == true)
            {
                //data sucessfully inserted
                MessageBox.Show("Product successfully created");
                clear();
            }
            else
            {
                //failed to insert data
                MessageBox.Show("Failed to add new Product");
            }
            //refrehing data grid view
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get index of particular row
            int rowIndex = e.RowIndex;
            txtProductID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get values from user ui
            p.id = int.Parse(txtProductID.Text);
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = Decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;

            String loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            p.added_by = usr.id;

            //updating data into database
            bool success = pdal.Update(p);
            //if data is updated successfully then the value of success will be true else false
            if (success == true)
            {
                //data updated successfully
                MessageBox.Show("Product successfully updated");
                clear();
            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to update Product");
            }
            //refrehing data grid view
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //getting user from form
            p.id = Convert.ToInt32(txtProductID.Text);

            bool success = pdal.Delete(p);
            //if data is deleted then the value of success will be true else false
            if (success == true)
            {
                //data updated successfully
                MessageBox.Show("Product successfully deleted");
                clear();
            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to delete Product");
            }
            //refrehing data grid view
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get keyword from textbox
            string keywords = txtSearch.Text;

            //check if keywords has value or not 
            if (keywords != null)
            {
                //show user based on keywords
                DataTable dt = pdal.Search(keywords);
                dgvProducts.DataSource = dt;
            }
            else
            {
                //show all users from database
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
        }
    }
}
