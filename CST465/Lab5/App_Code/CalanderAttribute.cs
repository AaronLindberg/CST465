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
    Date
}
public interface ICalanderAttribute
{
    AttributeType getType();
    String ToString();
}

public class IntCalanderAttribute:ICalanderAttribute
{
    public int mData { get; set; }

    public String ToString()
    {
        return mData.ToString();
    }
    public AttributeType getType()
    {
        return AttributeType.Integer;
    }
}

