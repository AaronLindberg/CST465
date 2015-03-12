<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="True" CodeBehind="EventViewer.aspx.cs" Inherits="Lab5.Account.EventViewer" %>
<%@ Register TagPrefix="uwc" TagName="PropertyAssociator" Src="PropertyAssociator.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Event Viewer</title>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
    <h1>Event Viewer</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div>
        <fieldset>
            <legend><asp:Literal ID="uxEventName" runat="server"></asp:Literal></legend>
            <div>
                <b>Owner Username: </b>
                <asp:Literal ID="uxUsername" runat="server"></asp:Literal>
                <br />
            </div>
            <b>Schedule Date:</b>
            <asp:Literal ID="uxDateScheduled" runat="server"></asp:Literal>
            <br />
            <b>Description: </b><p class="eventDesc"><asp:Literal ID="uxEventDescription" runat="server"></asp:Literal></p>
            <br />
            <asp:Panel ID="AttributeFieldset" runat="server">
                <fieldset class="attributeFieldset">
                    <legend>Attributes</legend>
                    <asp:GridView ID="AttributeGrid" CssClass="attributeGrid" AutoGenerateColumns="false" runat="server">
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
            </asp:Panel>

            <asp:Panel ID="PropertyFieldset" Visible='<%# currentCalendarEvent.Properties.Count > 0 %>' runat="server">
                <fieldset>
                    <legend>Properties</legend>
                    <uwc:PropertyAssociator ID="uxProperties" AllowFieldEdit="false" Editable="false" EnableViewState="true" runat="server" />
                </fieldset>
            </asp:Panel>
        </fieldset>
        <asp:SqlDataSource ID="uxCommentDataSource" ConnectionString="<%$ ConnectionStrings:SqlSecurityDB%>" 
            SelectCommand="SELECT UserName, TimeStamp, Comment FROM EventComment JOIN aspnet_Users ON UserFK = UserId WHERE EventFK = @EventId;" 
            InsertCommand="INSERT INTO EventComment (UserFK, EventFK, Comment, TimeStamp) VALUES (@UserId, @EventId, @Comment, @TimeStamp);"
            runat="server">
            <InsertParameters>
                <asp:QueryStringParameter Name="UserId" QueryStringField="UserId" />
                <asp:QueryStringParameter Name="EventId" QueryStringField="EventId" />
                <asp:QueryStringParameter Name="Comment" QueryStringField="Comment" />
                <asp:QueryStringParameter Name="TimeStamp" QueryStringField="TimeStamp" />
            </InsertParameters>
            <SelectParameters>
                <asp:QueryStringParameter Name="EventId" QueryStringField="EventId"/>
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:ScriptManagerProxy runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/JS/jquery-2.1.1.min.js" />
            </Scripts>
        </asp:ScriptManagerProxy>
        <script>
            $(function () {
                $("#<%= uxViewComments.ClientID %>").on('click', function () {
                    if ($("#<%= uxViewComments.ClientID %>").is(':checked')) {
                        $("#commentsContainer").show();
                        console.log("show comments");
                    } else {
                        $("#commentsContainer").hide();
                        console.log("hide comments");
                    }
                });
            })
        </script>
        <fieldset>
            <legend>Comments<br /><asp:Label ID="lblViewComments" AssociatedControlID="uxViewComments" Text="View Comments" CssClass="viewComment" runat="server"></asp:Label><asp:CheckBox ID="uxViewComments" Checked="true" runat="server"/></legend>
            <span id="commentsContainer">
                <asp:GridView DataSourceID="uxCommentDataSource" CssClass="borderlessGrid" AutoGenerateColumns="false" runat="server" >
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                               <h2 class="commenter">User</h2>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="commenter">
                                    <%# Eval("UserName") %>
                                    <br />
                                    <%# Eval("TimeStamp") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <h2 class="comment">Comment</h2>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="comment">
                                    <%# Eval("Comment") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:LoginView ID="uxCommentLoginView" runat="server">
                    <AnonymousTemplate>
                        <div>Please Login to comment</div>
                    </AnonymousTemplate>
                    <LoggedInTemplate>
                        <div>
                            <asp:Label Text="Leave a Comment, " AssociatedControlID="uxInsertComment" runat="server"></asp:Label>
                            <b><asp:LoginName runat="server"/></b>
                            <br />
                            <asp:TextBox ID="uxInsertComment" TextMode="MultiLine" runat="server"></asp:TextBox>
                            <br />
                            <asp:Button ID="uxSubmitComment" Text="Submit Comment" OnClick="uxSubmitComment_Click" runat="server" />
                        </div>
                    </LoggedInTemplate>
                </asp:LoginView>
            </span>
        </fieldset>

    </div>
</asp:Content>
