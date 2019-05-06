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
    public partial class SelectProjectAdd : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService _dataService = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                if (Request.QueryString["ProjectId"] != null)
                {
                    int projectid = Convert.ToInt32(Request.QueryString["ProjectId"].ToString());

                    SelectProject model = _dataService.GetSelectProjectByID(projectid);

                    this.txtProjectName.Text = model.ProjectName;
                    this.txtProjectCompany.Text = model.ProjectCompany;
                    this.txtProjectLeader.Text = model.ProjectLeader;
                    this.txtProjectIntro.Text = model.ProjectIntro;
                    this.txtProjectLink.Text = model.ProjectLink;
                    this.hidSmallPic.Value = model.ProjectPic;
 
                    this.img.Src = "../uploadpic/" + model.ProjectPic;

                    if (model.TuiJian == 1)
                    {
                        cbTuiJian.Checked = true;
                    }


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
            int tuijian = 0;
            if (cbTuiJian.Checked)
            {
                tuijian = 1;
            }

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

            if (Request.QueryString["ProjectId"] != null)
            {
                int projectId = Convert.ToInt32(Request.QueryString["ProjectId"].ToString());
                
                SelectProject model = _dataService.GetSelectProjectByID(projectId);

                model.ProjectName = this.txtProjectName.Text;
                model.ProjectCompany = this.txtProjectCompany.Text;
                model.ProjectLeader = this.txtProjectLeader.Text;
                model.ProjectIntro = this.txtProjectIntro.Text;
                model.ProjectLink = this.txtProjectLink.Text;
                model.ProjectPic = this.hidSmallPic.Value;
                model.FirstClassID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                model.SecondClassID = Convert.ToInt32(DropDownList2.SelectedItem.Value);
 
                model.TuiJian = tuijian;
                model.ZhiDing = zhiding;
                model.UserID = userid;
                model.AddDateTime = DateTime.Now;
 

                if (_dataService.UpdateSelectProject(model) > 0)
                {
                    bc.MessageBox("修改成功！", "SelectProjectList.aspx");
                }
                else
                {
                    bc.MessageBox1("修改失败！");
                }
            }
            else
            {
                SelectProject model = new SelectProject();

                model.ProjectName = this.txtProjectName.Text;
                model.ProjectCompany = this.txtProjectCompany.Text;
                model.ProjectLeader = this.txtProjectLeader.Text;
                model.ProjectIntro = this.txtProjectIntro.Text;
                model.ProjectLink = this.txtProjectLink.Text;
                model.ProjectPic = this.hidSmallPic.Value;
                model.FirstClassID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                model.SecondClassID = Convert.ToInt32(DropDownList2.SelectedItem.Value);
           

                model.ActiveFlag = activeFlag;
                model.HitCount = 0;
                model.TuiJian = tuijian;
                model.ZhiDing = zhiding;
                model.UserID = userid;
                model.AddDateTime = DateTime.Now;


                if (_dataService.AddSelectProject(model) > 0)
                {
                    bc.MessageBox("添加成功！", "SelectProjectList.aspx");
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
            IEnumerable<NewsClass> firstClasses = _dataService.GetSubClasses(7);

            if (firstClasses.Count() > 0)
            {
                DropDownList1.DataSource = firstClasses;
                DropDownList1.DataValueField = "ClassID";
                DropDownList1.DataTextField = "ClassName";
                DropDownList1.DataBind();
            }   
        }

        public void BindSecondClass()
        {
            if (DropDownList1.Items.Count > 0)
            {
                int classid = Convert.ToInt32(DropDownList1.SelectedItem.Value);

                IEnumerable<NewsClass> secondClasses = _dataService.GetSubClasses(classid);

                DropDownList2.DataSource = secondClasses;
                DropDownList2.DataValueField = "ClassID";
                DropDownList2.DataTextField = "ClassName";
                DropDownList2.DataBind();
            }
        }
 
    }
}