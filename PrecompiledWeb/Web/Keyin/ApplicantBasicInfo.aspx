<%@ page language="C#" autoeventwireup="true" inherits="PageApplicantBasicInfo, App_Web_q-xenaxn" masterpagefile="~/Keyin/ApplicantPortalMaster.master" %>

<asp:Content ID="cntBasicInfo" ContentPlaceHolderID="mainHolder" runat="server">
<div style="width: 100%;">
<div id="ch_title" class="panel_title_expand">
    Step 1: Application Basic Information
</div>
<div id="ch_content" class="panel_content">

    <table class="applicants_table">
        <tr>
            <td>
                First Name:
            </td>
            <td>
                <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="tbFirstName"
                    ErrorMessage="*" ToolTip="User Name is required." ValidationGroup="BasicInfo"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr>
            <td>
                Last Name:    
            </td>
            <td>
                <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="tbLastName"
                    ErrorMessage="*" ValidationGroup="BasicInfo"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr>
            <td>
                Name in Chinese:
            </td>
            <td>
                <asp:TextBox ID="tbChineseName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Gender:
            </td>
            <td>
                <asp:DropDownList ID="ddlGender" runat="server">
                    <asp:ListItem Selected="True" Value="0">Male</asp:ListItem>
                    <asp:ListItem Value="1">Female</asp:ListItem>
                    <asp:ListItem Value="2">Unknown</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ID Card No.
            </td>
            <td>
                <asp:TextBox ID="tbIdNum" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvIdNum" runat="server" ControlToValidate="tbIdNum"
                    ErrorMessage="*" ValidationGroup="BasicInfo" Enabled="False"></asp:RequiredFieldValidator>
                <asp:Label ID="lbIdWarning" runat="server" Text="This Id No. has been used!" Visible="False" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Nationality:
            </td>
            <td>
                <asp:DropDownList ID="ddlNation" runat="server">
<%--                    <asp:ListItem Selected="True" Value="0">China</asp:ListItem>
                    <asp:ListItem Value="1">America</asp:ListItem>
                    <asp:ListItem Value="2">Canada</asp:ListItem>
                    <asp:ListItem Value="3">Australia</asp:ListItem>
                    <asp:ListItem Value="4">England</asp:ListItem>
                    <asp:ListItem Value="5">Janpan</asp:ListItem>
                    <asp:ListItem Value="6">Korea</asp:ListItem>--%>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td >
                WebPage:
            </td>
            <td >
                <asp:TextBox ID="tbWebPage" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Contact Phone No.
            </td>
            <td >
                <asp:TextBox ID="tbPhone" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="tbPhone"
                ErrorMessage="*" ValidationGroup="BasicInfo"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Address:
            </td>
            <td>
                <asp:TextBox ID="tbAddress" runat="server" Columns="36" Rows="5" TextMode="MultiLine"></asp:TextBox>
<%--                <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="tbAddress"
                ErrorMessage="*" ValidationGroup="BasicInfo"></asp:RequiredFieldValidator>
--%>            </td>
        </tr>
        <tr>
            <td >
                Current City:
            </td>
            <td >
                <asp:TextBox ID="tbCity" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="tbCity"
                    ErrorMessage="*" ValidationGroup="BasicInfo"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <%--Removed--%>
        <%--
        <tr>
            <td>
                Current Province:
            </td>
            <td>
                <asp:TextBox ID="tbProvince" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvProvince" runat="server" ControlToValidate="tbProvince"
                ErrorMessage="*" ValidationGroup="BasicInfo"></asp:RequiredFieldValidator>
            </td>
        </tr>
         --%>
        <tr>
            <td>
                Current Country:
            </td>
            <td>
                <asp:DropDownList ID="ddlCountry" runat="server">
<%--                    <asp:ListItem Value="0" Selected="True">China</asp:ListItem>
                    <asp:ListItem Value="1">America</asp:ListItem>
                    <asp:ListItem Value="2">Canada</asp:ListItem>
                    <asp:ListItem Value="3">Australia</asp:ListItem>
                    <asp:ListItem Value="4">England</asp:ListItem>
                    <asp:ListItem Value="5">Janpan</asp:ListItem>
                    <asp:ListItem Value="6">Korea</asp:ListItem>--%>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
               <asp:Button ID="btnSubmit" runat="server" Text="Next Step" OnClick="btnSubmit_Click" ValidationGroup="BasicInfo" CausesValidation="true" />
<%--               <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/ApplicantInformationView.aspx" CssClass="support_link">Back to main page</asp:HyperLink>
--%>               <br />
               <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</div>
</div>        
</asp:Content>