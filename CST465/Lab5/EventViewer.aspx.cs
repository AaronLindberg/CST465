using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lab5.App_Code;
namespace Lab5.Account
{
    public partial class EventViewer : System.Web.UI.Page
    {
        public CalendarEvent currentCalendarEvent = new CalendarEvent();

        protected void Page_Load(object sender, EventArgs e)
        {
            long cEventId;
            if (Request.QueryString.Count >= 1 && long.TryParse(Request.QueryString[0], out cEventId))
            {
                uxCommentDataSource.SelectParameters["EventId"].DefaultValue = Request.QueryString[0];

                currentCalendarEvent.ID = cEventId;
                currentCalendarEvent.loadEvent();
                uxEventName.Text = currentCalendarEvent.Name;
                uxUsername.Text = currentCalendarEvent.OwnerUserName;
                uxDateScheduled.Text = currentCalendarEvent.ScheduleDate.ToString("M/d/yyyy h:m:s tt");
                uxEventDescription.Text = currentCalendarEvent.Description;
                AttributeGrid.DataSource = currentCalendarEvent.Attributes;
                AttributeGrid.DataBind();
                if (currentCalendarEvent.Attributes.Count <= 0)
                {
                    AttributeFieldset.Visible = false;
                }
                PropertyFieldset.Visible = currentCalendarEvent.Properties.Count > 0;
                uxProperties.AssociatedProperties = currentCalendarEvent.Properties;
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void uxSubmitComment_Click(object sender, EventArgs e)
        {
            uxCommentDataSource.InsertParameters["UserId"].DefaultValue = Membership.GetUser().ProviderUserKey.ToString();
            uxCommentDataSource.InsertParameters["EventId"].DefaultValue = Request.QueryString[0];
            TextBox comment = uxCommentLoginView.FindControl("uxInsertComment") as TextBox;
            uxCommentDataSource.InsertParameters["Comment"].DefaultValue = comment.Text;
            uxCommentDataSource.InsertParameters["TimeStamp"].DefaultValue = DateTime.Now.ToString("s");
            uxCommentDataSource.Insert();
        }
    }
}