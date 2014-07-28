<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmploymentDecision.aspx.cs" Inherits="EmploymentDecision" MasterPageFile="~/SpringfieldMaster.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="cntMain" runat="server" ContentPlaceHolderID="mainPlaceHolder">
<div style="width: 80%;">
    <asp:DropDownList ID="ddlStatus" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="4">Hired</asp:ListItem>
            <asp:ListItem Value="5">Rejected</asp:ListItem>
        </asp:DropDownList>
    <div id="content_title" class="panel_title_expand">
        <asp:Literal ID="litHeader" runat="server"></asp:Literal></div>
    <div id="content_content" class="panel_content">    
        <asp:GridView ID="gvDecidedApplicants" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="applicants_table" AllowSorting="True" OnPageIndexChanging="gvDecidedApplicants_PageIndexChanging" OnRowCommand="gvDecidedApplicants_RowCommand" OnRowDataBound="gvDecidedApplicants_RowDataBound" PageSize="15">
            <EmptyDataTemplate>
            You don't have to sent any rejection email
            </EmptyDataTemplate>
            <PagerSettings Mode="NumericFirstLast" />
            <Columns>
                <asp:TemplateField AccessibleHeaderText="Checked" HeaderText="Checked">
                    <ItemTemplate>
                        <input type="checkbox" name="cbChecked" id="cbChecked" value='<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>' />
                        <input type="hidden" name="<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>" id="<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>" value='<%# DataBinder.Eval(Container.DataItem, "InterviewId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="ApplicantName" HeaderText="ApplicantName">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlnkApplicant" runat="server" NavigateUrl='<%# Eval("ApplicantId", "ShowApplication.aspx?applicant={0}&tab=1") %>'
                            Target="_blank" Text='<%# ParseName(Container.DataItem) %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Decision" HeaderText="Decision">
                    <ItemTemplate>
                    <%# ParseDecision(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Reject Date" HeaderText="Reject Date">
                    <ItemTemplate>
                        <%# ParseDateTime(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Mentor" HeaderText="Mentor">
                    <ItemTemplate>
                        <%# ParseMentor(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Group Manager" HeaderText="Group Manager">
                    <ItemTemplate>
                        <%# ParseGroupManager(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                    <ItemTemplate>
<%--                        <asp:Button ID="btnReject" CommandName="Reject" runat="server" Text="Reject" />
--%>                    
                        <a target="_blank" href="AddNote.aspx?applicant=<%# Eval("ApplicantId") %>"><img src="ProUI\images\addnote.gif" style="width: 16px; height: 16px;border: 0px;" alt="add/read note" /></a>&nbsp;&nbsp;&nbsp;<input type='button' onclick="OpenReferralWindow('AddReferral.aspx','<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>');" value='Forward' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        <asp:Button ID="btnMultiReject" runat="server" Text="Send Reject Letter" OnClick="btnMultiReject_Click" />
        <asp:Button ID="btnHire" runat="server" Text="Send Hire Letter" OnClick="btnHire_Click" />
        <asp:Button ID="btnMultiRecommend" runat="server" Text="Forward to Mentor" OnClick="btnMultiRecommend_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete without Sending Email" OnClick="btnDelete_Click" />
    </div>
</div>
 <asp:Panel ID="Panel1" runat="server" Style="display: none">
            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup">
                <div>
                    <p>
                        Are you sure to send the <asp:Literal ID="litHint" runat="server"></asp:Literal> letters?</p>
                    <p style="text-align: center;">
                        <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="OkButton_Click" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
                    </p>
                </div>
            </asp:Panel>
        </asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server"
            TargetControlID="btnHire"
            PopupControlID="Panel1"
            BackgroundCssClass="modalBackground"
            CancelControlID="CancelButton"
            DropShadow="true" 
            Drag="true"
            PopupDragHandleControlID="Panel3"/>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
            TargetControlID="btnMultiReject"
            PopupControlID="Panel1"
            BackgroundCssClass="modalBackground"
            CancelControlID="CancelButton"
            DropShadow="true" 
            Drag="true"
            PopupDragHandleControlID="Panel3"/>
</asp:Content>