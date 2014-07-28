<%@ page language="C#" autoeventwireup="true" inherits="ArrangeInterview, App_Web_ymsvljak" %>
<%@ Register TagPrefix="Springfield" TagName="BasicInfo" Src="~/Controls/BasicInfo.ascx" %>

<?xml version="1.0" encoding="utf-8"?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-CN">
<head runat="server">
	<title> Schedule Interview </title>
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
<body  style="text-align: center;">
    <form id="frmArrangeInterview" runat="server">
    <div>
    <div style="text-align: left;">
    <ul>
        <li>
        An email(interview request) will be send to the interviewer(s) you typed in the textbox.
        </li>
        <li>
        He will get a reminder email around the due date.
        </li>
    </ul>
    </div>
    <div style="width: 80%;">
    <Springfield:BasicInfo ID="basicInfo" runat="server" /><br />
    <div id="ch_title" class="panel_title_expand">
    Schedule Interview
    </div>
    <div id="ch_content" class="panel_content">

    <table class="applicants_table">
        <tr>
            <td>
                Interviewer Alias:
            </td>
            <td>
                Please fill the alias of the interviewer here, spliting the email by ";".<br />
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
        <tr>
            <td>
                Due Date:
            </td>
            <td>
                <input id="txt_due_date" name="txt_due_date" type="text" readonly="readonly" class="unabled_input" />&nbsp;&nbsp;<input name="btn_due_date" onclick="popUpCalendar(this,frmArrangeInterview.txt_due_date,'mm/dd/yyyy');" type="button" value="Select" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnArrange" runat="server" Text="Schedule Interview" OnClick="btnArrange_Click" OnClientClick="return ConfirmMultiInterview();" />
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
