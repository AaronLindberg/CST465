<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="~/PropertyAssociator.ascx.cs" Inherits="Lab5.PropertyAssociator" %>
<%@ Register TagPrefix="uwc" TagName="PropertyViewer" Src="PropertyViewer.ascx" %>
<script runat="server">
    private void PropertyCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        String msgOut = "";
        args.IsValid = Property.validate(uxPropertyName.Text, out msgOut);
        (source as CustomValidator).ErrorMessage = msgOut;
    }
</script>
<div>
    <asp:ScriptManagerProxy runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="AttributesUpdatePanel" EnableViewState="true" runat="server">
        <ContentTemplate>
            <div style="float:left;clear:left;">
                <asp:Repeater ID="lstAssociatedProperties" ViewStateMode="Enabled" OnItemCreated="lstAssociatedProperties_ItemCreated" OnItemDataBound="lstAssociatedProperties_ItemDataBound" DataSource="<%# AssociatedProperties %>" OnItemCommand="lstAssociatedProperties_ItemCommand" EnableViewState="true" runat="server">
                    <ItemTemplate>
                        <div style="float:left;clear:left;">
                            <div style="float:left;">
                                <uwc:PropertyViewer ID="uxPropertyViewer" Editable="<%# Editable %>" AllowFieldEdit="<%# AllowFieldEdit %>" EnableViewState="true" runat="server" />
                            </div>
                            <div style="float:right;">
                                <asp:Button ID="uxRemoveProperty" UseSubmitBehavior="true" ValidationGroup="None" EnableViewState="true" Visible="<%# Editable %>" CommandName="Delete" Text="Remove" runat="server"/>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <span id="newProp" visible='<%# Editable %>' enableviewstate="true" runat="server">
                <div style="float:left;clear:left;">
                    <asp:HiddenField ID="hidden_RowIndex" runat="server" Value="0" />
                    <asp:Label Text="Property Name" EnableViewState="true" AssociatedControlID="uxPropertyName" runat="server"></asp:Label>
                    <asp:DropDownList ID="uxPropertyName" DataTextField="Name" DataValueField="PropertyId" OnSelectedIndexChanged="uxPropertyName_SelectedIndexChanged" EnableViewState="true" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
            
                    <uwc:PropertyViewer AllowFieldEdit="false" ID="uxProperty" EnableViewState="true" runat="server" />
                    <asp:Button ID="uxAssociateProperty" Text='<%# Eval("AssociatePropertyButtonText") %>' ValidationGroup="PropertyName" UseSubmitBehavior="true" EnableViewState="true" OnClick="uxAssociateProperty_Click" CausesValidation="true" runat="server" />
                </div>
            </span>
        </ContentTemplate>   
        </asp:UpdatePanel>
</div>