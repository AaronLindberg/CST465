<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PropertyCreator.ascx.cs" Inherits="Lab5.PropertyCreator" %>
<script runat="server">
    private void PropertyCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        String msgOut = "";
        args.IsValid = Property.validate(uxPropertyName.Text, out msgOut);
        (source as CustomValidator).ErrorMessage = msgOut;
    }
</script>
<script>
    function attributeDataValidation (source, args)
    {
        var tmp = $("#<%= uxNewAttrType.ClientID%>")[0].selectedIndex;
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
        return args.IsValid;
    }
    function deleteRowItem(source) {
        
        $("#<%= hidden_RowIndex.ClientID %>").val($(source).closest('tr').prevAll().length - 1);
        __doPostBack('GridViewRowDelete', $("#<%= hidden_RowIndex.ClientID %>").val());
    }
</script>
<div>
    <fieldset>
        <legend>Property Creator</legend>
        <asp:Label Text="Property Name" AssociatedControlID="uxPropertyName" runat="server"></asp:Label>
        <asp:TextBox Text="" ID="uxPropertyName" ValidationGroup="PropertyName" runat="server"></asp:TextBox>

        <asp:CustomValidator ID="PropertyNameCustomValidator" CssClass="validation" Display="Static" ControlToValidate="uxPropertyName" OnServerValidate="PropertyCustomValidator_ServerValidate" Text="*" ValidationGroup="PropertyName" runat="server"></asp:CustomValidator>
        <asp:RequiredFieldValidator CssClass="validation" ControlToValidate="uxPropertyName" EnableClientScript="true" Text="*" ErrorMessage="Property Name is required for creating a new property." runat="server"></asp:RequiredFieldValidator>
        <asp:ScriptManagerProxy runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="AttributesUpdatePanel" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hidden_RowIndex" runat="server" Value="0" />
                <asp:GridView ID="uxAttribute" AutoGenerateColumns="false" EnableViewState="true" DataKeyNames="Name,Type,Value" runat="server">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <div>Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div><%# Eval("Name") %></div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <div>Data Type</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div><%# Eval("Type") %></div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <div>Value</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div><%# Eval("Value") %></div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button OnClientClick="deleteRowItem(this)" Text="Remove"  CausesValidation="false" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Label AssociatedControlID="uxNewAttrName" Text="Attribute Name" runat="server"></asp:Label>
                <asp:TextBox ID="uxNewAttrName" ValidationGroup="AddNewPropAttribute" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator Text="*" ControlToValidate="uxNewAttrName" ValidationGroup="AddNewPropAttribute" EnableClientScript="true" runat="server">
                </asp:RequiredFieldValidator>
                <asp:Label AssociatedControlID="uxNewAttrType" Text="Attribute Type" runat="server"></asp:Label>
                <asp:DropDownList ID="uxNewAttrType" ValidationGroup="AddNewPropAttribute" runat="server">
                    <asp:ListItem Text="String"     Value="String"></asp:ListItem>
                    <asp:ListItem Text="Integer"    Value="Integer"></asp:ListItem>
                    <asp:ListItem Text="Decimal"    Value="Decimal"></asp:ListItem>
                    <asp:ListItem Text="DateTime"   Value="DateTime"></asp:ListItem>
                </asp:DropDownList>
                <asp:Label AssociatedControlID="uxNewAttrData" Text="Attribute Data" runat="server"></asp:Label>
                <asp:TextBox ID="uxNewAttrData" TextMode="MultiLine" ValidationGroup="AddNewPropAttribute" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator Text="*" ControlToValidate="uxNewAttrData" EnableClientScript="true" ValidationGroup="AddNewPropAttribute" runat="server"></asp:RequiredFieldValidator>
                <asp:CustomValidator ControlToValidate="uxNewAttrData" ClientValidationFunction="attributeDataValidation" ValidateEmptyText="true" EnableClientScript="true" Text="*" ValidationGroup="AddNewPropAttribute" runat ="server"></asp:CustomValidator>
                <asp:Button ID="uxAddAttribute" Text="Add Attribute" ValidationGroup="AddNewPropAttribute" OnClick="uxAddAttribute_Click" CausesValidation="true" runat="server"/>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="uxCreateProperty" Text="Create Property" ValidationGroup="PropertyName" runat="server" />
    </fieldset>
</div>