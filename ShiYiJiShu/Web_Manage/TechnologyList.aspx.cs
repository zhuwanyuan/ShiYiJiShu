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
    public partial class TechnologyList : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService _dataService = new DataService();
 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!bc.CheckAdminLogin(this))
                {
                    bc.MessageBox("请登录！", "login.aspx");
                }

                int classid = Convert.ToInt32(Convert.ToInt32(Request.QueryString["classid"]));
                NewsClass newsClass = _dataService.GetNewsClassByClassID(classid);
                this.title.Text = newsClass.ClassName;

                BindRepeater();
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (e.CommandName == "delete")
                {
                    Label lbTechnologyID = (Label)e.Item.FindControl("TechnologyID");
                    string sql = "Delete from Technology where TechID=" + Convert.ToInt32(lbTechnologyID.Text);
                    bc.ExecuteSQL(sql);
                    BindRepeater();
                }
            }
        }

        protected void BindRepeater()
        {
            int curpage = 1;

            if (Request.QueryString["curpage"] != null)
            {
                curpage = int.Parse(Request.QueryString["curpage"]);
            }

            int classid = Convert.ToInt32(Convert.ToInt32(Request.QueryString["classid"]));

            DataSet ds = bc.GetDataSetByPage("Technology", "*", "TechID", 20, curpage, 0, 1, " ClassID=" + classid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.Repeater1.DataSource = ds;
                this.Repeater1.DataBind();

                int TotalCount = bc.GetTotalCount("Technology", " ClassID=" + classid);
                if (TotalCount > 20)
                {
                    this.pager.InnerHtml = bc.GetPageLink(20, TotalCount, "classid", classid.ToString());
                }
            }
            else
            {
                this.Repeater1.Visible = false;
                lbResult.Text = "<font style='font-size:14px; color:red'>没有找到相关记录!</font>";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}