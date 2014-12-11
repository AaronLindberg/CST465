using System;
using System.Collections.Specialized;
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
            uxScheduleDate.Text = DateTime.Now.ToString("M/d/yyyy h:m:s tt");
            NewEvent = new CalendarEvent();
            ViewState["newEvent"] = NewEvent;
        }
    }

    protected void uxAddAttribute_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ICalendarAttribute i;
            String ErrorMessage;
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
            if (i != null && i.validate(uxData.Text, out ErrorMessage))
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
    }
    protected void uxScheduleEvent_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            NewEvent = (CalendarEvent)ViewState["newEvent"];
            NewEvent.Name = uxEventName.Text.Trim();
            NewEvent.ScheduleDate = DateTime.ParseExact(uxScheduleDate.Text, "M/d/yyyy h:m:s tt", System.Globalization.CultureInfo.InvariantCulture);
            NewEvent.UserId = Membership.GetUser().ProviderUserKey;
            NewEvent.Description = uxEventDescription.Text;
            NewEvent.Insert();
            Response.Redirect("~/CalendarViewer.aspx");
        }
    }

    protected void ExistingEvent_ServerValidate(object source, ServerValidateEventArgs args)
    {
        DateTime s;
        args.IsValid = true;
        if (DateTime.TryParseExact(uxScheduleDate.Text, "M/d/yyyy h:m:s tt", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out s))
        {
            if(CalendarEvent.FindEvent(uxEventName.Text, s, Membership.GetUser().ProviderUserKey) != null)
            {
                args.IsValid = false;
                CustomEventNameValidator.ErrorMessage = "The event trying to be shcedued has already been scheduled by you with the same name and time.";
            }
        }
        else
        {
            CustomEventNameValidator.ErrorMessage = "Date is Invalid, unable to determine if event has already been scheduled.";
            args.IsValid = false;
        }
    }

    protected void ScheduleDate_ServerValidate(object source, ServerValidateEventArgs args)
    {
        DateTime s;
        args.IsValid = DateTime.TryParseExact(uxScheduleDate.Text, "M/d/yyyy h:m:s tt", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out s);
        if (!args.IsValid)
        {
            (source as CustomValidator).ErrorMessage = "Unable to parse the schedule date, the date is not properly formatted with correct values.";
        }
    }

    protected void uxData_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        ICalendarAttribute i = null;
        switch (uxDataType.SelectedValue)
        {
            case "String":
                i = new StringCalendarAttribute();
                break;
            case "Integer":
                i = new IntegerCalendarAttribute();
                break;
        }
        string errmsg;
        args.IsValid = i.validate(args.Value,out errmsg );
        (source as CustomValidator).ErrorMessage = errmsg;
        //args.IsValid = Int32.TryParse(foo, out i);
    }
}