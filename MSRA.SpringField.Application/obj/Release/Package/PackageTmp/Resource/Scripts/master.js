/*
Author: NickLedson
Date: 2006-4-11
Content: Float Windows Manager
Version: 1.00 alpha
*/

//panel onclick
function ChangeStyle(obj,nextObj)
{
	var oContent = document.getElementById(nextObj);
	if(oContent.style.display == "block")
	{
	    obj.className = "panel_title_close"; 
		oContent.style.display = "none";
	}
	else
	{
		obj.className = "panel_title_expand";
		oContent.style.display = "block";
	}
}

//abandon selectioin
//window.document.onselectstart = function()
//{
//	event.cancelBubble = true
//	event.returnValue = false;

//	return false;
//}

//var obj;
//function AddFloatWindow()
//{
//    try
//    {
//        obj = windowManager.CreateNewWindow("test","test","test");
//        var temp = document.getElementById("temp_content");
//        //alert(temp.innerHTML);
//        var str = temp.innerHTML;
//        obj.SetContent(str);
//    }
//    catch(e)
//    {
//        alert(e);
//    }
//}

//function CloseTheWindow()
//{
//    if(obj == null) return;
//    obj.CloseWindow();
//}

function OpenNewWindow(url, para)
{
    window.open(url + "?applicant=" + para,"_blank",'height=560,width=760,status=no,toolbar=no,menubar=no,location=no,resizable=no,scrollbars=yes');
}
function OpenReferralWindow(url, para)
{
    window.open(url + "?applicant=" + para,"_self",'height=900,width=1100,status=no,toolbar=no,menubar=no,location=no,resizable=no,scrollbars=yes');
}

function ConfirmDelete()
{
    return confirm("Are your sure you want to delete the selected applicant(s)?\nIt's an unrecovered operation!");
}

//function MultiInterview()
//{
//    var curForm = document.forms();
//    var oldAction = curForm.action;
//    var oldTarget = curForm.target;
//    //alert(oldAction);
//    //alert(oldTarget);
//    window.open("about:blank","winInterview",'height=500,width=680,status=no,toolbar=no,menubar=no,location=no,resizable=no,scrollbars=yes');
//    curForm.target = "winInterview";
//    curForm.action = "AddReferral.aspx";
//    curForm.submit();
//    curForm.target = oldTarget;
//    curForm.action = oldAction;
//}

function ConfirmMultiInterview() {
    var isValidated = true;
    var dateObj = document.getElementById("txt_due_date");
    if (dateObj.value == "") {
        var reqDateObj = document.getElementById("reqDateInput");
        reqDateObj.innerHTML = "Required!";
        //return false;
        isValidated = false;
    }

    var txtObj = document.getElementById("tbInterviewer");
    if (txtObj != null && txtObj.value == "") {
        var reqInterviewer = document.getElementById("reqInterviewer");
        reqInterviewer.innerHTML = "Required!";
        //return false;
        isValidated = false;
    }

    if (isValidated)
        return confirm("Are you sure to schedule this interview?");
    else
        return false;
}

//function ConfirmFeedback()
//{
//    return confirm("Are you sure you want to submit this feedback?");
//}

function ConfirmApproval()
{
    return confirm("Are you sure to approve this suggestion?");
}

function ConfirmApprovalValidate()
{
    ValidateGroup = arguments[0];
   if(Page_ClientValidate(ValidateGroup))
   {
        return confirm('Are you sure to approve this suggestion?');
   }
   else
   {
        return false;
   }
}

function ConfirmMentorSuggestion()
{
    ValidateGroup = arguments[0];
   if(Page_ClientValidate(ValidateGroup))
   {
        return confirm('Are you sure to submit this request?');
   }
   else
   {
        return false;
   }
}

function ConfirmReferral()
{
    return confirm("Are you sure to forward this applicant?");
}

function ConfirmURReferral()
{
    return confirm("Are you sure to refer these applicants?");
}

function ConfirmMultiRefer()
{
    return confirm("Are you sure to refer these applicants?");
}

function ConfirmDeleteComment()
{
    return confirm("Are you sure to delete the selected comment?\nIt's an unrecovered operation!");
}
function ConfirmOnBoard()
{
    return confirm("Are you sure to change this candidate's status to \"On Board\"");
}
function ConfirmDecline()
{
    return confirm("Are you sure to change this candidate's status to \"Offer Decline\"");
}
function ConfirmReject()
{
    return confirm("Are you sure to change this candidate's status to \"Rejected\"");
}
function ConfirmDeletePA()
{
    return confirm("Are you sure to delete this Performance Assessment?");
}

function ConfirmChangeInterviewer() {
    return confirm("Are you sure to change the interviewer?");
}