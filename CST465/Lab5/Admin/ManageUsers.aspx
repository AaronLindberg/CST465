<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="Lab5.Admin.ManageUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Manage Users</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
    <h1>Manage Users</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div>
        <fieldset id="addNewRole" runat="server">
            <legend>Add Roles</legend>
            <label for="uxRoleName">Role Name</label>
            <asp:Textbox ID="uxRoleName" runat="server"/>
            <asp:Button ID="uxCreateRole" runat="server" Text="Create Role" OnClick="uxCreateRole_Click"></asp:Button>
        </fieldset>
        <fieldset id="membershipRole">
            <legend>Role Membership</legend>
            <asp:Label ID="lblUsers" AssociatedControlID="uxUsers" runat="server">Users</asp:Label>        
            <asp:DropDownList ID="uxUsers" runat="server" />
            <br />
            <asp:Label ID="lblRoles" AssociatedControlID="uxRoles" runat="server">Roles</asp:Label>
            <asp:DropDownList ID="uxRoles" runat="server" />
            <br />
            <asp:Button ID="uxAddUserToRole" ValidationGroup="UserInRole" Text="Add User To Role" OnClick="uxAddUserToRole_Click" runat="server" />
        </fieldset>
    </div>
</asp:Content>
