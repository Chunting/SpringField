<%@ Control Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Controls.Controls_ManagerResult" Codebehind="ManagerResult.ascx.cs" %>
<div id="Div1">
        <div class="panel_title_expand">
        Group Manager Result
        </div>
        <div class="panel_content">
        <table class="applicants_table" style="table-layout: fixed;">
            <tr>
                <td style="width: 30%;">
                    Time:
                </td>
                <td style="width: 70%;">
                    <asp:Label ID="lbGMDecisionTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%;">
                    Alias:
                </td>
                <td style="width: 70%;">
                    <asp:Label ID="lbGMAlias" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Suggestion:
                </td>
                <td>
                    <asp:Label ID="lbGMSuggestion" runat="server" Text=""></asp:Label>
                </td>
            </tr>
                        <tr id="tr_ApprovalEmail" runat="server" visible="false">
                <td>
                    Approval Email:
                </td>
                <td>
                <a href="#" id="link_download" runat="server">Download</a> 
                </td>
            </tr>
            <tr>
                <td>
                    Feedback:
                </td>
                <td style="line-height:150%;">
                  <div style="max-height: 500px; overflow: auto;">
                    <asp:Label ID="lbGMComment" runat="server" Text=""></asp:Label>
                  </div>
                </td>
            </tr>
        </table>
        </div>
        </div>
