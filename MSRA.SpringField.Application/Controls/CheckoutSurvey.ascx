<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckoutSurvey.ascx.cs" Inherits="MSRA.SpringField.Application.Controls.CheckoutSurvey" %>

<table style="width:98%">
<tr>
<td align="center">
    <asp:GridView ID="gvSurveyList" runat="server" AutoGenerateColumns="False" OnRowCommand="gvSurveyList_RowCommand" Width="100%"
    DataKeyNames="id" OnPreRender="gvSurveyList_PreRender" 
    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
    CellPadding="3" GridLines="Vertical">
    <Columns>
        <asp:TemplateField Visible="false">
            <ItemTemplate>
                <asp:Label ID="Label1" Text='<%# Eval("id")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "CheckInDate", "{0:yyyy-MM-dd}")%>
            </ItemTemplate>
            <HeaderTemplate>
                Check-In Date</HeaderTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "CheckOutDate", "{0:yyyy-MM-dd}")%>
            </ItemTemplate>
            <HeaderTemplate>
                Check-Out Date</HeaderTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# GetGroupNameByID(DataBinder.Eval(Container.DataItem, "GroupId").ToString().Trim())%>
            </ItemTemplate>
            <HeaderTemplate>
                Group</HeaderTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# GetInternshipDurationByID(DataBinder.Eval(Container.DataItem, "InternshipDuration").ToString().Trim())%>
            </ItemTemplate>
            <HeaderTemplate>
                InternshipDuration</HeaderTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:LinkButton runat="server" ID="lnkbtnDetail" Visible="false" CommandName="SurveyDetail"
                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id").ToString() %>'
                    Text="Detail"></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton runat="server" ID="lnkbtnDeletePA" Visible="false" OnClientClick="return ConfirmDeletePA();"
                    CommandName="DeleteSurvey" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id").ToString() %>'
                    Text="Delete"></asp:LinkButton>
            </ItemTemplate>
            <HeaderTemplate>
            <div style="width:100%;vertical-align:top;text-align:center">Action</div>
            </HeaderTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        No Checkout Survey</EmptyDataTemplate>
    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#9c969c" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="#DCDCDC" />
</asp:GridView>
</td>
</tr>
<tr>
<td>
<asp:Panel ID="PanelPADetail" runat="server" Visible="false">
    <p style="font-size: 20px">
        Detail Information:</p>
    <div class="panel_title_expand" onclick="ChangeStyle(this,'tablepart1')" style="text-align:left">
        Part I. Intern Information
    </div>
    <table class="applicants_table">
        <tr>
            <td>
                <strong>Intern Name:</strong></td>
            <td>
                <asp:Label ID="lbInternName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <strong>Group: </strong>
            </td>
            <td>
                <asp:Label ID="lbGroup" runat="server"></asp:Label>
            </td>
            <td >
                <strong>InternshipDuration: </strong>
            </td>
            <td>
                <asp:Label ID="lbInternshipDuration" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <strong>Check-In Date:</strong></td>
            <td>
                <asp:Label ID="lbCheckInDate" runat="server"></asp:Label></td>
            <td>
                <strong>Check-Out Date: </strong>
            </td>
            <td>
                <asp:Label ID="lbCheckOutDate" runat="server"></asp:Label>
            </td>
        </tr>       
    </table>
    <br />
    <div class="panel_title_expand" onclick="ChangeStyle(this,'TablePerformanceLevel')" style="text-align:left">
        Part II. My overall experience at MSRA
    </div>
    <table id="TablePerformanceLevel" width="100%" class="PerformanceLevel_table" 
        style="display: block; height: 167px;">
        <tr>
            <td colspan="2" class="PerformanceLevel_td">
                <b>A.My overall view of my MSRA internship experience</b>
            </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                1. As an intern, I had a fulfilling and meaningful experience in working at MSRA.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblOverallView" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
               Comments:
            </td>
            <td class="PerformanceLevel_td">
                 <asp:Label ID="lbOverallComments" runat="server" style="word-break:break-all"></asp:Label>
             </td>
        </tr>
    </table>
    <br />
    <table id="Table1" width="100%" class="PerformanceLevel_table" 
        style="display: block; height: 167px;">
        <tr>
            <td colspan="2" class="PerformanceLevel_td">
                <b>B.My work at MSRA</b>
            </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                1. I liked the kind of work/project I was associated with.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblLikeWork" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                2. I am now equipped with enough background information to continue down my future career path and excel.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblBackground" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                3. The amount of work assigned was appropriate for the length of my internship.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblWorkAmount" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                4. My work at MSRA matched my personal objectives.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblObjects" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                5. Overall, I developed technical and professional skills during my internship at MSRA that will be useful in my future career.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblDevelopmentSkill" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Research papers writing
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblResearchSkill" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Software development
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblSDESkill" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Project management
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblProjectSkill" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Design ability
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblDesignSkill" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Effective communications/presentations
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblCommunicationSkill" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Teamwork
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblTeamwork" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Comments
            </td>
            <td class="PerformanceLevel_td">
                 <asp:Label ID="lbWorkComments" runat="server" style="word-break:break-all"></asp:Label>
             </td>
        </tr>
    </table>
    <br />
    <table id="Table2" width="100%" class="PerformanceLevel_table" 
        style="display: block; height: 167px;">
        <tr>
            <td colspan="2" class="PerformanceLevel_td">
                <b>C.My mentor and work group</b>
            </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                1. Clear goals were explained and set for me from the very beginning of my internship.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblMentorSetGoal" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                2. I could obtain help and coaching from my mentor in a timely manner.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblHelpFromMentor" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                3. My mentor and work group made good use of my skills and abilities.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblMakeGoodUse" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                4. Group members treated each other with respect.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblRespect" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Comments
            </td>
            <td class="PerformanceLevel_td">
                 <asp:Label ID="lbMentorComments" runat="server" style="word-break:break-all"></asp:Label>
             </td>
        </tr>
    </table>
    <br />
    <table id="Table3" width="100%" class="PerformanceLevel_table" 
        style="display: block; height: 167px;">
        <tr>
            <td colspan="2" class="PerformanceLevel_td">
                <b>D.My Training and activity here</b>
            </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                1. My overall experiences with the intern trainings and activities were excellent.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblTrainingView" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                2. I found the frequency and times of the trainings to be suitable for interns. 
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblTrainingSuitable" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                3. I found the trainings (such as NIO, research skill training, soft skill 
                training, etc.) were essential and beneficial for me as an intern.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblTrainingEssential" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                4. I found the activities (such as Family Day, New Year Party, 
                sports club activities and competitions, etc.) were interesting and engaging.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblActivityInterest" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Comments
            </td>
            <td class="PerformanceLevel_td">
                 <asp:Label ID="lbTrainingComments" runat="server" style="word-break:break-all"></asp:Label>
             </td>
        </tr>
    </table>
    <br />
    <table id="Table4" width="100%" class="PerformanceLevel_table" 
        style="display: block; height: 167px;">
        <tr>
            <td colspan="2" class="PerformanceLevel_td">
                <b>E. My life here</b>
            </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                1. I found a good work/life balance here at MSRA.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblBalance" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                2. The working environment here enabled me to do my work efficiently.   
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblWorkEnvironment" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                3. The total compensation package of MSRA (stipend, meal allowance,
                   etc.) was competitive within the market
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblCompensation" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                4. Overall, I’m satisfied with the support of Internship Program Team. 
                   I received support in the following areas:
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblSatisfaction" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;On board process
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblOnBoard" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Accommodation arrangements
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblAccommodation" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Timely payment of salary and meal allowance
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblSalaryAndMeal" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Reimbursement of air/train tickets, etc.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblReimbursement" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IT group support (E-mail account, PC, access, etc.)
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblITSupport" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Daily support and help
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblDailySupport" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Comments
            </td>
            <td class="PerformanceLevel_td">
                 <asp:Label ID="lbLifeComments" runat="server" style="word-break:break-all"></asp:Label>
             </td>
        </tr>
    </table>
    <br />
    <table id="Table5" width="100%" class="PerformanceLevel_table" 
        style="display: block; height: 167px;">
        <tr>
            <td colspan="2" class="PerformanceLevel_td">
                <b>F.My feeling and opinion of MS/MSRA</b>
            </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                1. I think Microsoft will lead a technology trend in the following 5 to 10 years.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblLeading" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                2. I think Microsoft has an innovative, open and attractive working environment.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblMSCulture" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                3. I am interested in returning to MS/MSRA for another internship.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblReturnAsIntern" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                4. I would like to join MS/MSRA after graduation.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblJoinMS" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                5. I would recommend MSRA to my schoolmates and friends as an excellent company to undertake an internship with.
            </td>
            <td class="PerformanceLevel_td" width="60%">
                 <asp:RadioButtonList ID="rblRecommend" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
        </tr>
        <tr>
            <td class="PerformanceLevel_td">
                Comments
            </td>
            <td class="PerformanceLevel_td">
                 <asp:Label ID="lbMSRAComments" runat="server" style="word-break:break-all"></asp:Label>
             </td>
        </tr>
    </table>
    <br/>
    <div class="panel_title_expand" onclick="ChangeStyle(this,'divPart3')" style="text-align:left">
        Part III. Open questions (You may write your comments and suggestions about the MSRA Intership Program in Chinese or English)
    </div>
     <table id="Table6" width="100%" class="PerformanceLevel_table" 
        style="display: block;">
        <tr>
            <td class="PerformanceLevel_td">
                 <asp:Label ID="lbComments" runat="server" Height="80px" style="word-break:break-all"></asp:Label>
             </td>
        </tr>
    </table>
</asp:Panel>
</td>
</tr>
</table>
