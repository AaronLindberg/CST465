<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="EventBuilder" Codebehind="EventBuilder.aspx.cs" %>
<asp:Content ContentPlaceHolderID="heading" runat="server">
    <h1>Event Builder</h1>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderId="body" runat="server">
    <div id="divEventBuilder">
    <asp:Label ID="lblEventName" AssociatedControlID="uxEventName" Text="Event Name" runat="server"></asp:Label>
    <asp:TextBox ID="uxEventName" TextMode="SingleLine" runat="server"></asp:TextBox>
        <asp:Label ID="lblScheduleDate" AssociatedControlID="uxScheduleDate" Text="Schedule Date" runat="server"></asp:Label>
    <asp:TextBox ID="uxScheduleDate" TextMode="DateTime" runat="server"></asp:TextBox>
    <div>
        <h2>Attributes</h2>
        <asp:Literal ID="literalAttributes" runat="server"></asp:Literal>
        <div class="attributeComp">
            <asp:Label ID="lblAttributeId" Text="Attribute Alias" AssociatedControlID="uxAttributeId" runat="server"></asp:Label>
            <br />
            <asp:TextBox ID="uxAttributeId" TextMode="SingleLine" runat="server"></asp:TextBox>
        </div>
        <div class="attributeComp">
            <asp:Label ID="lblDataType" AssociatedControlID="uxDataType" Text="Data Type" runat="server"></asp:Label>
            <br />
            <asp:DropdownList ID="uxDataType" runat="server" AutoPostBack="true">
                <asp:ListItem Text="String"></asp:ListItem>
                <asp:ListItem Text="int"></asp:ListItem>
                <asp:ListItem Text="DateTime"></asp:ListItem>
            </asp:DropdownList>
        </div>
        <div class="attributeComp">
            <asp:Label ID="lblData" AssociatedControlID="uxData" Text="Attribute Data" runat="server"></asp:Label>
            <br />
            <asp:TextBox ID="uxData" TextMode="MultiLine" runat="server"></asp:TextBox>
        </div>    
    </div>
    <br />
    <div class="submission">
        <asp:Button ID="uxAddAttribute" Text="Add Attribute" runat="server" OnClick="uxAddAttribute_Click" />
        <asp:Button ID="uxScheduleEvent" Text="ScheduleEvent" runat="server" />
    </div>
</div>
    
</asp:Content>