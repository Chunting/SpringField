<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InterviewHistory.ascx.cs"
    Inherits="Controls_InterviewHistory" %>
<div id="completed_history">
    <div id="ch_title" class="panel_title_expand">
        Complete Interview History
    </div>
    <div id="ch_content" class="panel_content">
        <asp:DataList ID="dlInterviewHistory" runat="server" RepeatLayout="Flow">
            <ItemTemplate>
                <table class="applicants_table" style="table-layout: fixed;">
                    <tr>
                        <td style="width: 30%;">
                            Time:
                        </td>
                        <td style="width: 70%">
                            <%# DataBinder.Eval(Container.DataItem, "InterviewDate") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Interviewer:
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "InterviewerAlias") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Suggestion:
                        </td>
                        <td>
                            <%# ParseSuggestion(Container.DataItem) %>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            Comment:
                        </td>
                        <td style="line-height: 150%;">
                            <div style="max-height: 500px; overflow: auto;">
                            <%# ParseFeedback(Container.DataItem) %>
                            </div>
                        </td>
                    </tr>
                    
                </table>
            </ItemTemplate>
        </asp:DataList>
    </div>
</div>
