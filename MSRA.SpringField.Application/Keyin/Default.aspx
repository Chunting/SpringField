<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/SpringfieldMaster.master" Inherits="MSRA.SpringField.Application.Keyin._Default" Codebehind="Default.aspx.cs" %>
<%@ Register TagPrefix="SFUserControl" TagName="ISG" Src="~/Keyin/UserControls/InfoSourceGenerator.ascx" %>
<asp:Content ID="cntPage" ContentPlaceHolderID="mainPlaceHolder" runat="Server" EnableViewState="false">   

<div id="notionbar" class="infobar">
    <ul style="margin:5 30">
        <li>MIATS is MSRA Intern Application Tracking System.</li>
        <li>Codename: Springfield.</li>
    </ul>    
</div>
            <h4>Step 1:</h4>
<table class="sys_info_table" id="sys_info" style="width:100%">
    <tr class="bold_font">
        <td>
        Email:
        </td>
        <td>
        <asp:TextBox ID="tbEmail" runat="server" Columns="60"></asp:TextBox><span style="color:Red;font-weight:bold">*</span>
        <asp:RequiredFieldValidator ControlToValidate="tbEmail" ID="rfvEmail" runat="server" ErrorMessage="Please input Email."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="tbEmail" ID="revEmail" runat="server" ErrorMessage="Invalid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        
        </td>
    </tr>
    <tr>
    <td colspan="2" align="center"><asp:Label ID="lbMsg" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2">
        <div class="toolbar">
        <table style="height:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td runat="server" id="btnMultiInterview" style="padding:0 10;height:30;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CausesValidation="true" CssClass="img_icon" ImageUrl="~/Resource/Images/next_step.png" ID="btnNextCurrent"
                        runat="server" AlternateText="Next Step" OnClick="btnNext_Click" ImageAlign="AbsMiddle" />
                        <label for="<%=btnNextCurrent.ClientID %>"><span style="cursor:hand">Next Step</span></label>
                </td>
            </tr>
        </table>        
    </div>
        </td>
    </tr>
</table>
</asp:Content>

