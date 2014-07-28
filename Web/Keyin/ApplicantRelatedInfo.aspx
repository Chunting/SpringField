<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicantRelatedInfo.aspx.cs" Inherits="PageApplicantRelatedInfo" MasterPageFile="~/Keyin/ApplicantPortalMaster.master" %>
<%@ Register TagPrefix="SFUserControl" TagName="ISG" Src="~/Keyin/UserControls/InfoSourceGenerator.ascx" %>

<asp:Content ContentPlaceHolderID="mainHolder" ID="cntRelatedInfo" runat="server">
<div style="width: 100%;">
<div id="ch_title" class="panel_title_expand">
    Step 3: Application Related Information
</div>
<div id="ch_content" class="panel_content">

    <table class="applicants_table">
        <tr>
            <td>
                Availability start Date:
            </td>
            <td>
                <asp:TextBox ID="tbFirstDate" runat="server"></asp:TextBox>
                <input type="button" value="Select" id="btnFirstDate" name="btnFirstDate" runat="server" />
<%--                <asp:RequiredFieldValidator ID="rfvFirstDate" runat="server" ErrorMessage="*" ControlToValidate="tbFirstDate" ValidationGroup="RelatedInfo"></asp:RequiredFieldValidator>
--%>            
            </td>
        </tr>
        <tr>
            <td>
                Internship end Date:
            </td>
            <td>
                <asp:TextBox ID="tbSecondDate" runat="server"></asp:TextBox>
                <input type="button" value="Select" id="btnSecondDate" name="btnSecondDate" runat="server" />
<%--                <asp:RequiredFieldValidator ID="rfvSecondDate" runat="server" ErrorMessage="*" ControlToValidate="tbSecondDate" ValidationGroup="RelatedInfo"></asp:RequiredFieldValidator>
--%>            
            </td>
        </tr>
        <tr>
            <td>
                Interested Group:
            </td>
            <td style="font-size: 9pt;">
<%--                <asp:DropDownList ID="ddlGroup" EnableViewState="true" runat="server" >
                    <asp:ListItem Selected="True"></asp:ListItem>
                    <asp:ListItem>Center of Devices and Platforms</asp:ListItem>
                    <asp:ListItem>Center of Interaction Design</asp:ListItem>
                    <asp:ListItem>Incubation Group</asp:ListItem>
                    <asp:ListItem>Internet Graphics Group</asp:ListItem>
                    <asp:ListItem>Internet Media Group</asp:ListItem>
                    <asp:ListItem>Media Communication Group</asp:ListItem>
                    <asp:ListItem>Multimodal User Interface Group</asp:ListItem>
                    <asp:ListItem>Natural Language Computing Group</asp:ListItem>
                    <asp:ListItem>Search Technology Center</asp:ListItem>
                    <asp:ListItem>Speech Group</asp:ListItem>
                    <asp:ListItem>System Group</asp:ListItem>
                    <asp:ListItem>Technology Transfer Group</asp:ListItem>
                    <asp:ListItem>University Relationship Group</asp:ListItem>
                    <asp:ListItem>Visual Computing Group</asp:ListItem>
                    <asp:ListItem>Web Search and Data Mining Group</asp:ListItem>
                    <asp:ListItem>Wireless and Networking Group</asp:ListItem>
                    <asp:ListItem>Human Resource Group</asp:ListItem>
                    <asp:ListItem>IT Group</asp:ListItem>
                    <asp:ListItem>Operations Group</asp:ListItem>
                    <asp:ListItem>Public Relationship Group</asp:ListItem>
                </asp:DropDownList>--%>
                <asp:CheckBoxList ID="cblGroup" runat="server">
                </asp:CheckBoxList>
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
                    <%--<asp:CheckBox ID="cbSpecialProgram" Text="<%#Container.DataItem%>" Checked="<%# GetSpecialChoosedProgram().Contains(Container.DataItem.ToString())?"true":"false" %>" runat="server" />--%>
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

<%--                <asp:DropDownList ID="ddlInfoSource" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInfoSource_SelectedIndexChanged">
                    <asp:ListItem Value="0">Job Posting</asp:ListItem>
                    <asp:ListItem Value="1">Microsoft Technology Club</asp:ListItem>
                    <asp:ListItem Value="2">Campus Poster</asp:ListItem>
                    <asp:ListItem Value="3">BBS</asp:ListItem>
                    <asp:ListItem Value="4">Activities</asp:ListItem>
                    <asp:ListItem Value="5">Others</asp:ListItem>
                    <asp:ListItem Value="1000" Selected="True">Please Select</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlInfoSourceDetail" runat="server" Visible="false" OnSelectedIndexChanged="ddlInfoSourceDetail_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:TextBox ID="tbInfoSourceText" runat="server" Visible="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvText" ControlToValidate="tbInfoSourceText" Enabled="false" runat="server" ErrorMessage="*" ValidationGroup="RelatedInfo"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td colspan="2">
               <asp:Button ID="btnSubmit" runat="server" Text="Finish" OnClick="btnSubmit_Click" ValidationGroup="RelatedInfo" CausesValidation="true" />
<%--               <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/ApplicantInformationView.aspx" CssClass="support_link">Back to main page</asp:HyperLink>
--%>               <br />
               <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    
</div>
</div>
</asp:Content>
    
