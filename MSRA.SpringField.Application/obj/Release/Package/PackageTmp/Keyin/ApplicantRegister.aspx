<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Keyin.ApplicantRegister" Codebehind="ApplicantRegister.aspx.cs" %>

<?xml version="1.0" encoding="utf-8"?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-CN">
<head runat="server">
	<title> MSRA Intern Application Tracking System </title>
	<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
	<meta http-equiv="Content-Language" content="zh-CN" />
	<meta content="all" name="robots" />
	<meta name="title" content="MIATS" />
	<meta name="author" content="ChenYuan" />
	<meta name="subject" content="MIATS" />
	<meta name="language" content="zh-CN" />
	<meta name="keywords" content="MIATS,ChenYuan" />
	<link rel="stylesheet" href="global.css" type="text/css" media="all"  />
    <script language="JavaScript" type="text/javascript" src="scripts/master.js"></script>
    <script language="JavaScript" src="scripts/popcalendar.js" type="text/javascript"></script>
</head>
<body style="background: white;">
    <form id="frmRegister" runat="server">
    <div>
        <div style="width: 50%;">
        <div id="ch_title" class="panel_title_expand">
            Register Account
        </div>
        <div id="ch_content" class="panel_content">

        <table class="applicants_table">
            <tr>
                <td class="bold_font" style="width: 30%;">
                    Email:
                </td>
                <td>
                    <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="email illegal" ControlToValidate="tbEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Register"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td class="bold_font">
                    Password:    
                </td>
                <td>
                    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="*" ControlToValidate="tbPassword" ValidationGroup="Register"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="bold_font">
                    Confirm Password:
                </td>
                <td>
                    <asp:TextBox ID="tbConfirm" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="tbPassword"
                        ControlToValidate="tbConfirm" ErrorMessage="confirm password should not be different"
                        ValidationGroup="Register"></asp:CompareValidator></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="login_button" OnClick="btnRegister_Click" ValidationGroup="Register" />
                    <input type="reset" value="Reset" class="login_button" />
                    <asp:HyperLink ID="lnkDefault" runat="server" NavigateUrl="~/default.aspx" CssClass="support_link">Back to main page</asp:HyperLink><br />
                    <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        
        </div>
        </div>
    </div>
    </form>
</body>
</html>
