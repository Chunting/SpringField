<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Keyin/ApplicantPortalMaster.master" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="SFUserControl" TagName="ISG" Src="~/Keyin/UserControls/InfoSourceGenerator.ascx" %>
<asp:Content ID="cntPage" ContentPlaceHolderID="mainHolder" runat="Server" EnableViewState="false">   
<p>
    MIATS is MSRA Intern Application Tracking System.<br />
    Codename: Springfield.<br />
    Step 1:
</p>
<table class="sys_info_table" id="sys_info">
    <tr class="bold_font">
        <td>
        Email:
        </td>
        <td>
        <asp:TextBox ID="tbEmail" runat="server" Columns="60"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="tbEmail" ID="rfvEmail" runat="server" ErrorMessage="Required!"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="tbEmail" ID="revEmail" runat="server" ErrorMessage="Invalid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnNext" runat="server" Text="Next Step" OnClick="btnNext_Click" CausesValidation="true" /><br />
            <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
</asp:Content>

