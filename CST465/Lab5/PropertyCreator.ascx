<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="~/PropertyCreator.ascx.cs" Inherits="Lab5.PropertyCreator" %>
<script runat="server">
    private void PropertyCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        String msgOut = "";
        args.IsValid = NewProperty.validate(uxPropertyName.Text, out msgOut);
        (source as CustomValidator).ErrorMessage = msgOut;
    }
</script>
<script>
    function editAttributeDataValidation(source, args) {       
        var type = $(source).closest('tr').find('select')[0].val();
        console.log(type);
        attributeDataValidation(source, args, type);
        return args.IsValid;
    }

    function clientAttributeDataValidation (source, args)
    {
        var type = $("#<%= uxNewAttrType.ClientID%>").val();
        attributeDataValidation(source, args, type);
        return args.IsValid;
    }

    function deleteRowItem(source) {
        
        $("#<%= hidden_RowIndex.ClientID %>").val($(source).closest('tr').prevAll().length - 1);
        __doPostBack('GridViewRowDelete', $("#<%= hidden_RowIndex.ClientID %>").val());
    }
</script>
<div>
    <asp:ScriptManagerProxy runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="AttributesUpdatePanel" runat="server">
        <ContentTemplate>
            <fieldset>
                <legend>Property Manager <asp:Button ID="uxToggleEditCreate" OnInit="uxToggleEditCreate_Init" CausesValidation="false" OnClick="uxToggleEditCreate_Click" EnableViewState="true" Text='<%# IsPropertyEditor? "Add New":"Edit Existing" %>' runat="server"/></legend>
                <span id="creatorPropertyName" visible='<%# !Boolean.Parse(uxToggleEditCreate.Attributes["IsEditMode"])%>' runat="server">
                    <asp:Label Text="Property Name" AssociatedControlID="uxPropertyName" runat="server"></asp:Label>
                    <asp:TextBox Text="" ID="uxPropertyName" ValidationGroup="PropertyName" runat="server"></asp:TextBox>
                </span>
                <span visible='<%# Boolean.Parse(uxToggleEditCreate.Attributes["IsEditMode"])%>' runat="server">
                    <asp:Label ID="lblExistingPropertyName" Text="ExistingProperty Name" AssociatedControlID="uxExistingPropertyName" runat="server"></asp:Label>
                    <asp:DropDownList ID="uxExistingPropertyName" DataSource='<%# AvailableProperties %>' DataTextField="Name" DataValueField="PropertyId" OnSelectedIndexChanged="uxExistingPropertyName_SelectedIndexChanged" EnableViewState="true" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="uxChangeName" Text="Edit Name" EnableViewState="true" runat="server" />
                </span>
                <asp:CustomValidator ID="PropertyNameCustomValidator" CssClass="validation" Display="Static" ControlToValidate="uxPropertyName" OnServerValidate="PropertyCustomValidator_ServerValidate" Text="*" ValidationGroup="PropertyName" runat="server"></asp:CustomValidator>
                
                <asp:HiddenField ID="hidden_RowIndex" runat="server" Value="0" />
                <asp:GridView ID="uxAttribute"  AutoGenerateColumns="false" DataSource='<%# IsPropertyEditor?CurrentProperty.Attributes:NewProperty.Attributes %>' EnablePersistedSelection="true" EnableViewState="true" OnRowEditing="uxAttribute_RowEditing" OnRowCancelingEdit="uxAttribute_RowCancelingEdit" OnRowDeleting="uxAttribute_RowDeleting" OnRowUpdating="uxAttribute_RowUpdating" DataKeyNames="Name,Type,Value" runat="server">
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
                                    <asp:RequiredFieldValidator Text="*" ControlToValidate="uxEditAttrName" ValidationGroup="UpdatePropAttribute" EnableClientScript="true" runat="server">
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
                                <asp:DropDownList ID="uxEditAttrType" ValidationGroup="UpdatePropAttribute" EnableViewState="true" runat="server">
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
                                <div><asp:Textbox ID="uxEditAttrValue" Text='<%# Bind("Value") %>' runat="server" ValidationGroup="UpdatePropAttribute" CausesValidation="true" EnableViewState="true" ></asp:Textbox>
                                    <asp:CustomValidator ControlToValidate="uxEditAttrValue" ClientValidationFunction="clientAttributeDataValidation" ValidateEmptyText="true" EnableClientScript="true" Text="*" ValidationGroup="UpdatePropAttribute" runat ="server"></asp:CustomValidator>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button CommandName="Edit" Text="Edit" CausesValidation="false" UseSubmitBehavior="true" runat="server" />
                                <asp:Button CommandName="Delete" Text="Remove"  CausesValidation="false" UseSubmitBehavior="true" runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button CommandName="Update" Text="Update" CausesValidation="true" UseSubmitBehavior="true" ValidationGroup="UpdatePropAttribute" runat="server" />
                                <asp:Button CommandName="Cancel" Text="Cancel" CausesValidation="false" runat="server" />
                                <asp:Button CommandName="Delete" Text="Remove"  CausesValidation="false" UseSubmitBehavior="true" runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <fieldset>
                    <legend>Add an Attribute</legend>
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
                    <asp:CustomValidator ControlToValidate="uxNewAttrData" ClientValidationFunction="clientAttributeDataValidation" ValidateEmptyText="true" EnableClientScript="true" Text="*" ValidationGroup="AddNewPropAttribute" runat ="server"></asp:CustomValidator>
                    <asp:Button ID="uxAddAttribute" Text="Add Attribute" ValidationGroup="AddNewPropAttribute" OnClick="uxAddAttribute_Click" CausesValidation="true" UseSubmitBehavior="true" runat="server"/>
                </fieldset>
                <asp:Button ID="uxCreateProperty" Text='<%# Boolean.Parse(uxToggleEditCreate.Attributes["IsEditMode"])?"Update":"Create"%>' ValidationGroup="PropertyName" UseSubmitBehavior="true" OnClick="uxCreateProperty_Click" CausesValidation="true" runat="server" />
                <asp:Button ID="uxDeleteProperty" Text="Delete" Visible="<%# IsPropertyEditor %>" CausesValidation="false" UseSubmitBehavior="true" OnClick="uxDeleteProperty_Click" runat="server" />
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>