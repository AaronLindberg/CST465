<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="~/PropertyAssociator.ascx.cs" Inherits="Lab5.PropertyAssociator" %>
<script runat="server">
    private void PropertyCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        String msgOut = "";
        args.IsValid = Property.validate(uxPropertyName.Text, out msgOut);
        (source as CustomValidator).ErrorMessage = msgOut;
    }
</script>
<script>
    function editAttributeDataValidation(source, args) {
       
        var type = $(source).closest('tr').find('select')[0].selectedIndex;
        console.log(type);
        TypeValidation(type, source, args);
        return args.IsValid;
    }
    function TypeValidation(type, source, args)
    {
        switch(type)
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
        return args.IsValid;
    }

    function deleteRowItem(source) {
        
        $("#<%= hidden_RowIndex.ClientID %>").val($(source).closest('tr').prevAll().length - 1);
        __doPostBack('GridViewRowDelete', $("#<%= hidden_RowIndex.ClientID %>").val());
    }
</script>
<div>
    <fieldset>
        <legend>Property Associator</legend>
        <asp:Label Text="Property Name" AssociatedControlID="uxPropertyName" runat="server"></asp:Label>
        <asp:SqlDataSource ID="dsPropertyNames" ConnectionString="<%$ ConnectionStrings:SqlSecurityDB %>" SelectCommand="Property_SelectByCreator" SelectCommandType="StoredProcedure" runat="server">
            <SelectParameters>
                <asp:Parameter Name="Creator" DbType="Guid" DefaultValue="" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:DropDownList ID="uxPropertyName" DataSourceID="dsPropertyNames" AutoPostBack="true" runat="server"></asp:DropDownList>

        <asp:CustomValidator ID="PropertyNameCustomValidator" CssClass="validation" Display="Static" ControlToValidate="uxPropertyName" OnServerValidate="PropertyCustomValidator_ServerValidate" Text="*" ValidationGroup="PropertyName" runat="server"></asp:CustomValidator>
        <asp:RequiredFieldValidator CssClass="validation" ControlToValidate="uxPropertyName" EnableClientScript="true" Text="*" ErrorMessage="Property Name is required for creating a new property." runat="server"></asp:RequiredFieldValidator>
        <asp:ScriptManagerProxy runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="AttributesUpdatePanel" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hidden_RowIndex" runat="server" Value="0" />
                
                <asp:GridView ID="uxProperty" AutoGenerateColumns="false" EnablePersistedSelection="true" EnableViewState="true" OnRowEditing="uxAttribute_RowEditing" OnRowCancelingEdit="uxAttribute_RowCancelingEdit" OnRowDeleting="uxAttribute_RowDeleting" OnRowUpdating="uxAttribute_RowUpdating" DataKeyNames="Name,Type,Value" runat="server">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <div>Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div><%# Eval("Name") %></div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div>
                                    <asp:Textbox ID="uxEditAttrName" Text='<%# Bind("Name") %>' EnableViewState="true" runat="server"></asp:Textbox>
                                    <asp:RequiredFieldValidator Text="*" ControlToValidate="uxEditAttrName" ValidationGroup="AddNewPropAttribute" EnableClientScript="true" runat="server">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <div>Data Type</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div><%# Eval("Type") %></div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="uxEditAttrType" ValidationGroup="UpdatePropAttribute" DataTextField='<%# Bind("Type") %>'  DataValueField='<%# Bind("Type") %>' EnableViewState="true" runat="server">
                                    <asp:ListItem Text="String"     Value="String"></asp:ListItem>
                                    <asp:ListItem Text="Integer"    Value="Integer"></asp:ListItem>
                                    <asp:ListItem Text="Decimal"    Value="Decimal"></asp:ListItem>
                                    <asp:ListItem Text="DateTime"   Value="DateTime"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <div>Value</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div><%# Eval("Value") %></div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div><asp:Textbox ID="uxEditAttrValue" Text='<%# Bind("Value") %>' runat="server" EnableViewState="true" ></asp:Textbox>
                                    <asp:CustomValidator ControlToValidate="uxEditAttrValue" ClientValidationFunction="editAttributeDataValidation" ValidateEmptyText="true" EnableClientScript="true" Text="*" ValidationGroup="UpdatePropAttribute" runat ="server"></asp:CustomValidator>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button CommandName="Edit" Text="Edit" CausesValidation="false" runat="server" />
                                <asp:Button CommandName="Delete" Text="Remove"  CausesValidation="false" runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button CommandName="Update" Text="Update" CausesValidation="true" ValidationGroup="UpdatePropAttribute" runat="server" />
                                <asp:Button CommandName="Cancel" Text="Cancel" CausesValidation="false" runat="server" />
                                <asp:Button CommandName="Delete" Text="Remove"  CausesValidation="false" runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            
        </asp:UpdatePanel>
        <asp:Button ID="uxCreateProperty" Text="Create Property" ValidationGroup="PropertyName" UseSubmitBehavior="true" OnClick="uxCreateProperty_Click" CausesValidation="true" runat="server" />
    </fieldset>
</div>