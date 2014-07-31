<%@ Page Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="SurveyCommentsReport.aspx.cs" Inherits="MSRA.SpringField.Application.SurveyCommentsReport1" %>

<asp:Content ID="mainContent" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <script language="JavaScript" src="../../Resource/Scripts/popcalendar.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Resource/Scripts/master.js"></script>
    <asp:Label Visible="false" ID="lbDateSpan" runat="server" Font-Bold="True"></asp:Label>
    <div class="toolbar"> 
        <table style="height:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';" 
                    style="padding:0 10;height:30;"><a href="../../Default.aspx" style="text-decoration: none">
                    <img src="../../Resource/Images/backhome.png" width="24" height="24" align="absmiddle" />
                    <b>Back to Home</b></a></td>
                <td onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';"
                    style="padding:0 10;height:30;">
                    <asp:ImageButton Width="24" Height="24" ImageAlign="AbsMiddle" ImageUrl="~/Resource/Images/export_excel.png" OnClick="Export_Click" ID="expExcel" runat="server" Font-Bold="true"/>
                    <label style="cursor:hand" for="<%=expExcel.ClientID %>"><b>Export to Excel</b></label>                    
                    </td>
            </tr>
        </table>
        </div>       
    
    <div id="filter_title" class="panel_title_expand" >Filter Condition</div> <%--onclick="ChangeStyle(this,'filter_content')"--%>
    <div id="filter_content" class="panel_content" style="display: block;border-left:solid 1px #999;border-right:solid 1px #999">
    <table style="width:100%">
        <tr>
            <td align="right">Checkout Date: </td>
            <td>
                <asp:TextBox ID="tbStartDate" runat="server"></asp:TextBox>&nbsp;&nbsp;To
                <asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox><br/>
                <asp:CompareValidator ID="cmpDate" runat="server" ControlToValidate="tbEndDate" ControlToCompare="tbStartDate"
        Operator="GreaterThan" SetFocusOnError="true" ErrorMessage="End date must be greater than start date!"
        Type="Date" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td align="right">Group: </td>
            <td colspan ="3">
                <asp:DropDownList ID="ddlGroup" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" style="width:150px; height: 29px;">InternshipDuration: </td>
            <td colspan ="3" style="height: 29px">
                <asp:DropDownList ID="ddlTimeSpan" runat="server" Width="">
                    <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="0 - 3M" Value="1"></asp:ListItem>
                    <asp:ListItem Text="4 - 6M" Value="2"></asp:ListItem>
                    <asp:ListItem Text="7 - 12M" Value="3"></asp:ListItem>
                    <asp:ListItem Text="One year above" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>   
        </tr>
        <tr>
            <td colspan="2" align="center" style="height:30px">
                <div style="width:120px;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                     <asp:ImageButton Width="24" Height="24" CssClass="img_icon" ImageUrl="~/Resource/Images/generate.png" ID="btnSubmit"
                            runat="server" AlternateText="Generate" OnClick="btnSubmit_Click" 
                            CausesValidation="true" ImageAlign="AbsMiddle" />
                            <label for="<%=btnSubmit.ClientID %>"><span style="cursor:hand">Generate</span></label>
                </div> 
           </td>
        </tr>
    </table>
    </div>
    <div>
        <div id="content_title" class="panel_title_expand" ">
            Applicants -
            <asp:Label ID="lbCount" runat="server" Text="0"></asp:Label>&nbsp;Records Found
        </div>
        <div id="content_content" class="panel_content" style="display: block; overflow:scroll">
            <asp:GridView ID="gvSurveyReport" Width="100%" AllowPaging="True" Pagesize="15"
                OnPageIndexChanging="gvSurveyReport_PageIndexChanging" RowStyle-Height="30"
                runat="server" AutoGenerateColumns="False" Height="">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Name</HeaderTemplate>
                        <ItemTemplate>
                            <a id="A1" href='<%# GetViewSurveyLink(Eval("ApplicantId").ToString().Trim())%>' runat="server">
                                <%# Eval("InternName").ToString().Trim()%>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Mentor</HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "MentorAlias").ToString().Trim()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Group</HeaderTemplate>
                        <ItemTemplate>
                            <%# GetGroupNameByID(Eval("GroupId").ToString().Trim())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Duration</HeaderTemplate>
                        <ItemTemplate>
                            <%# GetDurationByID(Eval("InternshipDuration").ToString().Trim())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            University</HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "HighestEducationalInstitution").ToString().Trim()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField>
                            <HeaderTemplate>
                                Degree</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetDegree(DataBinder.Eval(Container.DataItem, "ApplicantId").ToString().Trim())%>
                            </ItemTemplate>
                      </asp:TemplateField>
                    
                    <%--
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Overall View Comments</HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "OverallComments").ToString().Length > 10 ? DataBinder.Eval(Container.DataItem, "OverallComments").ToString().Substring(0, 10) + "..." : DataBinder.Eval(Container.DataItem, "OverallComments").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Work Comments</HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "WorkComments").ToString().Length > 10 ? DataBinder.Eval(Container.DataItem, "WorkComments").ToString().Substring(0, 10) + "..." : DataBinder.Eval(Container.DataItem, "WorkComments").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Mentor Comments</HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "MentorComments").ToString().Length > 10 ? DataBinder.Eval(Container.DataItem, "MentorComments").ToString().Substring(0, 10) + "..." : DataBinder.Eval(Container.DataItem, "MentorComments").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Training Comments</HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "TrainingComments").ToString().Length > 10 ? DataBinder.Eval(Container.DataItem, "TrainingComments").ToString().Substring(0, 10) + "..." : DataBinder.Eval(Container.DataItem, "TrainingComments").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Life Comments</HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "LifeComments").ToString().Length > 10 ? DataBinder.Eval(Container.DataItem, "LifeComments").ToString().Substring(0, 10) + "..." : DataBinder.Eval(Container.DataItem, "LifeComments").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            MSRA Comments</HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "MSRAComments").ToString().Length > 10 ? DataBinder.Eval(Container.DataItem, "MSRAComments").ToString().Substring(0, 10) + "..." : DataBinder.Eval(Container.DataItem, "MSRAComments").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    
                    
                    <%--<asp:TemplateField>
                        <HeaderTemplate>
                            Comments</HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Comments").ToString().Length > 10 ? DataBinder.Eval(Container.DataItem, "Comments").ToString().Substring(0, 10) + "..." : DataBinder.Eval(Container.DataItem, "Comments").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <EmptyDataTemplate>No Checkout Survey meet your conditions.</EmptyDataTemplate>
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#9C969C" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="#DCDCDC" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
