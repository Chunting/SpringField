<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddReferral.aspx.cs" Inherits="AddReferral" MasterPageFile="~/SpringfieldMaster.master" %>
<%@ Register TagPrefix="Springfield" TagName="BasicInfo" Src="~/Controls/BasicInfo.ascx" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="cntForward" runat="server">
    <div style="width: 80%;">
    <Springfield:BasicInfo runat="server" ID="basicInfo" /><br />
    <br />
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
            <td colspan="2">
                <asp:Button ID="btnAddReferral" runat="server" Text="Forward" OnClientClick="return ConfirmReferral();" OnClick="btnAddReferral_Click" />
                <%--<asp:ImageButton ImageUrl="~/ProUI/images/check.gif" ID="btnAddReferral" runat="server" AlternateText="Recommend" OnClientClick="return ConfirmReferral();" OnClick="btnAddReferral_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="image" src="ProUI/images/close.gif"  alt="Close" onclick="window.close();" />--%>
                <input type="button" value="Close" onclick="window.close();" />
            </td>
        </tr>
    </table>
    
    </div>
    </div>
    
    </div>
</asp:Content>