using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Web_Master
{
    public partial class NewsAdd : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService service = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                int classID = int.Parse(Request.QueryString["classid"].ToString());

                this.lbName.Text = service.GetNewsClassByClassID(classID).ClassName + " 添加";
                this.txtSource.Text = "健康服务适宜技术分会";

                if (Request.QueryString["newsid"] != null)
                {
                    int newsID = Convert.ToInt32(Request.QueryString["newsid"].ToString());

                    News model = service.GetNewsByNewsID(newsID);

                    this.txtNewsTitle.Text = model.NewsTitle;
                    this.txtKeyword.Text = model.Keyword;
                    this.txtDescription.Text = model.Description;
                    this.txtContent.Value = model.NewsContent;
                    this.txtSource.Text = model.Source;
                    this.hidSmallPic.Value = model.NewsPic;
                    this.img.Src = System.Configuration.ConfigurationManager.AppSettings["DomainName"].ToString() + "/uploadpic/" + model.NewsPic;

                    if (model.ZhiDing == 1)
                    {
                        cbZhiDing.Checked = true;
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int userid = Convert.ToInt32(Request.Cookies["AdminUser"]["userid"]);
            int userGrade = Convert.ToInt32(Request.Cookies["AdminUser"]["usergrade"]);

            string newsTitle = this.txtNewsTitle.Text;
            string keyword = this.txtKeyword.Text;
            string description = this.txtDescription.Text;
            string newsContent = this.txtContent.Value.Trim().ToString();
            string source = this.txtSource.Text;
            int classid = Convert.ToInt32(Request.QueryString["classid"].ToString());
            DateTime createDate = DateTime.Now;
            DateTime modifyDate = DateTime.Now;
            string createBy = Request.Cookies["AdminUser"].Value;
            string newsPic = hidSmallPic.Value;

            int activeFlag = 0;
            if (userGrade == 1)
            {
                activeFlag = 1;
            }
 
            int bigclassid = service.GetParentClassID(classid);

            int zhiding = 0;
            if (cbZhiDing.Checked)
            {
                zhiding = 1;
            }

            if (Request.QueryString["newsid"] != null)
            {
                int newsid = Convert.ToInt32(Request.QueryString["newsid"].ToString());

                News model = service.GetNewsByNewsID(newsid);
                model.NewsTitle = newsTitle;
                model.Keyword = keyword;
                model.Description = description;
                model.NewsContent = newsContent;
                model.Source = source;
                model.BigClassID = bigclassid;
                model.SmallClassID = classid;
                model.NewsID = newsid;
                model.ModifyDate = modifyDate;
                model.ZhiDing = zhiding;
                model.NewsPic = newsPic;

                if (service.UpadteNews(model) > 0)
                {
                    bc.MessageBox("修改成功！", "list.aspx?classid=" + classid);
                }
                else
                {
                    bc.MessageBox1("修改失败！");
                }
            }
            else
            {
                News model = new News();
                model.NewsTitle = newsTitle;
                model.Keyword = keyword;
                model.Description = description;
                model.NewsContent = newsContent;
                model.Source = source;
                model.BigClassID = bigclassid;
                model.SmallClassID = classid;
                model.ModifyDate = modifyDate;
                model.CreateBy = createBy;
                model.HitCount = 0;
                model.CreateDate = createDate;
                model.ZhiDing = zhiding;
                model.NewsPic = newsPic;
                model.ActiveFlag = activeFlag;
                model.UserID = userid;

                if (service.AddNews(model) > 0)
                {
                    bc.MessageBox("添加成功！", "list.aspx?classid=" + classid);
                }
                else
                {
                    bc.MessageBox1("添加失败！");
                }
            }
        }
    }
}