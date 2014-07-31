<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.GeneralInfo" MasterPageFile="~/SpringfieldMaster.master" Codebehind="Default.aspx.cs" %>
<asp:Content ID="cntGeneralInfo" runat="server" ContentPlaceHolderID="mainPlaceHolder">
    <h5>About Springfield</h5>
<p>
    With this tool, you can see the candidates who have applied for the internship program at Microsoft Research Asia. 
    All resumes are collected by using the 
    [Resume Collector] 
        Tool and [Key In] Tool. You can also search for applicants, save a list of interesting applicants, 
        and start the recruiting process for an applicant.
</p>

<div style="width:100%;position:relative">
<table style="width:100%;border:solid 0px #fff" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width:65%;padding-right:15px">
            <table width="100%" style="position:relative;line-height: 150%;" cellpadding="0" cellspacing="0">
   
    <tr>
    <td style="vertical-align: top; width: 50%;">
        <!--Weekly New Candidates-->
        <div style="margin-bottom:5px;margin-right: 5px;height:100%">
            <div style="border-bottom:solid 1px #888;margin-bottom:5px">
                <div id="Div3" style="margin-left:15px;font-weight:bold;color:#555333;line-height:20px"><span style="margin-bottom:5px">
                    Weekly New Candidates</span>
                </div>
            </div>
            <div style="margin-left:15px">
                <asp:Label ID="lbWeeklyNewCandidates" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
    <td style="vertical-align: top;">       
        <div style="margin-bottom:5px; margin-right: 5px;" id="sitesummary" runat="server" visible="false">
            <div style="border-bottom:solid 1px #888;">                
               <div id="Div1" style="margin-left:15px;font-weight:bold;color:#555333;line-height:20px"><span>
                    Site Summary</span>
                </div>                
            </div>            
            <div>
            <table cellpadding="0" cellspacing="0" border="0" style="line-height:22px;margin-top:10px;margin-left:8px">
                <tr>
                    <td><img src="ProUI/images/square.gif" style="margin:8 8" /></td>
                    <td valign="middle">Completed Interview Feedback: <asp:Label ID="lbCompleteFeedback" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td><img src="ProUI/images/square.gif" style="margin:8px;"/></td>
                    <td valign="middle">Incompleted Interview Feedback: <asp:Label ID="lbIncompleteFeedback" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td><img src="ProUI/images/square.gif" style="margin:8px;"/></td>
                    <td valign="middle">Hired Recruiting Process: <asp:Label ID="lbHiredInterview" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td><img src="ProUI/images/square.gif" style="margin:8px;"/></td>
                    <td valign="middle">Rejected Recruiting Process: <asp:Label ID="lbRejectedInterview" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td><img src="ProUI/images/square.gif" style="margin-right:8px;margin-left:8px" /></td>
                    <td valign="middle">Completed Recruiting Process: <asp:Label ID="lbCompleteInterview" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>                  
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
    </td>
</tr>
</table>
        </td>
        <td valign="top">
         
        <!--My To-Do List-->
        <div style="margin-bottom:5px;margin-right: 5px; width: 100%;">
            <div style="border-bottom:solid 1px #888;margin-bottom:5px">
                <div id="ch_title" style="margin-left:15px;font-weight:bold;color:#555333;line-height:20px"><span style="margin-bottom:5px">My To-Do List</span></div>
            </div>            
            <div id="ch_content">
                <div style="margin-top:3px;">
                <table cellpadding="0" cellspacing="0" border="0" style="line-height:22px;margin-top:10px;margin-left:8px">
                    <tr>
                        <td><img src="ProUI/images/square.gif" style="margin-right:8px;margin-left:8px" /></td>
                        <td><asp:HyperLink ID="lnkApplicantCount" runat="server" NavigateUrl="~/Applicants.aspx"></asp:HyperLink> applications now.</td>
                    </tr>
                    <tr>
                        <td><img src="ProUI/images/square.gif" style="margin-right:8px;margin-left:8px" /></td>
                        <td><asp:HyperLink ID="lnkProcssing" runat="server" NavigateUrl="~/Procssing.aspx"></asp:HyperLink> applications on processing by you.</td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlIRInfo" Visible="false" Width="100%">
                    <tr>
                        <td><img src="ProUI/images/square.gif" style="margin-right:8px;margin-left:8px" /></td>
                        <td>
                            
                                <asp:HyperLink ID="lnkDecision" runat="server" NavigateUrl="~/EmploymentDecision.aspx"></asp:HyperLink> rejection mail you should send.
                        </td>
                    </tr></asp:Panel>
                </table>                    
                </div>
            </div>
        </div>
        </td>
    </tr>
</table>

</div>

<%--<h4>Notice</h4>
<p>
Currently, Springfield is still under testing. Both old resumes and new applications will be uploaded continually to the database.
 If any questions, please contact with <a href="mailto:wenchen@microsoft.com">Wen Chen</a>(ext.5357)
</p>
--%>
</asp:Content>
