﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using Lab5.App_Code;

/// <summary>
/// Summary description for CalanderEvent
/// </summary>
[Serializable]
public class CalendarEvent
{
    public ArrayList Attributes { get; set; }
    int _EventId = -1;
    public int ID
    {
        get { return _EventId; }
        set { _EventId = value; }
    }
    DateTime _ScheduleDate;
    String _Name;
    String _Desc;
    object _UserId = null;
    public object UserId { get { return _UserId; } set { _UserId = value; } }
    String _OwnerUserName;
    public String OwnerUserName { get { return _OwnerUserName; } set { _OwnerUserName = value; } }
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
	public CalendarEvent()
	{
        Attributes = new ArrayList();
	}
    public CalendarEvent(int EventId, String EventName, String description, DateTime Scheduled, object UserID)
    {
        _EventId = EventId;
        _Name = EventName;
        _ScheduleDate = Scheduled;
        _UserId = UserID;
        _Desc = description;
    }
    public static CalendarEvent FindEvent(String EventName, DateTime Scheduled, Object UserId)
    {
        CalendarEvent cEvent = null;

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
        SqlCommand command = new SqlCommand("SELECT EventMemoryId, UserFK, EventName, EventDescription, Scheduled, UserName FROM EventMemory JOIN aspnet_Users ON UserId = UserFK WHERE EventName = @EventName AND UserId = @UserId AND Scheduled = @Scheduled;", connection);
        SqlDataReader reader = null;
        try
        {
            command.Parameters.AddWithValue("EventName", EventName);
            command.Parameters.AddWithValue("Scheduled", Scheduled);
            command.Parameters.AddWithValue("UserId", UserId);

            connection.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows && reader.Read())
            {
                cEvent = new CalendarEvent();
                cEvent._EventId = (int)reader[0];
                cEvent._UserId = reader[1];
                cEvent._Name = (String)reader[2];
                cEvent._Desc = (String)reader[3];
                cEvent._ScheduleDate = (DateTime)reader[4];
                cEvent._OwnerUserName = (String)reader[5];
            }
            connection.Close();
            connection.Dispose();
            connection = null;

            cEvent.Attributes = new ArrayList();
            cEvent.Attributes.AddRange(StringCalendarAttribute.getEventStringAttributes(cEvent._EventId));

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
        return cEvent;
    }
    public void loadEvent()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
        SqlCommand command = new SqlCommand("SELECT EventMemoryId, UserFK, EventName, EventDescription, Scheduled, UserName FROM EventMemory JOIN aspnet_Users ON UserId = UserFK WHERE EventMemoryId = @ID;", connection);
        SqlDataReader reader = null ;
        try
        {
            command.Parameters.AddWithValue("ID", _EventId);

            connection.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows && reader.Read())
            {
                _EventId = (int)reader[0];
                _UserId = reader[1];
                _Name = (String)reader[2];
                _Desc = (String)reader[3];
                _ScheduleDate = (DateTime)reader[4];
                _OwnerUserName = (String)reader[5];
            }
            connection.Close();
            connection.Dispose();
            connection = null;
            
            Attributes = new ArrayList();
            Attributes.AddRange(StringCalendarAttribute.getEventStringAttributes(_EventId));
            Attributes.AddRange(IntegerCalendarAttribute.getEventIntegerAttributes(_EventId));
            Attributes.AddRange(DecimalCalendarAttribute.getEventDecimalAttributes(_EventId));
            Attributes.AddRange(DateTimeCalendarAttribute.getEventDateTimeAttributes(_EventId));
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

    public void Insert()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
        SqlCommand command = new SqlCommand("INSERT INTO EventMemory (UserFK, EventName, EventDescription, Scheduled) VALUES (@UserId, @EventName, @EventDescription, @DateScheduled)", connection);
        try
        {
            command.Parameters.AddWithValue("UserId", _UserId);
            command.Parameters.AddWithValue("DateScheduled", _ScheduleDate.ToString("yyyy-MM-ddTHH:mm:ss"));
            command.Parameters["DateScheduled"].DbType = DbType.DateTime;
            command.Parameters.AddWithValue("EventDescription", _Desc);
            command.Parameters.AddWithValue("EventName", _Name);

            //command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();
            connection = null;
            command = null;
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            command = new SqlCommand("SELECT EventMemoryId FROM EventMemory WHERE UserFK = @UserId AND Scheduled = @Scheduled AND EventName = @EventName AND EventDescription = @EventDescription;", connection);
            command.Parameters.AddWithValue("UserId", _UserId);
            command.Parameters.AddWithValue("EventName", _Name);
            command.Parameters.AddWithValue("EventDescription", _Desc);
            command.Parameters.AddWithValue("Scheduled", _ScheduleDate.ToString("s"));
            command.Parameters["Scheduled"].DbType = DbType.DateTime;
            connection.Open();
            _EventId = (int)command.ExecuteScalar();
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