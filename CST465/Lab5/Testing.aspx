<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Testing.aspx.cs" Inherits="Lab5.Testing" %>
<%@ Register TagPrefix="lab" TagName="PropertyCreator" Src="~/PropertyCreator.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>My Testing Page</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
    <h1>Beware of Dragons</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <lab:PropertyCreator EnableViewState="true" ViewStateMode="Enabled" PropertyName="Facker" ID="uxTest" runat="server"></lab:PropertyCreator>
</asp:Content>
