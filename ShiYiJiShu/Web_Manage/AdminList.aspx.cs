using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.web_manage
{
    public partial class AdminList : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService _dataService = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                BindRepeater();
            }
        }

      
        protected void repAdminList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (e.CommandName == "delete")
                {
                    Label lbUserID = (Label)e.Item.FindControl("lbUserID");

                    int userid = Convert.ToInt32(lbUserID.Text);

                    if (_dataService.DeleteAdminUser(userid) > 0)
                    {
                        BindRepeater();
                    }
                }
            }
        }

        protected void BindRepeater()
        {
            List<AdminUser> admins = _dataService.GetAdminUserByLevel(2);
            repAdminList.DataSource = admins;
            repAdminList.DataBind();
        }
    }
}