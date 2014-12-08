using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Lab5.App_Code
{
    [Serializable]
    public class Calendar
    {
        //DateTime key of the scheduled date for a List<CalendarEvent>
        public Hashtable Events {get; set;}
        public Calendar()
        {
            Events = new Hashtable();
        }
        public void loadUserEvents(int year, int month, object UserId)
        {
            SqlDataReader reader;
            DateTime lower = new DateTime(year, month, 1, 0, 0, 0, 0);
            DateTime upper = new DateTime(year, month, DateTime.DaysInMonth(year,month),23, 59, 59, 99);
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT EventMemoryId, UserFK, EventName, EventDescription, Scheduled FROM EventMemory WHERE UserFK = @UserID;", connection);
            try
            {
                command.Parameters.AddWithValue("UserID", UserId);
                command.Parameters.AddWithValue("lower", lower);
                command.Parameters.AddWithValue("upper", upper);

                connection.Open();
                reader = command.ExecuteReader();
                while (reader.HasRows && reader.Read())
                {
                    CalendarEvent e = new CalendarEvent((int)reader.GetValue(0), (String)reader.GetValue(2), (String)reader.GetValue(3), (DateTime)reader.GetValue(4), reader.GetValue(1));
                    DateTime d = (DateTime)reader.GetValue(4);
                    d = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0, 0);
                    if (Events.ContainsKey(d))
                    {
                        (Events[d] as List<CalendarEvent>).Add(e);
                    }
                    else
                    {
                        Events[d] = new List<CalendarEvent>() { e };
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}