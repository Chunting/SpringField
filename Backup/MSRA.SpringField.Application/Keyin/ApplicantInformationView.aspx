<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/SpringfieldMaster.master" Inherits="MSRA.SpringField.Application.Keyin.ApplicantInformationView" Codebehind="ApplicantInformationView.aspx.cs" %>

<asp:Content ID="cntView" ContentPlaceHolderID="mainPlaceHolder" runat="Server" EnableViewState="false">   

<div style="width: 100%;">
<div id="ch_title" class="panel_title_expand">
    Your Application Information
</div>
<div id="ch_content" class="panel_content">
<table  class="applicants_table">
    <tr>
        <td>
        <a href = "ApplicantBasicInfo.aspx" class="support_link">Basic Information(<asp:Label
            ID="lbBasicInfo" runat="server" Text=""></asp:Label>)</a>
        </td>
    </tr>
    <tr>
        <td>
        <a href = "ApplicantRelatedInfo.aspx" class="support_link">Related Information(<asp:Label
            ID="lbRelatedInfo" runat="server" Text=""></asp:Label>)</a>
        </td>
    </tr>
    <tr>
        <td>
            <a href = "ApplicantEduBackground.aspx" class="support_link">Education Background(<asp:Label
                ID="lbEduBackground" runat="server" Text=""></asp:Label>)</a>
        </td>
    </tr>
    <tr>
        <td class="bold_font">
            Status:
            <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
</div>
</div>

</asp:Content>
