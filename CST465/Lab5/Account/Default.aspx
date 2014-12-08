<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/MasterPage.master" Inherits="_Default" Codebehind="~/Account/Default.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderId="head" runat="server">
    <title>My Planner Calander view</title>
</asp:Content>  
<asp:Content ContentPlaceHolderID="heading" runat="server">
    <h1>My Calendar</h1>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderId="body" runat="server">
    <asp:UpdatePanel ID="uxViewingDateUpdatePanel" runat="server" >
        <ContentTemplate>
            <asp:ScriptManagerProxy ID="uxScriptManagerProxy" runat="server" >
                <Scripts>
                    <asp:ScriptReference Path="~/JS/Calendar.js" />
                </Scripts>
            </asp:ScriptManagerProxy>
            <div id="divDate">
                <asp:Label AssociatedControlID="uxViewingDate" Text="Date"  runat="server"></asp:Label>
                <asp:TextBox ID="uxViewingDate" TextMode="SingleLine" CssClass="datePicker" runat="server" CausesValidation="true" ValidationGroup="ViewingDate"/>
                <asp:RequiredFieldValidator ID="uxViewingDate_RequiredFieldValidator" CssClass="dateValidation" ControlToValidate="uxViewingDate" Text="*" ErrorMessage="The Viewing Date is a Reqired Field." runat="server" ValidationGroup="ViewingDate" ></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="uxViewingDate_CustomValidator" CssClass="dateValidation" ControlToValidate="uxViewingDate" Text="*" runat="server" OnServerValidate="uxViewingDate_CustomValidator_ServerValidate" ValidationGroup="ViewingDate"></asp:CustomValidator>
                <br />
                <asp:ValidationSummary ID="uxViewingDate_ValidationSummary" CssClass="dateValidation" ValidationGroup="ViewingDate" runat="server" />
                <br />
                <asp:Button ID="uxAddEvent" Text="Add new Event" runat="server" OnClick="uxAddEvent_Click" />
                <br />
                <asp:Button ID="uxViewDate" Text="View Date" runat="server" OnClick="uxViewDate_Click" ValidationGroup="ViewingDate" CausesValidation="true"/>
                <br />
            </div>
            
            <span id="CalendarContainer">
                <h2 id='calanderHeading'><asp:Literal ID="uxCalendarHeading" runat="server"></asp:Literal></h2>
                <asp:Repeater ID='uxWeekDayNameRep' runat="server">
                    <HeaderTemplate>
                        <div class="week">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="dayOfWeek">
                            <h3 class="weekDayHeading"><%# Eval("Name") %></h3>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Repeater ID="uxWeekRepeater" runat="server">
                    <HeaderTemplate>
                        
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Repeater DataSource='<%# DataBinder.Eval(Container.DataItem, "WeekDays") %>' runat="server">
                            <HeaderTemplate>
                                <div class="week">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="<%# Eval("className") %>" onclick="dayClicked(event)" data-day="<%# Eval("Day") %>" >
                                    <b>(<%# Eval("Day") %>)</b>
                                    <asp:Repeater DataSource='<%# DataBinder.Eval(Container.DataItem, "EventMemories") %>' runat="server">
                                        <HeaderTemplate>
                                            <br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <b>* <%# Eval("Name") %></b>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>    
            </span>
        </ContentTemplate> 
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="uxCalendarUpdatePanel" UpdateMode="Always" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="uxViewDate" />
        </Triggers>
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div class="loadingContainer">
                        <asp:Image ID="Image1" CssClass="loadingImage" ImageUrl="https://lh6.googleusercontent.com/-SMD3BmmLm7A/U1qmn10oLTI/AAAAAAAAIv4/pDtIYjNZJyU/w1400-h960/hummingbird-animated.gif" runat="server" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>