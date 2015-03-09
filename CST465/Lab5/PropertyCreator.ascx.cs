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
            int i = uxExistingPropertyName.SelectedIndex;
            base.DataBind();
            uxExistingPropertyName.SelectedIndex = i >= 0 ? (i < uxExistingPropertyName.Items.Count ? i : uxExistingPropertyName.Items.Count - 1) : 0;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
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
            EnableViewState = true;

        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
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
        }

        IPropertyAttribute GetNewAttr(String Type, String Name, String Value)
        {
            IPropertyAttribute ret = null;
            switch (Type)
            {
                case "String":
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
            if (IsPropertyEditor)
            {
                if ((CurrentProperty.Attributes[e.RowIndex] as IPropertyAttribute).Type.ToString() == (String)e.NewValues["Type"])
                {
                    (CurrentProperty.Attributes[e.RowIndex] as IPropertyAttribute).Name = (String)e.NewValues["Name"];
                    (CurrentProperty.Attributes[e.RowIndex] as IPropertyAttribute).Value = (String)e.NewValues["Value"];
                }
                else
                {
                    CurrentProperty.Attributes.RemoveAt(e.RowIndex);
                    _currentProperty.Attributes.Insert(e.RowIndex, GetNewAttr((String)e.NewValues["Type"], (String)e.NewValues["Name"], (String)e.NewValues["Value"]));
                }
                ViewState["CurrentProperty"] = _currentProperty;
            }
            else
            {
                NewProperty.Attributes.RemoveAt(e.RowIndex);
                _newProperty.Attributes.Insert(e.RowIndex, GetNewAttr((String)e.NewValues["Type"], (String)e.NewValues["Name"], (String)e.NewValues["Value"]));
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
            IsPropertyEditor = !IsPropertyEditor;
            LoadSelectedExistingProperty();
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
            if (AvailableProperties.Count > 0 && uxExistingPropertyName.SelectedIndex < _availableProperties.Count && uxExistingPropertyName.SelectedIndex >= 0)
            {
                if (CurrentUser != null)
                {
                    CurrentProperty = (CalendarProperty)AvailableProperties[uxExistingPropertyName.SelectedIndex];
                }
                if (AvailableProperties.Count == 0)
                {
                    IsPropertyEditor = false;
                    uxToggleEditCreate.Enabled = false;
                }
                else
                {
                    uxToggleEditCreate.Enabled = true;
                    _currentProperty.loadProperty(_currentProperty.PropertyId);
                    CurrentProperty = _currentProperty;
                }

            }
            DataBind();
        }
        protected void uxDeleteProperty_Click(object sender, EventArgs e)
        {
            //CurrentProperty.Delete();
            UpdateAvailableProperties();
        }
    }
}