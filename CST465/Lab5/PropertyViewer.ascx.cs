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
    public partial class PropertyViewer : System.Web.UI.UserControl
    {
        public GridViewUpdateEventHandler Updated { get; set; }

        public Boolean Editable { get { return mEditable; } set { mEditable = value; } }
        Boolean mEditable = true;

        public Boolean AllowFieldEdit { get { return mAllowFieldEdit; } set { mAllowFieldEdit = value; } }
        Boolean mAllowFieldEdit = true;

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
                SetPropertyUi();
            }
        }
        void SetPropertyUi()
        {
            ltrlPropertyName.Text = Property.Name;
            uxProperty.DataSource = Property.Attributes;
            uxProperty.DataBind();
        }
        CalendarProperty _property = null;
        public Object CurrentUser
        { 
            get 
            {
                Object usr = Membership.GetUser();
                return ((usr != null) ? (((MembershipUser)usr).ProviderUserKey) : null); 
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

                //Property.Name = uxPropertyName.SelectedItem.Text;
                //ViewState["PropertyAssociator"] = _property;
                
                //uxProperty.DataSource = Property.Attributes;
                //uxProperty.DataBind();
            }
            else
            {
                
            }


        }
        
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
        
        IPropertyAttribute GetNewAttr(String Type, String Name, String Value)
        {
            IPropertyAttribute ret = null;
            switch (Type)
            {
                case"String":
                    ret = new StringPropertyAttribute();
                    break;
                default:
                    break;
            }
            if (ret != null)
            {
                ret.Name = Name;
                ret.Value = Value;
            }
            return ret;
        }

        protected void uxAttribute_RowEditing(object sender, GridViewEditEventArgs e)
        {
            uxProperty.DataSource = Property.Attributes;
            uxProperty.DataBind();
            //e.Cancel = false;
            uxProperty.EditIndex = e.NewEditIndex;
            DataBind();
        }

        protected void uxAttribute_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            uxProperty.DataSource = Property.Attributes;
            uxProperty.DataBind();
            e.Cancel = false;
            uxProperty.EditIndex = -1;
            DataBind();
        }

        protected void uxAttribute_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String Type = e.NewValues["Type"] as String;
            String Field = e.NewValues["Name"] as String;
            String Value = e.NewValues["Value"] as String;
            IPropertyAttribute a = Property.Attributes[e.RowIndex] as IPropertyAttribute;
            if (a.Type != CalendarProperty.GetType(Type))
            {
                _property.Attributes[e.RowIndex] = a = CalendarProperty.GetNewAttr(Type, Field, Value);
            }
            else
            {
                a.Name = Field;
                a.Value = Value;
            }
            ViewState["Property"] = _property;
            uxProperty.EditIndex = -1;
            if(Updated != null)
                Updated(this, e);
            DataBind();
        }

        protected void uxAttribute_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Property.Attributes.RemoveAt(e.RowIndex);
            Property = _property;
            uxProperty.EditIndex = -1;
            DataBind();
        }
    }
}