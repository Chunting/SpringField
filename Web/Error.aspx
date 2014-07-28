<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<?xml version="1.0" encoding="utf-8"?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-CN">
<head runat="server">
	<title> Some Exception Occurs </title>
	<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
	<meta http-equiv="Content-Language" content="zh-CN" />
	<meta content="all" name="robots" />
	<meta name="title" content="MIATS" />
	<meta name="author" content="ChenYuan" />
	<meta name="subject" content="MIATS" />
	<meta name="language" content="zh-CN" />
	<meta name="keywords" content="MIATS,ChenYuan" />
	<link rel="stylesheet" href="master.css" type="text/css" media="all"  />
	<script language="JavaScript" src="scripts/popcalendar.js" type="text/javascript"></script>
	<script language="JavaScript" src="scripts/master.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmReferral" runat="server">
    <div style="width: 80%;">
    
    <div>
    <div id="ch_title" class="panel_title_expand">
    Error Message (Please report it to the site administrator):
    </div>
    <div id="ch_content" class="panel_content">

    <table class="applicants_table">
        <tr>
            <td>
                <asp:Label ID="lbErrorMsg" runat="server" Text=""></asp:Label><br />
                <asp:LinkButton ID="lnkbtnHome" runat="server" Text="Home" OnClick="lnkbtnHome_Click"></asp:LinkButton>
                <%-- <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/default.aspx">Home</asp:HyperLink>--%>
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

