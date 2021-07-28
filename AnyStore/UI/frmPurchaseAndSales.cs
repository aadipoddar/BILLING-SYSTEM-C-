using AnyStore.DAL;
using AnyStore.BLL;
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
    public partial class frmPurchaseAndSales : Form
    {
        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }

        DeaCustDAL dcDal = new DeaCustDAL();
        productsDAL pDal = new productsDAL();
        DataTable transactionDT = new DataTable();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblSubTotal_Click(object sender, EventArgs e)
        {

        }

        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            //get transationtype value from user dashborad
            string type = frmUserDashboard.transactionType;

            //set the value on lblTop
            lblTop.Text = type;

            //specify columns for aor transaction datatables
            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Total");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get keyword from text box
            string keyword = txtSearch.Text;

            if(keyword == "")
            {
                //clear all textboxes
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";

                return;
            }

            //write code to get details and set values on text boxes
            DeaCustBLL dc = dcDal.SearchDealerCustomerForTransaction(keyword);

            //now set the value from deacustdll to textboxes
            txtName.Text = dc.name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            //get keyword from text box
            string keyword = txtSearchProduct.Text;

            if (keyword == "")
            {
                //clear all textboxes
                txtProductName.Text = "";
                txtProductQty.Text = "";
                txtProductRate.Text = "";
                txtProductInventory.Text = "";

                return;
            }

            //write code to get details and set values on text boxes
            productsBLL p = pDal.GetProductsForTransaction(keyword);

            //now set the value from deacustdll to textboxes
            txtProductName.Text = p.name;
            txtProductInventory.Text = p.qty.ToString();
            txtProductRate.Text = p.rate.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //get product name rate and quantity
            string productName = txtProductName.Text;
            decimal Rate = decimal.Parse(txtProductRate.Text);
            decimal Qty= decimal.Parse(txtProductQty.Text);

            decimal Total = Rate * Qty;

            decimal subTotal = decimal.Parse(txtSubTotal.Text);
            subTotal = subTotal + Total;

            //check whether the product is selected or not
            if(productName == "")
            {
                MessageBox.Show("Selct Product first");
            }
            else
            {
                transactionDT.Rows.Add(productName, Rate, Qty, Total);

                dgvAddedProducts.DataSource = transactionDT;

                txtSubTotal.Text = subTotal.ToString();

                //clear textboxes
                txtSearchProduct.Text = "";
                txtProductRate.Text = "";
                txtProductName.Text = "";
                txtProductQty.Text = "";
                txtProductInventory.Text = "";
            }
        }
    }
}
