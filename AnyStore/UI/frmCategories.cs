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
    public partial class frmCategories : Form
    {
        public frmCategories()
        {
            InitializeComponent();
        }

        categoriesBLL c = new  categoriesBLL();
        categoriesDAL dal = new categoriesDAL();
        userDAL udal = new userDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //getiing data from UI
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;

            //getting username of logged in user
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            c.added_by = usr.id;

            //Inserting data into database
            bool success = dal.Insert(c);
            //if data is successfully inserted then the value of success will be true lese it will be false
            if (success == true)
            {
                //data sucessfully inserted
                MessageBox.Show("Category successfully created");
                clear();
            }
            else
            {
                //failed to insert data
                MessageBox.Show("failed to add new Category");
            }
            //refrehing data grid view
            DataTable dt = dal.Select();
            dgvCategories.DataSource = dt;
        }

        private void clear()
        {
            txtTitle.Text = "";
            txtCategoryID.Text = "";
            txtDescription.Text = "";
        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get index of particular row
            int rowIndex = e.RowIndex;
            txtCategoryID.Text = dgvCategories.Rows[rowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dgvCategories.Rows[rowIndex].Cells[1].Value.ToString();
            txtDescription.Text = dgvCategories.Rows[rowIndex].Cells[2].Value.ToString();
        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
            //refrehing data grid view
            DataTable dt = dal.Select();
            dgvCategories.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get values from user ui
            c.id = Convert.ToInt32(txtCategoryID.Text);
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;

            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            c.added_by = usr.id;

            //updating data into database
            bool success = dal.Update(c);
            //if data is updated successfully then the value of success will be true else false
            if (success == true)
            {
                //data updated successfully
                MessageBox.Show("Category successfully updated");
                clear();
            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to update Category");
            }
            //refrehing data grid view
            DataTable dt = dal.Select();
            dgvCategories.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //getting user from form
            c.id = Convert.ToInt32(txtCategoryID.Text);

            bool success = dal.Delete(c);
            //if data is deleted then the value of success will be true else false
            if (success == true)
            {
                //data updated successfully
                MessageBox.Show("Category successfully deleted");
                clear();
            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to delete Category");
            }
            //refrehing data grid view
            DataTable dt = dal.Select();
            dgvCategories.DataSource = dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get keyword from textbox
            string keywords = txtSearch.Text;

            //check if keywords has value or not 
            if (keywords != null)
            {
                //show user based on keywords
                DataTable dt = dal.Search(keywords);
                dgvCategories.DataSource = dt;
            }
            else
            {
                //show all users from database
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
        }
    }
}
