using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Lab5.App_Code
{
    [Serializable]
    public class CalendarProperty
    {
        //instance id, If null A new one is created when scheduled
        long _InstanceId = -1;
        public long InstanceId { get { return _InstanceId; } set { _InstanceId = value; } }
        long _PropertyId = -1;
        public long PropertyId { get { return _PropertyId; } set { _PropertyId = value; } }
        String _name = "";
        public String Name { get { return _name; } set { _name = value; } }
        ArrayList _attributes = new ArrayList();
        object _user = null;
        public Object User { get { return _user; } set { _user = value; } }
        public ArrayList Attributes { get { return _attributes; } set { _attributes = value; } }
        public bool validate(string input, out string errorMessage)
        {
            Boolean valid = true;
            errorMessage = "Passed!";
            if (input.Length > 0)
            {
                if (_attributes.Count > 0)
                {
                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
                    SqlCommand command = new SqlCommand("Property_Exsits", connection);
                    try
                    {
                        command.Parameters.AddWithValue("Name", input);
                        command.Parameters.AddWithValue("Creator", _user);

                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();
                        valid = !(Boolean)command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                else
                {
                    errorMessage = "Poperty must contain one or more attributes.";
                    valid = false;
                }

            }
            else
            {
                errorMessage = "Property Name must contain some characters to uniquely identify it";
                valid = false;
            }
            return valid;
        }
        public void AddAttribute(IPropertyAttribute attribute)
        {
            _attributes.Add(attribute);
        }
        public void RemoveAttribute(IPropertyAttribute attribute)
        {
            _attributes.Remove(attribute);
        }
        //Acceptable type names
        //
        public static IPropertyAttribute GetNewAttr(String Type, String Name, String Value)
        {
            IPropertyAttribute ret = null;
            switch (Type)
            {
                case "String":
                    ret = new StringPropertyAttribute();
                    break;
                case "Integer":
                    ret = new IntegerPropertyAttribute();     
                break;
                case "Decimal":
                ret = new DecimalPropertyAttribute();
                break;
                case "DateTime":
                    throw new NotImplementedException(String.Format("Type of \"{0}\" is an unimplimented attribute type for CalendarProperty's GetNewAttr", Type));
                break;
                default:
                    throw new Exception(String.Format("Invalid Type of \"{0}\" was supplied to CalendarProperty's GetNewAttr",Type));
            }
            if (ret != null)
            {
                ret.Name = Name;
                ret.Value = Value;
            }
            return ret;
        }
        public static AttributeType GetType(string type)
        {
            AttributeType ret;
            switch (type)
            {
                case "String":
                    ret = AttributeType.String;
                    break;
                case "Integer":
                    ret = AttributeType.Integer;
                    break;
                case "Decimal":
                    ret = AttributeType.Decimal;
                    break;
                case "DateTime":
                    throw new NotImplementedException(String.Format("Type of \"{0}\" is an unimplimented attribute type for CalendarProperty's GetNewAttr", type));
                    break;
                default:
                    throw new Exception(String.Format("Invalid Type of \"{0}\" was supplied to CalendarProperty's GetNewAttr", type));
            }
            return ret;
        }
        public void CreateProperty()//create or update
        {
            if (_attributes.Count > 0 && Membership.GetUser() != null)
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
                SqlCommand command = new SqlCommand("Property_InsertUpdate", connection);
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Id", _PropertyId);
                    command.Parameters.AddWithValue("Name", _name);
                    command.Parameters.AddWithValue("Creator", Membership.GetUser().ProviderUserKey);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.HasRows && reader.Read())
                    {
                        _PropertyId = (long)((int)reader.GetValue(0));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to Access the Database to Create Property");
                }
                finally
                {
                    connection.Close();
                }
                //need to remove attributes that no longer exist
                
                //?Yeah?
                
                //Do that ^^
                //get old Attributes
                //foreach exsisting attribute check if it still exsists
                CalendarProperty old =  new CalendarProperty();
                old.loadProperty(PropertyId);
                foreach (IPropertyAttribute ipa in old.Attributes)
                {
                    Boolean stillExists = false;
                    foreach (IPropertyAttribute current in Attributes)
                    {
                        if (current.ID != null)
                        {
                            if (ipa.Type == current.Type && (long)ipa.ID == (long)current.ID)
                            {
                                stillExists = true;
                                break;
                            }
                        }
                    }
                    if (!stillExists)
                    {
                        ipa.Delete();
                    }
                }

                foreach (IPropertyAttribute a in _attributes)
                {
                    a.Create(this);
                }
            }
        }
        public void Schedule(CalendarEvent e)
        {

            //check if id is null, create new if null
            if (InstanceId == -1)
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
                SqlCommand command = new SqlCommand("PropertyInstance_Create", connection);
                try
                {
                    command.Parameters.AddWithValue("EventId", e.ID);
                    command.Parameters.AddWithValue("PropertyId", PropertyId);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        InstanceId = (long)reader.GetValue(0);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to access database to Schedule Property Instance");
                }
                finally
                {
                    connection.Close();
                }
            }
            
            foreach (IPropertyAttribute a in _attributes)
            {
                a.Schedule(this);
            }
            //run schedule Property Insert Update Query
            //throw new NotImplementedException("Calendar Property Scheduling Not Implimented");
        }

        public void loadProperty(long propertyId)
        {
            CalendarProperty p = null;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("Property_Select", connection);
            try
            {
                command.Parameters.AddWithValue("PropertyId", propertyId);

                command.CommandType = CommandType.StoredProcedure;
                
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    //p = new CalendarProperty();
                    PropertyId = (long)reader.GetValue(0);
                    Name = reader.GetString(1);
                    User = reader.GetValue(2);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to access database to create String property attribute");
            }
            finally
            {
                connection.Close();
            }
            Attributes.Clear();
            Attributes.AddRange(StringPropertyAttribute.getAttributes(propertyId));
            Attributes.AddRange(IntegerPropertyAttribute.getAttributes(propertyId));
            Attributes.AddRange(DecimalPropertyAttribute.getAttributes(propertyId));
            //return p;
        }
        public static CalendarProperty loadPropertyInstance(long InstanceId)
        {

            CalendarProperty p = null;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("PropertyInstance_Select", connection);
            try
            {
                command.Parameters.AddWithValue("InstanceId", InstanceId);

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    p = new CalendarProperty() { 
                        PropertyId = (long)reader.GetValue(0), 
                        Name =  reader.GetString(1),
                        User = reader.GetValue(2),
                        InstanceId = InstanceId
                        };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to access database to create String property attribute");
            }
            finally
            {
                connection.Close();
            }
            p.Attributes.AddRange(StringPropertyAttribute.getAttributes(p.PropertyId, p.InstanceId));
            p.Attributes.AddRange(IntegerPropertyAttribute.getAttributes(p.PropertyId, p.InstanceId));
            return p;
        }
        public static ArrayList GetEventProperties(long EventId)
        {
            ArrayList Instances = new ArrayList();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("PropertyInstance_SelectByEvent", connection);
            try
            {
                command.Parameters.AddWithValue("EventId", EventId);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.HasRows && reader.Read())
                {
                    Instances.Add((long)reader.GetValue(0));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get all an event's properties.", ex);
            }
            finally
            {
                connection.Close();
            }
            ArrayList EventProperties = new ArrayList();
            foreach (long instance in Instances)
            {
                EventProperties.Add(CalendarProperty.loadPropertyInstance(instance));
            }
            return EventProperties;
        }
        public static ArrayList LoadAvailablePropertiesByUser(Guid User)
        {
            ArrayList AvailableProperties = new ArrayList();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("Property_SelectByCreator", connection);
            try
            {
                object t;
                command.Parameters.AddWithValue("Creator", User);
                command.Parameters[0].IsNullable = true;
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                ArrayList arr = new ArrayList();
                while (reader.HasRows && reader.Read())
                {

                    arr.Add(new CalendarProperty() { PropertyId = (long)reader.GetValue(0), Name = reader.GetString(1) });
                }
                AvailableProperties = arr;
            }
            catch (Exception ex)
            {
                //throw new Exception("Unable to access database to create integer property attribute");
            }
            finally
            {
                connection.Close();
            }
            return AvailableProperties;
        }
    }
}