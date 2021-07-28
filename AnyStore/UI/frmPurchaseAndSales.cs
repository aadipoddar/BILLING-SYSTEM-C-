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
    }
}
