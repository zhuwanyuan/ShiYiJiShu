using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

namespace ShiYiJiShu.Web_Master
{
    public partial class Top : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!bc.CheckAdminLogin(this))
                {
                    bc.MessageBox("请登录！", "login.aspx");
                }

                this.lbUserName.Text = bc.GetAdminUserName(this);
            }
        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["AdminUser"];
            cookie.Expires = DateTime.Today.AddDays(-1);
            Response.Cookies.Add(cookie);
            Response.Write("<script>parent.location.href='login.aspx';alert('注销成功');</script>");
        }
    }
}