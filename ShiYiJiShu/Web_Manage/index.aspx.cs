using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

namespace ShiYiJiShu.Web_Manage
{
    public partial class index : System.Web.UI.Page
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

            }
        }
    }
}