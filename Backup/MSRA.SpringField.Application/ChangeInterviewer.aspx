<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.ChangeInterviewer" Codebehind="ChangeInterviewer.aspx.cs" %>
<%@ Register TagPrefix="Springfield" TagName="BasicInfo" Src="~/Controls/BasicInfo.ascx" %>

<?xml version="1.0" encoding="utf-8"?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-CN">
<head runat="server">
	<title> Change Interviewer </title>
	<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
	<meta http-equiv="Content-Language" content="zh-CN" />
	<meta content="all" name="robots" />
	<meta name="title" content="MIATS" />
	<meta name="author" content="ChenYuan" />
	<meta name="subject" content="MIATS" />
	<meta name="language" content="zh-CN" />
	<meta name="keywords" content="MIATS,ChenYuan" />
	<link rel="stylesheet" href="Resource/CSS/global.css" type="text/css" media="all"  />
	<script language="JavaScript" src="Resource/Scripts/popcalendar.js" type="text/javascript"></script>
	<script language="JavaScript" src="Resource/Scripts/master.js" type="text/javascript"></script>
</head>
<body style="text-align: center">
    <form id="frmChangeInterviewer" runat="server">
    <div style="width: 80%;">
    <Springfield:BasicInfo runat="server" ID="basicInfo" />
    <br />
    <br />
    
    <div>
    <div id="ch_title" class="panel_title_expand">
    Change Interviewer
    </div>
    <div id="ch_content" class="panel_content">

    <table class="applicants_table">
        <tr>
            <td>
                Interviewer Alias:
            </td>
            <td>
                <asp:TextBox ID="tbInterviewer" runat="server" Columns="36" Rows="6" TextMode="MultiLine" CssClass="required_input"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvAlias" runat="server" ControlToValidate="tbInterviewer" ErrorMessage="Required!"></asp:RequiredFieldValidator>
            </td>
        </tr>
<%--        <tr>
            <td>
                Email Ext:
            </td>
            <td>
                <select id="mail_ext">
                    <option value="@microsoft.com">
                        @microsoft.com
                    </option>
                    <option value="@msrchina.research.microsoft.com" selected="selected">
                        @msrchina.research.microsoft.com
                    </option>
                </select>
            </td>
        </tr>--%>
<%--        <tr>
            <td>
                Due Date:
            </td>
            <td>
                <input id="txt_due_date" name="txt_due_date" type="text" /><input name="btn_due_date" onclick="popUpCalendar(this,frmChangeInterviewer.txt_due_date,'mm/dd/yyyy');" type="button" value="Select" />
            </td>
        </tr>
--%>        <tr>
            <td colspan="2">
                <asp:Button ID="btnChange" runat="server" Text="Change" OnClientClick="return ConfirmChangeInterviewer();" OnClick="btnChange_Click" />
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
