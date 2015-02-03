using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        
        public CalendarProperty Property
        {
            get
            {
                if (_property == null)
                {
                    _property = (CalendarProperty)ViewState["PropertyAssociator"];
                    if (_property == null)
                    {
                        _property = new CalendarProperty();
                    }
                }
                return _property;
            }
        }
        public Guid CurrentUser
        { get { return (Guid)Membership.GetUser().ProviderUserKey; } }
        CalendarProperty _property = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                Property.Name = uxPropertyName.Text;
                ViewState["PropertyAssociator"] = _property;
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
        protected void uxCreateProperty_Click(object sender, EventArgs e)
        {
            Property.Name = uxPropertyName.Text;
            _property.CreateProperty();
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
        protected void uxAddAttribute_Click(object sender, EventArgs e)
        {
        }
        protected void uxRemoveAttribute(int index)
        {
        }
        protected void GridViewRowDelete(int rowIndex)
        {
            
        }

        protected void uxAttribute_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }

        protected void uxAttribute_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        protected void uxAttribute_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        protected void uxAttribute_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
    }
}