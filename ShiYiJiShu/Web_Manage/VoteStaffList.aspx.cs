using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;

namespace ShiYiJiShu.Web_Manage
{
    public partial class VoteStaffList : System.Web.UI.Page
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

                BindRepeater();
            }
        }

        protected void BindRepeater()
        {
            int curpage = 1;

            if (Request.QueryString["curpage"] != null)
            {
                curpage = int.Parse(Request.QueryString["curpage"]);
            }

            DataSet ds = bc.GetDataSetByPage("VoteStaff", "*", "StaffID", 20, curpage, 0, 1, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.Repeater1.DataSource = ds;
                this.Repeater1.DataBind();

                int TotalCount = bc.GetTotalCount("VoteStaff", "1=1");
                if (TotalCount > 20)
                {
                    this.pager.InnerHtml = bc.GetPageLink(20, TotalCount);
                }
            }
            else
            {
                lbResult.Text = "<font style='font-size:14px; color:red'>没有找到相关记录!</font>";
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (e.CommandName == "delete")
                {
                    Label lbStaffID = (Label)e.Item.FindControl("lbStaffID");
                    string sql = "DELETE FROM VoteStaff WHERE StaffID=" + Convert.ToInt32(lbStaffID.Text);
                    bc.ExecuteSQL(sql);
                    BindRepeater();
                }
            }
        }

  
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.txtKey.Text.Trim();
            string sql = "select [StaffID],[StaffPhoto],[StaffName],[Company] from VoteStaff where StaffName like '%" + keyword + "%'  order by StaffID desc";
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