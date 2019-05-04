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
    public partial class JiDiAdd : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService _dataService = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                if (Request.QueryString["JiDiId"] != null)
                {
                    int jiDiId = Convert.ToInt32(Request.QueryString["JiDiId"].ToString());

                    JiDiJianShe model = _dataService.GetJiDiByID(jiDiId);

                    this.txtJiDiName.Text = model.JiDiName;
                    this.txtJiDiCompany.Text = model.JiDiCompany;
                    this.txtJiDiLeader.Text = model.JiDiLeader;
                    this.txtJiDiJobContent.Text = model.JiDiJobContent;
                    this.txtJiDiIntro.Value = model.JiDiIntro;
                    this.hidSmallPic.Value = model.JiDiPic;
 
                    this.img.Src = "../uploadpic/" + model.JiDiPic;

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

                    //绑定省份
                    BindProvince();
                    for (int i = 0; i < DDLProvice.Items.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(model.ProvinceID) && model.ProvinceID.ToString().Trim() == DDLProvice.Items[i].Value)
                        {
                            DDLProvice.Items[i].Selected = true;
                        }
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

            if (Request.QueryString["JiDiId"] != null)
            {
                int jiDiId = Convert.ToInt32(Request.QueryString["JiDiId"].ToString());
                
                JiDiJianShe model = _dataService.GetJiDiByID(jiDiId);

                model.JiDiName = this.txtJiDiName.Text;
                model.JiDiCompany = this.txtJiDiCompany.Text;
                model.JiDiLeader = this.txtJiDiLeader.Text;
                model.JiDiJobContent = this.txtJiDiJobContent.Text;
                model.JiDiIntro = this.txtJiDiIntro.Value;
                model.JiDiPic = this.hidSmallPic.Value;
                model.FirstClassID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                model.SecondClassID = Convert.ToInt32(DropDownList2.SelectedItem.Value);
                model.ProvinceID = DDLProvice.SelectedItem.Value;

                model.TuiJian = tuijian;
                model.UserID = userid;
                model.AddDateTime = DateTime.Now;
 

                if (_dataService.UpdateJiDi(model) > 0)
                {
                    bc.MessageBox("修改成功！", "JiDiList.aspx");
                }
                else
                {
                    bc.MessageBox1("修改失败！");
                }
            }
            else
            {
                JiDiJianShe model = new JiDiJianShe();

                model.JiDiName = this.txtJiDiName.Text;
                model.JiDiCompany = this.txtJiDiCompany.Text;
                model.JiDiLeader = this.txtJiDiLeader.Text;
                model.JiDiJobContent = this.txtJiDiJobContent.Text;
                model.JiDiIntro = this.txtJiDiIntro.Value;
                model.JiDiPic = this.hidSmallPic.Value;
                model.FirstClassID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                model.SecondClassID = Convert.ToInt32(DropDownList2.SelectedItem.Value);
                model.ProvinceID = DDLProvice.SelectedItem.Value;

                model.ActiveFlag = activeFlag;
                model.HitCount = 0;
                model.TuiJian = tuijian;
                model.UserID = userid;
                model.AddDateTime = DateTime.Now;


                if (_dataService.AddJiDi(model) > 0)
                {
                    bc.MessageBox("添加成功！", "JiDiList.aspx");
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
            IEnumerable<NewsClass> firstClasses = _dataService.GetSubClasses(5);

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

                if (secondClasses.Count() > 0)
                {
                    DropDownList2.DataSource = secondClasses;
                    DropDownList2.DataValueField = "ClassID";
                    DropDownList2.DataTextField = "ClassName";
                    DropDownList2.DataBind();
                }
            }
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