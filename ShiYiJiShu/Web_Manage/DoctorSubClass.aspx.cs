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
    public partial class DoctorSubClass : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService _dataService = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                BindRepeater();

                int classid = Convert.ToInt32(Request.QueryString["classid"].ToString());

                NewsClass model = _dataService.GetNewsClassByClassID(classid);
                this.lbName.Text = model.ClassName;
 
                if (Request.QueryString["subclassid"] != null)
                {
                    int subclassid = Convert.ToInt32(Request.QueryString["subclassid"].ToString());

                    NewsClass subclass = _dataService.GetNewsClassByClassID(subclassid);
                    this.txtClassName.Text = subclass.ClassName;
                    this.Button1.Text = "修改";
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int classid = Convert.ToInt32(Request.QueryString["classid"].ToString());
           

            if (Request.QueryString["subclassid"] != null)
            {
                int subclassid = Convert.ToInt32(Request.QueryString["subclassid"].ToString());

                NewsClass model = _dataService.GetNewsClassByClassID(subclassid);
                model.ClassName = this.txtClassName.Text;
                model.UpdateTime = DateTime.Now;
 
                if (_dataService.UpdateNewsClass(model) > 0)
                {
                    bc.MessageBox("修改成功！", "DoctorSubClass.aspx?classid=" + classid);
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
                model.ClassLevel = 3;
                model.ParentClassID = classid;
                model.ClassType = "1";
                model.UpdateTime = DateTime.Now;

                if (_dataService.AddNewsClass(model) > 0)
                {
                    bc.MessageBox("添加成功！", "DoctorSubClass.aspx?classid=" + classid);
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
                    Label lbClassID = (Label)e.Item.FindControl("lbClassID");
                    int classid = Convert.ToInt32(lbClassID.Text);

                    if (_dataService.DeleteFriendLink(classid) > 0)
                    {
                        BindRepeater();
                    }
                }
            }
        }

        protected void BindRepeater()
        {
            int classid = Convert.ToInt32(Request.QueryString["classid"].ToString());

            IEnumerable<NewsClass> friendLinks = _dataService.GetSubClasses(classid);
            repClassList.DataSource = friendLinks;
            repClassList.DataBind();
        }
    }
}