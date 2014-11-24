<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Lab5.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Register</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
    <h1>Get Registered</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div>
        <asp:CreateUserWizard ID="CreateUserWizard1" MembershipProvider="SqlMembership" ContinueDestinationPageUrl="~/Login.aspx" runat="server"></asp:CreateUserWizard>
    </div>
</asp:Content>
