<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="True" CodeBehind="EventViewer.aspx.cs" Inherits="Lab5.Account.EventViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <b>Description: </b><asp:Literal ID="uxEventDescription" runat="server"></asp:Literal>
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
        
        <fieldset>
            <legend>Comments</legend>
            <asp:GridView DataSourceID="uxCommentDataSource" AutoGenerateColumns="false" runat="server" >
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            User
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div>
                                <%# Eval("UserName") %>
                                <br />
                                <%# Eval("TimeStamp") %>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Comment
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div>
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
                        <asp:LoginName ID="LoginName1" runat="server"/>
                        <br />
                        <asp:Label Text="Comment" AssociatedControlID="uxInsertComment" runat="server"></asp:Label>
                        <asp:TextBox ID="uxInsertComment" TextMode="MultiLine" runat="server"></asp:TextBox>
                        <asp:Button ID="uxSubmitComment" Text="Submit Comment" OnClick="uxSubmitComment_Click" runat="server" />
                    </div>
                </LoggedInTemplate>
            </asp:LoginView>
        </fieldset>
        
    </div>
</asp:Content>
