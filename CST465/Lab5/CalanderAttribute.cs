using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CalanderAttribute
/// </summary>
public enum AttributeType
{
    String,
    Integer,
    Decimal,
    Date
}
public interface ICalanderAttribute
{
    String getAttributeName();
    AttributeType getType();
    String ToString();
}
public class DateTimeCalanderAttribute : ICalanderAttribute
{
    public int Id { get; set; }
    public DateTime mData { get; set; }
    public String AttributeName { get; set; }
    public String getAttributeName()
    {
        return AttributeName;
    }
    public String ToString()
    {
        return mData.ToString();
    }
    public AttributeType getType()
    {
        return AttributeType.Date;
    }
}

public class DecimalCalanderAttribute : ICalanderAttribute
{
    public int Id { get; set; }
    public Double mData { get; set; }
    public String AttributeName { get; set; }
    public String getAttributeName()
    {
        return AttributeName;
    }
    public String ToString()
    {
        return mData.ToString();
    }
    public AttributeType getType()
    {
        return AttributeType.Decimal;
    }
}

public class StringCalanderAttribute : ICalanderAttribute
{
    public int Id { get; set; }
    public String mData { get; set; }
    public String AttributeName { get; set; }
    public String getAttributeName()
    {
        return AttributeName;
    }
    public String ToString()
    {
        return mData.ToString();
    }
    public AttributeType getType()
    {
        return AttributeType.String;
    }
}
public class IntCalanderAttribute:ICalanderAttribute
{
    public int Id { get; set; }
    public int mData { get; set; }
    public String AttributeName { get; set; }
    public String getAttributeName()
    {
        return AttributeName;
    }
    public String ToString()
    {
        return mData.ToString();
    }
    public AttributeType getType()
    {
        return AttributeType.Integer;
    }
}

