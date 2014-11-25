using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Lab5.Account
{
    public partial class EditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT UserId, FirstName, LastName FROM UserProfile Where UserId = @UserId;", connection);
            try
            {
                command.Parameters.AddWithValue("UserId", Membership.GetUser().ProviderUserKey);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    uxFirstName.Text = (string)reader.GetValue(1);
                    uxLastName.Text = (string)reader.GetValue(2);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
        }

        protected void uxSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("UserProfile_InsertUpdate", connection);
            try
            {
                command.Parameters.AddWithValue("UserId", Membership.GetUser().ProviderUserKey);
                command.Parameters.AddWithValue("FirstName", uxFirstName.Text);
                command.Parameters.AddWithValue("LastName", uxLastName.Text);

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