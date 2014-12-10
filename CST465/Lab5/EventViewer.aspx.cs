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
        CalendarEvent CalendarEvent = new CalendarEvent();
        protected void Page_Load(object sender, EventArgs e)
        {
            int cEventId;
            if (Request.QueryString.Count >= 1 && int.TryParse(Request.QueryString[0], out cEventId))
            {
                uxCommentDataSource.SelectParameters["EventId"].DefaultValue = Request.QueryString[0];

                CalendarEvent.ID = cEventId;
                CalendarEvent.loadEvent();
                uxEventName.Text = CalendarEvent.Name;
                uxUsername.Text = CalendarEvent.OwnerUserName;
                uxDateScheduled.Text = CalendarEvent.ScheduleDate.ToString("MM/dd/yyyy");
                uxEventDescription.Text = CalendarEvent.Description;
                AttributeGrid.DataSource = CalendarEvent.Attributes;
                AttributeGrid.DataBind();
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