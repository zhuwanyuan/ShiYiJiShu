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
    public partial class SelectProjectList : System.Web.UI.Page
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
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lbProjectID = (Label)e.Item.FindControl("lbProjectID");

                if (e.CommandName == "delete")
                {
                    string sql = "delete from SelectProject where ProjectID=" + Convert.ToInt32(lbProjectID.Text);
                    bc.ExecuteSQL(sql);
                    BindRepeater();
                }

                if (e.CommandName == "approve")
                {
                    string sql = "UPDATE SelectProject SET ACTIVEFLAG = 1 where ProjectID=" + Convert.ToInt32(lbProjectID.Text);
                    bc.ExecuteSQL(sql);
                    //BindRepeater();
                    Response.Redirect("SelectProjectList.aspx");
                }

                if (e.CommandName == "unapprove")
                {
                    string sql = "UPDATE SelectProject SET ACTIVEFLAG = 0 where ProjectID=" + Convert.ToInt32(lbProjectID.Text);
                    bc.ExecuteSQL(sql);
                    Response.Redirect("SelectProjectList.aspx");
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

            int userid = bc.GetAdminUserID();
            int usergrade = bc.GetAdminGrade();

            List<SelectProject> prolist = service.GetSelectProjectsByAdminID(userid, 20, curpage);
            if (prolist.Count > 0)
            {
                this.Repeater1.DataSource = prolist;
                this.Repeater1.DataBind();

                int TotalCount = service.GetSelectProjectTotalCountByAdminID(userid);
                if (TotalCount > 20)
                {
                    this.pager.InnerHtml = bc.GetPageLink(20, TotalCount);
                }
            }
            else
            {
                this.Repeater1.DataSource = null;
                this.Repeater1.DataBind();
                lbResult.Text = "<font style='font-size:14px; color:red'>没有找到相关记录!</font>";
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
            string sql = "select [ProjectID],[ProjectPic],[ProjectName],[TuiJian],[ActiveFlag] from SelectProject where ProjectName like '%" + keyword + "%'  order by ProjectID desc";
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

        public string CheckTuiJian(string tuijian)
        {
            string result = "";
            if (tuijian == "1")
            {
                result = "<font color='red'>[推荐]</font>";
            }

            return result;
        }
    }
}