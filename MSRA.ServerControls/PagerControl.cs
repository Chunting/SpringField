using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace MSRA.SpringField.Controls
{
    public delegate void PagerClickEventHandler(object sender, PagerEventArgs e);//处理页面按钮事件
    public enum PagerStyleEnnm
    {
        Digg,
        Google
    }
    [DefaultProperty("PagerStyle"),
 ToolboxData("<{0}:PagerControl runat='server'></{0}:PagerControl>")]
    public class PagerControl : WebControl, IPostBackEventHandler, INamingContainer
    {
        private static readonly object EventClick = new object();
        [Category("Appearance"), DefaultValue(PagerStyleEnnm.Digg)]
        public virtual PagerStyleEnnm PagerStyle
        {
            get
            {
                object style = ViewState["PagerStyle"];
                return ((style == null) ? PagerStyleEnnm.Digg : (PagerStyleEnnm)style);
            }
            set
            {
                ViewState["PagerStyle"] = value;
            }
        }
        [Browsable(false)]
        public int CurrentPage
        {
            set
            {
                ViewState["CurrentPage"] = value;
            }
            get
            {
                object o = ViewState["CurrentPage"];
                return ((o == null) ? 1 : (int)ViewState["CurrentPage"]);
            }
        }
        [Browsable(false)]
        public int TotalCount
        {
            set
            {
                ViewState["TotalCount"] = value;
                ViewState["CurrentPage"] = 1;
            }
            get
            {
                object o = ViewState["TotalCount"];
                return ((o == null) ? 0 : (int)ViewState["TotalCount"]);
            }
        }

        [DefaultValue(10)]
        public int PageSize
        {
            set
            {
                ViewState["PageSize"] = value;
            }
            get
            {
                object o = ViewState["PageSize"];
                return ((o == null) ? 10 : (int)ViewState["PageSize"]);
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (PagerStyle == PagerStyleEnnm.Digg)
            {
                CreatDiggSytlePager(writer);
            }
            else
            {
                CreateGoogleStylePager(writer);
            }
        }
        #region Digg Style
        private void CreatDiggSytlePager(HtmlTextWriter writer)
        {
            int TotalPage = GetTotalPage(TotalCount);
            if (CurrentPage == 1 && TotalPage != 0)
            {
                WritePreviousAnchor(writer, false);
            }
            else if(CurrentPage != 1 && TotalPage != 0)
            {
                WritePreviousAnchor(writer, true);
            }
            if (TotalPage <= 15)
            {
                for (int i = 1; i <= TotalPage; i++)
                {
                    if (CurrentPage == i)
                    {
                        WriteCurrentPage(writer);
                    }
                    else
                    {
                        WriteAnchor(writer, i);
                    }
                }
            }
            else
            {
                if (CurrentPage <= 8)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        if (CurrentPage == i)
                        {
                            WriteCurrentPage(writer);
                        }
                        else
                        {
                            WriteAnchor(writer, i);
                        }
                    }
                    WriteSuspensionPoint(writer);
                    WriteAnchor(writer, TotalPage - 1);
                    WriteAnchor(writer, TotalPage);
                }
                else if (CurrentPage < TotalPage - 6)
                {
                    WriteAnchor(writer, 1);
                    WriteAnchor(writer, 2);
                    WriteSuspensionPoint(writer);
                    for (int i = -5; i <= 4; i++)
                    {
                        if (i == 0)
                        {
                            WriteCurrentPage(writer);
                        }
                        else
                        {
                            WriteAnchor(writer, CurrentPage + i);
                        }
                    }
                    WriteSuspensionPoint(writer);
                    WriteAnchor(writer, TotalPage - 1);
                    WriteAnchor(writer, TotalPage);
                }
                else
                {
                    WriteAnchor(writer, 1);
                    WriteAnchor(writer, 2);
                    WriteSuspensionPoint(writer);
                    for (int i = TotalPage - 9; i <= TotalPage; i++)
                    {
                        if (CurrentPage == i)
                        {
                            WriteCurrentPage(writer);
                        }
                        else
                        {
                            WriteAnchor(writer, i);
                        }
                    }
                }
            }
            if (CurrentPage == TotalPage && TotalPage != 0)
            {
                WriteNextAnchor(writer, false);
            }
            else if (CurrentPage != TotalPage && TotalPage != 0)
            {
                WriteNextAnchor(writer, true);
            }
        }
        private void WriteAnchor(HtmlTextWriter writer, int PageNumber)
        {
            System.Web.UI.ClientScriptManager cs = Page.ClientScript;
            writer.Write("<a href=\"");
            writer.Write(cs.GetPostBackClientHyperlink(this, PageNumber.ToString()));
            //writer.Write("javascript:__doPostBack('PagerControl1','3');");
            //writer.Write("default.aspx?pageindex=" + PageNumber.ToString());
            writer.Write("\">" + PageNumber.ToString() + "</a>");                      
        }

        private void WriteSuspensionPoint(HtmlTextWriter writer)
        {
            writer.Write("<span>...</span>");
        }

        private void WriteCurrentPage(HtmlTextWriter writer)
        {
            writer.Write("<span class='current'>" + CurrentPage.ToString() + "</span>");
        }
        private void WritePreviousAnchor(HtmlTextWriter writer, bool Enable)
        {
            writer.Write(String.Format("<span style=\"font-weight:bold;\">Page {0} of {1}({2} items)&nbsp;</span>", CurrentPage, GetTotalPage(TotalCount), TotalCount));
            System.Web.UI.ClientScriptManager cs = Page.ClientScript;            
            if (Enable)
            {
                writer.Write("<a class='nextprev' title='Go to Previous Page' href=\"");
                writer.Write(cs.GetPostBackClientHyperlink(this, Convert.ToString(CurrentPage - 1)));
               // writer.Write("default.aspx?pageindex=" + Convert.ToString(CurrentPage - 1));
                writer.Write("\">&#171; Previous</a>");
            }
            else
            {
                writer.Write("<span class='nextprev'>&#171; Previous</span>");
            }
        }
        private void WriteNextAnchor(HtmlTextWriter writer, bool Enable)
        {
            System.Web.UI.ClientScriptManager cs = Page.ClientScript;
            if (Enable)
            {
                writer.Write("<a class='nextprev' title='Go to Next Page' href=\"");
                writer.Write(cs.GetPostBackClientHyperlink(this, Convert.ToString(CurrentPage + 1)));
                //writer.Write("default.aspx?pageindex=" +Convert.ToString(CurrentPage + 1));
                writer.Write("\">Next &#187;</a>");
            }
            else
            {
                writer.Write("<span class='nextprev'>Next &#187;</span>");
            }
        }
        #endregion

        #region Google Style
        private void CreateGoogleStylePager(HtmlTextWriter writer)
        {
            int TotalPage = GetTotalPage(TotalCount);
            if (CurrentPage != 1)
            {
                WritePreviousAnchor(writer, true);
            }
            for (int i = -10; i < 10; i++)
            {

                if (CurrentPage + i > 0 && CurrentPage + i <= TotalPage)
                {
                    if (i == 0)
                    {
                        WriteCurrentPage(writer);
                    }
                    else
                    {
                        WriteAnchor(writer, CurrentPage + i);
                    }
                }
            }
            if (CurrentPage != TotalPage)
            {
                WriteNextAnchor(writer, true);
            }
        }
        #endregion
        protected virtual void OnPagerClick(PagerEventArgs e)
        {
            PagerClickEventHandler clickHandler = (PagerClickEventHandler)Events[EventClick];
            if (clickHandler != null)
            {
                clickHandler(this, e);
            }
        }
        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            CurrentPage = Convert.ToInt32(eventArgument);            
            PagerEventArgs e = new PagerEventArgs(CurrentPage - 1, PageSize);
            OnPagerClick(e);
        }
        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                if (Page != null)
                {
                    Page.VerifyRenderingInServerForm(this);
                }

                base.Render(writer);
            }
            catch
            {
                writer.Write("PagerControl");
            }
            
        }
        protected override void OnPreRender(EventArgs e)
        {
            string includeTemplate =
            "<link rel='stylesheet' text='text/css' href='{0}' />";
            string includeLocation =
                  Page.ClientScript.GetWebResourceUrl(this.GetType(), "MSRA.SpringField.Controls.pagercontrol.css");
            LiteralControl include =
                  new LiteralControl(String.Format(includeTemplate, includeLocation));
            this.Page.Header.Controls.Add(include);
            base.OnPreRender(e);
        }
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "pages");
        }
        private int GetTotalPage(int TotalCount)
        {
            int TotalPage = TotalCount / PageSize;
            if (TotalCount % PageSize != 0)
            {
                TotalPage++;
            }
            return TotalPage;
        }
        [Category("Action")]
        public event PagerClickEventHandler PagerClick
        {
            add
            {
                Events.AddHandler(EventClick, value);
            }
            remove
            {
                Events.RemoveHandler(EventClick, value);
            }
        }
    }
}
