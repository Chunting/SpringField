<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.FavoritesList" MasterPageFile="~/SpringfieldMaster.master" Codebehind="FavoritesList.aspx.cs" %>
<%@ Register Src="Controls/ApplicantsList.ascx" TagName="ApplicantsList" TagPrefix="uc1" %>
<asp:Content ID="ctntFavoriteList" runat="server" ContentPlaceHolderID="mainPlaceHolder">
<div style="width: 100%;">
<p style="font-size:20">Favorite Applicants</p>
<div id="notionbar" style="padding:5 0;background-color:#FFFFE1;border:solid 1px #FEC951;height:50;vertical-align:middle;margin:3 0;text-align:left">
            <table style="width:100%;height:100%">
                <tr>
                    <td align="left" valign="middle">
                        <ul>
                            <li>
                            This page shows the applicants in your favorite list. 
                            </li>
                            <li>
                            You can click their names to see detail information, or click related icons for more actions.
                            </li>
                        </ul>    
                    </td>
                </tr>
            </table>                
            </div>


     <uc1:ApplicantsList ID="ApplicantsList1" runat="server" OnPagerClickChanged="ApplicantsList1_PagerClick" ListType="favorite"/>
</div>
</asp:Content>