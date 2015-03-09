using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lab5.App_Code;

namespace Lab5
{
    
    public partial class PropertyAssociator : System.Web.UI.UserControl
    {
        public GridViewUpdateEventHandler Updated { get; set; }

        public String AssociatePropertyButtonText { get { return mAssociatePropertyButtonText; } set { mAssociatePropertyButtonText = value; } }
        String mAssociatePropertyButtonText = "Add Property";

        public Boolean Editable { get { return mEditable; } set { mEditable = value; UpdateEditable(); } }
        Boolean mEditable = true;

        public Boolean AllowFieldEdit { get { return mAllowFieldEdit; } set { mAllowFieldEdit = value; } }
        Boolean mAllowFieldEdit = false;

        void UpdateEditable()
        {
            newProp.Visible = Editable;
        }

        public CalendarProperty Property
        {
            get
            {
                if (_property == null)
                {
                    _property = (CalendarProperty)ViewState["Property"];
                    if (_property == null)
                    {
                        _property = new CalendarProperty();
                    }
                }
                return _property;
            }
            set
            {
                ViewState["Property"] = _property = value;
            }
        }
        public Object CurrentUser
        { 
            get 
            {
                Object usr = Membership.GetUser();
                return ((usr != null) ? (((MembershipUser)usr).ProviderUserKey) : null); 
            } 
        }
        CalendarProperty _property = null;
        public ArrayList  AvailableProperties 
        {get
            {
                if (_availableProperties == null)
                {
                    _availableProperties = (ArrayList)ViewState["AvailableProperties"];
                    if (_availableProperties == null)
                    {
                        _availableProperties = new ArrayList();
                    }
                }
                return _availableProperties;
            }
            set
            {
                ViewState["AvailableProperties"] = _availableProperties = value;
                lstAssociatedProperties.DataSource = _availableProperties;
                lstAssociatedProperties.DataBind();
            }
        }
        ArrayList _availableProperties = null;
        ArrayList _associatedProperties;
        public ArrayList AssociatedProperties 
        {
            get
            {
                if (_associatedProperties == null)
                {
                    _associatedProperties = (ArrayList)ViewState["AssociatedProperties"];
                    if (_associatedProperties == null)
                    {
                        _associatedProperties = new ArrayList();
                    }
                }
                return _associatedProperties;
            }
            set
            {
                ViewState["AssociatedProperties"] = lstAssociatedProperties.DataSource = _associatedProperties = value;
                lstAssociatedProperties.DataBind();
            }
        }
        public void Schedule(CalendarEvent Event)
        {
            Property.Schedule(Event);
            //foreach (IPropertyAttribute i in Property.Attributes)
            //{
            //    i.Schedule(Event);
            //}
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Page.IsPostBack)
            {
                Property = uxProperty.Property;
                //Property.Name = uxPropertyName.SelectedItem.Text;
                //ViewState["PropertyAssociator"] = _property;
                //uxAssociateProperty.Text = mAssociatePropertyButtonText;
                //lstAssociatedProperties.DataSource = AssociatedProperties;
                //lstAssociatedProperties.DataBind();
                //uxProperty.DataSource = Property.Attributes;
                //uxProperty.DataBind();
            }
            else
            {
                Updated += ViewerUpdated;
                lstAssociatedProperties.DataSource = AssociatedProperties;
                lstAssociatedProperties.DataBind();
                uxAssociateProperty.Text = mAssociatePropertyButtonText;
                loadAvailableProperties();
                SetPropertyNames();
                LoadPropertyAttributeTemplate();
                newProp.Visible = Editable;
            }
            

        }
        protected void ViewerUpdated(object sender, GridViewUpdateEventArgs e)
        {
            PropertyViewer pv = sender as PropertyViewer;
            int Index = int.Parse(pv.Attributes["Index"]);
            AssociatedProperties[Index] = pv.Property;
            ViewState["AssociatedProperties"] = _associatedProperties;
            if(Updated != null)
                Updated(this, new GridViewUpdateEventArgs(Index));
        }
        void LoadPropertyAttributeTemplate()
        {
            //uxProperty.EditIndex = -1;
            if (AvailableProperties.Count > 0 && uxPropertyName.SelectedIndex < _availableProperties.Count && uxPropertyName.SelectedIndex >= 0)
            {
                Property = (CalendarProperty)_availableProperties[uxPropertyName.SelectedIndex];
                Property.loadProperty(_property.PropertyId);
                uxProperty.Property = Property;
                uxProperty.DataBind();
            }
        }
        void SetPropertyNames()
        {
            ArrayList arr;
            uxPropertyName.DataSource= arr = AvailableProperties;
            uxPropertyName.DataBind();
            if (!(uxPropertyName.Enabled = arr.Count > 0))
            {
                uxPropertyName.Items.Add(new ListItem("No Properties Available"));
            }
            else
            {
                Property = (CalendarProperty)_availableProperties[uxPropertyName.SelectedIndex];
            }
        }
        void loadAvailableProperties()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlSecurityDB"].ConnectionString);
            SqlCommand command = new SqlCommand("Property_SelectByCreator", connection);
            try
            {
                object t;
                command.Parameters.AddWithValue("Creator", CurrentUser);
                command.Parameters[0].IsNullable = true;
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                ArrayList arr = new ArrayList();
                while (reader.HasRows && reader.Read())
                {

                    arr.Add(  new CalendarProperty() { PropertyId = (long)reader.GetValue(0), Name = reader.GetString(1) } );
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
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
        protected void uxAssociateProperty_Click(object sender, EventArgs e)
        {
            CalendarProperty p = new CalendarProperty();
            p.loadProperty(long.Parse(uxPropertyName.SelectedValue));
            AssociatedProperties.Add(Property);
            ViewState["AssociatedProperties"] = _associatedProperties;
            lstAssociatedProperties.DataSource = _associatedProperties;
            lstAssociatedProperties.DataBind();
        }

        IPropertyAttribute GetNewAttr(String Type, String Name, String Value)
        {
            return CalendarProperty.GetNewAttr(Type, Name, Value);
        }
        protected void uxAddAttribute_Click(object sender, EventArgs e)
        {
            
        }
        protected void uxRemoveAttribute(int index)
        {
        }
        protected void GridViewRowDelete(int rowIndex)
        {
            
        }


        protected void uxAttribute_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            AssociatedProperties.RemoveAt(e.RowIndex);
            AssociatedProperties = _availableProperties;
        }
        protected void uxPropertyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPropertyAttributeTemplate();
        }

        protected void lstAssociatedProperties_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            PropertyViewer p = e.Item.FindControl("uxPropertyViewer") as PropertyViewer;
            p.Attributes.Add("Index", e.Item.ItemIndex.ToString());
            p.Property = e.Item.DataItem as CalendarProperty;
        }

        protected void lstAssociatedProperties_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    AssociatedProperties.RemoveAt(e.Item.ItemIndex);
                break;
            }
            lstAssociatedProperties.DataSource = AssociatedProperties;
            lstAssociatedProperties.DataBind();
            AssociatedProperties = _associatedProperties;
            //DataBind();
        }

        protected void uxProperty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           /* uxProperty.DataSource = Property.Attributes;
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "Edit":
                    //uxProperty.SetEditRow(index);
                break;
                case "Cancel":
                    uxProperty.EditIndex = -1;
                break;
            }
            //DataBind();
        */}

        protected void uxProperty_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //uxProperty.Rows[e.NewEditIndex].RowState = DataControlRowState.Edit;
            DataBind();
        }

        protected void lstAssociatedProperties_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            PropertyViewer p = e.Item.FindControl("uxPropertyViewer") as PropertyViewer;
            p.Updated += ViewerUpdated;
        }
    }
}