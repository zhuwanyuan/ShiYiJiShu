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
    public partial class List : System.Web.UI.Page
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

                int classid = Convert.ToInt32(Request.QueryString["classid"]);
                this.title.Text = service.GetNewsClassByClassID(classid).ClassName;
            }
        }

        protected void BindRepeater()
        {
            int classid = Convert.ToInt32(Request.QueryString["classid"]);
            int curpage = 1;

            if (Request.QueryString["curpage"] != null)
            {
                curpage = int.Parse(Request.QueryString["curpage"]);
            }

            int userid = bc.GetAdminUserID();
            int usergrade = bc.GetAdminGrade();

            List<News> newslist = null;

            newslist = service.GetNewsListByAdminID(userid, classid, 20, curpage);

            if (newslist.Count > 0)
            {
                this.Repeater1.DataSource = newslist;
                this.Repeater1.DataBind();

                int TotalCount = service.GetNewsTotalCountByAdminID(userid, classid);
                if (TotalCount > 20)
                {
                    this.pager.InnerHtml = bc.GetPageLink(20, TotalCount, "classid", classid.ToString());
                }
            }         
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                string classid = Request.QueryString["classid"].ToString();
                Label lbNewsID = (Label)e.Item.FindControl("NewsID");

                if (e.CommandName == "delete")
                {
                    string sql = "Delete From News where NewsID=" + Convert.ToInt32(lbNewsID.Text);
                    bc.ExecuteSQL(sql);
                    BindRepeater();
                }

                if (e.CommandName == "approve")
                {
                    string sql = "UPDATE News SET ACTIVEFLAG = 1 where NewsID=" + Convert.ToInt32(lbNewsID.Text);
                    bc.ExecuteSQL(sql);
                    //BindRepeater();
                    Response.Redirect("List.aspx?classid=" + classid);
                }

                if (e.CommandName == "unapprove")
                {
                    string sql = "UPDATE News SET ACTIVEFLAG = 0 where NewsID=" + Convert.ToInt32(lbNewsID.Text);
                    bc.ExecuteSQL(sql);
                    Response.Redirect("List.aspx?classid=" + classid);
                }
            }
        }

        public bool CheckAdminDisplay()
        {
            bool show = false;
            int usergrade = bc.GetAdminGrade();

            if (usergrade == 1)
            {
                show = true;
            }

            return show;
        }

        public bool CheckMemberDisplay()
        {
            bool show = false;
            int usergrade = bc.GetAdminGrade();

            if (usergrade == 2)
            {
                show = true;
            }

            return show;
        }

        public bool CheckApproveButtonShow(int activeflag)
        {
            bool show = false;
 
            if (activeflag == 0)
            {
                show = true;
            }

            return show;
        }

        public bool CheckUnApproveButtonShow(int activeflag)
        {
            bool show = false;

            if (activeflag == 1)
            {
                show = true;
            }

            return show;
        }

        public string CheckZhiDing(string zhiding)
        {
            string isZhiDing = "";
            if (zhiding == "1")
            {
                isZhiDing = "<font color='red'>[推荐]</font>";
            }

            return isZhiDing;
        }

        public string CheckShenHe(string shenhe)
        {
            string isShenHe = "未审核";
            if (shenhe == "1")
            {
                isShenHe = "<font color='green'>[已审核]</font>";
            }
            else if (shenhe == "0")
            {
                isShenHe = "<font color='red'>[未审核]</font>";
            }

            return isShenHe;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.txtKey.Text.Trim();
            string sql = "select [NewsID],[NewsTitle],[Keyword],[Description],[BigClassID],[SmallClassID],[ZhiDing],[ActiveFlag] from News where newstitle like '%" + keyword + "%'  order by NewsID desc";
            DataSet ds = bc.GetDataSet(sql);
 
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.Repeater1.DataSource = ds;
                this.Repeater1.DataBind();
            }
            else
            {
                this.Repeater1.Visible = false;
                lbResult.Text = "<font style='font-size:14px; color:red'>没有找到相关记录!</font>";
            }

            this.pager.Visible = false;
        }
    }
}