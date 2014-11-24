using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Lab5.Admin
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (MembershipUser user in Membership.GetAllUsers())
            {
                uxUsers.Items.Add(new ListItem(user.UserName));
            }
            foreach (string role in Roles.GetAllRoles())
            {
                uxRoles.Items.Add(new ListItem(role));
            }
        }

        protected void uxAddUserToRole_Click(object sender, EventArgs e)
        {
            if (!Roles.IsUserInRole(uxUsers.SelectedItem.Text, uxRoles.SelectedItem.Text))
            {
                Roles.AddUserToRole(uxUsers.SelectedItem.Text, "Admins");
            }
        }

        protected void uxCreateRole_Click(object sender, EventArgs e)
        {
            Roles.CreateRole(uxRoleName.Text);
        }
    }
}