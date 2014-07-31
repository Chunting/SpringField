<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InputSurvey.aspx.cs" Inherits="MSRA.SpringField.PA_Intern.InputSurvey" %>

<?xml version="1.0" encoding="utf-8" ?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-CN">
    <script src="Script/popcalendar.js" type="text/javascript" language="javascript"></script>

<head id="Head1" runat="server">
    <title>Checkout Survey</title>
    <link href="Style/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 47px;
        }
        .style2
        {
            height: 25px;
        }
        .style3
        {
            height: 46px;
        }
        .style4
        {
            height: 28px;
        }
        .style5
        {
            height: 70px;
        }
        .style6
        {
            height: 62px;
        }
    </style>
</head>
<body style="background-image: url('images/BG.png'); background-position: bottom;
    background-repeat: repeat-x; background-color: #e0eff6; text-align: center">
    <center>
    <form id="frmMain" runat="server">
        <div style="background-image: url('images/SurveyBanner.jpg'); width: 1200px; height: 83.5px;
            text-align: right; vertical-align: top">
            <span onclick="window.showModalDialog('Help.htm','','status:no;resizable:yes;dialogHeight:300px;dialogWidth:460px;unadorne:yes');"
                style="color: White; text-decoration: underline; cursor:hand">
                Help</span>
            &nbsp;&nbsp;&nbsp;&nbsp;</div>
        <div>
            <div style="background-color: #e0eff6; width: 1200px; border-bottom: solid 1px #a3c8df;
                border-left: solid 1px #a3c8df; border-right: solid 1px #a3c8df; text-align: left">
             
             <div style="min-height:700px">
        <br />
        <br />
        <div style="background-color: #879096; height: 20px; padding-left: 20px; vertical-align: middle">
            <b style="font-size: 16px; color: White">Notice</b></div>
        <div style="padding-left: 20px;">
            <ul style="margin-top: 0px;">
                <li>Please respond to this short survey (5-10 minutes) to inform MSRA of your experience, insights and recommendations for improvements 
                <br/>with the MSRA Internship program.  Thanks!</li>
            </ul>
        </div>
        <div>
            <div style="background-color: #879096; height: 20px; padding-left: 20px; vertical-align: middle;
                color: White">
                <b style="font-size: 16px;">Part I</b>. Input your information.</div>
            <div style="padding-left: 20px;">
            <br />
            <table class="BasicInfoTable">
            
                <tr>
                    <td style="font-size: 14px;">
                        Your Name(In English):
                    </td>
                    <td colspan ="3">
                        <asp:label ID="lbInternName" runat="server"></asp:label>
                    </td>
                </tr>
                
                <tr>
                    <td style="font-size: 14px;">
                        I'm currently working for Group:
                    </td>
                    <td colspan ="3">
                        <asp:DropDownList ID="ddlGroup" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 14px;">
                        I've worked for MSRA for(totally):
                    </td>
                    <td colspan ="3">
                        <asp:DropDownList ID="ddlTimeSpan" runat="server" Width="">
                            <asp:ListItem Text="0 - 3M" Value="1" ></asp:ListItem>
                            <asp:ListItem Text="4 - 6M" Value="2"></asp:ListItem>
                            <asp:ListItem Text="7 - 12M" Value="3"></asp:ListItem>
                            <asp:ListItem Text="One year above" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                    </td>   
                </tr>
                <tr>
                    <td style="font-size: 14px;">
                        Check in Date:
                    </td>
                    <td>
                        <asp:TextBox ID="tbCheckinDate" runat="server"></asp:TextBox>&nbsp;
                        <input type="button" value="Select" id="btnCheckinDate" name="btnCheckinDate" runat="server" /><br />
                        <asp:RequiredFieldValidator ID="rfvCheckinDate" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="tbCheckinDate"></asp:RequiredFieldValidator>
                    </td>
                    <td style="font-size: 14px;">
                        Check Out Date:
                    </td>
                    <td>
                        <asp:TextBox ID="tbCheckoutDate" runat="server"></asp:TextBox>&nbsp;
                        <input type="button" value="Select" id="btnCheckoutDate" name="btnCheckoutDate" runat="server" /><br />
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="tbCheckoutDate"></asp:RequiredFieldValidator>
                       
                      <%--  <asp:CompareValidator ID="cmpDate1" runat="server" ControlToValidate="tbCheckoutDate"
                    ControlToCompare="tbCheckinDate" Operator="LessThan" SetFocusOnError="true"
                    ErrorMessage="End date must be greater than start date!" Type="Date" Display="Dynamic" /> --%>
                    
                    </td>
                </tr>
               </table>                    
            </div>
        </div>
        <br />
        <div>
             <div style="background-color: #879096; height: 20px; padding-left: 20px; vertical-align: middle;
                color: White">
                <b style="font-size: 16px;">Part II</b>. My overall experience at MSRA.
             </div>
            <div style="padding-left: 20px;">
            <br />
                <table class="BasicInfoTable">
                    <tr>
                        <td colspan="2" style="font-size: 14px;">
                            <b>A.My overall view of my MSRA internship experience</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="font-size: 14px;">
                            1. As an intern, I had a fulfilling and meaningful experience in working at MSRA.
                        </td>
                        <td class="style1" width="60%" style="font-size: 12px;" >
                            <asp:RadioButtonList ID="rblOverallView" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate0" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblOverallView"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px;" class="style5">
                            Please leave your comments here if you choose somewhat or strongly
                            <br/>disagree when answering the above questions
                        </td>
                        <td class="style5">
                            <asp:TextBox ID="tbOverallComments" runat="server" TextMode="MultiLine" Height="80px" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                  </table>
                  <br />
                  <table class="BasicInfoTable">
                    <tr>
                        <td colspan="2" style="font-size: 14px;" class="style2">
                            <b>B.My work at MSRA</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" style="font-size: 14px;">
                            1. I liked the kind of work/project I was associated with.
                        </td>
                        <td class="style2" width="60%" style="font-size: 12px;">
                            <asp:RadioButtonList ID="rblLikeWork" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate1" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblLikeWork"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" style="font-size: 14px;">
                            2. I am now equipped with enough background information to continue down my future career path.
                        </td>
                        <td class="style2">
                            <asp:RadioButtonList ID="rblBackground" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate2" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblBackground"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" style="font-size: 14px;">
                            3. The amount of work assigned was appropriate for the length of my internship.
                        </td>
                        <td class="style2">
                            <asp:RadioButtonList ID="rblWorkAmount" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate3" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblWorkAmount"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" style="font-size: 14px;">
                            4. My work at MSRA matched my personal objectives.
                        </td>
                        <td class="style2">
                            <asp:RadioButtonList ID="rblObjects" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate4" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblObjects"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style6" style="font-size: 14px;">
                            5. Overall, I developed technical and professional skills during my 
                            <br/>internship at MSRA that will be useful in my future career:
                        </td>
                        <td class="style6">
                            <asp:RadioButtonList ID="rblDevelopmentSkill" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate5" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblDevelopmentSkill"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size: 14px;">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Research papers writing
                        </td>
                        <td class="style3">
                            <asp:RadioButtonList ID="rblResearchSkill" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate6" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblResearchSkill"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size: 14px;">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Software development
                        </td>
                        <td class="style3">
                            <asp:RadioButtonList ID="rblSDESkill" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate7" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblSDESkill"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size: 14px;">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Project management
                        </td>
                        <td class="style3">
                            <asp:RadioButtonList ID="rblProjectSkill" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate8" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblProjectSkill"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size: 14px;">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Design ability
                        </td>
                        <td class="style3">
                            <asp:RadioButtonList ID="rblDesignSkill" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate9" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblDesignSkill"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size: 14px;">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Effective communications/presentations
                        </td>
                        <td class="style3">
                            <asp:RadioButtonList ID="rblCommunicationSkill" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate10" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblCommunicationSkill"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size: 14px;">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Teamwork
                        </td>
                        <td class="style3">
                            <asp:RadioButtonList ID="rblTeamwork" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate11" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblTeamwork"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px;">
                            Please leave your comments here if you choose somewhat or strongly  
                            <br />disagree by answering the above questions
                        </td>
                        <td>
                            <asp:TextBox ID="tbWorkComments" runat="server" TextMode="MultiLine" Height="80px" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                 </table>
                 <br/>
                 <table class="BasicInfoTable">
                    <tr>
                        <td colspan="2" style="font-size: 14px;" class="style4">
                            <b>C.My mentor and work group</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            1. Clear goals were explained and set for me from the very beginning of my internship.
                        </td>
                        <td class="style4" width="60%" style="font-size: 12px;">
                            <asp:RadioButtonList ID="rblMentorSetGoal" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate12" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblMentorSetGoal"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            2. I could obtain help and coaching from my mentor in a timely manner.
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rblHelpFromMentor" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate13" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblHelpFromMentor"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr> 
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            3. My mentor and work group made good use of my skills and abilities.
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rblMakeGoodUse" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate14" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblMakeGoodUse"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            4. Group members treated each other with respect.
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rblRespect" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate15" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblRespect"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px;">
                            Please leave your comments here if you choose somewhat or strongly  
                            <br/>disagree by answering the above questions
                        </td>
                        <td>
                            <asp:TextBox ID="tbMentorComments" runat="server" TextMode="MultiLine" Height="80px" Width="100%"></asp:TextBox>
                        </td>
                    </tr>                     
                </table>  
                <br/>
                 <table class="BasicInfoTable">
                    <tr>
                        <td colspan="2" style="font-size: 14px;">
                            <b>D.My training and activity sessions</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            1. My overall experiences with the intern trainings and activities were excellent.
                        </td>
                        <td class="style4" width="60%" style="font-size: 12px;">
                            <asp:RadioButtonList ID="rblTrainingView" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate16" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblTrainingView"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            2. I found the frequency and times of the trainings to be suitable for interns.  
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rblTrainingSuitable" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate17" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblTrainingSuitable"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr> 
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            3. I found the trainings (such as NIO, research skill training, soft skill 
                            <br/>training, etc.) were essential and beneficial for me as an intern.
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rblTrainingEssential" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate18" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblTrainingEssential"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            4. I found the activities (such as Family Day, New Year Party, 
                            <br/>sports club activities and competitions, etc.) were interesting and engaging.
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rblActivityInterest" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate19" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblActivityInterest"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px;">
                            Please leave your comments here if you choose somewhat or strongly  
                            <br />disagree by answering the above questions
                        </td>
                        <td>
                            <asp:TextBox ID="tbTrainingComments" runat="server" TextMode="MultiLine" Height="80px" Width="100%"></asp:TextBox>
                        </td>
                    </tr>                     
                </table>  
                <br/>
                 <table class="BasicInfoTable">
                    <tr>
                        <td colspan="2" style="font-size: 14px;">
                            <b>E. My life at MSRA</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            1. I found a good work/life balance here at MSRA.
                        </td>
                        <td class="style4" width="60%" style="font-size: 12px;">
                            <asp:RadioButtonList ID="rblBalance" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate20" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblBalance"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            2. The working environment here enabled me to do my work efficiently. 
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rblWorkEnvironment" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate21" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblWorkEnvironment"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr> 
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            3. The total compensation package from MSRA (stipend, meal allowance,
                            etc.) was competitive within the market
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rblCompensation" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate22" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblCompensation"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            4. Overall, I’m satisfied with the support from the Internship Program Team. 
                            I received support in the following areas:
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rblSatisfaction" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate23" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblSatisfaction"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size: 14px;">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;On board process
                        </td>
                        <td class="style3">
                            <asp:RadioButtonList ID="rblOnBoard" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate24" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblOnBoard"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size: 14px;">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Accommodation arrangements
                        </td>
                        <td class="style3">
                            <asp:RadioButtonList ID="rblAccommodation" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate25" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblAccommodation"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size: 14px;">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Timely payment of salary and meal allowance
                        </td>
                        <td class="style3">
                            <asp:RadioButtonList ID="rblSalaryAndMeal" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate26" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblSalaryAndMeal"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size: 14px;">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Reimbursement of air/train tickets, etc.
                        </td>
                        <td class="style3">
                            <asp:RadioButtonList ID="rblReimbursement" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate27" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblReimbursement"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size: 14px;">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IT group support (E-mail account, PC, access, etc.)
                        </td>
                        <td class="style3">
                            <asp:RadioButtonList ID="rblITSupport" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate28" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblITSupport"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size: 14px;">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Daily support and help
                        </td>
                        <td class="style3">
                            <asp:RadioButtonList ID="rblDailySupport" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate29" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblDailySupport"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px;">
                            Please leave your comments here if you choose somewhat or strongly  
                            <br />disagree by answering the above questions
                        </td>
                        <td>
                            <asp:TextBox ID="tbLifeComments" runat="server" TextMode="MultiLine" Height="80px" Width="100%"></asp:TextBox>
                        </td>
                    </tr>                     
                </table>
                <br/>
                <table class="BasicInfoTable">
                    <tr>
                        <td colspan="2" style="font-size: 14px;">
                            <b>F.My feelings and opinions of MS/MSRA</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            1. I think Microsoft will lead a technology trend in the following 5 to 10 years.
                        </td>
                        <td class="style4" width="60%" style="font-size: 12px;">
                            <asp:RadioButtonList ID="rblLeading" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate30" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblLeading"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            2. I think Microsoft has an innovative, open and attractive working environment. 
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rblMSCulture" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate31" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblMSCulture"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr> 
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            3. I am interested in returning to MS/MSRA for another internship.
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rblReturnAsIntern" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate32" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblReturnAsIntern"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            4. I would like to join MS/MSRA after graduation.
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rblJoinMS" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate33" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblJoinMS"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size: 14px;">
                            5. I would recommend MSRA to my schoolmates and friends as an excellent company to undertake an internship with.
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rblRecommend" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >strongly agree</asp:ListItem>
                                <asp:ListItem Value="1" >somewhat agree</asp:ListItem>
                                <asp:ListItem Value="2" >neither agree nor disagree</asp:ListItem>
                                <asp:ListItem Value="3" >somewhat disagree</asp:ListItem>
                                <asp:ListItem Value="4" >strongly disagree</asp:ListItem>
                                <asp:ListItem Value="5" >N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCheckoutDate34" runat="server" ErrorMessage="Required Field!"
                            ForeColor="red" ControlToValidate="rblRecommend"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px;" class="style4">
                            Please leave your comments here if you choose somewhat or strongly 
                            <br/>disagree by answering the above questions
                        </td>
                        <td class="style4">
                            <asp:TextBox ID="tbMSRAComments" runat="server" TextMode="MultiLine" Height="80px" Width="100%"></asp:TextBox>
                        </td>
                    </tr>                     
                </table>
            </div>  
        </div>
        <br/>
        <div>
             <div style="background-color: #879096; height: 20px; padding-left: 20px; vertical-align: middle;
                color: White">
                <b style="font-size: 16px;">Part III</b>. Open questions (You may write your comments and suggestions about the MSRA Intership Program in Chinese or English)</div>
            <div style="padding-left: 20px;">
              <table class="BasicInfoTable">
               <tr>
                <td class="style5">
                    <asp:TextBox ID="tbComments" runat="server" TextMode="MultiLine" Height="120px" Width="100%"></asp:TextBox>
                </td>
               </tr>
              </table>
            </div>  
        </div>
         <br/>
        <div style="text-align: center;">
            <asp:Button ID="btnSummit" Width="100px" BackColor="#35769E"  ForeColor="white" runat="server" Text="Submit" OnClick="btnSummit_Click" />
        </div>
        <br />
    </div>
   </div>
            <div style="width: 1200px; height: 35px; color: White; text-align: right; font-size: 12px;
                padding-top: 10px">
                Internal use only, ©2009 Microsoft Research Asia, Uiversity Relation Group. All
                rights reserved.<img src="images/logo_ms-w.png" /></div>
        </div>
    </form>
    </center>
</body>
</html>
