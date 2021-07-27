using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class productsDAL
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
                string sql = "SELECT * FROM tbl_products";

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

        #region Insert new Category
        public bool Insert(productsBLL p)
        {
            //create a bool variable and set its value to false and return it
            bool isSuccess = false;

            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //writing querry to add new category
                String sql = "INSERT INTO tbl_products (name, category,description,rate,qty,added_date,added_by) VALUES (@name,@category,@description,@rate,@qty,@added_date,@added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@title", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);

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
        public bool Update(productsBLL p)
        {
            //create a bool variable and set its value to false and return it
            bool isSuccess = false;

            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE tbl_products SET name=@name,category=@category, description=@description, rate=@rate, qty=@qty, added_date=@added_date, added_by=@added_by WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);
                cmd.Parameters.AddWithValue("@id", p.id);

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

        public bool Delete(productsBLL p)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_products WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", p.id);
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
                String sql = "SELECT * FROM tbl_products WHERE id LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' OR category LIKE '%" + keywords + "%' OR description LIKE '%" + keywords + "%' ";
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
    }
}
