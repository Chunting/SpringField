<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowPA.aspx.cs" Inherits="MSRA.SpringField.Application.ShowPA" %>

<%@ Register Src="Controls/PerformanceAssessment.ascx" TagName="PerformanceAssessment"
    TagPrefix="uc10" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> <%=GetName()%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Language" content="zh-CN" />
    <%--<link rel="stylesheet" href="Resource/CSS/global.css" type="text/css" media="all"  />--%>
    <style type="text/css">
        img
        {
            border: 0px;
        }
        html
        {
            min-width: 776px;
        }
        body
        {
            margin: 0px;
            padding: 0px;
            border: 0px;
            color: #000000;
            font-size: 9pt;
            text-align: left;
            background: #ffffff;
            font-family: verdana,arial,sans-serif, 'Lucida Grande' , 'Lucida Sans Unicode';
        }
        table
        {
            font-size: 9pt;
        }
        #full
        {
            margin-right: auto;
            margin-left: auto;
            margin-top: 0px;
            margin-bottom: 0px;
            padding: 0px;
            background: white;
            width: 100%;
        }
        #header
        {
            margin: 0px;
            padding: 0px;
            text-align: left;
            vertical-align: middle;
        }
        #container
        {
            padding: 0px;
            margin: 0px;
            width: 1000px;
        }
        #content
        {
            width: 780px;
            margin: 0px;
            padding-top: 20px;
            padding-left: 5px;
            text-align: left;
        }
        #filter
        {
            margin: 0px;
            padding: 0px;
        }
        #menu
        {
            float: left;
            width: 200px;
            margin: 0px;
            padding-top: 12px;
            padding-left: 12px;
            text-align: left;
            background: #eaeaea;
            border-right: 1px solid #999999;
        }
        #footer
        {
            clear: both;
            height: auto;
            background: white;
            margin: 0px;
            margin-right: auto;
            margin-left: auto;
            padding: 0px;
            color: #333333;
            font-size: 7pt;
            font-weight: bold;
            text-align: center;
        }
        #menu a
        {
            margin: 0px;
            color: black;
            padding: 2px;
        }
        #menu a:link
        {
            color: black;
            text-decoration: none;
        }
        #menu a:visited
        {
            text-decoration: none;
        }
        #menu a:hover
        {
            border: 1px solid #999999;
            color: white;
            background: #cccccc;
            text-decoration: none;
        }
        a
        {
            padding: 0px;
            margin: 0px;
        }
        a:link
        {
            color: blue;
            text-decoration: none;
        }
        a:visited
        {
            color: blue;
            text-decoration: none;
        }
        a:hover
        {
            color: #4b834b;
            text-decoration: none;
        }
        .line
        {
            padding: 0px;
            margin-left: auto;
            margin-right: auto;
            border-top: 1px solid #cccccc;
            width: 100%;
            text-align: center;
        }
        #copyright
        {
            padding: 0px;
            margin: 0px;
            margin-left: 30px;
        }
        .header_table
        {
            padding: 0px;
            margin: 0px;
            border: 0px;
            width: 100%;
            border-collapse: collapse;
            border-bottom: 1px solid #999999;
        }
        .header_table td
        {
            padding: 0px;
            margin: 0px;
            border: 0px;
            font-weight: bold;
            font-size: 10pt;
            vertical-align: middle;
        }
        .child_menu
        {
            padding: 0px;
            margin-bottom: 8px;
            margin-left: 10px;
            font-size: 8pt;
        }
        .parent_menu
        {
            padding: 0px;
            margin-bottom: 8px;
            font-size: 8pt;
            font-weight: bold;
        }
        .split_line
        {
            padding: 0px;
            margin-left: auto;
            margin-right: auto;
            border: 3px dashed #cccccc;
            height: 3px;
            width: 96%;
            text-align: center;
        }
        .mask
        {
            z-index: 1000;
            position: absolute;
            left: 0px;
            top: 0px;
            background: #eaeaea;
            display: none;
            width: 100%;
            height: 100%;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=20);
        }
        .module_window
        {
            background: white;
            z-index: 2000;
            position: absolute;
            top: 200px;
            left: 200px;
            margin-left: auto;
            margin-right: auto;
            margin-top: auto;
            margin-bottom: auto;
            display: none;
            width: 60%;
        }
        .bold_font
        {
            font-size: 12px;
            font-weight: bold;
            text-align: left;
            width: 18%;
        }
        .little_applicants_table
        {
            width: 100%;
            border: solid 1px #999999;
            padding: 0px;
            margin: 0px;
            font-size: 11px;
            border-collapse: collapse;
            border-spacing: 0px;
        }
        .little_applicants_table td
        {
            /*border: solid 1px #999999;*/
            margin: 0px;
            padding: 5px;
        }
        .applicants_table
        {
            border: solid 1px #999999;
            padding: 2px;
            margin: 0px;
            font-size: 12px;
            width: 100%;
            border-collapse: collapse;
            border-spacing: 0px;
        }
        .applicants_table_narrow
        {
            border: solid 1px #999999;
            padding: 2px;
            margin: 0px;
            font-size: 12px;
            border-collapse: collapse;
            border-spacing: 0px;
        }
        .applicants_table td
        {
            border: solid 1px #999999;
            margin: 0px;
            padding: 5px;
            word-wrap: break-word;
        }
        .panel_action
        {
            margin: 0px;
            padding: 0px;
            border: 1px solid #999999;
            color: #528f52;
            width: 100%;
            background-color: #eaeaea;
        }
        .panel_title_expand
        {
            margin: 0px;
            padding: 0px;
            text-align: center;
            height: 20px;
            background: url("images/arrow_down.gif") #eaeaea no-repeat 8px;
            border: 1px solid #999999;
            color: #528f52;
            font-size: 12pt;
            font-weight: bold;
            cursor: hand;
            width: 100%;
        }
        .panel_title_close
        {
            margin: 0px;
            padding: 0px;
            text-align: center;
            height: 20px;
            background: url("images/arrow_up.gif") #ffffff no-repeat 8px;
            border: 1px solid #999999;
            color: #333333;
            font-size: 12pt;
            font-weight: bold;
            cursor: hand;
            width: 100%;
        }
        .panel_content
        {
            text-align: left;
            border: 1px solid #999999;
            font-size: 10pt;
            word-spacing: normal;
            border-top: none;
            border-left: none;
            border-right: none;
            white-space: nowrap;
            width: 100%;
            padding: 0 0;
            margin: 0 0;
        }
        /*
input
{
    border: solid 1px #cccccc;
    background: #e8e8e8;
    padding: 0px;
    margin: 0px;
}
*/
        .unabled_input
        {
            border: 1px solid #cccccc;
            background: #eaeaea;
            padding: 0px;
            margin: 0px;
        }
        .editable_input
        {
            background: white;
            padding: 0px;
            margin: 0px;
        }
        .required_input
        {
            background: #ffff99;
            padding: 0px;
            margin: 0px;
        }
        .img_icon
        {
            width: 18px;
            height: 18px;
            border: 0px;
        }
        .modalPopup
        {
            background-color: #ffffdd;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 400px;
            height: 120px;
            vertical-align: middle;
        }
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=20);
            opacity: 0.2;
        }
        #ctl00_mainPlaceHolder_Tabs_header
        {
            font-weight: bold;
            height: 20;
        }
        #__tab_ctl00_mainPlaceHolder_Tabs_tabInterviewHistory, __
        tab_ctl00_mainPlaceHolder_Tabs_tabBasicInfo, __
        tab_ctl00_mainPlaceHolder_Tabs_tabPerformanceAssessment
        {
            height: 20;
        }
        .LibC_o
        {
            float: left;
            background: url(../images/LibC.gif) 0px 0px;
            width: 16px;
            height: 16px;
        }
        .LibC_e
        {
            float: left;
            background: url(../images/LibC.gif) -16px 0px;
            width: 16px;
            height: 16px;
        }
        .LibC_c
        {
            float: left;
            background: url(../images/LibC.gif) -32px 0px;
            width: 16px;
            height: 16px;
        }
        .PerformanceLevel_table
        {
            border: solid 1px #999999;
            padding: 0px;
            margin: 0px;
            font-size: 12px;
            width: 100%;
            border-collapse: collapse;
            border-spacing: 0px;
        }
        .PerformanceLevel_td
        {
            border: solid 1px #999999;
            margin: 0px;
            padding: 5px;
            word-wrap: break-word;
        }
        /*TabContainer Style*/
        .ajax_tab_menu .ajax__tab_header /*整体按钮底样式*/
        {
            font-family: 宋体;
            height: 28px;
            font-size: 12px;
            cursor: pointer;
            background-color: White; /*background:url("../../images/tabcontainer/Tab_Option_bg.gif") repeat-x bottom;*/
        }
        .ajax_tab_menu .ajax__tab_body /*资料区*/
        {
            font-family: 宋体;
            font-size: 12px;
            border: 1px solid #999999;
            border-top: 0;
            background-color: #ffffff;
        }
        .ajax_tab_menu .ajax__tab_tab /*预设样式*/
        {
            /*background:url("../../images/tabcontainer/Tab_Option_bg_OFF.gif") repeat-x;*/
            width: 78px;
            height: 28px;
            line-height: 28px;
            text-align: center;
            margin-right: 4px;
            margin: 0;
        }
        .ajax_tab_menu .ajax__tab_hover .ajax__tab_tab /*鼠标经过样式*/
        {
            /*background:url("../../images/tabcontainer/Tab_Option_bg_ON.gif") repeat-x;*/
            width: 78px;
            height: 28px;
            color: #FFFFFF;
            line-height: 28px;
            text-align: center;
        }
        .ajax_tab_menu .ajax__tab_active .ajax__tab_tab /*当前使用中样式*/
        {
            /*background:url("../../images/tabcontainer/Tab_Option_bg_ON.gif") repeat-x;*/
            width: 78px;
            height: 28px;
            line-height: 28px;
            text-align: center;
            color: #FFFFFF;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc10:PerformanceAssessment ID="ucPerformanceAssessment" runat="server" />
    </div>
    </form>
</body>
</html>
