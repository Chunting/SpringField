<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="MSRA.SpringField.Application.Controls.Controls_InterviewHistory" Codebehind="InterviewHistory.ascx.cs" %>
  
<div id="completed_history" style="margin:5 0">
    <div id="ch_title" class="panel_title_expand">Complete Interview History</div>
    <div id="ch_content" class="panel_content">
        <asp:DataList ID="dlInterviewHistory" runat="server" RepeatLayout="Flow" 
            onitemdatabound="dlInterviewHistory_ItemDataBound" 
            onitemcommand="dlInterviewHistory_ItemCommand">
            <ItemTemplate>
                <input type="hidden" name="FeedbackId" value='<%#Eval("FeedbackId") %>' />
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
                            Feedback:
                        </td>
                        <td style="line-height: 150%;">
                            <div style="max-height: 500px; overflow: auto;">
                            <%# ParseFeedback(Container.DataItem) %>
                            </div>
                        </td>
                    </tr>
                    <tr runat="server" id="trUpdateFeedback">
                        <td colspan="2" align="right" valign="top">
                            <div style="cursor:pointer;width:150;padding:1 1;margin:1 1" 
                            onclick="javascript:var obj = document.getElementById('fbEditor_'+'<%# DataBinder.Eval(Container.DataItem, "InterviewerAlias") %>'); obj.style.display = (obj.style.display == 'none' ? 'block' : 'none');">
                            <img alt="modify feedback" id="imgModifyFeedback"                                  
                                src="Resource/Images/update_feedback.png" align="absmiddle" width="22" height="22"/>                            
                            <label for="imgModifyFeedback">Update Feedback</label>
                            </div>
                            
                            <div id="fbEditor_<%# DataBinder.Eval(Container.DataItem, "InterviewerAlias") %>" style="width:100%;border:none 0px;text-align:center;display:none;vertical-align:top">                                
                                <div style="border:solid 0px;padding:3 3;margin:5 5">
                                    <asp:TextBox ID="tbFeedbackContent" runat="server" TextMode="MultiLine" Text='<%# Eval("FeedbackContent") %>' 
                                        Width="500" Height="120"></asp:TextBox>
                                    <span style="vertical-align:top; color:#ff0000">*</span>
                                </div>
                                <div style="padding:2 2;margin:5 5">
                                <asp:RequiredFieldValidator ControlToValidate="tbFeedbackContent"
                                        runat="server" ID="rfvFeedback" 
                                        ErrorMessage="Please input the feedback!" Display="Static"></asp:RequiredFieldValidator>
                                </div>
                                <div style="margin:0 5 6 5;text-align:center">
                                    <asp:ImageButton runat="server" ID="ibUpdate" 
                                        CommandName="UpdateFeedback" CommandArgument='<%# Eval("FeedbackId") %>' 
                                        ImageUrl="~/Resource/Images/submit.png" Width="24" Height="24" ImageAlign="Middle"/>
                                    <label runat="server" id="lblUpdate" style="cursor:pointer;vertical-align:middle">Submit</label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <img align="absmiddle" onclick="javascript:document.getElementById('fbEditor_'+'<%# DataBinder.Eval(Container.DataItem, "InterviewerAlias") %>').style.display='none';" 
                                    src="Resource/Images/cancel.png" width="22" style="cursor:pointer" height="22" alt="Cancel" id="imgCancel" />
                                    <label for="imgCancel" style="cursor:pointer" onclick="javascript:document.getElementById('fbEditor_'+'<%# DataBinder.Eval(Container.DataItem, "InterviewerAlias") %>').style.display='none';">Cancel</label>
                                </div>
                                <%--<asp:Button ID="btnAddFeedback" CommandName="UpdateFeedback" CommandArgument='<%# Eval("FeedbackId") %>' Width="120" runat="server" Text="Submit Feedback" CausesValidation="true" />--%><br />
                            </div>
                        </td>
                    </tr>
                    
                </table>
            </ItemTemplate>
        </asp:DataList>
    </div>
</div>
