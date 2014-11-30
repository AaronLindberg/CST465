<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ViewProfile.aspx.cs" Inherits="Lab5.Account.ViewProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>View My Profile</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
    <h1>View My Profile</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div>
        <fieldset>
            <legend>User Information</legend>
            <b>First Name:</b>
            <asp:Literal ID="literalFirstname" runat="server"></asp:Literal>
            <br />
            <b>Last Name:</b>
            <asp:Literal ID="literalLastname" runat="server"></asp:Literal>
            <br />
            <asp:Button ID="uxEditProfile" Text="Edit Profile Information" OnClick="uxEditProfile_Click" runat="server" />
        </fieldset>
    </div>
</asp:Content>
