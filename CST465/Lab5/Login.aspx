<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="Lab5.Login" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Login</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="heading" runat="server">
    <h1>Login</h1>
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <div>
        <asp:Login ID="uxLogin" MembershipProvider="SqlMembership" CreateUserText="Register" CreateUserUrl="~/Register.aspx" runat="server"></asp:Login>
    </div>
</asp:Content>
