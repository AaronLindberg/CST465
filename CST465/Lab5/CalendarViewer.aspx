<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/MasterPage.master" Inherits="CalendarViewer" Codebehind="~/CalendarViewer.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderId="head" runat="server">
    <title>My Planner Calander view</title>
</asp:Content>  
<asp:Content ContentPlaceHolderID="heading" runat="server">
    <h1>My Calendar</h1>
    <script>
        function validateDate(sender, args) {
            var date = args.Value.split("/");
            var month = date[0];
            var year = date[2];
            var day = date[1];
            args.IsValid = true;
            if (month < 1 || month > 12) {
                args.IsValid = false;
                sender.errormessage = "The month value of the date needs to be between 1 and 12.";
            } else if (year < 1800 || year > 9999) {
                args.IsValid = false;
                sender.errormessage = "The year value of the date needs to be between 1800 and 9999.";
            } else {
                var lastDay = new Date(year, month, 0);
                if (day > lastDay.getDate() || day < 1) {
                    args.IsValid = false;
                    sender.errormessage = "The day value of the date needs to be between 1 and " + lastDay.getDate() + ".";
                }
            }
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(DocumentLoad);
                
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderId="body" runat="server">
    <asp:UpdatePanel ID="uxViewingDateUpdatePanel" RenderMode="Block" runat="server" >
            <ContentTemplate>
            <asp:ScriptManagerProxy ID="uxScriptManagerProxy" runat="server" >
                <Scripts>
                    <asp:ScriptReference Path="~/JS/Calendar.js" />
                </Scripts>
            </asp:ScriptManagerProxy>
            <div id="divDate">
                <asp:Label AssociatedControlID="uxViewingDate" Text="Date" CssClass="ViewDate"  runat="server"></asp:Label>
                <asp:TextBox ID="uxViewingDate" TextMode="SingleLine" CssClass="ViewDate" runat="server" CausesValidation="true" ValidationGroup="ViewingDate"/>
                
                <asp:RequiredFieldValidator ID="uxViewingDate_RequiredFieldValidator" CssClass="dateValidation" ControlToValidate="uxViewingDate" Text="*" ErrorMessage="The Viewing Date is a Reqired Field." runat="server" ValidationGroup="ViewingDate" ></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="uxViewingDate_CustomValidator" CssClass="dateValidation" ControlToValidate="uxViewingDate" Text="*" runat="server" OnServerValidate="uxViewingDate_CustomValidator_ServerValidate" ClientValidationFunction="validateDate" Display="Dynamic" EnableClientScript="True" ValidationGroup="ViewingDate"></asp:CustomValidator>
                
                <asp:Button ID="uxViewDate" CssClass="ViewDate" Text="View Date" runat="server" OnClick="uxViewDate_Click" ValidationGroup="ViewingDate" CausesValidation="true"/>
                <asp:Label Text="Show Events for All Days" AssociatedControlID="uxShowAllEvents" CssClass="showAllEvents" runat="server"></asp:Label>
                <asp:CheckBox ID="uxShowAllEvents" Checked="false" CssClass="showAllEvents" runat="server" />
                <asp:Button ID="uxAddEvent" CssClass="AddEvent" Text="Add new Event" runat="server" OnClick="uxAddEvent_Click" />
                <br />
                <asp:ValidationSummary ID="uxViewingDate_ValidationSummary" CssClass="dateValidation" ValidationGroup="ViewingDate" runat="server" />
                <br />
            </div>
            <span id="CalendarContainer" hidden="hidden">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="loadingContainer">
                            <asp:Image ID="Image1" CssClass="loadingImage" ImageUrl="~/Calendar-optimized.gif" runat="server" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <center>
                    <h2 id='calanderHeading'><asp:Literal ID="uxCalendarHeading" runat="server"></asp:Literal></h2>
                </center>
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
                                <div class="<%# Eval("className") %>" data-day="<%# Eval("Day") %>" >
                                    <span <%# Eval("HiddenDay") %>>
                                        <b>(<%# Eval("Day") %>)</b>
                                        <br />
                                        <span class="EventVisibility" <%# Eval("HiddenEvents") %> >
                                            <asp:Repeater DataSource='<%# DataBinder.Eval(Container.DataItem, "EventMemories") %>' runat="server">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="calendarEvent" data-event-id="<%# Eval("ID") %>">
                                                    <a href="EventViewer.aspx/?eventId=<%# Eval("ID") %>"><b data-event-id="<%# Eval("ID") %>">* <%# Eval("Name") %></b></a>
                                                    </div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    
                                                </FooterTemplate>
                                            </asp:Repeater> 
                                        </span>
                                    </span>
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
</asp:Content>