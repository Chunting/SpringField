/*
Author: NickLedson
Date: 2006-4-12
Content: Float Windows Manager
Version: 1.00 alpha

Hint: Remember to include FloatWindow.css and floatwindow images folder!
*/

var currentMoveObj = null;    //当前拖动对象
var relLeft = 0;    //鼠标按下位置相对对象位置
var relTop = 0;
var imageUrl = "Resource/Images/floatwindow/";
var windowArray = new Array();

//浮动窗口对象
function FloatWindow(window_name,window_title,window_content,z_index)
{
    var m_WinName = window_name;
    var m_Title = window_title;
    var m_Content = window_content;
    var m_zIndex = z_index;
    var old_index = 2000;
    
    //float window
    var oCurWindow = document.createElement("div");
    oCurWindow.id = m_WinName;
    oCurWindow.className = "float_window";
    
    //window title
    var oWinTitle = document.createElement("div");
    oWinTitle.id = m_WinName + "_title";
    oWinTitle.className = "window_title_expand";
    
    //title text
    var oWinTitleText = document.createElement("div");
    oWinTitleText.id = m_WinName + "_title_text";
    oWinTitleText.className = "window_title_text";
    if(m_Title != "")
    {
        oWinTitleText.innerText = window_title;
    }
    else
    {
        oWinTitleText.innerText = "Float Window";
    }
    oWinTitle.appendChild(oWinTitleText);
    
    //title button
    var oWinTitleButton = document.createElement("div");
    oWinTitleButton.id = m_WinName + "_title_button";
    oWinTitleButton.className = "window_title_button";
    
    //collapse button
    var oCollapseButton = document.createElement("img");
    oCollapseButton.id = m_WinName + "_collapse_button";
    oCollapseButton.alt = "Collapse";
    oCollapseButton.src = imageUrl + "collapse.gif";
    oCollapseButton.onmouseover = function()
    {
        oCollapseButton.src = imageUrl + "collapse_hover.gif";
    }
    oCollapseButton.onmouseout = function()
    {
        oCollapseButton.src = imageUrl + "collapse.gif";
    }
    oCollapseButton.onclick = function()
    {
        if(oWinContent.style.display == "none")
        {
            oWinContent.style.display = "block";
            oWinTitle.className = "window_title_expand";
        }
        else
        {
            oWinContent.style.display = "none";
            oWinTitle.className = "window_title_close";
        }
    }
    oWinTitleButton.appendChild(oCollapseButton);
    
    //close button
    var oCloseButton = document.createElement("img");
    oCloseButton.id = m_WinName + "_close_button";
    oCloseButton.alt = "close";
    oCloseButton.src = imageUrl + "close.gif";
    oCloseButton.onmouseover = function()
    {
        oCloseButton.src = imageUrl + "close_hover.gif";
    }
    oCloseButton.onmouseout = function()
    {
        oCloseButton.src = imageUrl + "close.gif";
    }
    oCloseButton.onclick = function()
    {
        windowManager.UnloadWindow(oCurWindow);
    }
    oWinTitleButton.appendChild(oCloseButton);
    
    oWinTitle.appendChild(oWinTitleButton);
    
    oWinTitle.onmousedown = function()
    {
        currentMoveObj = oCurWindow;    //当对象被按下时，记录该对象
        relLeft = event.x - oCurWindow.offsetLeft;
        relTop = event.y - oCurWindow.offsetTop;
    }

    oCurWindow.appendChild(oWinTitle);
    
    //window content
    var oWinContent = document.createElement("div");
    oWinContent.id = m_WinName + "_content";
    oWinContent.className = "window_content";
    oWinContent.innerHTML = m_Content;
    oCurWindow.appendChild(oWinContent);
    
    oCurWindow.style.zIndex = m_zIndex;
    
    //将窗体提到最前
    function PopUp()
    {
        var tempObj;
        var i;
        for(i = 0;i < windowArray.length;i++)
        {
            tempObj = windowArray[i];
            if(tempObj.GetZ_Index() > oCurWindow.style.zIndex)
            {
                tempObj.SetZ_Index(tempObj.GetZ_Index() - 1);
            }
        }
        oCurWindow.style.zIndex = 2000 + windowArray.length;
    }
    
    oCurWindow.onmousedown = function()
    {
        PopUp();
    }
        
        
    this.PopToFront = function()
    {
        PopUp();
    }
    
    this.SetTitle = function(title_str)
    {
        oWinTitleText.innerText = title_str;
    }
    
    this.SetContent = function(content_str)
    {
        oWinContent.innerHTML = content_str;
    }
    
    this.GetWindowRef = function()
    {
        return oCurWindow;
    }
    
    this.GetZ_Index = function()
    {
        return oCurWindow.style.zIndex;
    }
    
    this.SetZ_Index = function(zindex)
    {
        oCurWindow.style.zIndex = zindex;
    }
    
    this.GetWindowName = function()
    {
        return m_WinName;
    }
    
    this.CloseWindow = function()
    {
        windowManager.DestoryWindow(this);
    }
}

window.document.onmouseup = function()
{
    currentMoveObj = null;    //当鼠标释放时同时释放拖动对象
}

function MoveWindow()
{
    if(currentMoveObj != null)
    {
        currentMoveObj.style.pixelLeft = event.x - relLeft;
        currentMoveObj.style.pixelTop = event.y - relTop;
    } 
}

window.document.onmousemove = MoveWindow;



function WindowManager()
{
    var newWindow = null;
    var cur_zIndex;

    this.CreateNewWindow = function(window_name,window_title,window_content)
    {
        var i;
        var tempObj = null;
        for(i = 0;i < windowArray.length; i++)
        {
            tempObj = windowArray[i];
            if(tempObj.GetWindowName() == window_name)
            {
                //throw "[" + window_name + "] is an existed window.\nYou can't create two windows with a same name!";
                //return null;
            }
        }
        cur_zIndex = 2001 + windowArray.length;
        newWindow = new FloatWindow(window_name,window_title,window_content,cur_zIndex);
        window.status = newWindow.GetWindowRef().style.zIndex;
        document.body.appendChild(newWindow.GetWindowRef());
        windowArray.push(newWindow);
        window.status = "There is " + windowArray.length + " windows now.";
        return newWindow;
    }
    
    this.DestoryWindow = function(hWindow)
    {
        if(hWindow == null)
        {
            throw "The window you want to close is not existed!";
        }
        var i;
        var tempObj = null;
        for(i = 0;i < windowArray.length;i++)
        {
            tempObj = windowArray[i];
            if(tempObj == hWindow)
            {
                windowArray.splice(i,1);
                hWindow.GetWindowRef().removeNode(true);
                break;
            }        
        }
        window.status = "There is " + windowArray.length + " windows now.";
    }
    
    this.UnloadWindow = function(hWinDiv)
    {
        if(hWinDiv == null)
        {
            throw "The window you want to close is not existed!";
        }
        var i;
        var tempObj = null;
        for(i = 0;i < windowArray.length;i++)
        {
            tempObj = windowArray[i];
            if(tempObj.GetWindowRef() == hWinDiv)
            {
                windowArray.splice(i,1);
                hWinDiv.removeNode(true);
                break;
            }        
        }
        window.status = "There is " + windowArray.length + " windows now.";
    }
}

var windowManager = new WindowManager();