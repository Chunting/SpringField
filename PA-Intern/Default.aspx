<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="mainContentPlaceHolder" runat="server">

    <script src="Script/popcalendar.js" type="text/javascript" language="javascript"></script>

    <script language="javascript" type="text/javascript">
    function FindInput(nameregex,type)
    {
       re = new RegExp(nameregex);
       
       for(i = 0; i < document.forms[0].elements.length; i++)
       {
          elm = document.forms[0].elements[i];
          if (elm.type == type)
          {
             if (re.test(elm.name))
             {
                return elm;
             }
          }
       }
       return null;
    }

    function CheckCheckInAndCheckOutDay(sender, args)
    {
        var f1 = FindInput("tbCheckinDate","text").value;
        var checkInDay = Date.parse(f1);
        var f2 = FindInput("tbCheckoutDate","text").value;
        var CheckOutDay = Date.parse(f2);
        
        args.IsValid = (f1=="" || f2=="" ) || (checkInDay<CheckOutDay);
    }
    </script>

    <div style="min-height:700px">
        <br />
        <br />
        <div style="background-color: #879096; height: 20px; padding-left: 20px; vertical-align: middle">
            <b style="font-size: 16px; color: White">Notice</b></div>
        <div style="padding-left: 20px;">
            <ul style="margin-top: 0px;">
                <li>Intern and mentor are required to have a 1:1 meeting to discuss the intern's performance.</li>
                <li>Your mentor and group manager have right to see what you input.</li>
                <li>You can't modify this assessment after submiting it, please input carefully. </li>
            </ul>
        </div>
        <div id="divUncompletedPAlist" runat="server">
            <div style="background-color: #879096; height: 20px; padding-left: 20px; vertical-align: middle">
                <b style="font-size: 16px; color: White">Uncompleted Performance Assessemnt</b></div>
            <div style="padding-left: 20px;">
                You didn't complete the following performance assessments, please finish it. For
                duplicated one, please delete it.<br />
                <asp:GridView ID="gvUncompletedPA" runat="server" AutoGenerateColumns="False" OnRowCommand="gvUncompletedPA_RowCommand"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Check-in Date</HeaderTemplate>
                            <ItemTemplate>
                                <%# ParseCheckinDate(DataBinder.Eval(Container.DataItem, "CheckInDate", "{0:yyyy-MM-dd}"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Check-out Date</HeaderTemplate>
                            <ItemTemplate>
                                <%# ParseCheckoutDate(DataBinder.Eval(Container.DataItem, "CheckOutDate", "{0:yyyy-MM-dd}"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Group</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetGroupNameByID(Eval("GroupId").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Submit Date</HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "InsertDate", "{0:yyyy-MM-dd}")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Actions</HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnComplete" CommandName="completePA" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id").ToString() %>'
                                    Text="Complete" CausesValidation="false"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnDeletePA" OnClientClick="return confirm('Are you sure to delete this Performance Assessment?');"
                                    CommandName="deletePA" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id").ToString() %>'
                                    Text="Delete" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#879096" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                <br />
                <asp:Button ID="btnAddPA" runat="server" BackColor="#35769E" ForeColor="white" Text="Add Another Performance Assessment"
                    OnClick="btnAddPA_Click" /><br /></div>
        </div>
        <div id="divPADetail" runat="server" visible="false">
            <div style="background-color: #879096; height: 20px; padding-left: 20px; vertical-align: middle;
                color: White">
                <b style="font-size: 16px;">First Step</b>.Check your personal information, if incorrect,
                please modify it.</div>
            <div style="padding-left: 20px;">
                <br />
                Which department are you in?
                <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                </asp:DropDownList>
                <table class="BasicInfoTable">
                    <tr>
                        <td>
                            Name:
                        </td>
                        <td>
                            <asp:Label ID="lbName" runat="server"></asp:Label>
                        </td>
                        <td>
                            Phone:
                        </td>
                        <td>
                            <asp:TextBox ID="tbPhone" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Personal Email:<br />
                            <span style="font-size: 12px">Please don't input ***@microsoft.com</span>
                        </td>
                        <td>
                            <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox></td>
                        <td>
                            Group:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGroup" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Check in Date:
                        </td>
                        <td>
                            <asp:TextBox ID="tbCheckinDate" runat="server"></asp:TextBox>&nbsp;
                            <input type="button" value="Select" id="btnCheckinDate" name="btnCheckinDate" runat="server" /><br />
                            <asp:RequiredFieldValidator ID="rfvCheckinDate" runat="server" ErrorMessage="Required Field!"
                                ForeColor="red" ControlToValidate="tbCheckinDate"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            Check Out Date:
                        </td>
                        <td>
                            <asp:TextBox ID="tbCheckoutDate" runat="server"></asp:TextBox>&nbsp;
                            <input type="button" value="Select" id="btnCheckoutDate" name="btnCheckoutDate" runat="server" /><br />
                            <asp:RequiredFieldValidator ID="rfvCheckoutDate" runat="server" ErrorMessage="Required Field!"
                                ForeColor="red" ControlToValidate="tbCheckoutDate"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvCheckInandCheckOutDate" ClientValidationFunction="CheckCheckInAndCheckOutDay"
                                runat="server" ErrorMessage="Check-out date is earlier than or equal to Check-in date!"
                                ControlToValidate="tbCheckoutDate"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Mentor Alias:
                        </td>
                        <td>
                            <asp:TextBox ID="tbMentorAlias" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMentorAlias" runat="server" ErrorMessage="Required Field!"
                                ForeColor="red" ControlToValidate="tbMentorAlias"></asp:RequiredFieldValidator><br />
                            <asp:Label ForeColor="red" ID="lbAliasNotice" runat="server"></asp:Label>
                        </td>
                        <td>
                            Mentor Name:</td>
                        <td>
                            <asp:TextBox ID="tbMentorName" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revMentorName" runat="server" ControlToValidate="tbMentorName"
                                ValidationExpression="^(?!-)(?!.*?-$)[ a-zA-Z0-9-,\u4e00-\u9fa5]+$" ErrorMessage="incorrect format"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvMentorName" runat="server" ErrorMessage="Required Field!"
                                ForeColor="red" ControlToValidate="tbMentorName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Intern Position:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlInternPosition" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            Group Manager Alias:</td>
                        <td>
                            <asp:TextBox ID="tbGroupManagerAlias" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvGroupManagerAlias" runat="server" ErrorMessage="Required Field!"
                                ForeColor="red" ControlToValidate="tbGroupManagerAlias"></asp:RequiredFieldValidator><br />
                            <asp:Label ForeColor="red" ID="lbGroupManagerAlias" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trDisciplineandPipeline_STC" runat="server">
                        <td>
                            Discipline:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDiscipline_STC" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            Project-based or FTE pipeline:</td>
                        <td>
                            <asp:DropDownList ID="ddlPipeline_STC" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Project:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProject" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            Graduation Date:
                        </td>
                        <td>
                            <asp:TextBox ID="tbGraduationDate" runat="server"></asp:TextBox>&nbsp;
                            <input type="button" value="Select" id="btnGraduationDate" name="btnGraduationDate"
                                runat="server" /><br />
                            <asp:RequiredFieldValidator ID="rfvGraduationDate" runat="server" ErrorMessage="Required Field!"
                                ForeColor="red" ControlToValidate="tbGraduationDate"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnNext" runat="server" Text="NEXT>>" BackColor="#35769E" ForeColor="white"
                    OnClick="btnNext_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" BackColor="#35769E" ForeColor="white"
                    OnClick="btnCancel_Click"  CausesValidation="false"/><br /><br />
            </div>
        </div>
    </div>
</asp:Content>
