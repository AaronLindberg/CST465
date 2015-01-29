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
                    DateValidation(source, args);
                default:
                    console.log("unable to validate data.");
                    break;
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
                            ClientValidationFunction="DateValidation" ValidationGroup="EventScheduling" ControlToValidate="uxScheduleDate" 
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