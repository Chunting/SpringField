<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Comment.ascx.cs" Inherits="Controls_Comment" %>
<asp:GridView ID="gvComments" CssClass="applicants_table" runat="server" style="width: 100%;" AllowPaging="True" AutoGenerateColumns="False" PageSize="5" OnPageIndexChanging="gvComments_PageIndexChanging" OnRowDataBound="gvComments_RowDataBound" OnRowCommand="gvComments_RowCommand">
    <PagerSettings Mode="NumericFirstLast" />
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <table class="applicants_table" style="table-layout: fixed;">
                    <tr>
                        <td class="bold_font" width="12%">
                            Reviewer:
                        </td>
                        <td width="20%;">
                            <%# Eval("CommenterAlias") %>
                        </td>
                        <td class="bold_font" width="8%">
                            Time:
                        </td>
                        <td>
                            <%# Eval("CommentTime") %>&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ImageUrl="~/ProUI/images/delete.gif" AlternateText="delete comment" Visible="false" OnClientClick="return ConfirmDeleteComment();" CommandName="DeleteComment" runat="server" ID="btnDeleteComment" />
                        </td>
                    </tr>
                    <tr>
                        <td class="bold_font">
                            Comment:
                        </td>
                        <td colspan="3">
                             <div style="max-height: 500px; overflow: auto;">
                                <%# ParseCommentContent(Container.DataItem) %>
                            </div>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<div id="comment_title" class="panel_title_expand">
Click to Add Comment
</div>
<div id="comment_content" class="panel_content">
    <asp:TextBox ID="tbNewComment" runat="server" TextMode="MultiLine" Rows="6" Columns="56"></asp:TextBox>
    <asp:Button ID="btnAddComment" runat="server" Text="Add Comment" ValidationGroup="comment" CausesValidation="true" OnClick="btnAddComment_Click" />
    <asp:RequiredFieldValidator ID="rfvNewComment" runat="server" ErrorMessage="Required!" ControlToValidate="tbNewComment" ValidationGroup="comment"></asp:RequiredFieldValidator>
</div>