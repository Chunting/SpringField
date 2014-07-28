<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="GeneralInfo" MasterPageFile="~/SpringfieldMaster.master" %>
<asp:Content ID="cntGeneralInfo" runat="server" ContentPlaceHolderID="mainPlaceHolder">
    <h5>Welcome to the Springfield(beta)</h5>
<p>
    With this tool, you can see the candidates who have applied for the internship program at Microsoft Research Asia. All resumes are collected by using the [Resume Collector] Tool and [Key In] Tool. You can also search for applicants, save a list of interesting applicants, and start the recruiting process for an applicant.
</p>

<div style="width:100%;position:relative">
<table width="100%" style="position:relative;line-height: 150%;">
<tr>
    <td style="vertical-align: top; width: 50%;">
        <div style="margin-right: 10px;">
            <div class="panel_title_expand">
                Weekly New Candidates
            </div>
            <div class="panel_content">
                <asp:Label ID="lbWeeklyNewCandidates" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </td>
    <td style="vertical-align: top;">
        <div style="margin-right: 10px; width: 100%;">
            <div id="ch_title" class="panel_title_expand">
                My To-Do List
            </div>
            
            <div id="ch_content" class="panel_content">
                <div>
                    <ul>
                        <li>
                        There are <asp:HyperLink ID="lnkApplicantCount" runat="server" NavigateUrl="~/Applicants.aspx"></asp:HyperLink> applications now.
                        </li>
                        <li>
                        There are <asp:HyperLink ID="lnkProcssing" runat="server" NavigateUrl="~/Procssing.aspx"></asp:HyperLink> applications on processing by you.
                        </li>
                        <%--<li>
                        There are <asp:HyperLink ID="lnkFeedbackList" runat="server" NavigateUrl="~/FeedbackList.aspx"></asp:HyperLink> feedback you should send.
                        </li>--%>
                    </ul>    
                    <asp:Panel runat="server" ID="pnlIRInfo" Visible="false" Width="100%">
                    <ul>
                        <li>
                        There are <asp:HyperLink ID="lnkDecision" runat="server" NavigateUrl="~/EmploymentDecision.aspx"></asp:HyperLink> rejection mail you should send.
                        </li>
                    </ul>
                    </asp:Panel>
                </div>
            </div>
        </div><br />
        <div style="margin-right: 10px;">
            <div id="Div1" class="panel_title_expand">
                Site Summary
            </div>
            <div id="Div2" class="panel_content">
                <div>
                    <ul>
                        <li>
                        Completed Interview Feedback: <asp:Label ID="lbCompleteFeedback" runat="server" Text=""></asp:Label>
                        </li>
                        <li>
                        Incompleted Interview Feedback: <asp:Label ID="lbIncompleteFeedback" runat="server" Text=""></asp:Label>
                        </li>
                        <li>
                        Hired Recruiting Process: <asp:Label ID="lbHiredInterview" runat="server" Text=""></asp:Label>
                        </li>
                        <li>
                        Rejected Recruiting Process: <asp:Label ID="lbRejectedInterview" runat="server" Text=""></asp:Label>
                        </li>
                        <li>
                        Completed Recruiting Process: <asp:Label ID="lbCompleteInterview" runat="server" Text=""></asp:Label>
                        </li>
                    </ul>    
                </div>
            </div>
        </div>
    </td>
</tr>
<tr>
    <td colspan="2">
        
    </td>
</tr>
<tr>
    <td colspan="2">
    
<h5>Quick Start Guide</h5>
<p>
The Intern Tools enables you to specify which applicants you want to see.
</p>
<table cellpadding="0" cellspacing="2" border="0">
    <tr>
        <td style="color: white; background : #cccccc; font-weight: bold; font-size: 10pt; padding: 2px;">
        To
        </td>
        <td style="color: white; background: #cccccc; font-weight: bold; font-size: 10pt; padding: 2px;">
        Do This
        </td>
    </tr>
    <tr>
        <td>
        See a list of all applicants
        </td>
        <td>
        Click Applications Pool.
        </td>
    </tr>
    <tr>
        <td>
        See a list of only those applicants who are being considered for an<br /> internship and for whom the interview process has started.
        </td>
        <td>
        Click Applications In Progress.
        </td>
    </tr>
    <tr>
        <td>
        See a list of applicants you&iexcl;&macr;ve saved to your interesting applicants list.
        </td>
        <td>
         Click Interesting Applicants.
        </td>
    </tr>
    <tr>
        <td>
        Search to see a list of applicants that match that search criteria.
        </td>
        <td>
        Click Search Applicants
        </td>
    </tr>
    <tr>
        <td>
        Complete the interview feedback assigned to you.
        </td>
        <td>
        Click Incomplete Feedback
        </td>
    </tr>
    <tr>
        <td>
        Upload resume for a off-line hiring applicant
        </td>
        <td>
        Click Off-line Hiring
        </td>
    </tr>
</table>
<p>
In the list of applications, you&iexcl;&macr;ll often click an icon to perform a specific action. Here are brief descriptions of the icons you&iexcl;&macr;ll see.
</p>
<table cellpadding="0" cellspacing="2" border="0">
    <tr>
        <td style="color: white; background : #cccccc; font-weight: bold; font-size: 10pt; padding: 2px;">
        Icon
        </td>
        <td style="color: white; background: #cccccc; font-weight: bold; font-size: 10pt; padding: 2px;">
        Description
        </td>
    </tr>
    <tr>
        <td>
        <img src="ProUI/images/addfavorite.gif" class="img_icon" />
        </td>
        <td>
         Add an applicant to your interesting applicant list.
        </td>
    </tr>
    <tr>
        <td>
        <img src="ProUI/images/interview.jpg" class="img_icon" />
        </td>
        <td>
         Request the start of the recruiting process.
        </td>
    </tr>
    <tr>
        <td>
        <img src="ProUI/images/delete.gif" class="img_icon" />
        </td>
        <td>
        Remove a applicant from your interesting applicant list.
        </td>
    </tr>
    <tr>
        <td>
        <img src="ProUI/images/recommend.gif" class="img_icon" />
        </td>
        <td>
        Recommend an application to a colleague.
        </td>
    </tr>
    <tr>
        <td>
        <img src="ProUI/images/addnote.png" class="img_icon" />
        </td>
        <td>
        Add comment to an applicant.
        </td>
    </tr>
</table>
<p>
There are some persona in Springfield's interview processing workflow.
</p>
<table cellpadding="0" cellspacing="2" border="0">
    <tr>
        <td style="color: white; background : #cccccc; font-weight: bold; font-size: 10pt; padding: 2px;">
        Persona
        </td>
        <td style="color: white; background: #cccccc; font-weight: bold; font-size: 10pt; padding: 2px;">
        Description
        </td>
    </tr>
    <tr>
        <td>
        Applicant
        </td>
        <td>
        Students who is applying the internship in MSRA.
        </td>
    </tr>
    <tr>
        <td>
        Mentor
        </td>
        <td>
        Employee who want to hire an applicant,<br />everyone who has scheduled a interview for one applicant will be set as a mentor.
        </td>
    </tr>
    <tr>
        <td>
        Interviewer
        </td>
        <td>
        Employee who has been assigned to do an interview. He should get log his feedback in the Springfield.
        </td>
    </tr>
    <tr>
        <td>
        Group Manager
        </td>
        <td>
        BOSS!! If Hiring Manager wants to hire an applicant, he should send the approval request to his boss.
        </td>
    </tr>
    <tr>
        <td>
        Intern Recruiter
        </td>
        <td>
        Take charge of upload applications and send out rejection emails,<br />Who is the administrator of the web site. (Wen Chen)
        </td>
    </tr>
</table>
<p>
Applicants in the Springfield may have several status in the system. His status may change during the whole recruiting process.
</p>
<table cellpadding="0" cellspacing="2" border="0">
    <tr>
        <td style="color: white; background : #cccccc; font-weight: bold; font-size: 10pt; padding: 2px;">
        Applicant Status
        </td>
        <td style="color: white; background: #cccccc; font-weight: bold; font-size: 10pt; padding: 2px;">
        Description
        </td>
    </tr>
    <tr>
        <td>
        Available
        </td>
        <td>
        After applicant submit his application and resumes into Springfield database.
        </td>
    </tr>
    <tr>
        <td>
        Interview Processing
        </td>
        <td>
        Applicant is in the recruiting process, it means somebody(Hiring manager)<br /> may have scheduled some interviews for this applicant.
        </td>
    </tr>
    <tr>
        <td>
        Waiting For Approval
        </td>
        <td>
        Hiring manager decide to hire the applicant, then he can submit a approval request<br /> through Springfield to Group Manager.
        </td>
    </tr>
    <tr>
        <td>
        Reject
        </td>
        <td>
        Group manager or Hiring manager decide to reject the applicant.
        </td>
    </tr>
    <tr>
        <td>
        Decline Offer
        </td>
        <td>
        Applicant decline the offer during the interview process.
        </td>
    </tr>
</table>
<h5>Get help</h5>
<ul>
    <li>
    For more help with using the Springfield, visit the Springfield <a href="help.htm">Help</a> page. 
    </li>
    <li>
    For questions about the Springfield or for problems using the Springfield, please contatct <a href="mailto:wenchen@microsoft.com">Wen Chen</a>. 
    </li>
</ul>
    </td>
</tr>
</table>
</div>

<h4>Notice</h4>
<p>
Currently, Springfield is still under testing. Both old resumes and new applications will be uploaded continually to the database.
 If any questions, please contact with <a href="mailto:wenchen@microsoft.com">Wen Chen</a>(ext.5357)
</p>

</asp:Content>
