﻿using AnyStore.BLL;
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
    class userDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Data from Database
        public DataTable Select()
        {
            //Static method to connect to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //To hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //sql qurry to get data from database
                String sql = "SELECT * FROM tbl_users";
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

        #region Insert Data in Database
        public bool Insert(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_users (first_name, last_name, email, username, password, contact, address, gender, user_type, added_date, added_by) VALUES (@first_name, @last_name, @email, @username, @password, @contact, @address, @gender, @user_type, @added_date, @added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if querry is executed then the value of rows will be greater than 0 else it will be less than 0
                if(rows > 0)
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

        #region Update data in Database
        public bool Update(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE tbl_users SET first_name=@first_name, last_name=@last_name, email=@email, username=@username, password=@password, contact=@contact, address=@address, gender=@gender, user_type=@user_type, added_date=@added_date, added_by=@added_by WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@id", u.id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if(rows > 0)
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

        #region Delete data from database
        public bool Delete(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_users WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", u.id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows > 0)
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
