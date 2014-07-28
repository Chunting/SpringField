<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PerformanceAssessment.ascx.cs"
    Inherits="Controls_PerformanceAssessment" %>
<asp:GridView ID="gvPAList" runat="server" AutoGenerateColumns="False" OnRowCommand="gvPAList_RowCommand"
    DataKeyNames="id" OnPreRender="gvPAList_PreRender" OnRowDataBound="gvPAList_RowDataBound"
    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
    CellPadding="3" GridLines="Vertical">
    <Columns>
        <asp:TemplateField Visible="false">
            <ItemTemplate>
                <asp:Label Text='<%# Eval("id")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "CheckInDate", "{0:yyyy-MM-dd}")%>
            </ItemTemplate>
            <HeaderTemplate>
                Check-In Date</HeaderTemplate>
            <HeaderStyle Width="120px" HorizontalAlign="Center" />
            <ItemStyle Width="120px" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "CheckOutDate", "{0:yyyy-MM-dd}")%>
            </ItemTemplate>
            <HeaderTemplate>
                Check-out Date</HeaderTemplate>
            <HeaderStyle Width="120px" HorizontalAlign="Center" />
            <ItemStyle Width="120px" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# GetGroupNameByID(DataBinder.Eval(Container.DataItem, "GroupId").ToString().Trim())%>
            </ItemTemplate>
            <HeaderTemplate>
                Group</HeaderTemplate>
            <HeaderStyle Width="50px" HorizontalAlign="Center" />
            <ItemStyle Width="50px" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# GetPerformance(DataBinder.Eval(Container.DataItem, "OverrallEvaluation").ToString().Trim())%>
            </ItemTemplate>
            <HeaderTemplate>
                Performance</HeaderTemplate>
            <HeaderStyle Width="80px" HorizontalAlign="Center" />
            <ItemStyle Width="80px" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "MentorName").ToString().Trim()%>
            </ItemTemplate>
            <HeaderTemplate>
                Mentor Name</HeaderTemplate>
            <HeaderStyle Width="120px" HorizontalAlign="Center" />
            <ItemStyle Width="120px" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lbMentorAlias" Text='<%# DataBinder.Eval(Container.DataItem, "MentorAlias").ToString().Trim()%>'
                    runat="server"></asp:Label>
            </ItemTemplate>
            <HeaderTemplate>
                Mentor Alias</HeaderTemplate>
            <HeaderStyle Width="80px" HorizontalAlign="Center" />
            <ItemStyle Width="80px" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton runat="server" ID="lnkbtnDetail" Visible="false" CommandName="PAdetail"
                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id").ToString() %>'
                    Text="Detail"></asp:LinkButton>&nbsp; <a id="lnkEditPA" visible="false" runat="server"
                        href='<%# GetEditLink(DataBinder.Eval(Container.DataItem, "id").ToString()) %>'>
                        Edit </a>&nbsp;
                <asp:LinkButton runat="server" Visible="false" ID="lnkbtnDeletePA" OnClientClick="return ConfirmDeletePA();"
                    CommandName="deletePA" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id").ToString() %>'
                    Text="Delete"></asp:LinkButton>
            </ItemTemplate>
            <HeaderTemplate>
                Actions</HeaderTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        No Performance Assessment</EmptyDataTemplate>
    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#9c969c" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="#DCDCDC" />
</asp:GridView>
<asp:Panel ID="PanelPADetail" runat="server" Visible="false">
    <p style="font-size: 20px">
        Detail Information:</p>
    <table class="applicants_table">
        <tr>
            <td>
                <strong>Intern Name:</strong></td>
            <td>
                <asp:Label ID="lbInternName" runat="server"></asp:Label></td>
            <td>
                <strong>Intern Phone: </strong>
            </td>
            <td>
                <asp:Label ID="lbInternPhone" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <strong>Intern Email</strong>:</td>
            <td>
                <asp:Label ID="lbInternEmail" runat="server"></asp:Label></td>
            <td>
                <strong>Intern Position: </strong>
            </td>
            <td>
                <asp:Label ID="lbInternPosition" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <strong>Date:</strong></td>
            <td>
                <asp:Label ID="lbDate" runat="server"></asp:Label></td>
            <td>
                <strong>Group: </strong>
            </td>
            <td>
                <asp:Label ID="lbGroup" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <strong>Mentor Name:</strong></td>
            <td>
                <asp:Label ID="lbMentorName" runat="server"></asp:Label></td>
            <td>
                <strong>Mentor Alias: </strong>
            </td>
            <td>
                <asp:Label ID="lbMentorAlias" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <strong>Group Manager Alias:</strong></td>
            <td>
                <asp:Label ID="lbGMAlias" runat="server"></asp:Label></td>
            <td>
                <strong>Project: </strong>
            </td>
            <td>
                <asp:Label ID="lbProject" runat="server"></asp:Label>
            </td>
        </tr>
        <tr id="trPipelineDiscipline_STC" runat="server">
            <td>
                <strong>Project-based or FTE pipeline :</strong></td>
            <td>
                <asp:Label ID="lbPipeline_STC" runat="server"></asp:Label></td>
            <td>
                <strong>Discipline:</strong>
            </td>
            <td>
                <asp:Label ID="lbDiscipline_STC" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <strong>Graduation Date:</strong></td>
            <td>
                <asp:Label ID="lbGraduationDate" runat="server"></asp:Label></td>
        </tr>
    </table>
    <br />
    <div class="panel_title_expand" onclick="ChangeStyle(this,'tablepart1')">
        INTERN¡¯S PERFORMANCE ASSESSMENT [FINISH BY INTERN]
    </div>
    <table width="100%" class="applicants_table" style="display: block;" id="tablepart1">
        <tr>
            <td>
                Summarize your performance against each objective considering WHAT you have achieved
                and HOW you have achieved it.
            </td>
        </tr>
        <tr>
            <td>
                <strong>GOAL/OBJECTIVE: </strong>
                <br />
                <asp:Label ID="lbInternObjective" runat="server"></asp:Label><br />
                <strong>SELF EVALUATION: </strong>
                <br />
                <asp:Label ID="lbInternSelfEvaluation" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Please comment on your work assignment, your experience working with your mentor,
                our organization and the company Microsoft, or this review process. Please comment
                on your performance <strong>STRENGTHS and WEAKNESSES </strong>demonstrated in your
                daily work here.
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbStrengthsWeaknesses" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="tablePublications_MSRA" runat="server">
                    <p>
                        <strong>Info of the papers finished at MSRA:</strong></p>
                    <asp:GridView ID="gvPublication" runat="server" AutoGenerateColumns="False" DataKeyNames="PublicationId">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Publication" ItemStyle-Width="500px" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%#StatusIdToString(Eval("CurrentStatus").ToString())%>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    Status of the paper when they submit</HeaderTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlgvPaperStatus" DataSource='<%# Springfield.Components.PAResourceManager.GetTypeDisplayItems("PaperStatus") %>'>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            This intern didn't finish any paper.</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <div class="panel_title_expand" style="font-size: 12px" onclick="ChangeStyle(this,'TablePerformanceLevel')">
        GENERAL COMMENTS ON PERFORMANCE STRENGTHS AND WEAKNESSES [FINISH BY MENTOR]</div>
    <table id="TablePerformanceLevel" width="100%" class="PerformanceLevel_table" style="display: block;">
        <tr>
            <td colspan="2" class="PerformanceLevel_td">
                <b>Detailed evaluation:</b>
            </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Coding Skill:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblCodingskill" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Analytical Skill:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblAnalyticalSkill" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Problem Solving:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblProblemSolving" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Innovation:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblInnovation" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Driving for results:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblDrivingforResults" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Dealing with Ambiguity:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblDealingwithAmbiguity" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Quick on Learning:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblQuickonLearning" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                English:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblEnglish" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Communication Skills:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblCommunicationSkills" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Team Work:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblTeamWork" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Attitude:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblAttitude" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Microsoft Core Values:</b></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Integrity & Honesty:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblIntegrityandHonesty" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Open & Respectful:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblOpenandRespectful" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Big Challenges:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblBigChallenges" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Passion:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblPassion" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Accountable:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblAccountable" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Self-Critical:
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblSelfCritical" runat="server" RepeatDirection="Horizontal"
                    Enabled="false">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                <b>Overall evaluation of the student¡¯s performance:</b>
            </td>
            <td class="PerformanceLevel_td">
                <asp:RadioButtonList ID="rblOverallEvaluation" runat="server" RepeatDirection="Horizontal"
                    Enabled="false" ForeColor="red">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td colspan="2" class="PerformanceLevel_td">
                <strong>Comments:</strong></td>
        </tr>
        <tr>
            <td colspan="2" class="PerformanceLevel_td">
                <asp:Label runat="server" ID="lbComments"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="PerformanceLevel_td">
                <strong>Strength:</strong></td>
        </tr>
        <tr>
            <td colspan="2" class="PerformanceLevel_td">
                <asp:Label ID="lbStrength" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2" class="PerformanceLevel_td">
                <strong>Weakness: </strong>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="PerformanceLevel_td">
                <asp:Label ID="lbWeakness" runat="server"></asp:Label></td>
        </tr>
    </table>
    <br />
    <div class="panel_title_expand" onclick="ChangeStyle(this,'divPart3')">
        INFORMATION COLLECTION [FINISH BY MENTOR]</div>
    <div id="divPart3" style="display: block;" style="border: solid 1px #999999">
        <ol id="olFTE_STC" runat="server">
            <li>Do you want to hire this intern as FTE if he/she is qualified?
                <br />
                <asp:RadioButtonList ID="rblHiredAsFTE_STC" runat="server" Enabled="false">
                </asp:RadioButtonList>
            </li>
            <li>Is this intern ready for onsite-interview now?
                <br />
                <asp:RadioButtonList ID="rblOnsiteInterviewNow" runat="server" Enabled="false">
                </asp:RadioButtonList>
            </li>
            <li>If the intern is not ready for onsite-interview, when do you expect him/her be ready?<br />
                <asp:TextBox ID="tbExpectedOnsiteInterviewDate" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li>If the intern is not hirable, we suggest that you do not continue his/her internship.
                <br />
                For project-based intern,how long do you want to extend his/her service period?
                <asp:TextBox ID="tbExtendPeriod" runat="server" Enabled="false"></asp:TextBox>Months.(input
                0 represent that you do not continue his/her internship)<br />
            </li>
        </ol>
        <ol id="olFTE_MSRA" runat="server">
            <li>Do you want to hire this intern as FTE if he/she is qualified?
                <br />
                <asp:RadioButtonList ID="rblHiredAsFTE_MSRA" runat="server" Enabled="false">
                </asp:RadioButtonList>
            </li>
        </ol>
    </div>
</asp:Panel>
