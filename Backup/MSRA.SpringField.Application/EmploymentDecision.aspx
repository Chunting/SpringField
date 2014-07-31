<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.EmploymentDecision" MasterPageFile="~/SpringfieldMaster.master" Codebehind="EmploymentDecision.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="cntMain" runat="server" ContentPlaceHolderID="mainPlaceHolder">
<script type="text/javascript" src="scripts/master.js"></script>
<div style="width: 100%;">
    <div class="toolbar">
        <table style="height:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td runat="server" id="btnMultiReject" style="padding:0 10;height:30;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/reject_mail.png" ID="MultiReject"
                        runat="server" AlternateText="Send Reject Letter" OnClick="btnMultiReject_Click" ImageAlign="AbsMiddle" />
                        <label for="<%=btnMultiReject.ClientID %>"><span style="cursor:hand">Send Reject Mail</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding:0 10;height:30;" runat="server" id="btnHire"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/hiring_mail.png"
                        ID="Hire" runat="server" AlternateText="Send Hire Letter" OnClick="btnHire_Click" ImageAlign="AbsMiddle"/>
                         <label for="<%=btnHire.ClientID %>"><span style="cursor:hand">Send Hire Mail</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding:0 10;height:30;"   runat="server" id="btnMultiRecommend"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">                    
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/forward.png"
                        ID="MultiRecommend" runat="server" AlternateText="Forward to Mentor" OnClick="btnMultiRecommend_Click" ImageAlign="AbsMiddle"/>
                        <label for="<%=btnMultiRecommend.ClientID %>"><span style="cursor:hand">Forward To Mentor</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding:0 10;height:30;"    runat="server" id="btnDelete"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" ID="Delete" ImageUrl="~/Resource/Images/delete.png"
                        CssClass="img_icon" runat="server" AlternateText="Delete without Sending Email" OnClientClick="return ConfirmDelete();" 
                        Visible="true" OnClick="btnDelete_Click"  ImageAlign="AbsMiddle" />
                        <label for="<%=btnDelete.ClientID %>"><span style="cursor:hand">Remove from the list</span></label>
                </td>
            </tr>
        </table>        
    </div>

    <table style="width:100%">
        <tr>
            <td>
                <div>
                    <table>
                        <tr>
                            <td>
                            Status:
                            </td>
                            <td>
                              <asp:DropDownList ID="ddlStatus" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
            <asp:ListItem Value="4">Hired</asp:ListItem>
            <asp:ListItem Selected="True" Value="5">Rejected</asp:ListItem>
        </asp:DropDownList>  
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="content_title" style="border-bottom:none 0px #000" class="panel_title_expand"><asp:Literal ID="litHeader" runat="server"></asp:Literal></div>
                <asp:GridView Width="100%" HeaderStyle-Height="35" ID="gvDecidedApplicants" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="applicants_table" AllowSorting="True" OnPageIndexChanging="gvDecidedApplicants_PageIndexChanging" OnRowCommand="gvDecidedApplicants_RowCommand" OnRowDataBound="gvDecidedApplicants_RowDataBound" PageSize="15">
            <EmptyDataTemplate>
            You don't have to sent any rejection email
            </EmptyDataTemplate>
            <PagerSettings Mode="NumericFirstLast" />
            <Columns>
                <asp:TemplateField AccessibleHeaderText="Checked" HeaderText="Checked" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <input type="checkbox" name="cbChecked" id="cbChecked" value='<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>' />
                        <input type="hidden" name="<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>" id="<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>" value='<%# DataBinder.Eval(Container.DataItem, "InterviewId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="ApplicantName" HeaderText="Applicant Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlnkApplicant" runat="server" NavigateUrl='<%# Eval("ApplicantId", "ShowApplication.aspx?applicant={0}&tab=1") %>'
                            Target="_blank" Text='<%# ParseName(Container.DataItem) %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Decision" HeaderText="Decision"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                    <%# ParseDecision(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Reject Date" HeaderText="Reject Date"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# ParseDateTime(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Mentor" HeaderText="Mentor"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# ParseMentor(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Group Manager" HeaderText="Group Manager"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# ParseGroupManager(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action" ItemStyle-Width="100" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <table style="width:100%;border-style:none" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="border:none 0px #000">
                                    <div style="cursor:pointer;width:100%;padding:0 1;margin:0 1">
                                        <a id="lkAddnote" style="border:none 0px #000" target="_self" href="AddNote.aspx?applicant=<%# Eval("ApplicantId") %>">
                                            <img src="Resource/Images/addnote.png" align="absmiddle" style="width: 16px; height: 16px;border: 0px;" alt="add/read note" />
                                        </a><label style="cursor:pointer" for="lkAddnote">Add Note</label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td  style="border:none 0px #000">
                                <div style="cursor:pointer;width:100%;padding:0 1;margin:0 1">
                                    <a style="border:none 0px #000" id="lkForward" 
                                    target="_self" href="AddReferral.aspx?applicant=<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>">
                                        <img src="Resource/Images/forward.png" align="absmiddle" style="width: 16px; height: 16px;border: 0px;" alt="forward applicant" />
                                    </a><label style="cursor:pointer" for="lkForward">Forward</label>                                    
                                </div>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
            </td>
        </tr>
    </table>
     
    
    <%--<div id="content_content" class="panel_content">    --%>
        
        
        <%--<asp:Button ID="" runat="server" Text="Send Reject Letter" OnClick="btnMultiReject_Click" />
        <asp:Button ID="btnHire" runat="server" Text="Send Hire Letter" OnClick="btnHire_Click" />
        <asp:Button ID="btnMultiRecommend" runat="server" Text="Forward to Mentor" OnClick="btnMultiRecommend_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete without Sending Email" OnClick="btnDelete_Click" />--%>
    </div>
</div>



<!--Popup Layer-->
<asp:Panel ID="Panel1" runat="server" Style="display: none">
            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup">
                <div>
                    <p>
                        Are you sure to send the <asp:Literal ID="litHint" runat="server"></asp:Literal> letters?</p>
                    <p style="text-align: center;">
                        <asp:Button ID="OkButton" Width="100" runat="server" Text="OK" OnClick="OkButton_Click" />
                        <asp:Button ID="CancelButton" Width="100" runat="server" Text="Cancel" />
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
 <!--Popup Layer-->
</asp:Content>