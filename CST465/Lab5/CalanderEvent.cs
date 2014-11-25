using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for CalanderEvent
/// </summary>
public class CalanderEvent
{
    int EventIdentifier { get; set; }
    String EventName { get; set; }
    DateTime Scheduled { get; set; }
    List<ICalanderAttribute> attributes;
    public CalanderEvent(int EventId)
    {
        EventIdentifier = EventId;
    }
    private void load()
    {
        loadIntegers();
        loadStrings();
        loadDecimal();
    }
    private void loadIntegers()
    {
        SqlCommand cmd = new SqlCommand("SELECT IntegerId, EventMemoryFk, IntegerAttributeName, IntegerValue FROM IntegerAttribute WHERE EventMemoryFk = @EventMemoryId;");
        SqlDataReader reader = null;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
        try
        {
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("EventMemoryId", EventIdentifier);
            connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.HasRows && reader.Read())
            {
                IntCalanderAttribute a = new IntCalanderAttribute();
                a.mData = (int)reader.GetValue(3);
                a.AttributeName = (String)reader.GetValue(2);
                a.Id = (int)reader.GetValue(0);
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
            connection.Close();
        }
    }
    private void loadStrings()
    {
        SqlCommand cmd = new SqlCommand("SELECT StringId, EventMemoryFk, StringAttributeName, StringValue FROM StringAttribute WHERE EventMemoryFk = @EventMemoryId;");
        SqlDataReader reader = null;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
        try
        {
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("EventMemoryId", EventIdentifier);
            connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.HasRows && reader.Read())
            {
                IntCalanderAttribute a = new IntCalanderAttribute();
                a.mData = (int)reader.GetValue(3);
                a.AttributeName = (String)reader.GetValue(2);
                a.Id = (int)reader.GetValue(0);
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
            connection.Close();
        }
    }
    private void loadDecimal()
    {
        SqlCommand cmd = new SqlCommand("SELECT DecimalId, EventMemoryFk, DecimalAttributeName, DecimalValue FROM DecimalAttribute WHERE EventMemoryFk = @EventMemoryId;");
        SqlDataReader reader = null;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
        try
        {
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("EventMemoryId", EventIdentifier);
            connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.HasRows && reader.Read())
            {
                IntCalanderAttribute a = new IntCalanderAttribute();
                a.mData = (int)reader.GetValue(3);
                a.AttributeName = (String)reader.GetValue(2);
                a.Id = (int)reader.GetValue(0);
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
            connection.Close();
        }
    }
}