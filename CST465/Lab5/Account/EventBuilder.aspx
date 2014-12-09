<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/MasterPage.master" Inherits="EventBuilder" Codebehind="EventBuilder.aspx.cs" %>
<asp:Content ContentPlaceHolderID="heading" runat="server">
    <h1>Event Builder</h1>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderId="body" runat="server">
    <asp:UpdatePanel ID="uxEventBuilderUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="divEventBuilder">
            <asp:Label ID="lblEventName" AssociatedControlID="uxEventName" Text="Event Name" runat="server"></asp:Label>
            <asp:TextBox ID="uxEventName" CausesValidation="true" TextMode="SingleLine" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator Text="*" ControlToValidate="uxEventName" CssClass="dateValidation" ValidationGroup="EventScheduling" ErrorMessage="The Event is Required to have a unique name for the time scheduled." runat="server"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ValidationExpression="(^\s)*" Text ="*" ControlToValidate="uxEventName" CssClass="dateValidation" ValidationGroup="EventScheduling" ErrorMessage="An Event name should be some what descriptive" runat="server"></asp:RegularExpressionValidator>
                <asp:Label ID="lblScheduleDate" AssociatedControlID="uxScheduleDate" Text="Schedule Date" runat="server"></asp:Label>
            <asp:TextBox ID="uxScheduleDate" TextMode="DateTime" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblEventDescription" AssociatedControlID="uxEventDescription" Text="Description" runat="server"></asp:Label>
            <br />
            <asp:TextBox ID="uxEventDescription" TextMode="MultiLine" runat="server"></asp:TextBox>
            <br />
            <div>
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
            </fieldset>
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
                        <asp:ListItem Text="Integer"></asp:ListItem>
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
                <asp:Button ID="uxScheduleEvent" Text="ScheduleEvent" OnClick="uxScheduleEvent_Click" runat="server" />
            </div>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>