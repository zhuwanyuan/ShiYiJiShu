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
    public partial class ProjectAdd : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService _dataService = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                if (Request.QueryString["ProjectID"] != null)
                {
                    int projectID = Convert.ToInt32(Request.QueryString["ProjectID"].ToString());

                    Project model = _dataService.GetProjectByProjectID(projectID);

                    this.txtProjectName.Text = model.ProjectName;
                    this.txtProjectFullName.Text = model.ProjectFullName;
                    this.txtKeyword.Text = model.Keyword;
                    this.txtDescription.Text = model.Description;
                    this.txtProjectIntro.Value = model.ProjectIntro;
                    this.hidSmallPic.Value = model.ProjectPic;
                    

                    this.img.Src = "../uploadpic/" + model.ProjectPic;

                    if (model.TuiJian == 1)
                    {
                        cbTuiJian.Checked = true;
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

            int userid = Convert.ToInt32(Request.Cookies["AdminUser"]["userid"]);
            int userGrade = Convert.ToInt32(Request.Cookies["AdminUser"]["usergrade"]);

            int activeFlag = 0;
            if (userGrade == 1)
            {
                activeFlag = 1;
            }

            if (Request.QueryString["ProjectID"] != null)
            {
                int projectID = Convert.ToInt32(Request.QueryString["ProjectID"].ToString());
                Project model = _dataService.GetProjectByProjectID(projectID);
                model.ProjectName = this.txtProjectName.Text;
                model.ProjectFullName = this.txtProjectFullName.Text;
                model.Keyword = this.txtKeyword.Text;
                model.Description = this.txtDescription.Text;
                model.ProjectPic = this.hidSmallPic.Value;
                model.ProjectIntro = this.txtProjectIntro.Value;
                model.EditDateTime = DateTime.Now;
                model.TuiJian = tuijian;
                model.FirstClassID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                model.SecondClassID = Convert.ToInt32(DropDownList2.SelectedItem.Value);
                

                if (_dataService.UpadteProject(model) > 0)
                {
                    bc.MessageBox("修改成功！", "ProjectList.aspx");
                }
                else
                {
                    bc.MessageBox1("修改失败！");
                }
            }
            else
            {
                Project model = new Project();
                model.ProjectName = this.txtProjectName.Text;
                model.ProjectFullName = this.txtProjectFullName.Text;
                model.Keyword = this.txtKeyword.Text;
                model.Description = this.txtDescription.Text;
                model.ProjectPic = this.hidSmallPic.Value;
                model.ProjectIntro = this.txtProjectIntro.Value;
                model.HitCount = 0;
                model.EditDateTime = DateTime.Now;
                model.TuiJian = tuijian;
                model.UserID = userid;
                model.ActiveFlag = activeFlag;
                model.FirstClassID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                model.SecondClassID = Convert.ToInt32(DropDownList2.SelectedItem.Value);
        

                if (_dataService.AddProject(model) > 0)
                {
                    bc.MessageBox("添加成功！", "ProjectList.aspx");
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
            IEnumerable<NewsClass> firstClasses = _dataService.GetSubClasses(19);

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