using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using ShiYiJiShu.Data;
using System.Data;

namespace ShiYiJiShu.Web_Master
{
    public partial class Left : System.Web.UI.Page
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

                //判断权限
                int userGrade = Convert.ToInt32(Request.Cookies["AdminUser"]["usergrade"]);

                if (userGrade == 2)
                {
                    divGuanYuWoMen.Visible = false;
     
                    divZhiNengBuMen.Visible = false;
 
                    divTouPiaoGuanLi.Visible = false;
                    
                    divWangZhanGuanLi.Visible = false;
                    divZhuanJiaLeiBie.Visible = false;
                    divShiPinLeiBie.Visible = false;
                    divXiangMuLeiBie.Visible = false;
                    divTouPiaoGuanLi.Visible = false;

                    divJiDiLeiBie.Visible = false;
                }

                //文章管理
                IEnumerable<NewsClass> list1 = service.GetSubClasses(1);
                repWenZhangGuanLi.DataSource = list1;
                repWenZhangGuanLi.DataBind();

                //工作介绍
                IEnumerable<NewsClass> list139 = service.GetSubClasses(139);
                repGongZuoJieShao.DataSource = list139;
                repGongZuoJieShao.DataBind();

                //入选技术
                IEnumerable<NewsClass> list140 = service.GetSubClasses(140);
                repRuXuanJiShu.DataSource = list140;
                repRuXuanJiShu.DataBind();

                //会员中心
                IEnumerable<NewsClass> list9 = service.GetSubClasses(172);
                repHuiYuanZhongXin.DataSource = list9;
                repHuiYuanZhongXin.DataBind();

                //关于我们
                IEnumerable<NewsClass> list15 = service.GetSubClasses(15);
                repGuanYuWoMen.DataSource = list15;
                repGuanYuWoMen.DataBind();

                //职能部门
                IEnumerable<NewsClass> list28 = service.GetSubClasses(28);
                repZhiNengBuMen.DataSource = list28;
                repZhiNengBuMen.DataBind();

            }
        }

        public string GetLinkUrl(string classid, string type)
        {
            string link = "";
            string sql = "select classname from NewsSmallClass where ClassID=" + classid;
            DataSet ds = bc.GetDataSet(sql);
            string classname = ds.Tables[0].Rows[0][0].ToString();

            if (type == "1")
            {
                link = "<a href='info.aspx?classid=" + classid + "' target='mainFrame' class='left-font03'  onclick=" + "tupian('" + classid + "');" + ">" + classname + "</a>";

            }
            else
            {
                link = "<a href='list.aspx?classid=" + classid + "'  target='mainFrame' class='left-font03'  onclick=" + "tupian('" + classid + "');" + ">" + classname + "</a>&nbsp; |&nbsp; <a href='newsadd.aspx?classid=" + classid + "' target='mainFrame' class='left-font03'  onclick=" + "tupian('" + classid + "');" + ">添加</a>";
            }

            return link;
        }
    }
}