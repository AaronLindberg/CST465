<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PropertyManager.aspx.cs" Inherits="Lab5.PropertyManager" %>
<%@ Register TagPrefix="uc" TagName="PropertyCreator" Src="~/PropertyCreator.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Property Manager</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
    <h1>Property Creator</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <uc:PropertyCreator ID="uxPropertyCreator" EnableViewState="true" runat="server" />
</asp:Content>
