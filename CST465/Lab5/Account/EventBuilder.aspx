<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/MasterPage.master" Inherits="EventBuilder" Codebehind="EventBuilder.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Event Builder</title>
    <style type="text/css">
        #<%=uxEventBuilderUpdatePanel.ClientID%> {
            background-color:transparent;
        }
        #<%=uxEventDescription.ClientID%> {
            margin-left:3.2em;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="heading" runat="server">
    <h1>Event Builder</h1>
    <script>
        $(document).ready(function()
        {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(initialize)
            prm.add_endRequest(initialize);
        });

        function initialize()
        {

        }

        function attributeDataValidation (source, args)
        {
            var tmp = $("#<%= uxDataType.ClientID %>")[0].selectedIndex;
            switch(tmp)
            {
                case 0:
                    console.log("String Validation.");
                    stringAttributeValidation(source, args);
                    break;
                case 1:
                    console.log("Integer Validation.");
                    integerAttributeValidation(source, args);
                    break;
                case 2:
                    console.log("Decimal Validation.");
                    decimalAttributeValidation(source, args);
                    break;
                case 3:
                    console.log("DateTime Validation.");
                    eventDateValidation(source, args);
                default:
                    console.log("unable to validate data.");
                    break;
            }
        }

        var StringAttributeMaxLength = 2048;
        function stringAttributeValidation(source, args) {
            args.IsValid = false;
            if (args.Value.trim().length <= 0) {
                source.errormessage = 'String attribute must contain some displayable non white-space characters.';
                args.IsValid = false;
            } else if (args.Value.trim().length > StringAttributeMaxLength) {
                source.errormessage = 'String attribute must not contain more than ' + StringAttributeMaxLength + ' characters.';
                args.IsValid = false;
            } else {
                args.IsValid = true;
            }
        }

        function integerAttributeValidation(source, args) {
            if (isNumber(args.Value)) {
                 args.IsValid = true;
            }
            else {
                source.errormessage = 'Integer attribute must be a base 10 whole number.';
                args.IsValid = false;
            }
        }

        var AttributeNameMaxLength = 50;
        function attributeNameValidation(source, args) {
            args.IsValid = false;
            if (args.Value.trim().length <= 0) {
                source.errormessage = 'Attribute Identifier must contain some displayable non white-space characters.';
                args.IsValid = false;
            } else if (args.Value.trim().length > AttributeNameMaxLength) {
                source.errormessage = 'Attribute Identifier must not contain more than ' + AttributeNameMaxLength + ' characters.';
                args.IsValid = false;
            } else {
                args.IsValid = true;
            }
        }

        var EventNameMaxLength = 50;
        function eventNameValidation(source, args) {
            args.IsValid = false;
            if (args.Value.trim().length <= 0) {
                source.errormessage = 'Event name identifier must contain some displayable non white-space characters.';
                args.IsValid = false;
            } else if (args.Value.trim().length > EventNameMaxLength) {
                source.errormessage = 'Event name identifier must not contain more than ' + EventNameMaxLength + ' characters.';
                args.IsValid = false;
            } else {
                args.IsValid = true;
            }
        }
        function decimalAttributeValidation(source, args) {
            if (isFloat(args.Value)) {
                args.IsValid = true;
            }
            else {
                source.errormessage = 'Float attribute must be decimal number.';
                args.IsValid = false;
            }
        }
        function isFloat(i) {
            var tmp = parseFloat(i)
            return !isNaN(tmp) && tmp == i;
        }

        function isNumber(i) { 
            var tmp = parseInt(i)
            return !isNaN(tmp) && tmp == i;
        }

        function eventDateValidation(source, args) {
            args.IsValid = true;
            var regex = new RegExp(/^\d{1,2}\/\d{1,2}\/\d{4} \d{1,2}:\d{1,2}:\d{1,2} [aApP]{1}[mM]{1}$/);
            var input = args.Value.trim();
            if (!regex.test(input)) {
                args.IsValid = false;
                source.errormessage = 'The date is incorrectly formatted, required to be in "M/d/yyyy h:m:s tt" format.';
            } else {
                console.log("Datetime validation regex pass")
                var tmp;
                var timeParts;
                var dateParts = input.split('/');
                if (dateParts.length != 3) {//validate number of date seperators
                    args.IsValid = false;
                    source.errormessage = 'The date is incorrectly formatted, exactly two "/" date seperators are required to be in M/d/yyyy h:m:s tt format.';
                } else {
                    tmp = dateParts[2].split(' ');
                    if (tmp.length != 3) {//validate the number of spaced components
                        args.IsValid = false;
                        source.errormessage = 'The date is incorrectly formatted, containing too many spaces " " whithin the date sperating the year date component from the time and the 12 hr time modifier from the time.';
                    } else {
                        dateParts[2] = tmp[0];//put the year into the date parts
                        timeParts = tmp[1].split(':');
                        if (timeParts.length != 3) {//validate number of time seperated components
                            args.IsValid = false;
                            source.errormessage = 'The time component of the date is incorrectly formatted, required to contain exactly two ":" time seperators seperating three numbers hours:minutes:seconds.';
                        } else {
                            //validate each date component
                            timeParts[timeParts.length] = tmp[2];
                            var dateErrorMsg = 'The date date component is required to have ';
                            if (!isNumber(dateParts[2])) {
                                args.IsValid = false;
                                dateErrorMsg += 'an integer value for the year component.'
                            } else {//year is determined to be a number
                                if (dateParts[2] < 1800 || dateParts[2] > 9999) {//validate year range
                                    args.IsValid = false;
                                    dateErrorMsg += 'a year value between 1800 and 9999.';
                                }
                                else {//validate Month
                                    if (!isNumber(dateParts[0])) {
                                        args.IsValid = false;
                                        dateErrorMsg += 'an integer value for the month component.'
                                    } else {//month is determined to be a number
                                        if (dateParts[0] < 1 || dateParts[0] > 12) {//validate month range
                                            args.IsValid = false;
                                            dateErrorMsg += 'a month value between 1 and 12.';
                                        }
                                        else {//validate day
                                            if (!isNumber(dateParts[1])) {
                                                args.IsValid = false;
                                                dateErrorMsg += 'an integer value for the day component.'
                                            } else {//day is determined to be a number
                                                var MaxDay = new Date(dateParts[2], dateParts[0], 0).getDate();
                                                if (dateParts[1] < 1 || dateParts[1] > MaxDay) {//validate day range
                                                    args.IsValid = false;
                                                    dateErrorMsg += 'a day value between 1 and ' + MaxDay + ' for the month of ' + dateParts[0];
                                                }//date component validated
                                            }
                                        }
                                    }
                                }
                            }//validate Time Component
                            var timeErrorMsg = 'The time component is to have ';
                            if (!args.IsValid) {
                                source.errormessage = dateErrorMsg + '. ';
                            }
                            else {
                                source.errormessage = '';
                            }
                            var timeErrorOccurred = false;
                            if (!isNumber(timeParts[0])) {//check if the rour is a number
                                timeErrorMsg += 'an integer value for the hour component'
                                timeErrorOccurred = true;
                            } else {//hour is determined to be a number
                                if (timeParts[0] < 1 || timeParts[0] > 12) {//validate day range
                                    timeErrorMsg += 'a hour value between 1 and 12';
                                    timeErrorOccurred = true;
                                }//hour time component validated
                            }
                            //validate minute
                            if (!isNumber(timeParts[1])) {//
                                if (timeErrorOccurred) {
                                    timeErrorMsg += ", ";
                                }
                                timeErrorMsg += 'an integer value for the minute component';
                                timeErrorOccurred = true;
                            } else {//minute is determined to be a number
                                if (timeParts[1] < 0 || timeParts[1] > 59) {//validate day range

                                    if (timeErrorOccurred) {
                                        timeErrorMsg += ", ";
                                    }
                                    timeErrorMsg += 'a minute value between 1 and 59';
                                    timeErrorOccurred = true;
                                }//date component validate
                            }
                            if (!isNumber(timeParts[2])) {//

                                if (timeErrorOccurred) {
                                    timeErrorMsg += ", ";
                                }
                                timeErrorMsg += 'an integer value for the seconds component';
                                timeErrorOccurred = true;
                            } else {//minute is determined to be a number
                                if (timeParts[2] < 0 || timeParts[2] > 59) {//validate day range
                                    if (timeErrorOccurred) {
                                        timeErrorMsg += ", ";
                                    }
                                    timeErrorMsg += 'a seconds value between 1 and 59';
                                    timeErrorOccurred = true;
                                }//date component validate
                            }

                            timeParts[3] = timeParts[3].toUpperCase();
                            if (timeParts[3] == 'AM' || timeParts[3] == 'PM') {

                            }
                            else {
                                if (timeErrorOccurred) {
                                    timeErrorMsg += ", ";
                                }
                                timeErrorMsg += 'a time modifier of the value AM or PM';
                                timeErrorOccurred = true;
                            }
                            if (timeErrorOccurred) {
                                args.IsValid = false;
                            
                                source.errormessage += timeErrorMsg + '.';
                            
                            }
                        }
                    }

                }
            }
        }
    </script>
</asp:Content>
    
<asp:Content ID="BodyContent" ContentPlaceHolderId="body" runat="server">
    <asp:UpdatePanel ID="uxEventBuilderUpdatePanel"  runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="uxAddAttribute" />
        </Triggers>
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div class="loadingEventBuildingContainer">
                        <center>
                            <asp:Image ID="Image1" CssClass="loadingEventBuilderImage" ImageUrl="~/Calendar-optimized.gif" runat="server" />
                        </center>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:ScriptManagerProxy runat="server">
                <Scripts>
                    <asp:ScriptReference Path="~/JS/jquery-2.1.1.min.js" />
                    <asp:ScriptReference Path="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.9/jquery.validate.min.js" />
                    <asp:ScriptReference Path="//code.jquery.com/ui/1.10.4/jquery-ui.js" />
                    <asp:ScriptReference Path="~/JS/Calendar.js" />
                </Scripts>
            </asp:ScriptManagerProxy>
            <div id="divEventBuilder">
                
                <asp:Label ID="lblEventName" AssociatedControlID="uxEventName" Text="Event Name" runat="server"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" EnableClientScript="true" ControlToValidate="uxEventName" CssClass="dateValidation" ValidationGroup="EventScheduling" ErrorMessage="The Event is Required to have a unique name for the time scheduled." runat="server"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomEventNameValidator" CssClass="dateValidation" Display="Dynamic" OnServerValidate="ExistingEvent_ServerValidate"
                            ClientValidationFunction="eventNameValidation" ValidationGroup="EventScheduling" ControlToValidate="uxEventName" 
                            Text="*" EnableClientScript="true" runat="server"></asp:CustomValidator>
                <asp:TextBox ID="uxEventName" CausesValidation="true" TextMode="SingleLine" runat="server"></asp:TextBox>
                <asp:Label ID="lblScheduleDate" AssociatedControlID="uxScheduleDate" Text="Schedule Date" runat="server"></asp:Label>
                <asp:CustomValidator CssClass="dateValidation" Display="Dynamic"
                            ClientValidationFunction="eventDateValidation" ValidationGroup="EventScheduling" ControlToValidate="uxScheduleDate" 
                            Text="*" EnableClientScript="true" OnServerValidate="ScheduleDate_ServerValidate" runat="server"></asp:CustomValidator>
                <asp:RegularExpressionValidator ValidationGroup="EventScheduling" CssClass="dateValidation" ControlToValidate="uxScheduleDate" ValidationExpression="^\d{1,2}/\d{1,2}/\d{4} \d{1,2}:\d{1,2}:\d{1,2} [aApP]{1}[mM]{1}" Text="*" ErrorMessage="The date needs to be in the format [1-12]/[1-31*]/[1800-9999] [1-12]:[0-60]:[0-60] [AM|PM]" runat="server"></asp:RegularExpressionValidator>
                <asp:TextBox ID="uxScheduleDate" TextMode="DateTime" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="lblEventDescription" AssociatedControlID="uxEventDescription" Text="Description" runat="server"></asp:Label>
                <br />
                <asp:TextBox ID="uxEventDescription" CssClass="eventBuilderMultiline" TextMode="MultiLine" runat="server"></asp:TextBox>
                <br />
                <div class="validationSummary">
                    <asp:ValidationSummary ValidationGroup="EventScheduling" CssClass="dateValidation" ShowValidationErrors="true" ValidateRequestMode="Enabled" DisplayMode="BulletList" ShowSummary="true" EnableClientScript="true" runat="server" />
                </div>
                <div  class="attributeFieldset">
                    <fieldset class="attributeFieldset">
                        <legend>Attributes</legend>
                        <asp:GridView ID="uxAttributeGrid" CssClass="attributeGrid" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div class="attributeGrid_Head">Attribute Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="attributeGrid"><%# Eval("Name") %></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div class="attributeGridHead">Attribute Type</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="attributeGrid"><%# Eval("Type") %></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div class="attributeGridHead">Attribute Value</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="attributeGrid"><%# Eval("Value") %></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="attributeComp">
                            <asp:Label ID="lblAttributeId" Text="Attribute Alias" AssociatedControlID="uxAttributeId" runat="server"></asp:Label>
                            <asp:CustomValidator CssClass="dateValidation" Display="Dynamic" ValidateEmptyText="true" OnServerValidate="ExistingEvent_ServerValidate"
                                ClientValidationFunction="attributeNameValidation" ValidationGroup="AttributeData" ControlToValidate="uxAttributeId" 
                                Text="*" EnableClientScript="true" runat="server"></asp:CustomValidator>
                            <br />
                            <asp:TextBox ID="uxAttributeId" TextMode="SingleLine" ValidationGroup="AttributeData" runat="server"></asp:TextBox>
                        </div>
                        <div class="attributeComp">
                            <asp:Label ID="lblDataType" AssociatedControlID="uxDataType" Text="Data Type" runat="server"></asp:Label>
                            <br />
                            <asp:DropdownList ID="uxDataType" runat="server">
                                <asp:ListItem Text="String" Value="String"></asp:ListItem>
                                <asp:ListItem Text="Integer" Value="Integer"></asp:ListItem>
                                <asp:ListItem Text="Decimal" Value="Decimal"></asp:ListItem>
                                <asp:ListItem Text="DateTime" Value="DateTime"></asp:ListItem>
                            </asp:DropdownList>
                        </div>
                        <div class="attributeComp">
                            <asp:Label ID="lblData" AssociatedControlID="uxData" Text="Attribute Data" runat="server"></asp:Label>
                            <asp:CustomValidator ID="uxData_CustomValidator" CssClass="dateValidation" Display="Dynamic"  ValidateEmptyText="true" OnServerValidate="uxData_CustomValidator_ServerValidate"
                                ClientValidationFunction="attributeDataValidation" ValidationGroup="AttributeData" ControlToValidate="uxData" 
                                Text="*" EnableClientScript="true" ErrorMessage="Error Message" runat="server"></asp:CustomValidator>
                        
                            <br />
                            <asp:TextBox ID="uxData" CssClass="eventBuilderMultiline" CausesValidation="true" TextMode="MultiLine" ValidationGroup="AttributeData" runat="server"></asp:TextBox>
                        </div>
                        <div class="validationSummary">
                            <asp:ValidationSummary ID="AttributeValidationSummary" EnableClientScript="true" HeaderText="The following are required to add the attribute:" ShowSummary="true" ShowModelStateErrors="true" ShowValidationErrors="true" ValidationGroup="AttributeData" CssClass="dateValidation" DisplayMode="BulletList" runat="server" />
                        </div>
                        <asp:Button ID="uxAddAttribute" Text="Add Attribute" ValidationGroup="AttributeData" CausesValidation="true" OnClick="uxAddAttribute_Click" CssClass="validationSummary" runat="server"  /> 
                    </fieldset>
                </div>
                <br />
                <div class="submission">
                    <asp:Button ID="uxScheduleEvent" Text="ScheduleEvent" ValidationGroup="EventScheduling" OnClick="uxScheduleEvent_Click" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>