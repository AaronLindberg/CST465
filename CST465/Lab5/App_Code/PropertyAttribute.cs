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
        AttributeType Type { get; }
        Boolean validate(String input, out String errorMessage);
        String Name { get; set; }
        String Value { get; set; }
        String ToString();
        void Create(CalendarProperty p);
        void Schedule(CalendarEvent ce);
        void UpdateDb();
    }
    /*[Serializable]
    public class CalendarProperty
    {
        public long ID { get{return _mId;} }
        long _mId;
        public bool validate(string input, out string errorMessage)
        {
            Boolean valid = false;
            errorMessage = "Not Implimented or Unexpected";

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("Property_Exsits", connection);
            try
            {
                command.Parameters.AddWithValue("Name", _Name);
                command.Parameters.AddWithValue("Creator", _Name);

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                valid = (Boolean)command.ExecuteScalar();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return valid;
        }

        public Object CreatorId
        {
            get;
            set;
        }

        long EventId { get { return (_EventId == null)?0:(long)_EventId; } set {_EventId = value;} }
        Object _EventId = null;
        public ArrayList Attributes
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        String _Name = "";

        public void Create()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("Property_Exsits", connection);
            try
            {
                command.Parameters.AddWithValue("Name", _Name);
                command.Parameters.AddWithValue("Creator", _Name);

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

        public void Schedule(CalendarEvent ce)
        {
            throw new NotImplementedException();
        }

        public void UpdateDb()
        {
            throw new NotImplementedException();
        }
    }*/
    [Serializable]
    public class StringPropertyAttribute : IPropertyAttribute
    {
        const long MAX_LENGTH = 2048;
        private long _mId = -1;
        private String _Name;
        public String Name { get { return _Name; } set { _Name = value.Trim(); } }
        private Object _EventId = null;
        public long EventId { get { return (_EventId == null) ? -1 : (int)_EventId; } set { _EventId = value; } }
        private long _PropertyId = -1;
        public long PropertyId { get { return _PropertyId; } set { _PropertyId = value; } }
        private String _mData;
       
        public StringPropertyAttribute()
        {
        }
        public StringPropertyAttribute(int Id)
        {
            _mId = Id;
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

        public void Schedule(CalendarEvent ce)
        {
            _EventId = ce.ID;
            UpdateDb();
        }
        public void UpdateDb()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("PropertyStringAttribute_InsertUpdate", connection);
            try
            {
                command.Parameters.AddWithValue("EventFk", _EventId);
                command.Parameters.AddWithValue("Id", _mId);
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
                command.Parameters.AddWithValue("Id", _mId);
                command.Parameters.AddWithValue("PropertyId", _PropertyId = p.ID);
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
    }
    [Serializable]
    public class IntegerPropertyAttribute : IPropertyAttribute
    {
        const int MAX_LENGTH = 2048;
        private long _mId = -1;
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
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("Property_StringAttribute_InsertUpdate", connection);
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

        public void Schedule(CalendarEvent ce)
        {
            _EventId = ce.ID;
            UpdateDb();
        }
        public void UpdateDb()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("PropertyStringAttribute_InsertUpdate", connection);
            try
            {
                command.Parameters.AddWithValue("EventFk", _EventId);
                command.Parameters.AddWithValue("Id", _mId);
                command.Parameters.AddWithValue("PropertyId", _PropertyId);
                command.Parameters.AddWithValue("AttributeName", Name);
                command.Parameters.AddWithValue("DefaultValue", Value);

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to access database to update integer property attribute");
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
                command.Parameters.AddWithValue("Id", _mId);
                command.Parameters.AddWithValue("PropertyId", _PropertyId = p.ID);
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
    }
}
