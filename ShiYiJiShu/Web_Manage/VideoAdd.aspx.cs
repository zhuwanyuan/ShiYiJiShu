using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShiYiJiShu.Data;
using Common;

namespace ShiYiJiShu.Web_Manage
{
    public partial class VideoAdd : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService _dataService = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                if (Request.QueryString["VideoID"] != null)
                {
                    int videoID = Convert.ToInt32(Request.QueryString["VideoID"].ToString());

                    Video model = _dataService.GetVideoByVideoID(videoID);

                    this.txtVideoName.Text = model.VideoName;
                    this.txtVideoUrl.Text = model.VideoUrl;
                    this.txtVideoIntro.Value = model.VideoIntro;
                    this.hidSmallPic.Value = model.VideoFilePath;
                    this.hidFileName.Value = model.VideoUploadUrl;
                    this.spFileName.Text = model.VideoUploadUrl;

                    this.img.Src = "../uploadpic/" + model.VideoFilePath;

                    if (model.ZhiDing == 1)
                    {
                        cbZhiDing.Checked = true;
                    }

                    //绑定一级类别
                    BindFirstClass();
                    for (int i = 0; i < DropDownList1.Items.Count; i++)
                    {
                        if (model.FirstClassID.ToString() == DropDownList1.Items[i].Value)
                        {
                            DropDownList1.Items[i].Selected = true;
                        }
                    }

                    //绑定二级类别
                    BindSecondClass();
                    for (int i = 0; i < DropDownList2.Items.Count; i++)
                    {
                        if (model.SecondClassID.ToString() == DropDownList2.Items[i].Value)
                        {
                            DropDownList2.Items[i].Selected = true;
                        }
                    }

                }
                else
                {
                    BindFirstClass();
                    BindSecondClass();
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int zhiding = 0;
            if (cbZhiDing.Checked)
            {
                zhiding = 1;
            }

            int userid = Convert.ToInt32(Request.Cookies["AdminUser"]["userid"]);
            int userGrade = Convert.ToInt32(Request.Cookies["AdminUser"]["usergrade"]);

            int activeFlag = 0;
            if (userGrade == 1)
            {
                activeFlag = 1;
            }

            if (Request.QueryString["VideoID"] != null)
            {
                int videoID = Convert.ToInt32(Request.QueryString["VideoID"].ToString());
                Video model = _dataService.GetVideoByVideoID(videoID);
                model.VideoName = this.txtVideoName.Text;
                model.VideoUrl = this.txtVideoUrl.Text;
                model.VideoIntro = this.txtVideoIntro.Value;
                model.VideoFilePath = this.hidSmallPic.Value;
                model.ZhiDing = zhiding;
                model.FirstClassID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                model.SecondClassID = Convert.ToInt32(DropDownList2.SelectedItem.Value);
                model.VideoUploadUrl = this.hidFileName.Value;

                if (_dataService.UpadteVideo(model)>0)
                {
                    bc.MessageBox("修改成功！", "VideoList.aspx");
                }
                else
                {
                    bc.MessageBox1("修改失败！");
                }
            }
            else
            {
                Video model = new Video();
                model.VideoName = this.txtVideoName.Text;
                model.VideoUrl = this.txtVideoUrl.Text;
                model.VideoIntro = this.txtVideoIntro.Value;
                model.VideoFilePath = this.hidSmallPic.Value;
                model.AddDate = DateTime.Now;
                model.ZhiDing = 0;
                model.HitCount = zhiding;
                model.UserID = userid;
                model.ActiveFlag = activeFlag;
                model.FirstClassID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                model.SecondClassID = Convert.ToInt32(DropDownList2.SelectedItem.Value);
                model.VideoUploadUrl = this.hidFileName.Value;

                if (_dataService.AddVideo(model) > 0)
                {
                    bc.MessageBox("添加成功！", "VideoList.aspx");
                }
                else
                {
                    bc.MessageBox1("添加失败！");
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSecondClass();
        }

        public void BindFirstClass()
        {
            IEnumerable<NewsClass> firstClasses = _dataService.GetSubClasses(11);

            DropDownList1.DataSource = firstClasses;
            DropDownList1.DataValueField = "ClassID";
            DropDownList1.DataTextField = "ClassName";
            DropDownList1.DataBind();
        }


        public void BindSecondClass()
        {
            int classid = Convert.ToInt32(DropDownList1.SelectedItem.Value);

            IEnumerable<NewsClass> firstClasses = _dataService.GetSubClasses(classid);

            DropDownList2.DataSource = firstClasses;
            DropDownList2.DataValueField = "ClassID";
            DropDownList2.DataTextField = "ClassName";
            DropDownList2.DataBind();
        }
    }
}