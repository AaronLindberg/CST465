<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="True" CodeBehind="EventViewer.aspx.cs" Inherits="Lab5.Account.EventViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
    <h1>Event Viewer</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div>
        <fieldset>
            <legend><asp:Literal ID="uxEventName" runat="server"></asp:Literal></legend>
            <div>
                <b>Owner Username: </b>
                <asp:Literal ID="uxUsername" runat="server"></asp:Literal>
                <br />
            </div>
            <b>Schedule Date:</b>
            <asp:Literal ID="uxDateScheduled" runat="server"></asp:Literal>
            <br />
            <b>Description: </b><asp:Literal ID="uxEventDescription" runat="server"></asp:Literal>
            <fieldset class="attributeFieldset">
            <legend>Attributes</legend>
            <asp:GridView ID="AttributeGrid" CssClass="attributeGrid" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <div class="attributeGrid_Head">Attribute Name</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="attributeGrid"><%# Eval("Name") %></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <div class="attributeGridHead">Attribute Type</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="attributeGrid"><%# Eval("Type") %></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <div class="attributeGridHead">Attribute Value</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="attributeGrid"><%# Eval("Value") %></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </fieldset>
        </fieldset>
    </div>
</asp:Content>
