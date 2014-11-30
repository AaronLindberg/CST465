using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lab5.App_Code;
namespace Lab5.Account
{
    public partial class ViewProfile : System.Web.UI.Page
    {
        ProfileInformation profile = new ProfileInformation();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                profile.loadLoggedInUser();
                literalFirstname.Text = profile.Firstname;
                literalLastname.Text = profile.Lastname;
            }
        }

        protected void uxEditProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/EditProfile.aspx");
        }
    }
}