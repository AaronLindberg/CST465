<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="MasterPage.master" Inherits="_Default" Codebehind="~/Default.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderId="head" runat="server">
    <title>My Planner Calander view</title>
</asp:Content>  
<asp:Content ID="BodyContent" ContentPlaceHolderId="body" runat="server">
    <div id="divDate">
        <div id="divMonth" class="dateComp">
            <asp:Label ID="lblMonth" Text="Month" runat="server" AssociatedControlID ="uxMonth"></asp:Label>
            <asp:RequiredFieldValidator ID="uxMonthRequiredFieldValidator"
                Text="*" ForeColor="Red" ErrorMessage="The Month is required for viewing the calander." 
                ControlToValidate="uxMonth" runat ="server"></asp:RequiredFieldValidator>
            <br />
            <asp:DropDownList ID="uxMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="updateDayRangeValidator">
                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                <asp:ListItem Text="Febuary" Value="2"></asp:ListItem>
                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                <asp:ListItem Text="December" Value="12"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div id="divDay" class="dateComp">
            <asp:Label ID="lblDay" Text="Day" runat="server" AssociatedControlID ="uxDay"></asp:Label>
            <asp:RequiredFieldValidator ID="uxDayRequiredFieldValidator"
                Text="*" ForeColor="Red" ErrorMessage="The day is required for viewing the calander." 
                ControlToValidate="uxDay" runat ="server"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="uxDayRangeValidator" Type="Integer"
                Text="*" ForeColor="Red" ErrorMessage="The day is required to be between 1 and 31 for viewing the calander in the month of January for the year 2014." 
                ControlToValidate="uxDay" runat ="server" MinimumValue="1" MaximumValue="31"></asp:RangeValidator>
            <br />
            <asp:TextBox ID="uxDay" runat="server" TextMode="Number" Text="1" />
            </div>
        <div id="divYear" class="dateComp">
            <asp:Label ID="lblYear" Text="Year" runat="server" AssociatedControlID ="uxYear"></asp:Label>
            <asp:RequiredFieldValidator ID="uxYearRequiredFieldValidator"
                Text="*" ForeColor="Red" ErrorMessage="The Year is required for viewing a calander month." 
                ControlToValidate="uxYear" runat ="server"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="uxYearRangeValidator" Type="Integer"
                Text="*" ForeColor="Red" ErrorMessage="The Year must be an integer between 1 and 9999"
                ControlToValidate="uxYear" runat="server" MinimumValue="1" MaximumValue="9999"></asp:RangeValidator>
            <asp:RegularExpressionValidator ID="uxYearRegexValidator"
                Text="*" ForeColor="Red" ErrorMessage="The Year must be an integer between 1 and 9999"
                ControlToValidate="uxYear" runat="server" ValidationExpression="\d{1,4}"></asp:RegularExpressionValidator>
            <br />
            <asp:TextBox AutoPostBack="true" ID="uxYear" runat="server" TextMode="Number" Text="2014" OnTextChanged="updateDayRangeValidator"/>
        </div>
        <br />
        <asp:Button ID="uxAddEvent" Text="Add new Event" runat="server" OnClick="uxAddEvent_Click" />
        <br />
        <asp:Button ID="uxViewDate" Text="View Date" runat="server" OnClick="uxViewDate_Click"/>
        
    </div>
    <br />
    <asp:ValidationSummary ID="dateValidationSummary"
    HeaderText="You must enter a value in the following fields:"
    DisplayMode="BulletList"
    EnableClientScript="true"
    runat="server"/>
    <asp:Literal ID="literalCalander" runat="server"></asp:Literal>
    
</asp:Content>