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
    window.open(url + "?applicant=" + para,null,'height=500,width=720,status=no,toolbar=no,menubar=no,location=no,resizable=no,scrollbars=yes');
}

function OpenWindow(url, para)
{
    window.open(url + para,null,'height=500,width=720,status=no,toolbar=no,menubar=no,location=no,resizable=no,scrollbars=yes');
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

function ConfirmMultiInterview()
{
    return confirm("Are you sure to arrange this interview?");
}

function ConfirmFeedback()
{
    return confirm("Are you sure to submit this feedback?");
}

function ConfirmApproval()
{
    return confirm("Are you sure to approve this suggestion?");
}

function ConfirmReferral()
{
    return confirm("Are you sure to refer this applicant?");
}

function ConfirmURReferral()
{
    return confirm("Are you sure to refer these applicants?");
}

function ConfirmMultiRefer()
{
    return confirm("Are you sure to refer these applicants?");
}

function ConfirmOffLineHiring()
{
    return confirm("Are you sure to hire this applicant?");
}

function ConfirmKeyReferral()
{
    return confirm("Are you sure to refer this applicant?");
}

function ConfirmReject()
{
    return confirm("Are you sure to reject this applicant?");
}

function ConfirmPend()
{
    return confirm("Are you sure to pend this applicant?\nThis applicant will be add to your favorite list.");
}