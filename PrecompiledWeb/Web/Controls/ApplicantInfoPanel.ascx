<%@ control language="C#" autoeventwireup="true" inherits="Controls_ApplicantInfoPanel, App_Web_eecyfkj6" %>
<div id="basic_info">
    <div id="filter_title" class="panel_title_expand">
        Applicant Personal Information
    </div>
    <div id="basic_content" class="panel_content">
        <table class="applicants_table">
           <%-- <tr>
                <td width="20%">
                    First Name:</td>
                <td>
                    <asp:Label ID="lbFirstName" runat="server" Text=""></asp:Label>
                </td>
                <td>
                Last Name:</td>
            <td>
                <asp:Label ID="lbLastName" runat="server" Text=""></asp:Label></td>
            </tr>--%>
            <tr>
                <td>
                    Name in Chinese:
                </td>
                <td>
                    <asp:Label ID="lbChineseName" runat="server" Text=""></asp:Label></td>
                <td width="20%">
                    Gender:
                </td>
                <td>
                    <asp:Label ID="lbGender" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <%--<td>
                    ID Card:
                </td>
                <td>
                    <asp:Label ID="lbIDNum" runat="server" Text=""></asp:Label></td>--%>
                <td>
                    Nationality:
                </td>
                <td>
                    <asp:Label ID="lbNationality" runat="server" Text=""></asp:Label></td>
                    <td>
                    Web Page:
                </td>
                <td>
                    <asp:Label ID="lbWebPage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                
                <td>
                    Email:</td>
                <td>
                    <asp:Label ID="lbEmail" runat="server" Text=""></asp:Label></td>
                     <td>
                    Contact Phone Num:</td>
                <td>
                    <asp:Label ID="lbPhoneNum" runat="server" Text=""></asp:Label></td>
            </tr>
           <%-- <tr>
               
               <td>
                    Address:
                </td>
                <td>
                    <asp:Label ID="lbAddress" runat="server" Text=""></asp:Label></td>
            </tr>--%>
            <tr>
                <td>
                    Current City:
                </td>
                <td>
                    <asp:Label ID="lbCity" runat="server" Text=""></asp:Label></td>
                     <td>
                    Current Country:
                </td>
                <td>
                    <asp:Label ID="lbCountry" runat="server" Text=""></asp:Label></td>
                <%--<td>
                    Current Province:
                </td>
                <td>
                    <asp:Label ID="lbProvince" runat="server" Text=""></asp:Label></td>--%>
            </tr>
        </table>
    </div>
</div>
<h3>
</h3>
<div id="edu_info">
    <div id="edubg" class="panel_title_expand">
        Applicant Education Background
    </div>
    <div id="edu_content" class="panel_content">
        <table class="applicants_table">
            <tr>
                <td width="20%">
                    Highest Educational Institution (College):
                </td>
                <td>
                    <asp:Label ID="lbEduIns" runat="server" Text=""></asp:Label></td>
                <td>
                    Major Category:
                </td>
                <td>
                    <asp:Label ID="lbMajorCategory" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>
                    Major:
                </td>
                <td>
                    <asp:Label ID="lbMajor" runat="server" Text=""></asp:Label></td>
                <td>
                    Grade:
                </td>
                <td>
                    <asp:Label ID="lbYearOfStudy" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>
                    Enroll Date:
                </td>
                <td>
                    <asp:Label ID="lbEnrollDate" runat="server" Text=""></asp:Label></td>
                <td>
                    Graduate Date:
                </td>
                <td>
                    <asp:Label ID="lbGraduateDate" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>
                    Rank:
                </td>
                <td>
                    <asp:Label ID="lbRank" runat="server" Text=""></asp:Label></td>
                <td>
                    Advisor First Name:
                </td>
                <td>
                    <asp:Label ID="lbAdvisorFirstName" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>
                    Advisor Last Name:
                </td>
                <td>
                    <asp:Label ID="lbAdvisorLastName" runat="server" Text=""></asp:Label></td>
                <td>
                    Advisor Email:
                </td>
                <td>
                    <asp:Label ID="lbAdvisorEmail" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>
                    Advisor Organization:
                </td>
                <td>
                    <asp:Label ID="lbOrg" runat="server" Text=""></asp:Label></td>
                <td>
                    Resume:</td>
                <td>
                    &nbsp;<asp:HyperLink ID="lnkCV" runat="server"></asp:HyperLink></td>
            </tr>
            <tr>
                <td style="height: 21px">
                    Paper 1:</td>
                <td style="height: 21px">
                    &nbsp;<asp:HyperLink ID="lnkPaperA" runat="server"></asp:HyperLink></td>
                <td style="height: 21px">
                    Paper 2:
                </td>
                <td style="height: 21px">
                    &nbsp;<asp:HyperLink ID="lnkPaperB" runat="server"></asp:HyperLink></td>
            </tr>
            <tr>
                <td>
                    Program Participated:</td>
                <td colspan="3">
                    <asp:Label ID="lbSpecialProgram" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </table>
    </div>
</div>
<h3>
</h3>
<div id="relate_info">
    <div id="related_title" class="panel_title_expand">
        Applicant Related Info
    </div>
    <div id="related_content" class="panel_content">
        <table class="applicants_table">
            <tr>
                <td width="20%">
                    Preferred Internship Start Date:
                </td>
                <td>
                    <asp:Label ID="lbFirstAvail" runat="server" Text=""></asp:Label></td>
                <td>
                    Preferred Internship End Date:
                </td>
                <td>
                    <asp:Label ID="lbSecondAvail" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>
                    Interested Research Group:
                </td>
                <td>
                    <asp:Label ID="lbGroup" runat="server" Text=""></asp:Label></td>
                <td>
                    Interested Areas:
                </td>
                <td>
                    <asp:Label ID="lbAreas" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>
                    Apply For:
                </td>
                <td>
                    <asp:Label ID="lbInternType" runat="server" Text=""></asp:Label></td>
                <td>
                    Get Information From:
                </td>
                <td>
                    <asp:Label ID="lbInfoSource" runat="server" Text=""></asp:Label></td>
            </tr>
             <tr>
                <td>
                    Preferred Position:
                </td>
                <td colspan="3">
                    <asp:Label ID="lbPreferredPosition" runat="server" Text=""></asp:Label>
                </td>
                
            </tr>
        </table>
    </div>
</div>
