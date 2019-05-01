﻿using System;
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
    public partial class VideoClass : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService _dataService = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                BindRepeater();

                if (Request.QueryString["classid"] != null)
                {
                    int classid = Convert.ToInt32(Request.QueryString["classid"].ToString());

                    NewsClass model = _dataService.GetNewsClassByClassID(classid);

                    this.txtClassName.Text = model.ClassName;
                    this.Button1.Text = "修改";
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["classid"] != null)
            {
                int classid = Convert.ToInt32(Request.QueryString["classid"].ToString());

                NewsClass model = _dataService.GetNewsClassByClassID(classid);
                model.ClassName = this.txtClassName.Text;
                model.UpdateTime = DateTime.Now;
 
                if (_dataService.UpdateNewsClass(model) > 0)
                {
                    bc.MessageBox("修改成功！", "VideoClass.aspx");
                }
                else
                {
                    bc.MessageBox1("修改失败！");
                }
            }
            else
            {
                NewsClass model = new NewsClass();
                model.ClassName = this.txtClassName.Text;
                model.ClassLevel = 2;
                model.ParentClassID = 11;
                model.ClassType = "1";
                model.UpdateTime = DateTime.Now;

                if (_dataService.AddNewsClass(model) > 0)
                {
                    bc.MessageBox("添加成功！", "VideoClass.aspx");
                }
                else
                {
                    bc.MessageBox1("添加失败！");
                }
            }
        }

        protected void repClassList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (e.CommandName == "delete")
                {
                    Label lbLinkID = (Label)e.Item.FindControl("lbLinkID");
                    int linkid = Convert.ToInt32(lbLinkID.Text);

                    if (_dataService.DeleteFriendLink(linkid) > 0)
                    {
                        BindRepeater();
                    }
                }
            }
        }

        protected void BindRepeater()
        {
            IEnumerable<NewsClass> friendLinks = _dataService.GetSubClasses(11);
            repClassList.DataSource = friendLinks;
            repClassList.DataBind();
        }
    }
}