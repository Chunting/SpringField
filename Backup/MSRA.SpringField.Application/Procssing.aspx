<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Procssing" MasterPageFile="~/SpringfieldMaster.master" Codebehind="Procssing.aspx.cs" %>
<%@ Register Src="Controls/ApplicantsList.ascx" TagName="ApplicantsList" TagPrefix="uc1" %>
<asp:Content ID="ctntInterviewList" runat="server" ContentPlaceHolderID="mainPlaceHolder">
<div style="width: 100%;">
<script type="text/javascript">
    function SwitchTitleBar() {
        document.getElementById('notionbar').style.display = 
            document.getElementById('notionbar').style.display == 'none' ? 'block' : 'none';

        if (document.getElementById('notionbar').style.display == 'block') {
            //document.getElementById('titlebar').style.borderStyle = 'solid';
            document.getElementById('titlebar').style.borderBottomWidth = '0';
            //document.getElementById('titlebar').style.borderColor = '#FEC951';
        }
        else {
            document.getElementById('titlebar').style.borderStyle = 'solid';
            document.getElementById('titlebar').style.borderWidth = '1';
            document.getElementById('titlebar').style.borderColor = '#FEC951';
        }
    }
</script>
<p style="font-size: 20px">Applicants In Process</p>
<%--<div id="titlebar" style="height:20px;line-height:15px;background-color:#FFFFE1;border:solid 1px #FEC951;border-bottom:none 0px #000;margin:2 0 0 0;padding:1 1;text-align:right">
<span onclick="SwitchTitleBar()" style="width:20; cursor:pointer;color:#FEC951;font-weight:normal"> Close </span>
</div>
<div id="notionbar" style="background-color:#FFFFE1;border:solid 1px #FEC951;vertical-align:middle;margin:0 0 2 0;padding:2 5;text-align:left">
    <ul style="list-style-type:square">
        <li>This page lists the candidates you screened out for interview.</li>
        <li>You can click the name to see the detailed information.</li>
        <li>If you want to schedule new interview, click the check boxes bofore the applicants for whom you want to schedule new interview, and click "Schedule Interview".</li>
        <li>
        This list shows the applicants in recruiting progress owned by you.
        </li>
        <li>
        You can click applicant's name to see the detail information and recruiting status. 
        </li>
    </ul>
</div>--%>
     <uc1:ApplicantsList ID="ApplicantsList1" runat="server" OnPagerClickChanged="ApplicantsList1_PagerClick" ActiveTab="1"/>
</div>

</asp:Content>