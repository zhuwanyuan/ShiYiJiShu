using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Web_Master
{
    public partial class ProjectRegister_ : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        
        DataService service = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!bc.CheckAdminLogin(this))
                {
                    bc.MessageBox("请登录！", "login.aspx");
                }

                BindRepeater();

                int projectid = Convert.ToInt32(Request.QueryString["projectid"]);

                //int classid = Convert.ToInt32(Request.QueryString["classid"]);
                //this.title.Text = service.GetProjectByProjectID(projectid).ProjectName;
            }
        }

        protected void BindRepeater()
        {
            
            int curpage = 1;
            int projectid = Convert.ToInt32(Request.QueryString["projectid"]);

            if (Request.QueryString["curpage"] != null)
            {
                curpage = int.Parse(Request.QueryString["curpage"]);
            }
 
            List<ProjectRegister> newslist = service.GetProjectRegisterList(projectid, 20, curpage);

            this.Repeater1.DataSource = newslist;
            this.Repeater1.DataBind();

            int TotalCount = service.GetProjectRegisterTotalCount(projectid);
            if (TotalCount > 20)
            {
                this.pager.InnerHtml = bc.GetPageLink(20, TotalCount, "projectid", projectid.ToString());
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
               
                Label lbRegID = (Label)e.Item.FindControl("lbRegID");

                if (e.CommandName == "delete")
                {
                    string sql = "Delete From ProjectRegister where RegID=" + Convert.ToInt32(lbRegID.Text);
                    bc.ExecuteSQL(sql);
                    BindRepeater();
                }
 
            }
        }

      
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //string keyword = this.txtKey.Text.Trim();
            //string sql = "select [NewsID],[NewsTitle],[Keyword],[Description],[BigClassID],[SmallClassID],[ZhiDing],[ActiveFlag] from News where newstitle like '%" + keyword + "%'  order by NewsID desc";
            //DataSet ds = bc.GetDataSet(sql);
 
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    this.Repeater1.DataSource = ds;
            //    this.Repeater1.DataBind();
            //}
            //else
            //{
            //    this.Repeater1.Visible = false;
            //    lbResult.Text = "<font style='font-size:14px; color:red'>没有找到相关记录!</font>";
            //}

            //this.pager.Visible = false;
        }
    }
}