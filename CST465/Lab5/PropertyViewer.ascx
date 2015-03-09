<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="~/PropertyViewer.ascx.cs" Inherits="Lab5.PropertyViewer" %>

<div>
    <asp:ScriptManagerProxy runat="server"></asp:ScriptManagerProxy>
    <fieldset>
        <legend><asp:Literal ID="ltrlPropertyName" Text='<%# Property.Name %>' runat="server"></asp:Literal></legend>
        <asp:GridView ID="uxProperty" DataSource="<%# Property.Attributes %>" AutoGenerateColumns="false" EnableViewState="true" OnRowEditing="uxAttribute_RowEditing" OnRowCancelingEdit="uxAttribute_RowCancelingEdit" OnRowDeleting="uxAttribute_RowDeleting" OnRowUpdating="uxAttribute_RowUpdating" DataKeyNames="Name,Type,Value" EnableSortingAndPagingCallbacks="true" runat="server">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>Name</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div><%# Eval("Name") %></div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <span id="Span1" visible="<%# AllowFieldEdit %>" enableviewstate="true" runat="server">
                            <div>
                                <asp:Textbox ID="uxEditAttrName" Text='<%# Bind("Name") %>' EnableViewState="true" runat="server"></asp:Textbox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" ControlToValidate="uxEditAttrName" ValidationGroup="AddNewPropAttribute" EnableClientScript="true" runat="server">
                                </asp:RequiredFieldValidator>
                            </div>
                        </span>
                        <span id="Span2" visible="<%# !AllowFieldEdit %>" enableviewstate="true" runat="server">
                             <div><%# Eval("Name") %></div>
                        </span>
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
                        <span visible="<%# AllowFieldEdit %>" enableviewstate="true" runat="server">
                            <div>
                                <asp:DropDownList ID="uxEditAttrType" ValidationGroup="UpdatePropAttribute" DataTextField='<%# Bind("Type") %>'  DataValueField='<%# Bind("Type") %>' EnableViewState="true" runat="server">
                                    <asp:ListItem Text="String"     Value="String"></asp:ListItem>
                                    <asp:ListItem Text="Integer"    Value="Integer"></asp:ListItem>
                                    <asp:ListItem Text="Decimal"    Value="Decimal"></asp:ListItem>
                                    <asp:ListItem Text="DateTime"   Value="DateTime"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </span>
                        <span visible="<%# !AllowFieldEdit %>" enableviewstate="true" runat="server">
                             <div><%# Eval("Type") %></div>
                        </span>
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
                        <asp:Button CommandName="Edit" Text="Edit" CausesValidation="false" UseSubmitBehavior="true" Visible='<%# Editable %>' runat="server" />
                        <asp:Button CommandName="Delete" Text="Remove"  CausesValidation="false" UseSubmitBehavior="true" Visible='<%# Editable && AllowFieldEdit %>' runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button CommandName="Update" Text="Update" CausesValidation="true" Visible='<%# Editable %>' ValidationGroup="UpdatePropAttribute" runat="server" />
                        <asp:Button CommandName="Cancel" Text="Cancel" CausesValidation="false" Visible='<%# Editable %>' runat="server" />
                        <asp:Button CommandName="Delete" Text="Remove"  CausesValidation="false" Visible='<%# Editable  && AllowFieldEdit %>' runat="server" />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </fieldset>
</div>