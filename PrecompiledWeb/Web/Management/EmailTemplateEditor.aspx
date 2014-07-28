<%@ page language="C#" validaterequest="false" masterpagefile="~/SpringfieldMaster.master" autoeventwireup="true" inherits="Management_EmailTemplateEditor, App_Web_7bnxe2jf" title="SpringField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
<!-- TinyMCE -->
<script type="text/javascript" src="../jscripts/tiny_mce/tiny_mce.js"></script>
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
    Template<br />
    <asp:DropDownList ID="ddlMailTemplate" runat="server" OnSelectedIndexChanged="ddlMailTemplate_SelectedIndexChanged" AutoPostBack="True">
    </asp:DropDownList><br />
    (possible variables: 
    <asp:Label ID="lblSubjectVar" runat="server" Text="Label"></asp:Label>)<br />
    <br />
    From<br />
    <asp:TextBox ID="tbFrom" runat="server"></asp:TextBox>
    (//Variable Name//, alias or full address are all acceptted.&nbsp; Please split by ";")<br />
    To<br />
    <asp:TextBox ID="tbTo" runat="server"></asp:TextBox><br />
    Cc<br />
    <asp:TextBox ID="tbCc" runat="server"></asp:TextBox><br />
    Bcc<br />
    <asp:TextBox ID="tbBcc" runat="server"></asp:TextBox><br />
    Subject<br />
    <asp:TextBox ID="tbSubject" runat="server" Width="408px"></asp:TextBox><br />
    <br />
    Body<br />
    <asp:TextBox ID="tbBody" runat="server" Height="300px" TextMode="MultiLine" Width="700px"></asp:TextBox>
    <br />
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />&nbsp;
    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
</asp:Content>

