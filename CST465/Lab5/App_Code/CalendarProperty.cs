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
        long _id = -1;
        public long ID { get { return _id; } set { _id = value; } }
        String _name = "";
        public String Name { get { return _name; } set { _name = value; } }
        ArrayList _attributes = new ArrayList();
        object _user = null;
        Object User { get { return _user; } set { _user = value; } }
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
        public void CreateProperty()
        {
            if (_attributes.Count > 0)
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
                SqlCommand command = new SqlCommand("INSERT INTO [Property](Name, Creator) VALUES (@Name, @Creator);", connection);
                try
                {
                    command.Parameters.AddWithValue("Name", _name);
                    command.Parameters.AddWithValue("Creator", Membership.GetUser().ProviderUserKey);

                    connection.Open();
                    command.ExecuteScalar();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
                foreach (IPropertyAttribute a in _attributes)
                {
                    a.Create(this);
                }
            }
        }
        static CalendarProperty loadProperty(int id)
        {/*
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("StringAttribute_InsertUpdate", connection);
            try
            {
                command.Parameters.AddWithValue("AttributeId", _mId);
                command.Parameters.AddWithValue("EventFk", _EventId);
                command.Parameters.AddWithValue("AttributeName", _Name);
                command.Parameters.AddWithValue("AttributeValue", _mData);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }*/
            return new CalendarProperty() { ID = id };
        }
    }
}