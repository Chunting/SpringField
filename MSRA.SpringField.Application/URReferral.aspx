<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.URReferral" MasterPageFile="~/SpringfieldMaster.master" Codebehind="URReferral.aspx.cs" %>

<asp:Content ID="cntURReferral" runat="server" ContentPlaceHolderID="mainPlaceHolder">

<div>
<ul>
<li>
This page provides interface when UR Account Managers would like to recommend intern applicants to Intern Tool. 
</li>
<li>
After key in the applicants' email, an email invitation will be sent automatically to applicants, asking for his/her resume. 
</li>
<li>
Applicants referred from UR Referral Page will be recorded as UR Effort Candidates.
</li>
</ul>
<div id="ch_title" class="panel_title_expand">
UR Referral
</div>
<div id="ch_content" class="panel_content">

<table class="applicants_table">
    <tr>
        <td style="width: 26%;">
            Please add the referees' emails in the textbox, spliting the emails by ";". The
            referrees will receive the invitation emails to send their resumes to Intern Recruiter.<br />
        </td>
        <td>
            <asp:TextBox ID="tbReferree" runat="server"  Columns="50" Rows="20" TextMode="MultiLine" CssClass="required_input"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvAlias" runat="server" ControlToValidate="tbReferree" ErrorMessage="Required!"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnRefer" runat="server" Text="Send Invitation" OnClick="btnRefer_Click" CausesValidation="true"/>
        </td>
    </tr>
</table>

</div>
</div>

<br />
    
</asp:Content>