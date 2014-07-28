<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OffLineHiring.aspx.cs" Inherits="OffLineHiring"
    MasterPageFile="~/SpringfieldMaster.master" %>

<%@ Register Src="Controls/CheckInFormView.ascx" TagName="CheckInFormView" TagPrefix="uc2" %>
<%@ Register Src="Controls/CheckInFormEdit.ascx" TagName="CheckInFormEdit" TagPrefix="uc1" %>
<%@ Register TagPrefix="SFUserControl" TagName="CollegeSelector" Src="~/Controls/CollegeSelector.ascx" %>
<asp:Content ID="cntOffLineHiring" runat="server" ContentPlaceHolderID="mainPlaceHolder">
    <div>
        <ul>
            <li>This page provides the interface when you'd like to hire an intern whom is not in
                system. </li>
            <li>You can fill in this page and submit your recruiting request to your manager for
                approval directly. </li>
            <li><b>Items with yellow background are required fields.</b> </li>
        </ul>
        <div id="ch_title" class="panel_title_expand">
            <asp:Label ID="lbTitle" runat="server" Text="Off-Line Hiring"></asp:Label>
        </div>
        <div id="ch_content" class="panel_content">
            <table class="applicants_table">
                <tr>
                    <td>
                        First Name:
                    </td>
                    <td>
                        <asp:TextBox ID="tbFirstName" runat="server" CssClass="required_input"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revFirstName" runat="server" ControlToValidate="tbFirstName"
                            ValidationExpression="^(?!-)(?!.*?-$)[ a-zA-Z0-9-,\u4e00-\u9fa5]+$" ErrorMessage="incorrect format"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator SetFocusOnError="true" Display="Static" ControlToValidate="tbFirstName" ID="rfvFirstName"
                            runat="server" ErrorMessage="First Name should not be empty!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Last Name:
                    </td>
                    <td>
                        <asp:TextBox ID="tbLastName" runat="server" CssClass="required_input"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revLastName" runat="server" ControlToValidate="tbLastName"
                            ValidationExpression="^(?!-)(?!.*?-$)[ a-zA-Z0-9-,\u4e00-\u9fa5]+$" ErrorMessage="incorrect format"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ControlToValidate="tbLastName" ID="rfvLastName" runat="server"
                            ErrorMessage="Last Name should not be empty!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Name in Chinese:
                    </td>
                    <td>
                        <asp:TextBox ID="tbChineseName" runat="server" CssClass="required_input"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ControlToValidate="tbChineseName" ID="RequiredFieldValidator1"
                            runat="server" ErrorMessage="Name in Chinese should not be empty!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Gender:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGender" runat="server">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        University:
                    </td>
                    <td>
                        <%--                    <asp:DropDownList ID="ddlUniversity" runat="server">
                    </asp:DropDownList>--%>
                        <SFUserControl:CollegeSelector ID="cs" runat="server" ValidationGroup="Applicant" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 39px">
                        Major Category:
                    </td>
                    <td style="height: 39px">
                        <asp:DropDownList ID="ddlMajor" runat="server">
                            <asp:ListItem Text="Computer & EE Related" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Automation Related" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Math Related" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Physical Related" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Liberal Related" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Other" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Major:
                    </td>
                    <td>
                        <asp:TextBox ID="tbMajor" runat="server" CssClass="required_input"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ControlToValidate="tbMajor" ID="rfvMajor" runat="server"
                            ErrorMessage="Major should not be empty!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Degree:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDegree" runat="server">
                            <asp:ListItem Text="BS" Value="0"></asp:ListItem>
                            <asp:ListItem Text="MS" Value="1"></asp:ListItem>
                            <asp:ListItem Text="PHD" Value="2"></asp:ListItem>
                            <asp:ListItem Text="MBA" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Double BS" Value="4"></asp:ListItem>
                            <asp:ListItem Text="MS + PhD" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Diploma" Value="6"></asp:ListItem>
                            <asp:ListItem Text="Other" Value="7"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Grade:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGrade" runat="server">
                            <asp:ListItem Value="1">01</asp:ListItem>
                            <asp:ListItem Value="2">02</asp:ListItem>
                            <asp:ListItem Value="3">03</asp:ListItem>
                            <asp:ListItem Value="4">04</asp:ListItem>
                            <asp:ListItem Value="5">05</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Enroll date in university:</td>
                    <td>
                        <asp:TextBox ID="tbEnrollDate" runat="server" CssClass="required_input"></asp:TextBox>&nbsp;
                        <input type="button" value="Select" id="btnEnrollDate" name="btnEnrollDate" runat="server" />
                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvEnrollDate" runat="server" ControlToValidate="tbEnrollDate"
                            ErrorMessage="required!" Display="Static" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Graduation date in university:</td>
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
                        Email:
                    </td>
                    <td>
                        <asp:TextBox ID="tbEmail" runat="server" CssClass="required_input"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ControlToValidate="tbEmail" ID="rfvEmail" runat="server"
                            ErrorMessage="Email should not be empty!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Email address illegal"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="tbEmail"
                            ValidationGroup="Applicant"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td>
                        Phone Number:
                    </td>
                    <td>
                        <asp:TextBox ID="tbPhone" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Group Manager:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGroupManager" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Group Manager's Approval:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGMApproval" runat="server" OnSelectedIndexChanged="ddlGMApproval_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="Group Manager Approval Online" Value="online"></asp:ListItem>
                            <asp:ListItem Text="Upload Group Manager Approval Email" Value="email"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lbApprovalEmail" Text="Email File: " Visible="false" runat="server"></asp:Label>
                        <asp:FileUpload ID="fuApprovalEmail" runat="server" Visible="false" />
                        <asp:Label ID="lbGMApproval" runat="server" ForeColor="red"></asp:Label>
                        <asp:RegularExpressionValidator ControlToValidate="fuApprovalEmail" ValidationExpression="^(.*\.(((d|D)(o|O)(c|C)(x|X)?)|((p|P)(d|D)(f|F))|(t|T)(x|X)(t|T)))?$"
                            ID="revApprovalEmail" runat="server" ErrorMessage="Only DOC,DOCX,PDF,TXT document is allowed!"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Resume:
                    </td>
                    <td>
                        <asp:FileUpload ID="fuResume" runat="server" CssClass="required_input" />
                        <asp:Label ID="lbResume" runat="server" ForeColor="red"></asp:Label>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ControlToValidate="fuResume" ID="RequiredFieldValidatorResume"
                            runat="server" ErrorMessage="Upload file should not be empty!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ControlToValidate="fuResume" ValidationExpression="^.*\.(((d|D)(o|O)(c|C)(x|X)?)|((p|P)(d|D)(f|F))|(t|T)(x|X)(t|T))$"
                            ID="revResume" runat="server" ErrorMessage="Only DOC,DOCX,PDF,TXT resume is allowed!"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Comment:
                    </td>
                    <td>
                        You can fill this form to hire a student directly. A email will be automatically
                        sent to your group manager for his/her approval.<br />
                        <asp:TextBox ID="tbComment" runat="server" Columns="36" Rows="6" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel ID="up1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnPreview" />
                                <asp:AsyncPostBackTrigger ControlID="btnBack" />
                                <%-- <asp:AsyncPostBackTrigger ControlID="btnHire" /> --%>
                            </Triggers>
                            <ContentTemplate>
                                <uc1:CheckInFormEdit ID="CheckInFormEdit1" runat="server" />
                                <uc2:CheckInFormView ID="CheckInFormView1" runat="server" Visible="False" />
                                <div style="text-align: right; float: right">
                                    <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click"
                                        ValidationGroup="Applicant" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" Visible="False" OnClick="btnBack_Click" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button ID="btnHire" runat="server" Text="Submit Request" OnClick="btnHire_Click"
                            ValidationGroup="Applicant" CausesValidation="true" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
