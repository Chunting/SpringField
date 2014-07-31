<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="MSRA.SpringField.Application.Controls.Controls_UploadingMentorSuggestion" Codebehind="UploadingMentorSuggestion.ascx.cs" %>
<%@ Register Src="CheckInFormView.ascx" TagName="CheckInFormView" TagPrefix="uc2" %>
<%@ Register Src="CheckInFormEdit.ascx" TagName="CheckInFormEdit" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script language="javascript" type="text/javascript">
function OpenArrangeInterview(applicantId)
{
    window.open('ArrangeInterview.aspx?applicant='+applicantId,null,'height=700,width=760,status=no,toolbar=no,menubar=no,location=no,resizable=no,scrollbars=yes');
}

</script>
<div id="Div2">
    <div class="panel_title_expand">
       Uploading Mentor Suggestion
    </div>
    <div class="panel_content">
        <table class="applicants_table" width="100%">
            <tr>
                <td style="width: 30%;">
                    Suggestion:
                </td>
                <td style="width: 424px;">
                    <asp:DropDownList ID="ddlHMSuggestion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlHMSuggestion_SelectedIndexChanged">
                        <asp:ListItem Text="Qualified But Not Matched" Value="2" Selected="True"></asp:ListItem> 
                        <asp:ListItem Text="Final Reject" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Decided to Hire" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Decline Offer" Value="0"></asp:ListItem>
                    </asp:DropDownList>&nbsp;
                    
                        </td>
            </tr>
            <asp:PlaceHolder ID="Panel2" runat="server">
            
                        <tr>
                <td>
                    Group Manager:
                </td>
                <td style="width: 424px">
                    <asp:DropDownList ID="ddlGroupManager" runat="server" >
                    </asp:DropDownList>
                </td>
            </tr>
            
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phUploadingControl" runat="server">
            <tr>
                <td>
                    Email File:
                </td>
                <td style="width: 424px">
                    <asp:FileUpload ID="fuMentorApproval" runat="server" />
                    <asp:RegularExpressionValidator ControlToValidate="fuMentorApproval" ValidationExpression="^(.*\.(((d|D)(o|O)(c|C)(x|X)?)|((p|P)(d|D)(f|F))|(t|T)(x|X)(t|T)))?$"
                            ID="revMentorApproval" runat="server" ErrorMessage="Only DOC,DOCX,PDF,TXT document is allowed!"></asp:RegularExpressionValidator>
                    <asp:Label ID="lbMentorApproval" ForeColor="red" runat="server"></asp:Label>
                </td>
            </tr>
            
            </asp:PlaceHolder>              
            <tr>
                <td>
                    Comment:
                </td>
                <td style="width: 424px">
                    <asp:TextBox ID="tbHMComment" runat="server" Columns="50" Rows="5" TextMode="MultiLine"
                       ></asp:TextBox>
                </td>
            </tr>
            
            <asp:PlaceHolder ID="Panel3" runat="server">
            <tr>
                <td colspan="2">
                    <uc1:CheckInFormEdit ID="CheckInFormEdit1" runat="server" />
                    <uc2:CheckInFormView ID="CheckInFormView1" runat="server" />
                </td>
            </tr>
            </asp:PlaceHolder>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button Width="120" ID="btnHMApproval" runat="server" Text="Submit" OnClick="btnHMApproval_Click" CausesValidation="false" OnClientClick="return ConfirmApprovalValidate('Applicant');"  />&nbsp;&nbsp;
                    <asp:Button ID="btnPreview" Width="120" runat="server" OnClick="btnPreview_Click" Text="Preview" ValidationGroup="Applicant" />&nbsp;&nbsp;
                    <asp:Button ID="btnBack" Width="120" runat="server" OnClick="btnBack_Click" Text="Back" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                     <!-- Add CheckBox by Yuanqin, 2011.3.10 -->                       
                     <asp:CheckBox ID="CheckBox1" runat="server" Text='Not send mail to GroupManager!' />
                </td>
            </tr>
        </table>
    </div>
</div>


