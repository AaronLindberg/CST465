using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EventBuilder : System.Web.UI.Page
{
    CalendarEvent NewEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            NewEvent = ViewState["newEvent"] as CalendarEvent;
        }
        else
        {
            uxScheduleDate.Text = DateTime.Now.ToString("M/d/yyyy H:m:s");
            NewEvent = new CalendarEvent();
            ViewState["newEvent"] = NewEvent;
        }
    }

    protected void uxAddAttribute_Click(object sender, EventArgs e)
    {
        ICalendarAttribute i;
        switch (uxDataType.SelectedIndex)
        {
            case 0://String 
                i = new StringCalendarAttribute(); 
            break;
            case 1:
                i = new IntegerCalendarAttribute();
            break;
            default:
                i = null;
            break;
        }
        if (i != null && i.validate(uxData.Text))
        {
            i.Value = uxData.Text;
            i.Name = uxAttributeId.Text;
            NewEvent.Attributes.Add(i);
            uxData.Text = "";
            uxAttributeId.Text = "";
        }
        uxAttributeGrid.DataSource = NewEvent.Attributes;
        uxAttributeGrid.DataBind();
        ViewState["newEvent"] = NewEvent;
    }
    bool validate()
    {
        return true;
    }
    protected void uxScheduleEvent_Click(object sender, EventArgs e)
    {
        NewEvent = (CalendarEvent)ViewState["newEvent"];
        NewEvent.Name = uxEventName.Text.Trim();
        NewEvent.ScheduleDate = DateTime.ParseExact(uxScheduleDate.Text, "M/d/yyyy H:m:s", System.Globalization.CultureInfo.InvariantCulture);
        NewEvent.UserId = Membership.GetUser().ProviderUserKey;
        NewEvent.Description = uxEventDescription.Text;
        NewEvent.InsertUpdate();
    }
}