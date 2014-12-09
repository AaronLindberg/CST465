using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            if (Request.QueryString.Count >= 1)
            {
                int cEventId;

                int.TryParse(Request.QueryString[0], out cEventId);
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
    }
}