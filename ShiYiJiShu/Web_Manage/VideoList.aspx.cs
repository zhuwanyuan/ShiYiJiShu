﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Common;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Web_Manage
{
    public partial class VideoList : System.Web.UI.Page
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

                BindRepeater();
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lbVideoID = (Label)e.Item.FindControl("VideoID");

                if (e.CommandName == "delete")
                {
                    
                    string sql = "delete from Video where VideoID=" + Convert.ToInt32(lbVideoID.Text);
                    bc.ExecuteSQL(sql);
                    BindRepeater();
                }
                if (e.CommandName == "approve")
                {
                    string sql = "UPDATE Video SET ACTIVEFLAG = 1 where VideoID=" + Convert.ToInt32(lbVideoID.Text);
                    bc.ExecuteSQL(sql);
                    //BindRepeater();
                    Response.Redirect("VideoList.aspx");
                }

                if (e.CommandName == "unapprove")
                {
                    string sql = "UPDATE Video SET ACTIVEFLAG = 0 where VideoID=" + Convert.ToInt32(lbVideoID.Text);
                    bc.ExecuteSQL(sql);
                    Response.Redirect("VideoList.aspx");
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

            List<Video> vodlist = _dataService.GetVideoListByAdminID(userid, 20, curpage);
            if (vodlist.Count > 0)
            {
                this.Repeater1.DataSource = vodlist;
                this.Repeater1.DataBind();

                int TotalCount = _dataService.GetVideoTotalCountByAdminID(userid);
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
            string sql = "select [VideoID],[VideoFilePath],[VideoName],[ActiveFlag] from Video where VideoName like '%" + keyword + "%'  order by VideoID desc";
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