using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
namespace Lab5
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Init(Object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}