<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="Lab5.Account.EditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Edit Profile</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
    <h1>Edit Profile</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div>
        <fieldset>
            <legend>User Information</legend>
            <asp:Label AssociatedControlID="uxFirstName" Text="First Name" runat="server"></asp:Label>
            <asp:TextBox ID="uxFirstName" runat="server"></asp:TextBox>
            <br />
            <asp:Label AssociatedControlID="uxLastName" Text="Last Name" runat="server"></asp:Label>
            <asp:TextBox ID="uxLastName" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="uxSubmit" Text="Submit Changes" OnClick="uxSubmit_Click" runat="server" />
        </fieldset>
    </div>
</asp:Content>
