<%@ Control Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Controls.Controls_Comment"
    CodeBehind="Comment.ascx.cs" %>
<%@ Register TagPrefix="Springfield" TagName="BasicInfo" Src="~/Controls/BasicInfo.ascx" %>
<table style="width: 98%;" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <div style="display: none; float: none; padding: 15 0; margin: 15 0; background-color: #EFF8FF;
                height: 40; border: dashed 1px #B6C2D8; vertical-align: middle; text-align: center">
                <table style="height: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="padding: 0 10; height: 30;" onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                            onmouseout="this.style.borderWidth='0';">
                            <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/ProUI/images/addnote.png"
                                ID="btnAddComment" runat="server" AlternateText="Add Comment" OnClick="btnAddComment_Click"
                                ImageAlign="AbsMiddle" CausesValidation="true" ValidationGroup="comment" />
                            <label for="<%=btnAddComment.ClientID %>">
                                <span style="cursor: hand">Add</span></label>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td align="center">
            <div id="comment_title" class="panel_title_expand" style="margin: 15 0 0 0">
                Notes</div>
            <div class="panel_content" style="border-left: solid 1px #999999; border-right: solid 1px #999999">
                <div style="margin: 10 10; width: 100%;">
                    <asp:GridView ID="gvComments" runat="server" Style="width: 100%;" BorderWidth="0px"
                        BorderStyle="None" CssClass="applicant_list" AllowPaging="True" AutoGenerateColumns="False"
                        PageSize="5" OnPageIndexChanging="gvComments_PageIndexChanging" OnRowDataBound="gvComments_RowDataBound"
                        OnRowCommand="gvComments_RowCommand" ShowHeader="false">
                        <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table class="applicants_table" style="table-layout: fixed;">
                                        <tr>
                                            <td class="bold_font" width="12%">Reviewer: </td>
                                            <td width="20%;">
                                                <%# Eval("CommenterAlias") %>
                                            </td>
                                            <td class="bold_font" width="8%">Time: </td>
                                            <td>
                                                <%# Eval("CommentTime") %>&nbsp;&nbsp;&nbsp;
                                                <asp:ImageButton ImageUrl="~/Resource/Images/delete.png" Width="16" Height="16" AlternateText="delete note"
                                                    Visible="false" ImageAlign="AbsMiddle" OnClientClick="return ConfirmDeleteComment();"
                                                    CommandName="DeleteComment" runat="server" ID="btnDeleteComment" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="bold_font">Note: </td>
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
                        <EmptyDataTemplate>
                            <div style="text-align: center; padding: 10 0">
                                <p>There is no note here...</p>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>Note: </td>
                            <td valign="top">
                                <div style="padding: 5">
                                    <asp:TextBox ID="tbNewComment" runat="server" TextMode="MultiLine" Rows="6" Columns="56"></asp:TextBox>
                                </div>
                            </td>
                            <td valign="bottom" align="left" style="width: 90%; padding: 5">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="padding: 0 5; height: 30;" onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                                            onmouseout="this.style.borderWidth='0';">
                                            <asp:ImageButton Width="32" Height="32" CssClass="img_icon" ImageUrl="~/Resource/Images/addnote2.png"
                                                ID="btnAddComment2" runat="server" AlternateText="Add Note" OnClick="btnAddComment_Click"
                                                ImageAlign="AbsMiddle" CausesValidation="true" ValidationGroup="comment" />
                                            <label for="<%=btnAddComment2.ClientID %>">
                                                <span style="cursor: hand">Add Note</span></label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </td>
    </tr>
</table>
