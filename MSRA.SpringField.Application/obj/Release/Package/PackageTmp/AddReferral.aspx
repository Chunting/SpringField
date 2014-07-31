<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.AddReferral" MasterPageFile="~/SpringfieldMaster.master" Codebehind="AddReferral.aspx.cs" %>
<%@ Register TagPrefix="Springfield" TagName="BasicInfo" Src="~/Controls/BasicInfo.ascx" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="cntForward" runat="server">
    <div style="width: 100%;">
    <Springfield:BasicInfo runat="server" ID="basicInfo" />
    <div class="toolbar">
        <table style="height:100%" cellpadding="0" cellspacing="0">
            <tr>               
                <td style="padding:0 10;height:30;"   runat="server" id="btnAddReferral"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/forward.png"
                        ID="btnForward" runat="server" AlternateText="Forward to mentor" OnClientClick="return ConfirmReferral();" 
                        OnClick="btnAddReferral_Click" ImageAlign="AbsMiddle"/>
                        <label for="<%=btnForward.ClientID %>"><span style="cursor:hand">Forward</span></label>
                </td>
                <td style="width:40px">&nbsp;</td>
                <td style="padding:0 10;height:30;" runat="server" id="btnDeleteSelection"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/back.png"
                        ID="ImageButton1" runat="server" AlternateText="Return to previous page" OnClientClick="window.history.go(-1)"
                        ImageAlign="AbsMiddle"/>
                        <label for="btnReturn"><span style="cursor:hand" onclick="window.history.go(-1)">Back</span></label>
                </td>
            </tr>
        </table>        
    </div>
    
    <div>
    <div id="ch_title" class="panel_title_expand">
        Forward to Mentor
    </div>
    <div id="ch_content" class="panel_content">

    <table class="applicants_table">
        <tr>
            <td>
                Forward To:
            </td>
            <td>
                Please input the alias of the employees to whom you want to recommend this applicant.<br />
                <asp:TextBox ID="tbAccepters" runat="server" Columns="36" Rows="6" TextMode="MultiLine" CssClass="required_input"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvAlias" runat="server" ControlToValidate="tbAccepters" ErrorMessage="Required!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <%--<tr>
            <td colspan="2">
                <asp:Button ID="btnAddReferral" runat="server" Text="Forward" OnClientClick="return ConfirmReferral();" OnClick="btnAddReferral_Click" />
                
                <input type="button" value="Close" onclick="window.close();" />
            </td>
        </tr>--%>
    </table>
    
    </div>
    </div>
    
    </div>
</asp:Content>