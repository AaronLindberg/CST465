using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Default : System.Web.UI.Page
{
    DateTime LastValidDate = new DateTime();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
            LastValidDate = DateTime.Now;
    }
    void InitBuildCalander()
    {
        LastValidDate = new DateTime(Int32.Parse(uxYear.Text), Int32.Parse(uxMonth.Text), Int32.Parse(uxDay.Text));
        //For now display the date successfuly obtained from the controls
        literalCalander.Text = String.Format("{0}/{1}/{2}",uxMonth.SelectedIndex+1, uxDay.Text, uxYear.Text);
        //Will build a 7x5 week grid
        String calander = String.Format("<div id=\"divCalander\"><h2 id=\"calanderHeading\">{0} {1}</h2>",LastValidDate.ToString("MMMM"), LastValidDate.Year);
        int dayCount = 0;
        bool startCount = false;
        DateTime firstDay = new DateTime(LastValidDate.Year, LastValidDate.Month, 1);
        calander += "<div class=\"week\">";
        String[] daysOfTheWeek = new String[]{"Sunday", "Monday", "Tuesday", "Wednessday", "Thursday", "Friday", "Saturday"};
        for (int dayOfWeek = 0; dayOfWeek < 7; ++dayOfWeek)
        {
            //every day into it's divider
            calander += "<div class=\"day\">";
            calander += String.Format("<h3 class=\"weekDayHeading\">{0}</h3>", daysOfTheWeek[dayOfWeek]);
            calander += "</div>";
        }//close week divider
        calander += "</div>";

        for (int week = 0; week < 6; ++week)
        {
            //put every week into a divider
            calander += "<div class=\"week\">";
            for (int dayOfWeek = 0; dayOfWeek < 7; ++dayOfWeek)
            {
                if (dayOfWeek == (int)firstDay.DayOfWeek)
                {//dictate when to start labeling the days
                    startCount = true;
                }
                //every day into it's divider
                calander += String.Format("<div{0} class=\"day\">",((startCount && dayCount + 1 == LastValidDate.Day)?" id=\"divCalanderCurrentDay\"":""));
                
                if (startCount && dayCount < DateTime.DaysInMonth(LastValidDate.Year,LastValidDate.Month))
                {//show the day
                    ++dayCount;
                    calander += String.Format("<h3>{0}</h3>",dayCount);
                    //needs to look for events scheduled on the day and display them appropriately
 
                }//close day divider
                calander += "</div>";
            }//close week divider
            calander += "</div>";
        }//close calander divider
        calander += "</div>";
        literalCalander.Text = calander;
    }

    protected void uxViewDate_Click(object sender, EventArgs e)
    {
        updateDayRangeValidator(null, null);
        InitBuildCalander();
    }
    protected void updateDayRangeValidator(object sender, EventArgs e)
    {
        int year = LastValidDate.Year;
        Int32.TryParse(uxYear.Text, out year);
        if (year < 1 || year > 9999)
        {
            year = LastValidDate.Year;
        }
        int month = Int32.Parse(uxMonth.Text);
        
        uint day = (uint)LastValidDate.Day;
        UInt32.TryParse(uxDay.Text, out day);

        int daysInMonth = DateTime.DaysInMonth(year,month);

        if(day < 1 || day > daysInMonth)
            day = 1;
        
        LastValidDate = new DateTime(year, month, (int)day);

        uxDayRangeValidator.MaximumValue = DateTime.DaysInMonth(year, month).ToString();
        uxDayRangeValidator.ErrorMessage = String.Format("The day is required to be between 1 and {0} for viewing the calander in the month of {2} for the year {1}.", daysInMonth, year, LastValidDate.ToString("MMMM"));
    }
    protected void uxAddEvent_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Account/EventBuilder.aspx");
    }
}