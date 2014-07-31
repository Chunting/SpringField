<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Keyin.PageApplicantBasicInfo" MasterPageFile="~/SpringfieldMaster.master" Codebehind="ApplicantBasicInfo.aspx.cs" %>

<asp:Content ID="cntBasicInfo" ContentPlaceHolderID="mainPlaceHolder" runat="server">
<div style="width: 100%;">
<div id="ch_title" class="panel_title_expand">
    Step 2: Application Basic Information
</div>
<div id="ch_content" class="panel_content">

    <table class="applicants_table">
        <tr>
            <td>
                First Name:
            </td>
            <td>
                <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox><span style="color:#ff0000;font-weight:bold">*</span>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="tbFirstName"
                    ErrorMessage="Please input First Name." ToolTip="User Name is required." ValidationGroup="BasicInfo"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr>
            <td>
                Last Name:    
            </td>
            <td>
                <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox><span style="color:#ff0000;font-weight:bold">*</span>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="tbLastName"
                    ErrorMessage="Please input Last Name." ValidationGroup="BasicInfo"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr>
            <td>
                Name in Chinese:
            </td>
            <td>
                <asp:TextBox ID="tbChineseName" runat="server"></asp:TextBox><span style="color:#ff0000;font-weight:bold">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbChineseName"
                    ErrorMessage="Please input Chinees Name."></asp:RequiredFieldValidator>
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
                <%--<asp:RequiredFieldValidator ID="rfvIdNum" runat="server" ControlToValidate="tbIdNum"
                    ErrorMessage="*" ValidationGroup="BasicInfo" Enabled="False"></asp:RequiredFieldValidator>
                <asp:Label ID="lbIdWarning" runat="server" Text="This Id No. has been used!" Visible="False" ForeColor="Red"></asp:Label>--%>
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
                <asp:TextBox ID="tbPhone" runat="server"></asp:TextBox><span style="color:#ff0000;font-weight:bold">*</span>
                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="tbPhone"
                ErrorMessage="Please input Contact Phone No." ValidationGroup="BasicInfo"></asp:RequiredFieldValidator>
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
                <asp:TextBox ID="tbCity" runat="server"></asp:TextBox><span style="color:#ff0000;font-weight:bold">*</span>
                <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="tbCity"
                    ErrorMessage="Please input Current City." ValidationGroup="BasicInfo"></asp:RequiredFieldValidator>
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
                <asp:DropDownList ID="ddlCountry" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
        <td colspan="2" align="center">
        <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
        </td>
        </tr>
       
    </table>
   
</div>
</div>        
 <div class="toolbar">
        <table style="height:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td runat="server" id="btnMultiInterview" style="padding:0 10;height:30;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" ValidationGroup="BasicInfo" CausesValidation="true"  
                        CssClass="img_icon" ImageUrl="~/Resource/Images/next_step.png" ID="btnSubmit"
                        runat="server" AlternateText="Next Step" OnClick="btnSubmit_Click" ImageAlign="AbsMiddle" />
                        <label for="<%=btnSubmit.ClientID %>"><span style="cursor:hand">Next Step</span></label>
                </td>
            </tr>
        </table>        
    </div>
</asp:Content>