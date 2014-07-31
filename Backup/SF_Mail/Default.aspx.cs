using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SF_Mail
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ShowMailList();

            //
            //MailServiceProvider.interviewCheck();
        }
        public void ShowMailList()
        {
            MailDataClassesDataContext db = new MailDataClassesDataContext();
            var db_mail = db.sf_Emails;
            var query = from mails in db_mail
                        where mails.IsSend == 0
                        select mails;
            GridViewMailList.DataSource = query;
            GridViewMailList.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MailServiceProvider.SendUnsentMails(null);
            Label1.Text = "邮件已经全部发送，若下面有剩余，则代表发送错误，请检查日志文件";
        }

        //发送按钮的单击事件
        protected void GridViewMailList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Send")
            {
                int mailID = Convert.ToInt32(e.CommandArgument);

                //GridViewRow row = GridView1.Rows[index];
                MailDataClassesDataContext db = new MailDataClassesDataContext();
                var db_mail = db.sf_Emails;
                var query = from email in db.sf_Emails
                            where email.EmailId == mailID
                            select email;
                sf_Email mail = query.Single();

                if (MailServiceProvider.SendMail(mail, db))
                {
                    Label1.Text = "id为" + mailID.ToString() + "的邮件已成功发送";
                    //TODO 想把按钮改成 “已发送”，避免重复
                }
                else
                {
                    Label1.Text = "id为" + mailID.ToString() + "的邮件发送失败";
                }
            }

        }
    }
}
