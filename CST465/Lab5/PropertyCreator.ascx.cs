using System;
using System.Collections;
using System.Collections.Generic;
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

            string scriptKey = "intoPopupMessage:" + this.UniqueID;

            if (!Page.ClientScript.IsStartupScriptRegistered(scriptKey) && !Page.IsPostBack)
            {
                string scriptBlock = @"<script language=""JavaScript"">
               <!--
                    function attributeDataValidation (source, args)
                    {
                        var tmp = $(""#%%DATA_TYPE%%"")[0].selectedIndex;
                        switch(tmp)
                        {
                            case 0:
                                console.log('String Validation.');
                                stringAttributeValidation(source, args);
                                break;
                            case 1:
                                console.log('Integer Validation.');
                                integerAttributeValidation(source, args);
                                break;
                            case 2:
                                console.log('Decimal Validation.');
                                decimalAttributeValidation(source, args);
                                break;
                            case 3:
                                console.log('DateTime Validation.');
                                eventDateValidation(source, args);
                            default:
                                console.log('unable to validate data.');
                                break;
                        }
                    }
                // -->
               </script>
                    ";
                scriptBlock.Replace("%%DATA_TYPE%%", uxNewAttrData.ClientID);
                Page.ClientScript.RegisterClientScriptBlock(GetType(),scriptKey, scriptBlock);
            }
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
    }
}