<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.ReferApplication" MasterPageFile="~/SpringfieldMaster.master" Codebehind="ReferApplication.aspx.cs" %>
<%@ Register TagPrefix="SFUserControl" TagName="CollegeSelector" Src="~/Controls/CollegeSelector.ascx" %>

<asp:Content ID="cntReferral" runat="server" ContentPlaceHolderID="mainPlaceHolder">
    <div>
    <ul>
    <li>
    This page provides an interface when you get a referral from Managers or key customer. 
    </li>
    <li>
    When you use this page to refer applicant, this applicant will be recorded as high priority candidate. 
    </li>
    </ul>
    <div id="ch_title" class="panel_title_expand">
    Key Referral
    </div>
    <div id="ch_content" class="panel_content">
        <table class="applicants_table">
            <tr>
                <td>
                    First Name:
                </td>
                <td>
                    <asp:TextBox ID="tbFirstName" runat="server" CssClass="required_input"></asp:TextBox>
                    <asp:RequiredFieldValidator Display="Static" ControlToValidate="tbFirstName" ID="rfvFirstName" runat="server" ErrorMessage="Required!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Last Name:
                </td>
                <td>
                    <asp:TextBox ID="tbLastName" runat="server" CssClass="required_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tbLastName" ID="rfvLastName" runat="server" ErrorMessage="Required!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="height: 39px">
                    Name in Chinese:
                </td>
                <td style="height: 39px">
                    <asp:TextBox ID="tbChineseName" runat="server" CssClass="required_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tbChineseName" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    University:
                </td>
                <td>
<%--                    <asp:DropDownList ID="ddlUniversity" runat="server">
                    </asp:DropDownList>--%>
                    <SFUserControl:CollegeSelector ID="cs" runat="server" ValidationGroup="Applicant"></SFUserControl:CollegeSelector>
                </td>
            </tr>
            <tr>
                <td>
                    Major Category:
                </td>
                <td>
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
                    <asp:RequiredFieldValidator ControlToValidate="tbMajor" ID="rfvMajor" runat="server" ErrorMessage="Required!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
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
                <td>Enroll date in university:</td>
                <td>
                    <asp:TextBox ID="tbEnrollDate" runat="server" CssClass="required_input"></asp:TextBox>&nbsp;
                    <input type="button" value="Select" id="btnEnrollDate" name="btnEnrollDate" runat="server"  ValidationGroup="Applicant" />
                    <asp:RequiredFieldValidator ID="rfvEnrollDate" runat="server" ControlToValidate="tbEnrollDate" ErrorMessage="Required!" Display="Static" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Graduation date in university:</td>
                <td>
                    <asp:TextBox ID="tbGraduateDate" runat="server" CssClass="required_input"></asp:TextBox>&nbsp;
                    <input type="button" value="Select" id="btnGraduateDate" name="btnGraduateDate" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvGraduateDate" runat="server" ControlToValidate="tbGraduateDate"
                        Display="Static" ErrorMessage="Required!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                </td>
            </tr>
            
            <tr>
                <td>
                    Email:
                </td>
                <td>
                    <asp:TextBox ID="tbEmail" runat="server" CssClass="required_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tbEmail" ID="rfvEmail" runat="server" ErrorMessage="Required!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Email address illegal" ControlToValidate="tbEmail" ValidationGroup="Applicant" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
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
                    Resume:
                </td>
                <td>
                    <asp:FileUpload ID="fuResume" runat="server" CssClass="required_input" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="fuResume" ValidationGroup="Applicant" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ControlToValidate="fuResume" ValidationExpression="^.*\.(((d|D)(o|O)(c|C)(x|X)?)|((p|P)(d|D)(f|F))|(t|T)(x|X)(t|T))$" ID="revResume" runat="server" ErrorMessage="Only DOC,DOCX,PDF,TXT resume is allowed!"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Referrer<br />
                    First Name:
                </td>
                <td>
                    <asp:TextBox ID="tbReferrerFirstName" runat="server" CssClass="required_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tbReferrerFirstName" ID="rfvReferrerFN" runat="server" ErrorMessage="Required!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Referrer<br />
                    Last Name:
                </td>
                <td>
                    <asp:TextBox ID="tbReferrerLastName" runat="server" CssClass="required_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tbReferrerLastName" ID="rfvReferrerLN" runat="server" ErrorMessage="Required!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                </td>
            </tr>  
            <tr>
                <td style="height: 48px">
                    Referrer Email:
                </td>
                <td style="height: 48px">
                    <asp:TextBox ID="tbReferrerEmail" runat="server" CssClass="required_input"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revReferrerEmail" runat="server" ErrorMessage="Email address illegal" ControlToValidate="tbReferrerEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Applicant"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td style="height: 143px">
                    Refer To:
                </td>
                <td style="height: 143px">
                    You can send this recommendation to several hiring managers. Please add the alias
                    of hiring managers in the following text box, spliting the alias by ";".<br />
                    <asp:TextBox ID="tbAccepters" runat="server" Columns="36" Rows="6" TextMode="MultiLine" CssClass="required_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tbAccepters" ID="rfvAccepters" runat="server" ErrorMessage="Required!" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnRefer" runat="server" Text="Submit" CausesValidation="true" ValidationGroup="Applicant" OnClick="btnRefer_Click"  />
                </td>
            </tr>
        </table>
    </div>
    </div>

</asp:Content>
