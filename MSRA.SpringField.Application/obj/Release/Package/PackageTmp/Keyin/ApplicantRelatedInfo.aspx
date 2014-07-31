<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Keyin.PageApplicantRelatedInfo" MasterPageFile="~/SpringfieldMaster.master" Codebehind="ApplicantRelatedInfo.aspx.cs" %>
<%@ Register TagPrefix="SFUserControl" TagName="ISG" Src="~/Keyin/UserControls/InfoSourceGenerator.ascx" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="cntRelatedInfo" runat="server">
<script type="text/javascript" src="scripts/popcalendar.js"></script>
<div style="width: 100%;">
<div id="ch_title" class="panel_title_expand">
    Step 4: Application Related Information
</div>
<div id="ch_content" class="panel_content">

    <table class="applicants_table">
        <tr>
            <td>
                Availability start Date:
            </td>
            <td>
                <asp:TextBox ID="tbFirstDate" runat="server"></asp:TextBox><span style="color:#ff0000;font-weight:bold">*</span>               
                <asp:RequiredFieldValidator ID="rfvFirstDate" runat="server" ErrorMessage="Availability Start Date is required!" ControlToValidate="tbFirstDate"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Internship end Date:
            </td>
            <td>
                <asp:TextBox ID="tbSecondDate" runat="server"></asp:TextBox><span style="color:#ff0000;font-weight:bold">*</span>
                
<%--                <asp:RequiredFieldValidator ID="rfvSecondDate" runat="server" ErrorMessage="*" ControlToValidate="tbSecondDate" ValidationGroup="RelatedInfo"></asp:RequiredFieldValidator>
--%>            
            </td>
        </tr>
        <tr>
            <td>
                Interested Group:
            </td>
            <td style="font-size: 9pt;border:none">
                <div style="margin:0 0 5 0;">
                <asp:CheckBoxList CellPadding="0" CellSpacing="0" ID="cblGroup" runat="server" RepeatColumns="3" RepeatDirection="Vertical" RepeatLayout="Table"></asp:CheckBoxList>
                </div>
                Other group, please specify:<asp:TextBox ID="tbOtherGroup" runat="server"></asp:TextBox>
                (split by ";")</td>
        </tr>
        <tr>
            <td >
                Interested Areas:
            </td>
            <td>
                <asp:TextBox ID="tbAreas" runat="server" Columns="36" Rows="5" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvArea" runat="server" ControlToValidate="tbAreas"
                        ErrorMessage="*" ValidationGroup="RelatedInfo" Enabled="False"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Preferred Position:</td>
            <td>
                <asp:DropDownList ID="ddlPreferredPositon" runat="server">
                    <asp:ListItem Value="0">Unkown</asp:ListItem>
                    <asp:ListItem Value="0">Research Oriented</asp:ListItem>
                    <asp:ListItem Value="1">Engineering Oriented</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td >
                Special Program used to join:
            </td>
            <td>
                <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <asp:CheckBox ID="cbSpecialProgram" Text="<%#Container.DataItem %>" runat="server" Checked="<%# Check(Container.DataItem.ToString()) %>"/>                    
                </ItemTemplate>
                <SeparatorTemplate><br /></SeparatorTemplate>
                </asp:Repeater>
            </td>
        </tr>

        <tr>
            <td >
                Apply For:
            </td>
            <td >
                <asp:DropDownList ID="ddlInternType" runat="server">
                    <asp:ListItem Selected="True">FullTime</asp:ListItem>
                    <asp:ListItem>PartTime</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Get Information From
            </td>
            <td>
            <asp:UpdatePanel ID="up1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
<ContentTemplate>
            <SFUserControl:ISG ID="isg" runat="server" /><br />
            </ContentTemplate></asp:UpdatePanel>
            <asp:Label ID="lbInfoSource" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
               <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    
</div>
</div>
<div class="toolbar">
        <table style="height:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td runat="server" id="btnMultiInterview" style="padding:0 10;height:30;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" ValidationGroup="EduBackground" CausesValidation="true" 
                        CssClass="img_icon" ImageUrl="~/Resource/Images/finish.png" ID="btnSubmit"
                        runat="server" AlternateText="Finish" OnClick="btnSubmit_Click" ImageAlign="AbsMiddle" />
                        <label for="<%=btnSubmit.ClientID %>"><span style="cursor:hand">Finish</span></label>
                </td>
            </tr>
        </table>        
    </div>
</asp:Content>
    
