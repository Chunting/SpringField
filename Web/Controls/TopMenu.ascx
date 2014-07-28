<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopMenu.ascx.cs" Inherits="Controls_TopMenu" %>
<div style="float: left;">
<asp:Image ID="imgLogo" ImageUrl="~/images/sf.gif" runat="server" BorderWidth="0" />
</div>
<br />
<div id="menu">
    (Welcome you:<asp:Label ID="lbWelcome" runat="server" Text=""></asp:Label>)
    <asp:HyperLink ID="lnkGeneralInfo" NavigateUrl="~/Default.aspx" runat="server">Springfield Today |</asp:HyperLink>
    <asp:HyperLink ID="lnkApplicants" NavigateUrl="~/Applicants.aspx" runat="server">Applicants |</asp:HyperLink>
    <asp:HyperLink ID="lnkFeedback" runat="server" NavigateUrl="~/FeedbackList.aspx">Incomplete Feedback |</asp:HyperLink>
    <asp:HyperLink ID="lnkDecision" NavigateUrl="~/EmploymentDecision.aspx" Visible="False" runat="server">Rejection List |</asp:HyperLink>
    <asp:HyperLink ID="lnkURReferral" NavigateUrl="~/URReferral.aspx" Visible="false" runat="server">UR Referral |</asp:HyperLink>
    <asp:HyperLink ID="lnkKeyReferral" NavigateUrl="~/ReferApplication.aspx" Visible="false" runat="server">Key Referral |</asp:HyperLink>
    <asp:HyperLink ID="lnkOffLineHring" NavigateUrl="~/OffLineHiring.aspx" runat="server">Off-line Hiring |</asp:HyperLink>   
    <asp:HyperLink ID="lnkProcssing" NavigateUrl="~/Procssing.aspx" runat="server">My On-going Interview |</asp:HyperLink>
    <asp:HyperLink ID="lnkFavoritesList" NavigateUrl="~/FavoritesList.aspx" runat="server">Favorites |</asp:HyperLink>
    <asp:HyperLink ID="lnkReport" NavigateUrl="~/Report.aspx" runat="server" Visible="false">Report |</asp:HyperLink>
    <asp:HyperLink ID="lnkSearch" NavigateUrl="~/Search.aspx" runat="server">Search |</asp:HyperLink>
    <asp:HyperLink ID="lnkManagement" NavigateUrl="~/Management/SiteRoleManager.aspx" runat="server" Visible="false">Management |</asp:HyperLink>
    <asp:HyperLink ID="lnkHelp" NavigateUrl="~/Help.htm" runat="server">Help</asp:HyperLink>
</div>