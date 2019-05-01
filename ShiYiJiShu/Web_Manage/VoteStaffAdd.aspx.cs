using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Web_Manage
{
    public partial class VoteStaffAdd : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService service = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);
 
                if (Request.QueryString["staffid"] != null)
                {
                    int staffid = Convert.ToInt32(Request.QueryString["staffid"].ToString());

                    VoteStaff model = service.GetVoteStaffByID(staffid);
                    this.txtTechName.Text = model.TechName;
                    //this.txtStaffName.Text = model.StaffName;
                    //this.txtJobTitle.Text = model.JobTitle;
                    //this.txtCompany.Text = model.Company;
                    this.txtLinkUrl.Text = model.LinkUrl;
                    this.hidSmallPic.Value = model.StaffPhoto;
                    this.img.Src = "../uploadpic/" + model.StaffPhoto;
                    this.txtStaffDetail.Value = model.StaffDetail;

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

            int userid = Convert.ToInt32(Request.Cookies["AdminUser"]["userid"]);
            int userGrade = Convert.ToInt32(Request.Cookies["AdminUser"]["usergrade"]);

            int activeFlag = 0;
            if (userGrade == 1)
            {
                activeFlag = 1;
            }
 
            string techname = this.txtTechName.Text;
            //string staffname = this.txtStaffName.Text;
            //string jobTitle = this.txtJobTitle.Text;
            //string company = this.txtCompany.Text;
            string picture = this.hidSmallPic.Value;
            string staffdetail = this.txtStaffDetail.Value;
            string linkUrl = this.txtLinkUrl.Text;
 
            if (Request.QueryString["staffid"] != null)
            {
                int staffid = Convert.ToInt32(Request.QueryString["staffid"].ToString());

                VoteStaff model = service.GetVoteStaffByID(staffid);
                model.TechName = techname;
                model.StaffDetail = staffdetail;
                model.StaffPhoto = picture;
                model.LinkUrl = linkUrl;
                model.AddDateTime = DateTime.Now;
                model.FirstClassID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                model.SecondClassID = Convert.ToInt32(DropDownList2.SelectedItem.Value);

                if (service.UpdateVoteStaff(model) > 0)
                {
                    bc.MessageBox("修改成功！", "VoteStaffList.aspx");
                }
                else
                {
                    bc.MessageBox1("修改失败！");
                }
            }
            else
            {
                VoteStaff model = new VoteStaff();
                model.TechName = techname;
                model.StaffDetail = staffdetail;
                model.StaffPhoto = picture;
                model.VoteCount = 0;
                model.LinkUrl = linkUrl;
                model.AddDateTime = DateTime.Now;
                model.FirstClassID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                model.SecondClassID = Convert.ToInt32(DropDownList2.SelectedItem.Value);

                if (service.AddVoteStaff(model) > 0)
                {
                    bc.MessageBox("添加成功！", "VoteStaffList.aspx");
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
            IEnumerable<NewsClass> firstClasses = service.GetSubClasses(13);

            DropDownList1.DataSource = firstClasses;
            DropDownList1.DataValueField = "ClassID";
            DropDownList1.DataTextField = "ClassName";
            DropDownList1.DataBind();
        }


        public void BindSecondClass()
        {
            int classid = Convert.ToInt32(DropDownList1.SelectedItem.Value);

            IEnumerable<NewsClass> firstClasses = service.GetSubClasses(classid);

            DropDownList2.DataSource = firstClasses;
            DropDownList2.DataValueField = "ClassID";
            DropDownList2.DataTextField = "ClassName";
            DropDownList2.DataBind();
        }
    }
}