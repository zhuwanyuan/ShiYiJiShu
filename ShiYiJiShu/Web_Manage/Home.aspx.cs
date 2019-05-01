using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Web_Master
{
    public partial class Home : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
   

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

               
            }
        }
 
    }
}