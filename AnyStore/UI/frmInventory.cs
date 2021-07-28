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
    public partial class frmInventory : Form
    {
        public frmInventory()
        {
            InitializeComponent();
        }

        categoriesDAL cdal = new categoriesDAL();
        productsDAL pDal = new productsDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            //display categories in combo box
            DataTable cDt = cdal.Select();

            cmbCategories.DataSource = cDt;


            //give value member and display member
            cmbCategories.DisplayMember = "title";
            cmbCategories.ValueMember = "title";


            //display all products in data grid view
            DataTable pDt = pDal.Select();
            dgvProducts.DataSource = pDt;
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            //display all the products based on selected Category

            string category = cmbCategories.Text;

            DataTable dt = pDal.DisplayProductByCategory(category);

            dgvProducts.DataSource = dt;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            DataTable dt = pDal.Select();
            dgvProducts.DataSource = dt;
        }
    }
}
