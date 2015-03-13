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
    public partial class PropertyCreator : System.Web.UI.UserControl
    {
        public Object CurrentUser
        {
            get
            {
                Object usr = Membership.GetUser();
                return ((usr != null) ? (((MembershipUser)usr).ProviderUserKey) : null);
            }
        }

        public Boolean IsPropertyEditor
        {
            get
            {
                String isEdit = uxToggleEditCreate.Attributes["IsEditMode"];
                return (isEdit == null) ? IsPropertyEditor = false : Boolean.Parse(isEdit);
            }
            set
            {
                String isEdit = uxToggleEditCreate.Attributes["IsEditMode"];
                if (isEdit == null)
                {
                    uxToggleEditCreate.Attributes.Add("IsEditMode", value.ToString());
                }
                else
                {
                    uxToggleEditCreate.Attributes["IsEditMode"] = value.ToString();
                }
            }
        }

        public CalendarProperty NewProperty
        {
            get
            {
                if (_newProperty == null)
                {
                    _newProperty = (CalendarProperty)ViewState["NewProperty"];
                    if (_newProperty == null)
                    {
                        _newProperty = new CalendarProperty();
                    }
                }
                return _newProperty;
            }
        }
        CalendarProperty _newProperty = null;

        public CalendarProperty CurrentProperty
        {
            get
            {
                if (_currentProperty == null)
                {
                    _currentProperty = (CalendarProperty)ViewState["CurrentProperty"];
                    if (_currentProperty == null)
                    {
                        _currentProperty = new CalendarProperty();
                    }
                }
                return _currentProperty;
            }
            set
            {
                ViewState["CurrentProperty"] = _currentProperty = value;
            }
        }
        CalendarProperty _currentProperty = null;

        public ArrayList AvailableProperties
        {
            get
            {
                if (_availableProperties == null)
                {
                    _availableProperties = (ArrayList)ViewState["AvailableProperties"];
                    if (_availableProperties == null)
                    {
                        _availableProperties = CalendarProperty.LoadAvailablePropertiesByUser((Guid)CurrentUser);
                    }
                }
                return _availableProperties;
            }
            set
            {
                ViewState["AvailableProperties"] = _availableProperties = value;
                uxExistingPropertyName.DataSource = _availableProperties;
                //lstAssociatedProperties.DataBind();
            }
        }
        ArrayList _availableProperties = null;
        public override void DataBind()
        {
            if (AvailableProperties.Count > 0)
            {
                uxExistingPropertyName.DataSource = AvailableProperties;
                uxExistingPropertyName.DataBind();
                uxToggleEditCreate.Enabled = true;
                int i = uxExistingPropertyName.SelectedIndex;
                base.DataBind();
                uxExistingPropertyName.SelectedIndex = i >= 0 ? (i < uxExistingPropertyName.Items.Count ? i : uxExistingPropertyName.Items.Count - 1) : 0;
            }
            else
            {
                uxToggleEditCreate.Attributes["IsEditMode"] = false.ToString();
                uxToggleEditCreate.Enabled = false;
                base.DataBind();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            EnableViewState = true;

        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (Page.IsPostBack)
            {

                //NewProperty.Name = uxPropertyName.Text;
                //ViewState["NewProperty"] = _newProperty;
            }
            else
            {
                UpdateAvailableProperties();
                //LoadSelectedExistingProperty();

                DataBind();

            }
        }
        protected void uxCreateProperty_Click(object sender, EventArgs e)
        {
            if (IsPropertyEditor)
            {
                CurrentProperty.CreateProperty();
            }
            else
            {
                NewProperty.Name = uxPropertyName.Text;
                _newProperty.CreateProperty();
            }
            UpdateAvailableProperties();
            DataBind();
        }
        protected void uxAddAttribute_Click(object sender, EventArgs e)
        {
            IPropertyAttribute tmp = null;
            if ((tmp = CalendarProperty.GetNewAttr(uxNewAttrType.SelectedValue, uxNewAttrName.Text, uxNewAttrData.Text)) != null)
            {
                if (IsPropertyEditor)
                {
                    CurrentProperty.AddAttribute(tmp);
                    ViewState["CurrentProperty"] = _currentProperty;
                }
                else
                {
                    NewProperty.AddAttribute(tmp);
                    ViewState["NewProperty"] = _newProperty;
                }
                //uxAttribute.DataSource = _newProperty.Attributes;
                DataBind();//uxAttribute.DataBind();
            }
        }
        protected void uxRemoveAttribute(int index)
        {
            if (IsPropertyEditor)
            {
                CurrentProperty.Attributes.RemoveAt(index);
                ViewState["CurrentProperty"] = _currentProperty;
            }
            else
            {
                NewProperty.Attributes.RemoveAt(index);
                ViewState["NewProperty"] = _newProperty;
            }
            DataBind();
        }
        protected void GridViewRowDelete(int rowIndex)
        {
            this.uxAttribute.EditIndex = -1;
            if (this._newProperty.Attributes.Count > 0 && rowIndex < this._newProperty.Attributes.Count && rowIndex >= 0)
            {
                object p = this._newProperty.Attributes[rowIndex];
                if (p != null)
                {
                    this._newProperty.Attributes.RemoveAt(rowIndex);
                    ViewState["NewProperty"] = _newProperty;
                    uxAttribute.DataSource = _newProperty.Attributes;
                    DataBind();//uxAttribute.DataBind();
                }
            }
        }

        protected void uxAttribute_RowEditing(object sender, GridViewEditEventArgs e)
        {
            uxAttribute.EditIndex = e.NewEditIndex;
            DataBind();//BindAttributeGrid();
            if (IsPropertyEditor)
            {
                GridViewRow row = uxAttribute.Rows[e.NewEditIndex];

                DropDownList type = row.FindControl("uxEditAttrType") as DropDownList;
                int i = 0;
                foreach (ListItem li in type.Items)
                {
                    if (li.Value == (CurrentProperty.Attributes[e.NewEditIndex] as IPropertyAttribute).Type.ToString())
                    {
                        type.SelectedIndex = i;
                        break;
                    }
                    ++i;
                }
            }
        }


        //       private void BindAttributeGrid()
        //       {
        //           uxAttribute.DataSource = ((CalendarProperty)ViewState["NewProperty"]).Attributes;
        //           uxAttribute.DataBind();
        //       }

        protected void uxAttribute_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            uxAttribute.EditIndex = -1;
            DataBind();//BindAttributeGrid();
        }

        protected void uxAttribute_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string NewName = (uxAttribute.Rows[e.RowIndex].FindControl("uxEditAttrName") as TextBox).Text;
            string NewType = (uxAttribute.Rows[e.RowIndex].FindControl("uxEditAttrType") as DropDownList).SelectedValue;
            string NewValue = (uxAttribute.Rows[e.RowIndex].FindControl("uxEditAttrValue") as TextBox).Text;
            if (IsPropertyEditor)
            {
                
                if ((CurrentProperty.Attributes[e.RowIndex] as IPropertyAttribute).Type.ToString() == NewType)
                {
                    (CurrentProperty.Attributes[e.RowIndex] as IPropertyAttribute).Name = NewName;
                    (CurrentProperty.Attributes[e.RowIndex] as IPropertyAttribute).Value = NewValue;
                }
                else
                {
                    CurrentProperty.Attributes.RemoveAt(e.RowIndex);
                    _currentProperty.Attributes.Insert(e.RowIndex, CalendarProperty.GetNewAttr(NewType, NewName, NewValue));
                }
                ViewState["CurrentProperty"] = _currentProperty;
            }
            else
            {
                NewProperty.Attributes.RemoveAt(e.RowIndex);
                _newProperty.Attributes.Insert(e.RowIndex, CalendarProperty.GetNewAttr(NewType, NewName, NewValue));
                ViewState["NewProperty"] = _newProperty;
            }
            uxAttribute.EditIndex = -1;
            DataBind();//BindAttributeGrid();
        }

        protected void uxAttribute_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (IsPropertyEditor)
            {
                CurrentProperty.Attributes.RemoveAt(e.RowIndex);
                ViewState["CurrentProperty"] = _currentProperty;
            }
            else
            {
                NewProperty.Attributes.RemoveAt(e.RowIndex);
                ViewState["NewProperty"] = _newProperty;
            }
            uxAttribute.EditIndex = -1;
            //BindAttributeGrid();
            DataBind();
        }

        protected void uxToggleEditCreate_Click(object sender, EventArgs e)
        {
            if (AvailableProperties.Count > 0)
            {
                IsPropertyEditor = !IsPropertyEditor;
                LoadSelectedExistingProperty();
            }
        }

        protected void uxToggleEditCreate_Init(object sender, EventArgs e)
        {
            uxToggleEditCreate.Attributes.Add("IsEditMode", IsPropertyEditor.ToString());
        }

        protected void uxExistingPropertyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelectedExistingProperty();
        }
        void UpdateAvailableProperties()
        {
            if (CurrentUser != null)
            {
                AvailableProperties = CalendarProperty.LoadAvailablePropertiesByUser((Guid)CurrentUser);
            }
            else
            {
                AvailableProperties = new ArrayList();
            }
            LoadSelectedExistingProperty();
        }
        void LoadSelectedExistingProperty()
        {
            int index = uxExistingPropertyName.SelectedIndex;

            if (CurrentUser != null && AvailableProperties.Count > 0)
            {
                uxExistingPropertyName.DataSource = AvailableProperties;
                uxExistingPropertyName.DataBind();
                CurrentProperty = (CalendarProperty)AvailableProperties[uxExistingPropertyName.SelectedIndex];
                uxToggleEditCreate.Enabled = true;
                _currentProperty.loadProperty(_currentProperty.PropertyId);
                CurrentProperty = _currentProperty;
            }else
            {
                IsPropertyEditor = false;
                uxToggleEditCreate.Enabled = false;
            }
            DataBind();
        }
        protected void uxDeleteProperty_Click(object sender, EventArgs e)
        {
            //CurrentProperty.Delete();
            UpdateAvailableProperties();
        }

        protected void uxAttribute_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowState == DataControlRowState.Edit)
            {
                DropDownList type = e.Row.FindControl("uxEditAttrType") as DropDownList;
                int i = 0;
                foreach (ListItem li in type.Items)
                {
                    if (li.Value == (e.Row.DataItem as IPropertyAttribute).Type.ToString())
                    {
                        type.SelectedIndex = i;
                        break;
                    }
                    ++i;
                }
            }
        }
    }
}