using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Threading;
using System.Web.UI.WebControls;
using System.Text;
using Lab5.App_Code;
using System.Collections;
public class WeekDayName
{
    public String Name { get; set; }
}
public class Week
{
    public ArrayList WeekDays { get; set; }
}
public class WeekDay
{
    public int Day { get; set; }
    public String className { get; set; }
    public ArrayList EventMemories{get; set;}
}

public partial class _Default : System.Web.UI.Page
{
    DateTime LastValidDate = new DateTime();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LastValidDate = DateTime.Now;
            uxViewingDate.Text = LastValidDate.ToString("MM/dd/yyyy");
        }
        else
        {
            DateTime.TryParseExact(uxViewingDate.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal,out LastValidDate);
        }
    }

    public override void RenderControl(HtmlTextWriter writer)
    {
        uxCalendarHeading.Text = LastValidDate.ToString("MMMM yyyy");
        ArrayList weekDayNames = new ArrayList();
        weekDayNames.Add(new WeekDayName() { Name = "Sunday" });
        weekDayNames.Add(new WeekDayName() { Name = "Monday" });
        weekDayNames.Add(new WeekDayName() { Name = "Tuesday" });
        weekDayNames.Add(new WeekDayName() { Name = "Wednesday" });
        weekDayNames.Add(new WeekDayName() { Name = "Thursday" });
        weekDayNames.Add(new WeekDayName() { Name = "Friday" });
        weekDayNames.Add(new WeekDayName() { Name = "Saturday" });
        uxWeekDayNameRep.DataSource = weekDayNames;
        uxWeekDayNameRep.DataBind();
        
        int dayCount = 1;
        String className = "dayFiller";
        bool StartDayCount = false;
        bool StopDayCount = true;
        DateTime d = new DateTime(LastValidDate.Year, LastValidDate.Month, 1, 0,0,0,0);
        DateTime di = new DateTime(LastValidDate.Year, LastValidDate.Month, 1, 0, 0, 0, 0);
        Lab5.App_Code.Calendar calendar = new Lab5.App_Code.Calendar();
        calendar.loadUserEvents(LastValidDate.Year, LastValidDate.Month, Membership.GetUser().ProviderUserKey);
        ArrayList Weeks = new ArrayList();

        for (int i = 0; i < 5; ++i)
        {
            Week week = new Week() { WeekDays = new ArrayList() };
            foreach (WeekDayName w in weekDayNames)
            {
                
                if (!StartDayCount)
                {
                    if (d.DayOfWeek.ToString() == w.Name)
                    {
                        StartDayCount = true;
                        className = "day";
                    }
                }
                else
                {
                    if (StopDayCount)
                    {
                        ++dayCount;
                        int month = di.Month;
                        di = di.AddDays(1);
                        StopDayCount = month == di.Month;
                        if (!StopDayCount)
                        {
                            className = "dayFiller";
                        }
                    }
                }
                WeekDay wd = new WeekDay() { Day = dayCount, className = className, EventMemories=new ArrayList() };
                if(StartDayCount && StopDayCount)
                {
                    if (calendar.Events.ContainsKey(di))
                    {
                        foreach (CalendarEvent e in calendar.Events[di] as List<CalendarEvent>)
                        {
                            wd.EventMemories.Add(e);
                        }
                    }
                    if (dayCount == LastValidDate.Day)
                    {
                        wd.className = "CalendarCurrentDay";
                    }
                }
                week.WeekDays.Add(wd);
            }
            Weeks.Add(week);
        }
        uxWeekRepeater.DataSource = Weeks;
        uxWeekRepeater.DataBind();
        
        base.RenderControl(writer);
    }
    
    protected void uxAddEvent_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Account/EventBuilder.aspx");
    }

    protected void uxViewingDate_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        Regex r = new Regex(@"/");
        String[] sa = r.Split(uxViewingDate.Text);
        if (args.IsValid = sa.Length == 3)
        {
            int day;
            int month;
            int year;
            if (args.IsValid = (int.TryParse(sa[0], out month) && month > 0 && month <= 12))
            {
                if (args.IsValid = int.TryParse(sa[2], out year) && year > 1900)
                {
                    int DaysInMonth = DateTime.DaysInMonth(year, month);
                    if (args.IsValid = int.TryParse(sa[1], out day) && day > 0 && day <= DaysInMonth)
                    {

                    }
                    else
                    {
                        uxViewingDate_CustomValidator.ErrorMessage = String.Format(@"The day for the month of {0} in the year {1}, must be greater than or equal to 1 and less than or equal to {2}.", new DateTime(year, month, 1).ToString("MMMM"), year, DaysInMonth);
                    }

                }
                else
                {
                    uxViewingDate_CustomValidator.ErrorMessage = @"The Year is required to follow the last / and be greater than 1900"; 
                }
            }
            else
            {
                uxViewingDate_CustomValidator.ErrorMessage = @"Month is required to be at the begining of the date and to be a number between 1 and 12, followed by a /";
            }
        }
        else
        {
            uxViewingDate_CustomValidator.ErrorMessage = @"The date is required to be in the form MM/DD/YYYY."; 
        }
        

    }

    protected void uxViewDate_Click(object sender, EventArgs e)
    {

    }
}