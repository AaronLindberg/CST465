using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace Lab5.App_Code
{
    public class ProfileInformation
    {
        Object IdKey { get { return m_ID; } }
        Object m_ID = null;
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        
        public void loadLoggedInUser()
        {
            m_ID = Membership.GetUser().ProviderUserKey;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT UserId, FirstName, LastName FROM UserProfile Where UserId = @UserId;", connection);
            SqlDataReader reader = null;
            try
            {
                command.Parameters.AddWithValue("UserId", m_ID);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    Firstname = (string)reader.GetValue(1);
                    Lastname = (string)reader.GetValue(2);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (reader != null)
                    reader.Close();
                connection.Close();
            }
            loadUserNameEmail();
        }
        private void loadUserNameEmail()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT aspnet_Users.UserName, aspnet_Membership.Email FROM aspnet_Membership INNER JOIN aspnet_Users ON aspnet_Membership.UserId = aspnet_Users.UserId WHERE aspnet_Users.UserId = @UserId;", connection);
            SqlDataReader reader = null;
            try
            {
                command.Parameters.AddWithValue("UserId", m_ID);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    Username = (string)reader.GetValue(0);
                    Email = (string)reader.GetValue(1);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (reader != null)
                    reader.Close();
                connection.Close();
            }
        }
        public void UpdateProfile()
        {
            m_ID = Membership.GetUser().ProviderUserKey;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("UserProfile_InsertUpdate", connection);
            try
            {
                command.Parameters.AddWithValue("UserId", m_ID);
                command.Parameters.AddWithValue("FirstName", Firstname);
                command.Parameters.AddWithValue("LastName", Lastname);

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            UpdateEmail();
            UpdateUserName();
            
        }
        private void UpdateUserName()
        {
            if (!UserNameExsists())
            {
                m_ID = Membership.GetUser().ProviderUserKey;
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
                SqlCommand command = new SqlCommand("UPDATE aspnet_Users SET UserName = @UserName, LoweredUserName = @LoweredUserName WHERE UserId = @UserId;", connection);
                try
                {
                    command.Parameters.AddWithValue("UserId", m_ID);
                    command.Parameters.AddWithValue("UserName", Username);
                    command.Parameters.AddWithValue("LoweredUserName", Username.ToLower());

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
                UpdateUserName();
            }
        }
        public bool UserNameExsists()
        {
            bool ret = true;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT COUNT(aspnet_Users.LoweredUserName) FROM aspnet_Users WHERE aspnet_Users.LoweredUserName = @UserName;", connection);
            SqlDataReader reader = null;
            try
            {
                command.Parameters.AddWithValue("UserName", Username.ToLower());
                connection.Open();
                int count = (int)command.ExecuteScalar();
                ret = count >= 1;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (reader != null)
                    reader.Close();
                connection.Close();
            }
            return ret;
        }
        public void UpdateEmail()
        {
            if (!EmailExsists())
            {
                m_ID = Membership.GetUser().ProviderUserKey;
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
                SqlCommand command = new SqlCommand("UPDATE aspnet_Membership SET Email = @Email, LoweredEmail = @LoweredEmail WHERE UserId = @UserId;", connection);
                try
                {
                    command.Parameters.AddWithValue("UserId", m_ID);
                    command.Parameters.AddWithValue("Email", Email);
                    command.Parameters.AddWithValue("LoweredEmail", Email.ToLower());

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
                UpdateUserName();
            }
        }
        public bool EmailExsists()
        {
            bool ret = true;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT COUNT(LoweredEmail) FROM aspnet_Membership WHERE LoweredEmail = @Email;", connection);
            SqlDataReader reader = null;
            try
            {
                command.Parameters.AddWithValue("Email", Email.ToLower());
                connection.Open();
                int count = (int)command.ExecuteScalar();
                ret = count >= 1;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (reader != null)
                    reader.Close();
                connection.Close();
            }
            return ret;
        }
    }
}