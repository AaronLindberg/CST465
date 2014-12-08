using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for CalanderEvent
/// </summary>
[Serializable]
public class CalendarEvent
{
    public List<ICalendarAttribute> Attributes { get; set; }
    int _EventId = -1;
    bool _LoadedFromDb = false;
    DateTime _ScheduleDate;
    String _Name;
    String _Desc;
    object _UserId = null;
    public object UserId { get { return _UserId; } set { _UserId = value; } }
    public DateTime ScheduleDate
    {
        get { return _ScheduleDate; }
        set { _ScheduleDate = value; }
    }
    public String Name
    {
        get { return _Name; }
        set { _Name = value; }
    }
    public String Description
    {
        get { return _Desc; }
        set { _Desc = value; }
    }
    public bool IsLoadedFromDb { get { return _LoadedFromDb; } }
    public int ID
    {
        set
        {
            _EventId = value;
        }
        get { return (int)_EventId; }
    }
	public CalendarEvent()
	{
        Attributes = new List<ICalendarAttribute>();
	}
    public CalendarEvent(int EventId, String EventName, String description, DateTime Scheduled, object UserID)
    {
        _EventId = EventId;
        _Name = EventName;
        _ScheduleDate = Scheduled;
        _UserId = UserID;
        _Desc = description;
    }
    public void loadAttributes()
    {
        _LoadedFromDb = true;
    }
    public void InsertUpdate()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
        SqlCommand command = new SqlCommand("EventMemory_InsertUpdate", connection);
        try
        {
            command.Parameters.AddWithValue("UserId", _UserId);
            command.Parameters.AddWithValue("EventId", _EventId);
            command.Parameters.AddWithValue("DateScheduled", _ScheduleDate);
            command.Parameters.AddWithValue("EventDescription", _Desc);
            command.Parameters.AddWithValue("EventName", _Name);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
            connection.Dispose();
            connection = null;
            foreach (ICalendarAttribute a in Attributes)
            {
                a.Schedule(this);
            }
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }

        }
    }
}