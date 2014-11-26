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
        }
    }
}