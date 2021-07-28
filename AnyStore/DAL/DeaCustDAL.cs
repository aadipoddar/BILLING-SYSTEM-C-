using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyStore.BLL;

namespace AnyStore.DAL
{
    class DeaCustDAL
    {
        //static string method for database connection string 
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method
        public DataTable Select()
        {
            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //writing sql querry to get all data from database
                string sql = "SELECT * FROM tbl_dea_cust";

                //creating sql command to pass value
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //open database connection
                conn.Open();

                //adding the value from adapter to datatable dt
                adapter.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion

        #region Insert new Dealer or Customer
        public bool Insert(DeaCustBLL dc)
        {
            //create a bool variable and set its value to false and return it
            bool isSuccess = false;

            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //writing querry to add new category
                String sql = "INSERT INTO tbl_dea_cust (type,name,email,contact,address,added_date,added_by) VALUES (@type,@name,@email,@contact,@address,@added_date,@added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if querry is executed then the value of rows will be greater than 0 else it will be less than 0
                if (rows > 0)
                {
                    //querry Successfull
                    isSuccess = true;
                }
                else
                {
                    //querry failed
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update Method
        public bool Update(DeaCustBLL dc)
        {
            //create a bool variable and set its value to false and return it
            bool isSuccess = false;

            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE tbl_dea_cust SET type=@type,name=@name,email=@email,contact=@contact,address=@address, added_date=@added_date, added_by=@added_by WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
                cmd.Parameters.AddWithValue("@id", dc.id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    //querry is successfull
                    isSuccess = true;
                }
                else
                {
                    //querry is failed
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion

        #region Delete Category Method

        public bool Delete(DeaCustBLL dc)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_dea_cust WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", dc.id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    //querry is successfull
                    isSuccess = true;
                }
                else
                {
                    //query failed
                    {
                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion

        #region Method for Search Functionality
        public DataTable Search(string keywords)
        {
            //Static method to connect to database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //To hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql qurry to get data from database
                String sql = "SELECT * FROM tbl_dea_cust WHERE id LIKE '%" + keywords + "%' OR type LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' ";
                //for executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                conn.Open();
                //fill data in our datatable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //throw message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            //return the value in Datatable
            return dt;
        }
        #endregion

        #region Method to search dealer or customer for transaction module
        public DeaCustBLL SearchDealerCustomerForTransaction(string keyword)
        {
            //create an object for deacustbll class
            DeaCustBLL dc = new DeaCustBLL();

            //Create a database connectiom
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT name , email , contact , address from tbl_dea_cust WHERE id LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                adapter.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    dc.name = dt.Rows[0]["name"].ToString();
                    dc.email= dt.Rows[0]["email"].ToString();
                    dc.contact = dt.Rows[0]["contact"].ToString();
                    dc.address = dt.Rows[0]["address"].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dc;
        }
        #endregion
    }
}
