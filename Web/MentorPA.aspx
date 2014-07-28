<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MentorPA.aspx.cs" Inherits="MentorPA"
    MasterPageFile="~/SpringfieldMaster.master" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" runat="server" ID="cntLegalNotice">

    <script language="javascript" type="text/javascript">
		function OpenandClose()
		{	
			for(var i=0;i<arguments.length;i++)
			{
				id=arguments[i];				
				id_p = id + "_p";
				if(window.document.getElementById(id).style.display == "none")
				{
					window.document.getElementById(id).style.display = "block";					
					window.document.getElementById(id_p).className = "LibC_o";
				}
				else
				{
					window.document.getElementById(id).style.display = "none";
					window.document.getElementById(id_p).className = "LibC_c";
				}
			}
		}
    </script>

    <p>
        Intern's Department:
        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
        </asp:DropDownList></p>
    <div class="panel_title_expand" onclick="ChangeStyle(this,'divInternPA')" style="height: 25px;
        vertical-align: middle">
        INTERN¡¯S PERFORMANCE ASSESSMENT [FINISH BY INTERN]
        <asp:Button ID="btnEditInternPA" runat="server" Text="Edit" CausesValidation="False"
            OnClick="btnEditInternPA_Click" />
        <asp:Button ID="btnCancelEdittingInternPA" runat="server" OnClick="btnCancelEdittingInternPA_Click"
            Text="Cancel" Visible="False" CausesValidation="false" /></div>
    <div id="divInternPA" style="display: block;">
        <table class="applicants_table">
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
                    <asp:TextBox ID="tbPhone" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Personal Email:</td>
                <td>
                    <asp:TextBox ID="tbEmail" runat="server" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    Group:
                </td>
                <td>
                    <asp:DropDownList ID="ddlGroup" runat="server" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Check in Date:
                </td>
                <td>
                    <asp:TextBox ID="tbCheckinDate" runat="server" Enabled="False"></asp:TextBox>&nbsp;
                    <input type="button" value="Select" id="btnCheckinDate" name="btnCheckinDate" runat="server"
                        visible="false" />
                    <asp:RequiredFieldValidator ID="rfvCheckinDate" runat="server" ErrorMessage="Required Field!"
                        ForeColor="red" ControlToValidate="tbCheckinDate"></asp:RequiredFieldValidator>
                </td>
                <td>
                    Check Out Date:
                </td>
                <td>
                    <asp:TextBox ID="tbCheckoutDate" runat="server" Enabled="False"></asp:TextBox>&nbsp;
                    <input type="button" value="Select" id="btnCheckoutDate" name="btnCheckoutDate" runat="server"
                        visible="false" />
                    <asp:RequiredFieldValidator ID="rfvCheckoutDate" runat="server" ErrorMessage="Required Field!"
                        ForeColor="red" ControlToValidate="tbCheckoutDate"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Mentor Name:</td>
                <td>
                    <asp:TextBox ID="tbMentorName" runat="server" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMentorName" runat="server" ErrorMessage="Required Field!"
                        ForeColor="red" ControlToValidate="tbMentorName"></asp:RequiredFieldValidator>
                </td>
                <td>
                    Project:
                </td>
                <td>
                    <asp:DropDownList ID="ddlProject" runat="server" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Intern Position:
                </td>
                <td>
                    <asp:DropDownList ID="ddlInternPosition" runat="server" Enabled="False">
                    </asp:DropDownList>
                </td>
                <td>
                    Group Manager Alias:</td>
                <td>
                    <asp:TextBox ID="tbGroupManagerAlias" runat="server" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvGroupManagerAlias" runat="server" ErrorMessage="Required Field!"
                        ForeColor="red" ControlToValidate="tbGroupManagerAlias"></asp:RequiredFieldValidator>
                    <asp:Label ForeColor="red" ID="lbGroupManagerAlias" runat="server"></asp:Label>
                </td>
            </tr>
            <tr id="trDisciplineandPipeline_STC" runat="server">
                <td>
                    Discipline:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDiscipline_STC" runat="server" Enabled="False">
                    </asp:DropDownList>
                </td>
                <td>
                    Project-based or FTE pipeline:</td>
                <td>
                    <asp:DropDownList ID="ddlPipeline_STC" runat="server" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Graduation Date:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="tbGraduationDate" runat="server" Enabled="False"></asp:TextBox>&nbsp;
                    <input type="button" value="Select" id="btnGraduationDate" name="btnGraduationDate"
                        runat="server" visible="false" />
                    <asp:RequiredFieldValidator ID="rfvGraduationDate" runat="server" ErrorMessage="Required Field!"
                        ForeColor="red" ControlToValidate="tbGraduationDate"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    Summarize your performance against each objective considering WHAT you have achieved
                    and HOW you have achieved it.<br />
                    <br />
                    <strong>GOAL/OBJECTIVE:<br />
                    </strong>
                    <asp:TextBox ID="tbObjective" runat="server" TextMode="MultiLine" Enabled="false"
                        Height="300px" Width="100%"></asp:TextBox><br />
                    <br />
                    <strong>SELF EVALUATION:<br />
                    </strong>
                    <asp:TextBox ID="tbSelfEvaluation" runat="server" TextMode="MultiLine" Enabled="false"
                        Height="300px" Width="100%"></asp:TextBox><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    Please comment on your work assignment, your experience working with your mentor,
                    our organization and the company Microsoft, or this review process. Please comment
                    on your performance STRENGTHS and WEAKNESSES demonstrated in your daily work here.<br />
                    <br />
                    <asp:TextBox ID="tbInternSW" runat="server" TextMode="MultiLine" Enabled="false"
                        Height="300px" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr runat="server" id="trPublication1_MSRA">
                <td style="height: 16px" colspan="4">
                    <strong>Info of the papers finished at MSRA: </strong>
                    <br />
                    <br />
                    <asp:GridView ID="gvPublication" runat="server" AutoGenerateColumns="False" AutoGenerateDeleteButton="True"
                        AutoGenerateEditButton="True" OnRowEditing="gvPublication_RowEditing" DataKeyNames="PublicationId"
                        OnRowUpdating="gvPublication_RowUpdating" OnRowCancelingEdit="gvPublication_RowCancelingEdit"
                        OnRowDeleting="gvPublication_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Publication" ItemStyle-Width="100%" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%#StatusIdToString(Eval("CurrentStatus").ToString())%>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    Current status of the paper</HeaderTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlgvPaperStatus" DataSource='<%# Springfield.Components.PAResourceManager.GetTypeDisplayItems("PaperStatus") %>'>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            None</EmptyDataTemplate>
                    </asp:GridView>
                    <asp:Button ID="btnAddPublication" runat="server" Text="Add New Publication" OnClick="btnAddPublication_Click"
                        CausesValidation="false" />
                    <asp:Panel ID="panelAddNewPublication" runat="server" Visible="false" CausesValidation="false">
                        <p>
                            <b>Add New Publication:</b><br />
                            <i>Example:</i><br />
                            <u>In conference</u>:
                            <br />
                            Yong Wang, Jiang Li, Kun Zhou and Heung-Yeung Shum, ¡°Interacting with 3D Graphic
                            Objects in an Image-based Environment,¡± The Second IEEE Pacific-Rim Conference on
                            Multimedia, pp.229-236, Beijing, China, October 24-26, 2001.
                            <br />
                            <u>In Journal:</u><br />
                            Yueting Zhuang, Jiashi Chen, Fei Wu, Qiang Zhu£¬A Recursive Framework for Automatic
                            Face Tracking£¬¡¶Chinese Journal of Electronics¡·£¬Vol.13, No.1, Jan.,2004 [SCI/EIÊÕÂ¼]
                        </p>
                        <b>Publication:</b>
                        <br />
                        <asp:TextBox ID="tbNewPublication" runat="server" TextMode="MultiLine" Width="100%"
                            Height="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewPublication" runat="server" ControlToValidate="tbNewPublication"
                            ValidationGroup="paper" ErrorMessage="Required Field!" ForeColor="red"></asp:RequiredFieldValidator><br />
                        <b>Current status of the paper:</b><br />
                        <asp:DropDownList ID="ddlPaperStatus" runat="server">
                        </asp:DropDownList><p style="text-align: right">
                            <asp:Button ID="tbnSummitNewPublication" runat="server" Text="OK" OnClick="tbnSummitNewPublication_Click"
                                CausesValidation="true" ValidationGroup="paper" />
                            <asp:Button ID="btnCancelAddingPublication" runat="server" OnClick="btnCancelAddingPublication_Click"
                                CausesValidation="false" Text="Cancel" /></p>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="panel_title_expand" style="font-size: 12px" onclick="ChangeStyle(this,'TablePerformanceLevel')">
        GENERAL COMMENTS ON PERFORMANCE STRENGTHS AND WEAKNESSES [FINISH BY MENTOR]</div>
    <table id="TablePerformanceLevel" style="border: solid 1px black; width: 100%; display: block">
        <tr>
            <td colspan="2">
                <b>Detailed evaluation:</b>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblCodingskill"></asp:RequiredFieldValidator>
                Coding Skill:
            </td>
            <td>
                <asp:RadioButtonList ID="rblCodingskill" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblAnalyticalSkill"></asp:RequiredFieldValidator>Analytical
                Skill:
            </td>
            <td>
                <asp:RadioButtonList ID="rblAnalyticalSkill" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblProblemSolving"></asp:RequiredFieldValidator>Problem
                Solving:
            </td>
            <td>
                <asp:RadioButtonList ID="rblProblemSolving" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblInnovation"></asp:RequiredFieldValidator>Innovation:
            </td>
            <td>
                <asp:RadioButtonList ID="rblInnovation" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblDrivingforResults"></asp:RequiredFieldValidator>Driving
                for results:
            </td>
            <td>
                <asp:RadioButtonList ID="rblDrivingforResults" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblDealingwithAmbiguity"></asp:RequiredFieldValidator>Dealing
                with Ambiguity:
            </td>
            <td>
                <asp:RadioButtonList ID="rblDealingwithAmbiguity" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblQuickonLearning"></asp:RequiredFieldValidator>Quick
                on Learning:
            </td>
            <td>
                <asp:RadioButtonList ID="rblQuickonLearning" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblEnglish"></asp:RequiredFieldValidator>English:
            </td>
            <td>
                <asp:RadioButtonList ID="rblEnglish" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblCommunicationSkills"></asp:RequiredFieldValidator>Communication
                Skills:
            </td>
            <td>
                <asp:RadioButtonList ID="rblCommunicationSkills" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblTeamWork"></asp:RequiredFieldValidator>Team
                Work:
            </td>
            <td>
                <asp:RadioButtonList ID="rblTeamWork" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblAttitude"></asp:RequiredFieldValidator>Attitude:
            </td>
            <td>
                <asp:RadioButtonList ID="rblAttitude" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Microsoft Core Values:</b></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblIntegrityandHonesty"></asp:RequiredFieldValidator>Integrity
                & Honesty:
            </td>
            <td>
                <asp:RadioButtonList ID="rblIntegrityandHonesty" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblOpenandRespectful"></asp:RequiredFieldValidator>Open
                & Respectful:
            </td>
            <td>
                <asp:RadioButtonList ID="rblOpenandRespectful" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblBigChallenges"></asp:RequiredFieldValidator>Big
                Challenges:
            </td>
            <td>
                <asp:RadioButtonList ID="rblBigChallenges" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblPassion"></asp:RequiredFieldValidator>Passion:
            </td>
            <td>
                <asp:RadioButtonList ID="rblPassion" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblAccountable"></asp:RequiredFieldValidator>Accountable:
            </td>
            <td>
                <asp:RadioButtonList ID="rblAccountable" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblSelfCritical"></asp:RequiredFieldValidator>Self-Critical:
            </td>
            <td>
                <asp:RadioButtonList ID="rblSelfCritical" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Overall evaluation of the student¡¯s performance:</b>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="rblOverallEvaluation"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:RadioButtonList ID="rblOverallEvaluation" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td colspan="2">
                <strong>Comments:</strong></td>
        </tr>
        <tr>
            <td colspan="2" style="height: 42px">
                <asp:TextBox ID="tbComments" runat="server" TextMode="MultiLine" Width="100%" Height="196px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="tbComments"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <strong>Strength:</strong></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbStrength" runat="server" TextMode="MultiLine" Width="100%" Height="196px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Required Field!"
                    ForeColor="red" ControlToValidate="tbStrength"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="2">
                <strong>Weakness: </strong>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbWeakness" runat="server" TextMode="MultiLine" Width="100%" Height="196px"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator21" runat="server" ErrorMessage="Required Field!" ForeColor="red"
                    ControlToValidate="tbWeakness"></asp:RequiredFieldValidator></td>
        </tr>
    </table>
    <br />
    <div class="panel_title_expand" onclick="ChangeStyle(this,'divPart3')">
        INFORMATION COLLECTION [FINISH BY MENTOR]</div>
    <div id="divPart3" style="display: block;" style="border: solid 1px #999999">
        <ol id="olFTE_STC" runat="server">
            <li>Do you want to hire this intern as FTE if he/she is qualified?
                <br />
                <asp:RadioButtonList ID="rblHiredAsFTE_STC" runat="server">
                </asp:RadioButtonList>
            </li>
            <li>Is this intern ready for onsite-interview now?
                <br />
                <asp:RadioButtonList ID="rblOnsiteInterviewNow" runat="server">
                </asp:RadioButtonList>
            </li>
            <li>If the intern is not ready for onsite-interview, when do you expect him/her be ready?<br />
                <asp:TextBox ID="tbExpectedOnsiteInterviewDate" runat="server"></asp:TextBox>&nbsp;
                <input type="button" value="Select" id="btnExpectedOnsiteInterviewDate" name="tbExpectedOnsiteInterviewDate"
                    runat="server" /><br />
            </li>
            <li>If the intern is not hirable, we suggest that you do not continue his/her internship.
                <br />
                For project-based intern,how long do you want to extend his/her service period?
                <asp:TextBox ID="tbExtendPeriod" runat="server"></asp:TextBox>Months.(input 0 represent
                that you do not continue his/her internship)<br />
            </li>
        </ol>
        <ol id="olFTE_MSRA" runat="server">
            <li>Do you want to hire this intern as FTE if he/she is qualified?
                <br />
                <asp:RadioButtonList ID="rblHiredAsFTE_MSRA" runat="server">
                </asp:RadioButtonList>
            </li>
        </ol>
    </div>
    <div style="width: 100%; text-align: right">
        <br />
        <asp:Button ID="btnSummit" runat="server" Text="Submit" OnClick="btnSummit_Click" /></div>
</asp:Content>
