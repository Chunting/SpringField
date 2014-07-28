<%@ page language="C#" autoeventwireup="true" inherits="FavoritesList, App_Web_wkngwpi-" masterpagefile="~/SpringfieldMaster.master" %>
<%@ Register Src="Controls/ApplicantsList.ascx" TagName="ApplicantsList" TagPrefix="uc1" %>
<asp:Content ID="ctntFavoriteList" runat="server" ContentPlaceHolderID="mainPlaceHolder">
<div style="width: 100%;">
<ul>
<li>
This page shows the applicants in your favorite list. 
</li>
<li>
You can click their names to see detail information, or click related icons for more actions.
</li>
</ul>
     <uc1:ApplicantsList ID="ApplicantsList1" runat="server" OnPagerClickChanged="ApplicantsList1_PagerClick" ListType="favorite"/>
</div>
</asp:Content>