<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiReferral.aspx.cs" Inherits="MultiReferral" %>

<?xml version="1.0" encoding="utf-8"?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-CN">
<head runat="server">
	<title> Multi Recommendation </title>
	<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
	<meta http-equiv="Content-Language" content="zh-CN" />
	<meta content="all" name="robots" />
	<meta name="title" content="MIATS" />
	<meta name="author" content="ChenYuan" />
	<meta name="subject" content="MIATS" />
	<meta name="language" content="zh-CN" />
	<meta name="keywords" content="MIATS,ChenYuan" />
	<link rel="stylesheet" href="ProUI/global.css" type="text/css" media="all"  />
	<script language="JavaScript" src="scripts/popcalendar.js" type="text/javascript"></script>
	<script language="JavaScript" src="scripts/master.js" type="text/javascript"></script>
</head>
<body style="text-align: center">
    <form id="frmMultiReferral" runat="server">
    <div>
    
    <div style="width: 80%;">
    <div id="ch_title" class="panel_title_expand">
        Forward to Mentor
    </div>
    <div id="ch_content" class="panel_content">

    <table class="applicants_table">
        <tr>
            <td>
                Forward to:
            </td>
            <td>
                Please input the alias of the employees to whom you want to recommend this applicant.<br />
                <asp:TextBox ID="tbAccepters" runat="server" Columns="50" Rows="5" TextMode="MultiLine" CssClass="required_input"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvAlias" runat="server" ControlToValidate="tbAccepters" ErrorMessage="Required!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Applicants<br />List:
            </td>
            <td>
                <asp:Literal ID="literalApplicantList" runat="server"></asp:Literal>
                <%--<asp:ListBox ID="lbxApplicantList" runat="server" CssClass="unabled_input"></asp:ListBox> --%>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnRefer" runat="server" Text="Forward" OnClientClick="return ConfirmMultiRefer();" OnClick="btnRefer_Click" />
                <input type="button" value="Close" onclick="window.close();" />
            </td>
        </tr>
    </table>
    
    </div>
    </div>
    
    </div>
    </form>
    <hr class="split_line" />
    <div id="copyright">
    Copyright MSRA &copy;2006 All right reserved
    </div>
</body>
</html>
