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
    class categoriesDAL
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
                string sql = "SELECT * FROM tbl_categories";

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
        public bool Insert(categoriesBLL c)
        {
            //create a bool variable and set its value to false and return it
            bool isSuccess = false;

            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //writing querry to add new category
                String sql = "INSERT INTO tbl_categories (title,description,added_date,added_by) VALUES (@title,@description,@added_date,@added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);

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
        public bool Update(categoriesBLL c)
        {
            //create a bool variable and set its value to false and return it
            bool isSuccess = false;

            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE tbl_categories SET title=@title, description=@description, added_date=@added_date, added_by=@added_by WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                cmd.Parameters.AddWithValue("@id", c.id);

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

        public bool Delete(categoriesBLL c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_categories WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", c.id);
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
    }
}
