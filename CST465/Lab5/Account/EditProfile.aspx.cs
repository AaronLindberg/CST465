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
using Lab5.App_Code;
namespace Lab5.Account
{
    public partial class EditProfile : System.Web.UI.Page
    {
        ProfileInformation profile = new ProfileInformation();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                profile.loadLoggedInUser();
                uxFirstName.Text = profile.Firstname;
                uxLastName.Text = profile.Lastname;
            }
        }

        protected void uxSubmit_Click(object sender, EventArgs e)
        {
            profile.Firstname = uxFirstName.Text;
            profile.Lastname = uxLastName.Text;
            profile.UpdateProfile();
            Response.Redirect("~/Account/ViewProfile.aspx");
        }
    }
}