<%@ Page Language="C#" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="Springfield.Components" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    bool enable_installer = false;
    string internRecruiterAlias = "fareast\v-mwchen";
    string realName = "Wen CHEN";
    string groupName = "HR";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (enable_installer)
        { 
            if (internRecruiterAlias != null || internRecruiterAlias != string.Empty)
            {
                if (internRecruiterAlias.Split('\\').Length != 2)
                {
                    throw new Exception("Invalid admin name");
                }
                if (!Roles.IsUserInRole(internRecruiterAlias, RoleType.InternRecruiter.ToString()))
                {
                    Roles.AddUserToRole(internRecruiterAlias, RoleType.InternRecruiter.ToString());
                    SiteUser newUser = new SiteUser(internRecruiterAlias);
                    newUser.RealName = realName;
                    newUser.GroupName = groupName;
                    newUser.UpdateCommentInfo();
                }
                else
                {
                    SiteUser newUser = new SiteUser(internRecruiterAlias);
                    newUser.RealName = realName;
                    newUser.GroupName = groupName;
                    newUser.UpdateCommentInfo();
                }
            }
            Response.Write(String.Format("Springfield install successfully!{0} has been set as the admin role.", internRecruiterAlias));
        }    
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Springfield Install</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
