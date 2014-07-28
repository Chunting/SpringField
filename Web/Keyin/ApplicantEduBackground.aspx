<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicantEduBackground.aspx.cs" Inherits="PageApplicantEduBackground" MasterPageFile="~/Keyin/ApplicantPortalMaster.master" %>
<%@ Register TagPrefix="SFUserControl" TagName="CollegeSelector" Src="~/Keyin/UserControls/CollegeSelector.ascx" %>

<asp:Content ContentPlaceHolderID="mainHolder" ID="cntEduBkgrd" runat="server">
<div style="width: 100%;">
<div id="ch_title" class="panel_title_expand">
    Step 2: Application Education Background
</div>
<div id="ch_content" class="panel_content">

    <table class="applicants_table">
        <tr>
            <td>
                College:
            </td>
            <td>
<%--                <asp:DropDownList ID="ddlCollege" runat="server">
                </asp:DropDownList>--%>
                <SFUserControl:CollegeSelector ID="cs" runat="server" ValidationGroup="EduBackground" />
            </td>
        </tr>
        <tr>
            <td style="height: 49px">
                Major Category:
            </td>
            <td style="height: 49px">
                <asp:DropDownList ID="ddlMajorCategory" runat="server">
                    <asp:ListItem Selected="true">Computer,EE Related</asp:ListItem>
                    <asp:ListItem>Automation Related</asp:ListItem>
                    <asp:ListItem>Math Related</asp:ListItem>
                    <asp:ListItem>Physical Related</asp:ListItem>
                    <asp:ListItem>Liberal Related</asp:ListItem>
                    <asp:ListItem>Other</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Major:
            </td>
            <td>
                <asp:TextBox ID="tbMajor" runat="server" Columns="36"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="rfvMajor" runat="server" ErrorMessage="*" ControlToValidate="tbMajor" ValidationGroup="EduBackground"></asp:RequiredFieldValidator>
                
            </td>
        </tr>
        <tr>
            <td>
                Degree:
            </td>
            <td>
                    <asp:DropDownList ID="ddlDegree" runat="server" >
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
                Enroll Date:
            </td>
            <td>
                <asp:TextBox ID="tbEnrollDate" runat="server"></asp:TextBox>
                <input type="button" value="Select" id="btnSelectDate2" name="btnSelectDate2" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="tbEnrollDate" ValidationGroup="EduBackground"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="height: 36px">
                Graduate Date:
            </td>
            <td style="height: 36px">
                <asp:TextBox ID="tbGraduateDate" runat="server"></asp:TextBox>
                <input type="button" value="Select" id="btnSelectDate" name="btnSelectDate" runat="server" />
                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="*" ControlToValidate="tbGraduateDate" ValidationGroup="EduBackground"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <%--Removed--%>
        <%--
        <tr>
            <td>
                Rank:
            </td>
            <td>
                <asp:DropDownList ID="ddlRank" runat="server">
                 <asp:ListItem Selected="True">Top5</asp:ListItem>
                 <asp:ListItem>Top10</asp:ListItem>
                 <asp:ListItem>Top20</asp:ListItem>
                 <asp:ListItem>Top30</asp:ListItem>
                 <asp:ListItem>Other</asp:ListItem>
                </asp:DropDownList>
            </td>
         </tr>
          --%>
         
         <%--
         <tr>
             <td>
                 Research Approach:
             </td>
             <td>
                <asp:DropDownList ID="ddlResearchApproach" runat="server">
                 <asp:ListItem Selected="True">Theory</asp:ListItem>
                 <asp:ListItem>Practice</asp:ListItem>
                </asp:DropDownList>
             </td>
         </tr>
          --%>
         <tr>
             <td>
                 Advisor's First Name:
             </td>
             <td>
                 <asp:TextBox ID="tbAdvisorFirstName" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvAdvisorFirstName" runat="server" ControlToValidate="tbAdvisorFirstName"
                     ErrorMessage="*" ToolTip="First Name is required." ValidationGroup="EduBackground" Enabled="False"></asp:RequiredFieldValidator>
             </td>
         </tr>
         <tr>
             <td>
                 Advisor's Last Name
             </td>
             <td>
                 <asp:TextBox ID="tbAdvisorLastName" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvAdvisorLastName" runat="server" ControlToValidate="tbAdvisorLastName"
                     ErrorMessage="*" ToolTip="Last Name is required." ValidationGroup="EduBackground" Enabled="False"></asp:RequiredFieldValidator>
             </td>
         </tr>
         <tr>
             <td>
                 Advisor's Email:
             </td>
             <td>
                 <asp:TextBox ID="tbAdvisorEmail" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvAdvisorEmail" runat="server" ControlToValidate="tbAdvisorEmail"
                ErrorMessage="*" ValidationGroup="EduBackground" Enabled="False"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator 
                 id="revEmail" runat="server" 
                 ErrorMessage="The mail is not correct" 
                 ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                 ControlToValidate="tbAdvisorEmail"
                 ValidationGroup="EduBackground"></ASP:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td style="height: 35px">
                 Advisor's Organization:
             </td>
             <td style="height: 35px">
                 <asp:TextBox ID="tbOrganization" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvOrg" runat="server" ControlToValidate="tbOrganization"
                     ErrorMessage="*" ValidationGroup="EduBackground" Enabled="False"></asp:RequiredFieldValidator>
             </td>
         </tr>
         <tr>
            <td>
                Resume:
            </td>
            <td>
                <asp:FileUpload ID="fuResume" runat="server" />
                <asp:Button ID="btnAddResume" runat="server" Text="Upload" OnClick="btnAddResume_Click" CausesValidation="False" />
                <asp:RegularExpressionValidator ControlToValidate="fuResume" ValidationExpression="^(.*\.(((d|D)(o|O)(c|C)(x|X)?)|((p|P)(d|D)(f|F))|(t|T)(x|X)(t|T)))?$"
                            ID="revResume" runat="server" ErrorMessage="Only DOC,DOCX,PDF,TXT document is allowed!"></asp:RegularExpressionValidator>
                <asp:Label ID="lbResume" runat="server" Text=""></asp:Label>
            </td>
         </tr>
         <tr>
            <td>
                PaperA:
            </td>
            <td>
                <asp:FileUpload ID="fuPaperA" runat="server" />
                <asp:Button ID="btnPaperA" runat="server" Text="Upload" OnClick="btnPaperA_Click" CausesValidation="False" />
                <asp:RegularExpressionValidator ControlToValidate="fuPaperA" ValidationExpression="^(.*\.(((d|D)(o|O)(c|C)(x|X)?)|((p|P)(d|D)(f|F))|(t|T)(x|X)(t|T)))?$"
                            ID="revPaperA" runat="server" ErrorMessage="Only DOC,DOCX,PDF,TXT document is allowed!"></asp:RegularExpressionValidator>
                <asp:Label ID="lbPaperA" runat="server" Text=""></asp:Label>
            </td>
         </tr>
         <tr>
            <td>
                PaperB:
            </td>
            <td>
                <asp:FileUpload ID="fuPaperB" runat="server" />
                <asp:Button ID="btnPaperB" runat="server" Text="Upload" OnClick="btnPaperB_Click" CausesValidation="False" />
                <asp:RegularExpressionValidator ControlToValidate="fuPaperB" ValidationExpression="^(.*\.(((d|D)(o|O)(c|C)(x|X)?)|((p|P)(d|D)(f|F))|(t|T)(x|X)(t|T)))?$"
                            ID="revPaperB" runat="server" ErrorMessage="Only DOC,DOCX,PDF,TXT document is allowed!"></asp:RegularExpressionValidator>
                <asp:Label ID="lbPaperB" runat="server" Text=""></asp:Label>
            </td>
         </tr>
         <tr>
            <td colspan="2">
               <asp:Button ID="btnSubmit" runat="server" Text="Next Step" OnClick="btnSubmit_Click" ValidationGroup="EduBackground" CausesValidation="true" />
<%--               <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/ApplicantInformationView.aspx" CssClass="support_link">Back to main page</asp:HyperLink>
--%>               <br />
               <asp:Label ID="lbMsg" runat="server" Text="" EnableViewState="false" ForeColor="red"></asp:Label>
            </td>
         </tr>
    </table>
    
</div>
</div>
We suggest you combine the english & chinese resume together, the system will only accept ONE resume!

</asp:Content>
