using AnyStore.DAL;
using AnyStore.BLL;
using System.Transactions;
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
        userDAL uDal = new userDAL();
        DataTable transactionDT = new DataTable();
        transactionDAL tDal = new transactionDAL();
        transactionDetailDAL tdDal = new transactionDetailDAL();

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

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            string value = txtDiscount.Text;

            if(value == "")
            {
                MessageBox.Show("Please add discount first");
            }
            else
            {
                decimal subTotal = decimal.Parse(txtSubTotal.Text);

                decimal discount = decimal.Parse(txtDiscount.Text);

                decimal grandTotal = ((100 - discount) / 100) * subTotal;

                txtGrandTotal.Text = grandTotal.ToString();
            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            string check = txtGrandTotal.Text;
            if(check == "")
            {
                MessageBox.Show("Calulate the discount and set the grand total first");
            }
            else
            {
                decimal previousGT = decimal.Parse(txtGrandTotal.Text);
                decimal vat = decimal.Parse(txtVat.Text);
                decimal grandTotalWithVAT = ((100+vat)/100)*previousGT;

                txtGrandTotal.Text = grandTotalWithVAT.ToString();
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            decimal garndTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse(txtPaidAmount.Text);

            decimal returnAmount = paidAmount - garndTotal;

            txtReturnAmount.Text = returnAmount.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //get values from purchase sales from first
            transactionsBLL transaction = new transactionsBLL();   

            transaction.type = lblTop.Text;

            //get id from of dealer or customer here
            //lets get name of the dealer or customer
            string DeaCustName = txtName.Text;

            DeaCustBLL dc = dcDal.GetDeaCustIDFromName(DeaCustName);

            transaction.dea_cust_id = dc.id;
            transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text),2);
            transaction.transaction_date = DateTime.Now;
            transaction.tax = decimal.Parse(txtVat.Text);
            transaction.discount = decimal.Parse(txtDiscount.Text);

            //get username of logged in user
            string username = frmLogin.loggedIn;
            userBLL u  = uDal.GetIDFromUsername(username);

            transaction.added_by = u.id;
            transaction.transactionDetails = transactionDT;

            //lets create a boolean variable and set value to false
            bool success = false;

            //actual code to insert transaction and transaction details
            using(TransactionScope scope = new TransactionScope())
            {
                int transactionID = -1;

                //create bool value and insert transaction
                bool w = tDal.Insert_Transaction(transaction, out transactionID);

                //use fore loop to insert transaction details
                for(int i = 0; i < transactionDT.Rows.Count; i++)
                {
                    //get all deatils of products
                    transactionDetailBLL transactionDetail = new transactionDetailBLL();

                    //get the product name and convert it to id
                    string ProductName = transactionDT.Rows[i][0].ToString();

                    productsBLL p  = pDal.GetProductIDFromName(ProductName);

                    transactionDetail.product_id = p.id;
                    transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                    transactionDetail.qty = decimal.Parse(transactionDT.Rows[i][2].ToString());
                    transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()),2);
                    transactionDetail.dea_cust_id = dc.id;
                    transactionDetail.added_date = DateTime.Now;
                    transactionDetail.added_by = u.id;

                    //insert transaction details inside database
                    bool y = tdDal.InsertTransactionDetail(transactionDetail);                    
                    success = w && y;
                }

                if (success == true)
                {
                    //transaction scope
                    scope.Complete();

                    MessageBox.Show("Transaction Coompleted");

                    //clear data grid view and all text boxes
                    dgvAddedProducts.DataSource = null;
                    dgvAddedProducts.Rows.Clear();

                    txtSearch.Text = "";
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtContact.Text = "";
                    txtAddress.Text = "";
                    txtSearchProduct.Text = "";
                    txtProductName.Text = "";
                    txtProductInventory.Text = "";
                    txtProductRate.Text = "";
                    txtProductQty.Text = "";
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtVat.Text = "0";
                    txtGrandTotal.Text = "0";
                    txtReturnAmount.Text = "";
                    txtPaidAmount.Text = "0";
                }
                else
                {
                    MessageBox.Show("Transaction Failed");
                }
            }
        }
    }
}
