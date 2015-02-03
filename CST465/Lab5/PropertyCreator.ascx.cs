﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
            IPropertyAttribute tmp = null;
            if ((tmp = GetNewAttr(uxNewAttrType.SelectedValue, uxNewAttrName.Text, uxNewAttrData.Text)) != null)
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

        protected void uxAttribute_RowEditing(object sender, GridViewEditEventArgs e)
        {
            uxAttribute.EditIndex = e.NewEditIndex;
            BindAttributeGrid();
        }


        private void BindAttributeGrid()
        {
            uxAttribute.DataSource = ((CalendarProperty)ViewState["NewProperty"]).Attributes;
            uxAttribute.DataBind();
        }

        protected void uxAttribute_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            uxAttribute.EditIndex = -1;
            BindAttributeGrid();
        }

        protected void uxAttribute_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Property.Attributes.RemoveAt(e.RowIndex);
            _property.Attributes.Insert(e.RowIndex, GetNewAttr((String)e.NewValues["Type"],(String)e.NewValues["Name"], (String)e.NewValues["Value"]));
            ViewState["NewProperty"] = _property;
            uxAttribute.EditIndex = -1;
            BindAttributeGrid();
        }

        protected void uxAttribute_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Property.Attributes.RemoveAt(e.RowIndex);
            ViewState["NewProperty"] = _property;
            uxAttribute.EditIndex = -1;
            BindAttributeGrid();
        }
    }
}