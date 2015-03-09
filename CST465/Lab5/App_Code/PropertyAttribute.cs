using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lab5.App_Code
{
    public interface IPropertyAttribute
    {
        Object ID { get; set; }
        AttributeType Type { get; }
        Boolean validate(String input, out String errorMessage);
        //ArrayList getAttributes(long propertyId, Object eventId = null);//Needs to be static for every derived class
        String Name { get; set; }
        String Value { get; set; }
        String ToString();
        //void loadInstance(long InstanceId);
        //void loadTemplate(long PropertyArrtibuteId);
        void Create(CalendarProperty p);
        void Schedule(CalendarProperty ce);
        void Delete();
        //void UpdateDb();
    }
   
    [Serializable]
    public class StringPropertyAttribute : IPropertyAttribute
    {
        const long MAX_LENGTH = 2048;
        private object _mId = null;//valueID
        public Object ID { get { return _mId; } set { _mId = value; } }
        public long AttributeId { get { return (_mId == null)?-1:(long)_mId; } set { _mId = value; } }
        private String _Name;
        public String Name { get { return _Name; } set { _Name = value.Trim(); } }
        private Object _InstanceId = null;
        public long InstanceId { get { return (_InstanceId == null) ? -1 : (long)_InstanceId; } set { _InstanceId = value; } }
        private long _PropertyId = -1;
        public long PropertyId { get { return _PropertyId; } set { _PropertyId = value; } }
        private String _mData;
        public StringPropertyAttribute()
        {
        }
        
        void loadTemplate(long PropertyArrtibuteId)
        {
            throw new NotImplementedException("PropertyArrtibuteId is not implimented for StringProperty Attribute");
        }
        public static ArrayList LoadPropertyInstances(long PropertyInstance,long PropertyId)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("Property_StringAttribute_Select", connection);
            ArrayList arr = new ArrayList();
            try
            {

                command.Parameters.AddWithValue("PropertyInstance", PropertyInstance);
                command.Parameters.AddWithValue("PropertyId", PropertyId);

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader r = command.ExecuteReader();
                while (r.HasRows && r.Read())
                {
                    arr.Add(new StringPropertyAttribute() { PropertyId = PropertyId, AttributeId = (long)r.GetValue(0), InstanceId = PropertyInstance, Name = r.GetString(1), Value = r.GetString(2), ID=(long)r.GetSqlValue(3) });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to access database to update string property attribute");
            }
            finally
            {
                connection.Close();
            }
            return arr;
        }

        public AttributeType Type { get { return AttributeType.String; } }

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

        public void Schedule(CalendarProperty cp)
        {
            _InstanceId = cp.InstanceId;
            _PropertyId = cp.PropertyId;
            
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("PropertyStringAttribute_InsertUpdate", connection);
            try
            {

                command.Parameters.AddWithValue("InstanceId", (_InstanceId == null) ? DBNull.Value : _InstanceId);
                command.Parameters.AddWithValue("AttributeId", (_mId == null) ? DBNull.Value : _mId);
                command.Parameters.AddWithValue("PropertyId", _PropertyId);
                command.Parameters.AddWithValue("AttributeName", Name);
                command.Parameters.AddWithValue("DefaultValue", Value);

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to access database to update string property attribute");
            }
            finally
            {
                connection.Close();
            }
        }


        public void Create(CalendarProperty p)
        {
            //throw new NotImplementedException();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("PropertyStringAttribute_CreateUpdate", connection);

            try
            {
                command.Parameters.AddWithValue("Id", (_mId == null)?DBNull.Value:_mId);
                command.Parameters.AddWithValue("PropertyId", _PropertyId = p.PropertyId);
                command.Parameters.AddWithValue("AttributeName", Name);
                command.Parameters.AddWithValue("DefaultValue", Value);

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to access database to create String property attribute");
            }
            finally
            {
                connection.Close();
            }
        }

        //by not passing an instance id, the templated property is returned
        public static ArrayList getAttributes(long propertyId, object instanceId = null)
        {
            ArrayList PropertyStringAttributes = new ArrayList();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("Property_StringAttribute_Select", connection);
            try
            {
                SqlParameter param = command.Parameters.AddWithValue("PropertyInstance", ((instanceId == null) ? DBNull.Value : instanceId));
                param.IsNullable = true;
                command.Parameters.AddWithValue("PropertyId", propertyId);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.HasRows && reader.Read())
                {
                    PropertyStringAttributes.Add(new StringPropertyAttribute() { AttributeId = (long)reader.GetValue(0), Name = reader.GetString(1), Value = reader.GetString(2) });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get all string attributes for a property.", ex);
            }
            finally
            {
                connection.Close();
            }
            return PropertyStringAttributes;
        }

        
        void loadInstance()
        {
      
        }


        public void Delete()
        {
            if (ID != null)
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
                SqlCommand command = new SqlCommand("Property_StringAttribute_Delete", connection);
                try
                {
                    SqlParameter param = command.Parameters.AddWithValue("Id", ID);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to get all string attributes for a property.", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
    [Serializable]
    public class IntegerPropertyAttribute : IPropertyAttribute
    {
        const int MAX_LENGTH = 2048;
        private object _mId = null;
        public object ID
        {
            get
            {
                return _mId;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        private String _Name;
        public String Name { get { return _Name; } set { _Name = value.Trim(); } }
        private long _EventId = -1;
        public long EventId { get { return _EventId; } set { _EventId = value; } }
        private long _PropertyId = -1;
        public long PropertyId { get { return _PropertyId; } set { _PropertyId = value; } }
        private String _mData;

        public IntegerPropertyAttribute()
        {
        }
        public void load(int Id)
        {
            
        }
        public AttributeType Type { get { return AttributeType.String; } }

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

        public void Schedule(CalendarProperty ce)
        {
           throw new NotImplementedException();
        }


        public void Create(CalendarProperty p)
        {
            //throw new NotImplementedException();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("PropertyStringAttribute_CreateUpdate", connection);
            try
            {
                command.Parameters.AddWithValue("Id", _mId);
                command.Parameters.AddWithValue("PropertyId", _PropertyId = p.PropertyId);
                command.Parameters.AddWithValue("AttributeName", Name);
                command.Parameters.AddWithValue("DefaultValue", Value);

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to access database to create integer property attribute");
            }
            finally
            {
                connection.Close();
            }
        }


        public ArrayList getAttributes(long propertyId, object eventId = null)
        {
            //load(propertyId);
            return new ArrayList();
        }


        public void loadInstance(long InstanceId)
        {
            throw new NotImplementedException();
        }

        public void loadTemplate(long PropertyArrtibuteId)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
