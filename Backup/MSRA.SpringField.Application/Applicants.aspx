<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Applicants" MasterPageFile="~/SpringfieldMaster.master" Codebehind="Applicants.aspx.cs" %>

<%@ Register Src="Controls/ApplicantsList.ascx" TagName="ApplicantsList" TagPrefix="uc1" %>
<%@ Register TagPrefix="SFUserControl" TagName="CollegeSelector" Src="~/Controls/CollegeSelector.ascx" %>
<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
   
<script type="text/javascript">
    function CauseValidationForUniversityName() {
        var obj = document.getElementById("ctl00_mainPlaceHolder_cs_tbCollegeName");
        if (obj != null && obj.disabled == false) {
            if (obj.value == "") {
                alert("Please input a university name!");
                return false;
            }
            else
                return true;
        }
    } 
</script>
<div id="filter">
<!--onclick="ChangeStyle(this,'filter_content')"-->
   <div id="filter_title" class="panel_title_expand" style="border-bottom:none">Filter Condition</div>
   <div id="filter_content" class="panel_content" style="display:block;border-top:none;border-bottom:none">
   <table class="applicants_table">
        <tr>
            <td class="bold_font">
            Status:
            </td>
            <td>         
            <asp:DropDownList ID="ddlStatus" runat="server" Width="100%">
                <asp:ListItem Text="All" Value=""  Selected="False"></asp:ListItem>
                <asp:ListItem Text="Available" Value="1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Waiting For Interview Feedback" Value="2"></asp:ListItem>
                <asp:ListItem Text="Waiting For Mentor Decision" Value="3"></asp:ListItem>
                <asp:ListItem Text="Waiting For Group Manager Decision" Value="4"></asp:ListItem>
                <asp:ListItem Text="Hired" Value="5"></asp:ListItem>
                <%--Add by Yuanqin,2011.5.5; add a new status--%>
                <asp:ListItem Text="Qualified But Not Matched" Value="6"></asp:ListItem> 
                <asp:ListItem Text="Rejected" Value="7"></asp:ListItem>
                <asp:ListItem Text="Offer Declined" Value="8"></asp:ListItem>
                <asp:ListItem Text="On Board" Value="9"></asp:ListItem>
            </asp:DropDownList>
            </td>
            <td class="bold_font">
                Major Category:
            </td>
            <td>
                <asp:DropDownList ID="ddlMajor" runat="server" Width="100%">
                    <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
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
            <td class="bold_font" style="height: 33px">
              Interested Group:
            </td>
            <td>
              <asp:DropDownList ID="ddlGroup" runat="server" Width="100%" 
                OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="false">
              </asp:DropDownList>
              <div runat="server" id="div_OtherGorup" visible="false">
              Please specify:<asp:TextBox ID="tb_OtherGorup" runat="server"></asp:TextBox>
              </div>
            </td>
            
            <td class="bold_font" style="height: 33px">
              Degree:
            </td>
            <td style="height: 33px">
              <asp:DropDownList ID="ddlDegree" runat="server" Width="100%">
                <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
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
            <td class="bold_font">Applicant Name:</td>
            <td><asp:TextBox ID="tbName" runat="server"></asp:TextBox></td>
            <td class="bold_font">
                Interested Area:
            </td>
            <td>
                <asp:TextBox ID="tbArea" runat="server"></asp:TextBox>
            </td>
        </tr>
        <%--<asp:TextBox ID="tbResume" runat="server" Visible=false ></asp:TextBox>--%>
        <!--Add "Available Date:" by Yuanqin -->
        <tr>
            <td class="bold_font">
                Available Date:
            </td>
            <td>
                <asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
                <%--<input type="button" value="Select" id="btnEnrollDate" name="btnEnrollDate" runat="server" />--%>
            </td>
            <td class="bold_font">
                Internship Duration:
            </td>
            <td>
                <asp:DropDownList ID="ddlTimeSpan" runat="server" Width="100%">
                    <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="0 - 3M" Value="1"></asp:ListItem>
                    <asp:ListItem Text="3 - 6M" Value="2"></asp:ListItem>
                    <asp:ListItem Text="6 - 12M" Value="3"></asp:ListItem>
                    <asp:ListItem Text="One year above" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <!--Add "Apply Date:" by Yuanqin, 2011.3.15 -->
        <tr>
            <td class="bold_font">
                Apply Date:
            </td>
            <td colspan="3">
                From :
                <asp:TextBox ID="tbStartApplyDate" runat="server" ReadOnly="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp; To :
                <asp:TextBox ID="tbEndApplyDate" runat="server" ReadOnly="false"></asp:TextBox><br />
                <asp:CompareValidator ID="cmpDate1" runat="server" ControlToValidate="tbEndApplyDate"
                    ControlToCompare="tbStartApplyDate" Operator="GreaterThan" SetFocusOnError="true"
                    ErrorMessage="End date must be greater than start date!" Type="Date" Display="Dynamic" />   
            </td>
        </tr>
       <tr>
            <td class="bold_font">
                Keywords in Resume:
            </td>
            <td>
                <asp:TextBox ID="tbResume" runat="server"></asp:TextBox>
            </td>
            <td class="bold_font">
                Intern Position:
            </td>
            <td>
                <asp:DropDownList ID="ddlPosition" runat="server" Width="100%">
                    <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="ResearchIntern" Value="1"></asp:ListItem>
                    <asp:ListItem Text="EngineeringIntern" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Unknown" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="bold_font" style="height: 69px">University:</td>
            <td colspan="3" style="height: 69px"><SFUserControl:CollegeSelector ID="cs" runat="server" ValidationGroup="Applicant" /></td>
        </tr>        
        <tr>
            <td colspan="4" style="text-align: center;height:40px">
                <div style="width:120px;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                     <asp:ImageButton Width="24" Height="24" CssClass="img_icon" ImageUrl="~/Resource/Images/filter.png" ID="btnFilter"
                            runat="server" AlternateText="Schedule Interview" OnClientClick="return CauseValidationForUniversityName()" OnClick="btnApplyFilter_Click" CausesValidation="true" ImageAlign="AbsMiddle" />
                            <label for="<%=btnFilter.ClientID %>"><span style="cursor:hand">Apply Filter</span></label>
                </div>
            </td>
        </tr>
    </table>
    </div>
</div>

    <uc1:ApplicantsList ID="ApplicantsList1" runat="server" OnPagerClickChanged="ApplicantsList1_PagerClick" ListType="applicantlist" />

</asp:Content>