using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// Summary description for CalanderAttribute
/// </summary>
public enum AttributeType
{
    String,
    Integer,
    Date
}
public interface ICalendarAttribute
{
    AttributeType Type { get; }
    Boolean validate(String input, out String errorMessage);
    String Name { get; set; }
    String Value {get;set; }
    String ToString();
    void Schedule(CalendarEvent ce);
    void UpdateDb();
}
[Serializable]
public class StringCalendarAttribute:ICalendarAttribute
{
    const int MAX_LENGTH = 2048;
    private int _mId = -1;
    private String _Name;
    public String Name { get { return _Name; } set { _Name = value.Trim(); } }
    private int _EventId = -1;
    public int EventId { get { return _EventId; } set { _EventId = value; } }
    private String _mData;
    public static ArrayList getEventStringAttributes(int EventId)
    {
        ArrayList retArray = new ArrayList();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
        SqlCommand command = new SqlCommand("SELECT StringID, EventMemoryFK, StringAttributeName, StringValue FROM StringAttribute WHERE EventMemoryFK = @ID;", connection);
        SqlDataReader reader = null;
        try
        {
            command.Parameters.AddWithValue("ID", EventId);

            connection.Open();
            reader = command.ExecuteReader();
            while (reader.HasRows && reader.Read())
            {
                retArray.Add(new StringCalendarAttribute()
                {
                    _mId = (int)reader[0],
                    _EventId = (int)reader[1],
                    _Name = (String)reader[2],
                    _mData = (String)reader[3]
                });
            }
            connection.Close();
            connection.Dispose();
            connection = null;
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
        return retArray;
    }
    public StringCalendarAttribute()
    {
    }
    public StringCalendarAttribute(int Id, int EventFk, String data, string name)
    {
        _mId = Id;
        _EventId = EventFk;
        _mData = data;
        _Name = name;
    }
    public AttributeType Type{ get { return AttributeType.String; }}

    public Boolean validate(String input, out String errorMessage)
    {
        bool ret = true;
        errorMessage = "";
        if (input.Trim().Length <= 0)
        {
            errorMessage = "String Attribute must contain a string.";
            ret = false;
        }
        else if (input.Trim().Length > MAX_LENGTH)
        {
            errorMessage = "String Attribute must not contain more than {0} displayable characters.";
            ret = false;
        }
        return ret;
    }
    public string Value
    {
        get
        {
            return _mData;
        }
        set
        {
            _mData = value.Trim();
        }
    }

    public void Schedule(CalendarEvent ce)
    {
        _EventId = ce.ID;
        UpdateDb();
    }
    public void UpdateDb()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
        SqlCommand command = new SqlCommand("StringAttribute_InsertUpdate", connection);
        try
        {
            command.Parameters.AddWithValue("AttributeId", _mId);
            command.Parameters.AddWithValue("EventFk", _EventId);
            command.Parameters.AddWithValue("AttributeName", _Name);
            command.Parameters.AddWithValue("AttributeValue", _mData);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }
        finally
        {
            connection.Close();
        }
    }
}

[Serializable]
public class IntegerCalendarAttribute:ICalendarAttribute
{

    private int _mId = -1;
    private String _Name;
    public String Name { get { return _Name; } set { _Name = value; } }
    private int _EventId = -1;
    public int EventId { get { return _EventId; } set { _EventId = value; } }
    private int _mData;
    public static ArrayList getEventIntegerAttributes(int EventId)
    {
        ArrayList retArray = new ArrayList();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
        SqlCommand command = new SqlCommand("SELECT IntegerID, EventMemoryFK, IntegerAttributeName, IntegerValue FROM IntegerAttribute WHERE EventMemoryFK = @ID;", connection);
        SqlDataReader reader = null;
        try
        {
            command.Parameters.AddWithValue("ID", EventId);

            connection.Open();
            reader = command.ExecuteReader();
            while (reader.HasRows && reader.Read())
            {
                retArray.Add(new IntegerCalendarAttribute()
                {
                    _mId = (int)reader[0],
                    _EventId = (int)reader[1],
                    _Name = (String)reader[2],
                    _mData = (int)reader[3]
                });
            }
            connection.Close();
            connection.Dispose();
            connection = null;
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
        return retArray;
    }
    public IntegerCalendarAttribute()
    {
    }
    public IntegerCalendarAttribute( int Id, int EventFk, int data, string name)
    {
        _mId = Id;
        _EventId = EventFk;
        _mData = data;
        _Name = name;
    }
    public int mData { get{return _mData;} set{_mData = value;} }
    public String Value
    {
        get
        {
            return String.Format("{0}", _mData);
        }
        set
        {
            int.TryParse(value, out _mData);
        }
    }

    public AttributeType Type { get { return AttributeType.Integer; } }


    public bool validate(String input, out string Message)
    {
        Message = "Must be a whole base 10 number.";
        int tmp;
        return (input.Length > 0)?int.TryParse(input, out tmp):false;
    }

    public void Schedule(CalendarEvent ce)
    {
        _EventId = ce.ID;
        UpdateDb();
    }
    public void UpdateDb()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
        SqlCommand command = new SqlCommand("IntegerAttribute_InsertUpdate", connection);
        try
        {
            command.Parameters.AddWithValue("AttributeId",_mId);
            command.Parameters.AddWithValue("EventFk", _EventId);
            command.Parameters.AddWithValue("AttributeName", _Name);
            command.Parameters.AddWithValue("AttributeValue", _mData);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }
        finally
        {
            connection.Close();
        }
    }
}

