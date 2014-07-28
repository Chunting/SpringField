<%@ page language="C#" autoeventwireup="true" inherits="Procssing, App_Web_wkngwpi-" masterpagefile="~/SpringfieldMaster.master" %>
<%@ Register Src="Controls/ApplicantsList.ascx" TagName="ApplicantsList" TagPrefix="uc1" %>
<asp:Content ID="ctntInterviewList" runat="server" ContentPlaceHolderID="mainPlaceHolder">
<div style="width: 100%;">
<ul>
<li>
This list shows the applicants in recruiting progress owned by you.
</li>
<li>
You can click applicant's name to see the detail information and recruiting status. 
</li>
</ul>
     <uc1:ApplicantsList ID="ApplicantsList1" runat="server" OnPagerClickChanged="ApplicantsList1_PagerClick" ActiveTab="1"/>
    This page lists the candidates you screened out for interview. You can click the
    name to see the detailed information. If you want to schedule new interview, click
    the check boxes bofore the applicants for whom you want to schedule new interview,
    and click "Schedule Interview".
</div>

</asp:Content>