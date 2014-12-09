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
            if(uxRoleLoginView.FindControl("uxUsers") != null && uxRoleLoginView.FindControl("uxRoles") != null)
            {
                foreach (MembershipUser user in Membership.GetAllUsers())
                {
                    (uxRoleLoginView.FindControl("uxUsers") as DropDownList).Items.Add(new ListItem(user.UserName));
                }
                foreach (string role in Roles.GetAllRoles())
                {
                    (uxRoleLoginView.FindControl("uxRoles") as DropDownList).Items.Add(new ListItem(role));
                }
            }
        }

        protected void uxAddUserToRole_Click(object sender, EventArgs e)
        {
            if (uxRoleLoginView.FindControl("uxUsers") != null && uxRoleLoginView.FindControl("uxRoles") != null)
            {
                if (!Roles.IsUserInRole((uxRoleLoginView.FindControl("uxUsers") as DropDownList).SelectedItem.Text, (uxRoleLoginView.FindControl("uxRoles") as DropDownList).SelectedItem.Text))
                {
                    Roles.AddUserToRole((uxRoleLoginView.FindControl("uxUsers") as DropDownList).SelectedItem.Text, "Admins");
                }
            }
        }

        protected void uxCreateRole_Click(object sender, EventArgs e)
        {
            if (uxRoleLoginView.FindControl("uxRoleName") != null )
            {
                Roles.CreateRole((uxRoleLoginView.FindControl("uxRoleName") as TextBox).Text);
            }
        }
    }
}