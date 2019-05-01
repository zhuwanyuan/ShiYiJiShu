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
    public partial class DoctorAdd : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService _dataService = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                if (Request.QueryString["DoctorID"] != null)
                {
                    int doctorID = Convert.ToInt32(Request.QueryString["DoctorID"].ToString());

                    Doctor model = _dataService.GetDoctorByDoctorID(doctorID);

                    this.txtDoctorName.Text = model.DoctorName;
                    this.txtDoctorIntro.Value = model.DoctorIntro;
                    this.txtDoctorJobTitle.Text = model.DoctorJobTitle;
                    this.txtDoctorCompany.Text = model.DoctorCompany;
                    this.hidSmallPic.Value = model.DoctorPic;

                    this.img.Src = "../uploadpic/" + model.DoctorPic;

                    //专家类别
                    for (int i = 0; i < DDLDoctorType.Items.Count; i++)
                    {
                        if (model.DoctorType.ToString() == DDLDoctorType.Items[i].Value)
                        {
                            DDLDoctorType.Items[i].Selected = true;
                        }
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

                    //绑定省份
                    BindProvince();
                    for (int i = 0; i < DDLProvice.Items.Count; i++)
                    {
                        if (model.ProvinceID.ToString().Trim() == DDLProvice.Items[i].Value)
                        {
                            DDLProvice.Items[i].Selected = true;
                        }
                    }
 
                    if (model.TuiJian == 1)
                    {
                        cbTuiJian.Checked = true;
                    }
                }
                else 
                {
                    BindFirstClass();
                    BindSecondClass();
                    BindProvince();
                }   
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int userid = Convert.ToInt32(Request.Cookies["AdminUser"]["userid"]);
            int userGrade = Convert.ToInt32(Request.Cookies["AdminUser"]["usergrade"]);

            int tuijian = 0;
            if (cbTuiJian.Checked)
            {
                tuijian = 1;
            }

            int activeFlag = 0;
            if (userGrade == 1)
            {
                activeFlag = 1;
            }
 
     
            if (Request.QueryString["DoctorID"] != null)
            {
                int doctorID = Convert.ToInt32(Request.QueryString["DoctorID"].ToString());
                Doctor model = _dataService.GetDoctorByDoctorID(doctorID);
                model.DoctorName = this.txtDoctorName.Text;
                model.DoctorJobTitle = this.txtDoctorJobTitle.Text;
                model.DoctorCompany = this.txtDoctorCompany.Text;
                model.DoctorType = Convert.ToInt32(this.DDLDoctorType.SelectedItem.Value);
                model.FirstClassID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                model.FirstClassName = DropDownList1.SelectedItem.Text;
                model.SecondClassID = Convert.ToInt32(DropDownList2.SelectedItem.Value);
                model.SecondClassName = DropDownList2.SelectedItem.Text;
                model.ProvinceID = DDLProvice.SelectedItem.Value;
                model.DoctorPic = this.hidSmallPic.Value;
                model.DoctorIntro = this.txtDoctorIntro.Value;
                model.EditDateTime = DateTime.Now;
                model.TuiJian = tuijian;
            

                if (_dataService.UpadteDoctor(model) > 0)
                {
                    bc.MessageBox("修改成功！", "DoctorList.aspx");
                }
                else
                {
                    bc.MessageBox1("修改失败！");
                }
            }
            else
            {
                Doctor model = new Doctor();
                model.DoctorName = this.txtDoctorName.Text;
                model.DoctorJobTitle = this.txtDoctorJobTitle.Text;
                model.DoctorCompany = this.txtDoctorCompany.Text;
                model.DoctorType = Convert.ToInt32(this.DDLDoctorType.SelectedItem.Value);
                model.FirstClassID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                model.FirstClassName = DropDownList1.SelectedItem.Text;
                model.SecondClassID = Convert.ToInt32(DropDownList2.SelectedItem.Value);
                model.SecondClassName = DropDownList2.SelectedItem.Text;
                model.ProvinceID = DDLProvice.SelectedItem.Value;
                model.DoctorPic = this.hidSmallPic.Value;
                model.DoctorIntro = this.txtDoctorIntro.Value;
                model.HitCount = 0;
                model.EditDateTime = DateTime.Now;
                model.TuiJian = tuijian;
                model.UserID = userid;
                model.ActiveFlag = activeFlag;

                if (_dataService.AddDoctor(model) > 0)
                {
                    bc.MessageBox("添加成功！", "DoctorList.aspx");
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
            IEnumerable<NewsClass> firstClasses = _dataService.GetSubClasses(25);

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

        public void BindProvince()
        {
            List<Province> provinces = _dataService.GetProvinceList();

            DDLProvice.DataSource = provinces;
            DDLProvice.DataValueField = "Code";
            DDLProvice.DataTextField = "Name";
            DDLProvice.DataBind();
        }
    }
}