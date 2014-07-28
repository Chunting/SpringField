<%@ Control Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Controls.CheckInFormEdit" CodeBehind="CheckInFormEdit.ascx.cs" %>

<script language="javascript">
    function SetUniqueRadioButton(nameregex, current) {
        re = new RegExp(nameregex);
        var s = 0;
        for (i = 0; i < document.forms[0].elements.length; i++) {
            elm = document.forms[0].elements[i]
            if (elm.type == 'radio') {
                if (re.test(elm.name)) {
                    s++;
                    elm.checked = false;
                }
            }
        }
        //alert(s);
        current.checked = true;
    }

    function IsAnyRadioButtonSet(nameregex) {
        re = new RegExp(nameregex);
        var s = 0;
        var isCheck = false;
        for (i = 0; i < document.forms[0].elements.length; i++) {
            elm = document.forms[0].elements[i]
            if (elm.type == 'radio') {
                if (re.test(elm.name)) {
                    if (elm.checked) {
                        isCheck = true;
                    }
                }
            }
        }
        //alert(s);
        return isCheck;
    }

    function FindInput(nameregex, type) {
        re = new RegExp(nameregex);

        for (i = 0; i < document.forms[0].elements.length; i++) {
            elm = document.forms[0].elements[i];
            if (elm.type == type) {
                if (re.test(elm.name)) {
                    return elm;
                }
            }
        }
        return null;
    }
    function CheckGroups(sender, args) {
        args.IsValid = IsAnyRadioButtonSet("Repeater1.*group");
    }
    function CheckProjects(sender, args) {
        args.IsValid = IsAnyRadioButtonSet("Repeater2.*SpecialProject");
    }
    function CheckPositions(sender, args) {
        args.IsValid = IsAnyRadioButtonSet("Repeater3.*Position");
    }
    function CheckAdvisorApproval(sender, args) {
        args.IsValid = IsAnyRadioButtonSet("AdvisorApproval");
    }
    function CheckInternType(sender, args) {
        args.IsValid = IsAnyRadioButtonSet("InternType");
    }

    /*
    function CheckEnrollAndGraduate(sender, args)
    {
    var enrollMon = FindInput("ddlEnrollMonth","select-one").value;
    var enrollYear = FindInput("ddlEnrollYear","select-one").value;
    var graduateMon = FindInput("ddlGraduateMonth","select-one").value;
    var graduateYear = FindInput("ddlGraduateYear","select-one").value;
    args.IsValid = (enrollYear<graduateYear) || (enrollYear== graduateYear&& enrollMon < graduateMon);
    }
    */

    function CheckCheckInAndLastWorkingDay(sender, args) {
        var f1 = FindInput("tbCheckInDay", "text").value;
        var checkInDay = Date.parse(f1);
        var f2 = FindInput("tbLastWorkingDay", "text").value;
        var lastWorkingDay = Date.parse(f2);

        args.IsValid = (f1 == "" || f2 == "") || (checkInDay < lastWorkingDay);

    }
</script>

<div style="margin: 0 0 8 0">
    <div class="panel_title_expand">
        MSRA New Intern On-board Request</div>
    <div class="panel_content">
        <table class="applicants_table" style="table-layout: fixed;">
            <tr>
                <td Style="width: 20%">Intern's group</td>
                <td class="required_input">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <HeaderTemplate>
                            <table border="0">
                                <tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <td style="border-style: none;">
                                <asp:RadioButton ID="RadioButton1" Style="width: 180px;" runat="server" Text="<%#Container.DataItem %>" GroupName="group" />
                            </td>
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <%# (Container.ItemIndex % 5 == 4) ? "</tr><tr>" : "" %></SeparatorTemplate>
                        <FooterTemplate></tr></table></FooterTemplate>
                    </asp:Repeater>
                    <asp:CustomValidator ID="cvGroups" ClientValidationFunction="CheckGroups" runat="server" ErrorMessage="Required!" ValidationGroup="Applicant"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>Intern's project</td>
                <td>
                    <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                        <HeaderTemplate>
                            <table border="0">
                                <tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <td style="border-style: none;">
                                <asp:RadioButton ID="RadioButton2" runat="server" Text="<%#Container.DataItem %>" GroupName="SpecialProject" /></td>
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <%# (Container.ItemIndex % 3 == 2) ? "</tr><tr>" : "" %></SeparatorTemplate>
                        <FooterTemplate></tr></table></FooterTemplate>
                    </asp:Repeater>
                    <asp:CustomValidator ID="CustomValidator1" ClientValidationFunction="CheckProjects" runat="server" ErrorMessage="Required!"
                        ValidationGroup="Applicant"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>Intern's position</td>
                <td class="required_input">
                    <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                        <HeaderTemplate>
                            <table border="0">
                                <tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <td style="border-style: none;">
                                <asp:RadioButton ID="RadioButton3" Style="width: 150px; overflow: visible;" runat="server" Text="<%#Container.DataItem %>"
                                    GroupName="Position" />
                            </td>
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <%# (Container.ItemIndex % 3 == 2) ? "</tr><tr>" : "" %></SeparatorTemplate>
                        <FooterTemplate></tr></table></FooterTemplate>
                    </asp:Repeater>
                    <asp:CustomValidator ID="CustomValidator2" ClientValidationFunction="CheckPositions" runat="server" ErrorMessage="Required!"
                        ValidationGroup="Applicant"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>Intern Type</td>
                <td class="required_input">
                    <asp:RadioButton ID="rdInternType1" runat="server" Text="Full-time" GroupName="InternType" />
                    <asp:RadioButton ID="rdInternType2" runat="server" Text="Part-time" GroupName="InternType" />
                    <asp:CustomValidator ID="CustomValidator3" ClientValidationFunction="CheckInternType" runat="server" ErrorMessage="Required!"
                        ValidationGroup="Applicant"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>Mentor</td>
                <td>
                    <asp:TextBox ID="tbMentor" runat="server" CssClass="required_input"></asp:TextBox>
                    (email alias.)
                    <asp:Label runat="server" ForeColor="red" ID="lbErrorReportMentor" EnableViewState="false"></asp:Label>
                    <asp:RequiredFieldValidator ID="rqfMentor" runat="server" ControlToValidate="tbMentor" ErrorMessage="Required!" Display="Static"
                        ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Preferred check-in date</td>
                <td>
                    <asp:TextBox ID="tbCheckInDay" runat="server" CssClass="required_input"></asp:TextBox>
                    &nbsp;
                    <input type="button" value="Select" id="btnCheckInDay" name="btnCheckInDay" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvCheckInDay" runat="server" ControlToValidate="tbCheckInDay" ErrorMessage="Required!" Display="Static"
                        ValidationGroup="Applicant"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Preferred last working day (at least three months)</td>
                <td>
                    <asp:TextBox ID="tbLastWorkingDay" runat="server" CssClass="required_input"></asp:TextBox>&nbsp;
                    <input type="button" value="Select" id="btnLastWorkingDay" name="btnLastWorkingDay" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvLastWorkingDay" runat="server" ControlToValidate="tbLastWorkingDay" ErrorMessage="Required!"
                        Display="Static" ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CustomValidator ID="CustomValidator7" ClientValidationFunction="CheckCheckInAndLastWorkingDay" runat="server" ErrorMessage="Preferred last working day is earlier than or equal to preferred check-in date!"
                        ValidationGroup="Applicant"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>Has he/she got the approval letter from his/her advisor/university?</td>
                <td class="required_input">
                    <asp:RadioButton ID="rdAdvisor1" runat="server" Text="Yes - Please send a copy to MSRA intern support (Alias:msrainte)  "
                        GroupName="AdvisorApproval" ValidationGroup="AdvisorApproval" /><br />
                    <asp:RadioButton ID="rdAdvisor2" runat="server" Text="No - MSRA Intern Support Team will check with new students" GroupName="AdvisorApproval"
                        ValidationGroup="AdvisorApproval" />
                    <asp:CustomValidator ID="CustomValidator4" ClientValidationFunction="CheckAdvisorApproval" runat="server" ErrorMessage="Required!"
                        ValidationGroup="Applicant"></asp:CustomValidator>
                </td>
            </tr>
            <%--
        <tr>
        <td>Enroll date in university</td>
        <td>
        month <asp:DropDownList ID="ddlEnrollMonth" runat="server">
        </asp:DropDownList>
        in year
        <asp:DropDownList ID="ddlEnrollYear" runat="server">
        </asp:DropDownList></td>
        <tr><td>Graduation date in university</td>
        <td>
        month 
        <asp:DropDownList ID="ddlGraduateMonth" runat="server">
        </asp:DropDownList>
        in year
        <asp:DropDownList ID="ddlGraduateYear" runat="server">
        </asp:DropDownList>        <br /><asp:CustomValidator id="CustomValidator6"
        ClientValidationFunction="CheckEnrollAndGraduate" runat="server"
        ErrorMessage="Graduate date is earlier than or equal to enroll date!" ValidationGroup="Applicant">
        </asp:CustomValidator></td>
        </tr>
         --%>
            <tr>
                <td>Any special requirement?<br />
                    for example: if this intern use the Headcount of other group, please specify there</td>
                <td>
                    <asp:TextBox CssClass="required_input" ID="tbComments" runat="server" Height="194px" TextMode="MultiLine" Width="390px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbComments" ErrorMessage="Required!" Display="Static"
                        ValidationGroup="Applicant"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="text-align: center">
                        If any question, please email to <a href="mailto:msrainte@microsoft.com">msrainte@microsoft.com</a>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>
