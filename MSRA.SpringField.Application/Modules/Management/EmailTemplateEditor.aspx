<%@ Page Language="C#"  validateRequest="false" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Management.Management_EmailTemplateEditor" Title="SpringField" Codebehind="EmailTemplateEditor.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
<!-- TinyMCE -->
<script type="text/javascript" src="../../Resource/Scripts/tiny_mce/tiny_mce.js"></script>
<script type="text/javascript">
	tinyMCE.init({
		// General options
		mode : "textareas",
		theme : "advanced",
		plugins : "safari,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,inlinepopups",

		// Theme options
		theme_advanced_buttons1 : "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
		theme_advanced_buttons2 : "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
		theme_advanced_buttons3 : "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
		theme_advanced_buttons4 : "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak",
		theme_advanced_toolbar_location : "top",
		theme_advanced_toolbar_align : "left",
		//theme_advanced_statusbar_location : "bottom",
		//theme_advanced_resizing : true,

		// Example word content CSS (should be your site CSS) this one removes paragraph margins
		content_css : "css/word.css",

		// Drop lists for link/image/media/template dialogs
		template_external_list_url : "lists/template_list.js",
		external_link_list_url : "lists/link_list.js",
		external_image_list_url : "lists/image_list.js",
		media_external_list_url : "lists/media_list.js",

		// Replace values for the template plugin
		template_replace_values : {
			username : "Some User",
			staffid : "991234"
		},
		onchange_callback: "SetChanged",
		relative_urls : false
	});
</script>
<!-- /TinyMCE -->
<script type="text/javascript">
var changed = false;
var saving = false;

function SetChanged()
{
    if( !saving )
    {
        changed = true;
    }
}

function SetSaving()
{
    saving = true;
    changed = false;
}

function ConfirmExit()
{

  if( !saving && changed )
  {
    return "Leave without save?";
  }
}

window.onbeforeunload = ConfirmExit;
</script>

<table style="width:100%" cellpadding="0" cellspacing="0">
    <tr style="height:30px;display:none">
        <td colspan="2" align="center">
            <asp:Button ID="Button1" Width="120" runat="server" OnClick="btnSave_Click" Text="Save" />&nbsp;
            <asp:Button ID="Button2" Width="120" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table style="width:100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width:68px">Template:</td>
                    <td>
                    <asp:DropDownList ID="ddlMailTemplate" runat="server" Width="180"
                            OnSelectedIndexChanged="ddlMailTemplate_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    </td>
                    
                </tr>
                <%--<tr>
                    <td colspan="2">
                        <div id="notionbar" style="background-color:#FFFFE1;border:solid 1px #FEC951;height:90%;vertical-align:middle;margin:5 0;text-align:left">
                        <table style="width:100%;height:100%">
                            <tr>
                                <td align="left" valign="middle">
                        (possible variables: <asp:Label ID="lblSubjectVar" runat="server" Text="Label"></asp:Label>)
                        </td>
                            </tr>
                        </table>                
                        </div>
                    </td>
                </tr>--%>
                <tr>
                    <td  style="width:68px">From:</td>
                    <td><asp:TextBox ID="tbFrom" runat="server" Width="210"></asp:TextBox></td>
                    <td>
                     <div id="Div1" style="background-color:#FFFFE1;border:solid 1px #FEC951;height:90%;vertical-align:middle;margin:5 0;text-align:left" visible="false">
                        <table style="width:100%;height:100%;display:none">
                            <tr>
                                <td align="left" valign="middle">
                        (//Variable Name//, alias or full address are all acceptted.&nbsp; Please split by ";")
                        </td>
                            </tr>
                        </table>                
                        </div>
                    </td>    
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>    
    </tr>
    <tr style="height:30px">
        <td style="width:67px">To:</td>
        <td><asp:TextBox Width="210" ID="tbTo" runat="server"></asp:TextBox></td>
    </tr>
    <tr style="height:30px">
        <td>Cc:</td>
        <td><asp:TextBox Width="210" ID="tbCc" runat="server"></asp:TextBox></td>
    </tr>
    <tr style="height:30px">
        <td>Bcc:</td>
        <td><asp:TextBox Width="210" ID="tbBcc" runat="server"></asp:TextBox></td>
    </tr>
    <tr style="height:30px">
        <td>Subject:</td>
        <td><asp:TextBox ID="tbSubject" runat="server" Width="408px"></asp:TextBox></td>
    </tr>
    <tr style="height:30px">
        <td>Body:</td>
        <td><asp:TextBox ID="tbBody" runat="server" Height="300px" TextMode="MultiLine" Width="700px"></asp:TextBox></td>
    </tr>   
</table>
<br />
 <div class="toolbar">
        <table style="height:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td runat="server" id="btnMultiInterview" style="padding:0 10;height:30;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/save.png" ID="btnSave"
                        runat="server" AlternateText="Save" OnClick="btnSave_Click" ImageAlign="AbsMiddle" />
                        <label for="<%=btnSave.ClientID %>"><span style="cursor:hand">Save</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding:0 10;height:30;" runat="server" id="btnFavorite"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/cancel.png"
                        ID="btnCancel" runat="server" AlternateText="Cancel" OnClick="btnCancel_Click" ImageAlign="AbsMiddle"/>
                         <label for="<%=btnCancel.ClientID %>"><span style="cursor:hand">Cancel</span></label>
                </td>
            </tr>
        </table>        
    </div>
    
</asp:Content>

