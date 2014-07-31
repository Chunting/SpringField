<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.OnBoardAction"
    MasterPageFile="~/SpringfieldMaster.master" Codebehind="OnBoardAction.aspx.cs" %>
    
<%@ Register Src="Controls/CheckInFormView.ascx" TagName="CheckInFormView" TagPrefix="uc2" %>
<%@ Register Src="Controls/CheckInFormEdit.ascx" TagName="CheckInFormEdit" TagPrefix="uc1" %>
<asp:Content ID="cntOffLineHiring" runat="server" ContentPlaceHolderID="mainPlaceHolder">
    <div style="margin: 0 0 8 0">
        <ul>
            <li>This page provides the interface when the intern recruiter'd like to change the applicant's status
            which isn't complied with the normal process. </li>
            <li><b>Items with yellow background are required fields.</b> </li>
        </ul>
        <div id="ch_title" class="panel_title_expand">
            <asp:Label ID="lbTitle" runat="server" Text="On-Board Action"></asp:Label>
        </div>
        <div id="ch_content" class="panel_content">
            <table class="applicants_table"> 
                <tr>
                    <td >
                        First Name:
                    </td>
                    <td>
                        <asp:TextBox ID="tbFirstName" runat="server" ></asp:TextBox>                      
                    </td>
                </tr>
                <tr>
                    <td>
                        Last Name:
                    </td>
                    <td>
                        <asp:TextBox ID="tbLastName" runat="server" ></asp:TextBox>                     
                    </td>
                </tr>
                <tr>
                    <td>
                        Name in Chinese:
                    </td>
                    <td>
                        <asp:TextBox ID="tbChineseName" runat="server" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        University:
                    </td>
                    <td>
                        <asp:TextBox ID="tbUniversity" runat="server" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Major:
                    </td>
                    <td>
                        <asp:TextBox ID="tbMajor" runat="server" ></asp:TextBox>
                    </td>
                </tr> 
                <tr>
                    <td>
                        Interview Start Date:</td>
                    <td>
                        <asp:TextBox ID="tbEnrollDate" runat="server" CssClass="required_input"></asp:TextBox>&nbsp;
                        <input type="button" value="Select" id="btnEnrollDate" name="btnEnrollDate" runat="server" />
                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvEnrollDate" runat="server" ControlToValidate="tbEnrollDate"
                            ErrorMessage="required!" Display="Static" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Interview End Date:</td>
                    <td>
                        <asp:TextBox ID="tbGraduateDate" runat="server" CssClass="required_input"></asp:TextBox>&nbsp;
                        <input type="button" value="Select" id="btnGraduateDate" name="btnGraduateDate" runat="server" />
                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvGraduateDate" runat="server" ControlToValidate="tbGraduateDate"
                            ErrorMessage="required!" Display="Static" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ValidationGroup="Applicant" ID="cmpDate" runat="server"
                            Text="Graduation Date must be greater than Enroll Date!" ControlToValidate="tbGraduateDate"
                            ControlToCompare="tbEnrollDate" Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                    </td>
                </tr>           
                <tr>
                    <td>
                        Group Manager:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGroupManager" runat="server" CssClass="required_input">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Comment:
                    </td>
                    <td>
                        Hire Manager Comments<br />
                        <asp:TextBox ID="tbComment" runat="server" Columns="36" Rows="6" TextMode="MultiLine" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:UpdatePanel ID="up1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnPreviewRequest" />
                                <asp:AsyncPostBackTrigger ControlID="btnRequestBack" />
                                <%-- <asp:AsyncPostBackTrigger ControlID="btnHire" /> --%>
                            </Triggers>
                            <ContentTemplate>
                                <uc1:CheckInFormEdit ID="CheckInFormEdit1" runat="server" />
                                <uc2:CheckInFormView ID="CheckInFormView1" runat="server" Visible="False" />
                                <div style="text-align: right; float: right">
                                    <div style="text-align:center;padding:0 10;height:30;width:120px" runat="server" id="divPreview"
                                        onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                                        onmouseout="this.style.borderWidth='0';">
                                        <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/preview.png" ID="btnPreviewRequest"
                                            runat="server" ValidationGroup="Applicant" AlternateText="Preview" OnClick="btnPreview_Click" ImageAlign="AbsMiddle" />
                                            <label for="<%=btnPreviewRequest.ClientID %>"><span style="cursor:hand">Preview</span></label>
                                    </div>
                                    <%--<asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" ValidationGroup="Applicant" />--%>
                                    
                                     <div style="text-align:center;padding:0 10;height:30;width:120px" runat="server" id="btnBack" visible="false"
                                        onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                                        onmouseout="this.style.borderWidth='0';">
                                        <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/back.png" ID="btnRequestBack"
                                            runat="server" ValidationGroup="Applicant" AlternateText="Preview" OnClick="btnBack_Click" ImageAlign="AbsMiddle" />
                                            <label for="<%=btnRequestBack.ClientID %>"><span style="cursor:hand">Back</span></label>
                                    </div>
                                    <%--<asp:Button ID="btnBack" runat="server" Text="Back" Visible="False" OnClick="btnBack_Click" />--%>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                        <div style="padding:0 10;height:30;width:180px"
                            onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                            onmouseout="this.style.borderWidth='0';">
                            <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/submit.png" ID="btnOfflineHire"
                                runat="server" ValidationGroup="Applicant" AlternateText="Submit Request" OnClick="btnHire_Click" ImageAlign="AbsMiddle" />
                                <label for="<%=btnOfflineHire.ClientID %>"><span style="cursor:hand">Submit Request</span></label>
                        </div>                        
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
