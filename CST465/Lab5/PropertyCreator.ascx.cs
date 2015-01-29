using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lab5.App_Code;
namespace Lab5
{
    public partial class PropertyCreator : System.Web.UI.UserControl
    {
        public CalendarProperty Property
        {
            get
            {
                if (_property == null)
                {
                    _property = (CalendarProperty)ViewState["NewProperty"];
                    if (_property == null)
                    {
                        _property = new CalendarProperty();
                    }
                }
                return _property;
            }
        }

        CalendarProperty _property = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                Property.Name = uxPropertyName.Text;
                ViewState["NewProperty"] = _property;
            }
            if (Request["__EVENTTARGET"] != null)
            {
                switch (Request["__EVENTTARGET"])
                {
                    case "GridViewRowDelete":
                        if (Request["__EVENTARGUMENT"] != null)
                        {
                            int index = -1;
                            if (int.TryParse(Request["__EVENTARGUMENT"], out index))
                            {
                                this.GridViewRowDelete(index);
                            }
                        }
                        break;
                }
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
        }
        private void uxCreateProperty_Click(object sender, EventArgs e)
        {
            Property.Name = uxPropertyName.Text;
            _property.CreateProperty();
        }

        IPropertyAttribute GetNewAttr()
        {
            IPropertyAttribute ret = null;
            switch (uxNewAttrType.SelectedValue)
            {
                case"String":
                    ret = new StringPropertyAttribute();
                    break;
                default:
                    break;
            }
            if (ret != null)
            {
                ret.Name = uxNewAttrName.Text;
                ret.Value = uxNewAttrData.Text;
            }
            return ret;
        }
        protected void uxAddAttribute_Click(object sender, EventArgs e)
        {
            IPropertyAttribute tmp = null;
            if ((tmp = GetNewAttr()) != null)
            {
                Property.AddAttribute(tmp);
                ViewState["NewProperty"] = _property;
                uxAttribute.DataSource = _property.Attributes;
                uxAttribute.DataBind();
            }
        }
        protected void uxRemoveAttribute(int index)
        {
            _property.Attributes.RemoveAt(index);
            ViewState["NewProperty"] = _property;
        }
        protected void GridViewRowDelete(int rowIndex)
        {
            this.uxAttribute.EditIndex = -1;

            if (this._property.Attributes.Count > 0 && rowIndex < this._property.Attributes.Count && rowIndex >= 0)
            {
                object p = this._property.Attributes[rowIndex];
                if (p != null)
                {
                    this._property.Attributes.RemoveAt(rowIndex);
                    ViewState["NewProperty"] = _property;
                    uxAttribute.DataSource = _property.Attributes;
                    uxAttribute.DataBind();
                }
            }
        }
    }
}