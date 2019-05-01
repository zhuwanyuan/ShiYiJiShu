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
    public partial class TechnologyAdd : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService _dataService = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                int classid = Convert.ToInt32(Convert.ToInt32(Request.QueryString["classid"]));
                NewsClass newsClass = _dataService.GetNewsClassByClassID(classid);
                this.lbName.Text = newsClass.ClassName;

                if (Request.QueryString["TechnologyID"] != null)
                {
                    int techID = Convert.ToInt32(Request.QueryString["TechnologyID"].ToString());
 
                    Technology model = _dataService.GetTechnologyByTechnologyID(techID);

                    this.txtTechnologyName.Text = model.TechName;
                    this.txtTechnologyLink.Text = model.TechNameLink;
                    this.txtCompanyName.Text = model.TechCompany;
                    this.txtCompanyLink.Text = model.TechCompanyLink;
 
                    if (model.ZhiDing == 1)
                    {
                        cbZhiDing.Checked = true;
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string technologyName = this.txtTechnologyName.Text;
            string technologyLink = this.txtTechnologyLink.Text;
            string companyName = this.txtCompanyName.Text;
            string companyLink = this.txtCompanyLink.Text;
 
            int classid=Convert.ToInt32(Convert.ToInt32(Request.QueryString["classid"]));
 
            DateTime addDate = DateTime.Now;

            int zhiding = 0;
            if (cbZhiDing.Checked)
            {
                zhiding = 1;
            }


            if (Request.QueryString["TechnologyID"] != null)
            {
                int technologyID = Convert.ToInt32(Request.QueryString["TechnologyID"].ToString());
                Technology model = _dataService.GetTechnologyByTechnologyID(technologyID);

                model.TechName = technologyName;
                model.TechNameLink = technologyLink;
                model.TechCompany = companyName;
                model.TechCompanyLink = companyLink;
                model.ClassID = classid;
                model.ZhiDing = zhiding;
                model.ModifyDate = DateTime.Now;
 
                if (_dataService.UpadteTechnology(model) > 0)
                {
                    bc.MessageBox("修改成功！", "TechnologyList.aspx?classid=" + classid);
                }
                else
                {
                    bc.MessageBox1("修改失败！");
                }
            }
            else
            {
                Technology model = new Technology();
                model.TechName = technologyName;
                model.TechNameLink = technologyLink;
                model.TechCompany = companyName;
                model.TechCompanyLink = companyLink;
                model.ClassID = classid;
                model.ZhiDing = zhiding;
                model.AddDate = DateTime.Now;
                model.ModifyDate = DateTime.Now;
 
                if (_dataService.AddTechnology(model) > 0)
                {
                    bc.MessageBox("添加成功！", "TechnologyList.aspx?classid=" + classid);
                }
                else
                {
                    bc.MessageBox1("添加失败！");
                }
            }
        }
    }
}