﻿<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/MasterPage.master" CodeBehind="Register.aspx.cs" Inherits="Lab5.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderId="head" runat="server">
    <title>My Planner Calander view</title>
</asp:Content> 

<asp:Content ID="BodyContent" ContentPlaceHolderId="body" runat="server">
    <h1>Get Registered</h1>
    <asp:CreateUserWizard ID="CreateUserWizard1" MembershipProvider="SqlMembership" ContinueDestinationPageUrl="~/Login.aspx" runat="server"></asp:CreateUserWizard>
</asp:Content>
