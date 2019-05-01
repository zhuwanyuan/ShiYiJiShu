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
    public partial class CertificateAdd : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService service = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);
 
                if (Request.QueryString["certid"] != null)
                {
                    int certID = Convert.ToInt32(Request.QueryString["certid"].ToString());

                    Certificate model = service.GetCertificateByCertID(certID);

                    this.txtName.Text = model.Name;

                    for (int i = 0; i < DDLSearchType.Items.Count; i++)
                    {
                        if (DDLSearchType.Items[i].Text == model.SearchType.ToString())
                        {
                            DDLSearchType.Items[i].Selected = true;
                        }
                    }

                    this.hidSmallPic.Value = model.Picture;
                    this.img.Src = "../uploadpic/" + model.Picture;
                    this.txtIDNumber.Text = model.IDNumber;
                    this.txtCertNo.Text = model.CertNo;
                    this.txtJobTitle.Text = model.JobTitle;
                    this.txtCertSample.Value = model.CertSample;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = this.txtName.Text;
            string searchType = this.DDLSearchType.SelectedItem.Text;
            string picture = this.hidSmallPic.Value;
            string idNumber = this.txtIDNumber.Text;
            string certNo = this.txtCertNo.Text;
            string jobTitle = this.txtJobTitle.Text;
            string certSample = this.txtCertSample.Value;
            DateTime addDateTime = DateTime.Now;

            int userid = Convert.ToInt32(Request.Cookies["AdminUser"]["userid"]);
            int userGrade = Convert.ToInt32(Request.Cookies["AdminUser"]["usergrade"]);

            int activeFlag = 0;
            if (userGrade == 1)
            {
                activeFlag = 1;
            }

            if (Request.QueryString["certid"] != null)
            {
                int certid = Convert.ToInt32(Request.QueryString["certid"].ToString());

                Certificate model = service.GetCertificateByCertID(certid);
                model.Name = name;
                model.SearchType = searchType;
                model.Picture = picture;
                model.IDNumber = idNumber;
                model.CertNo = certNo;
                model.JobTitle = jobTitle;
                model.CertSample = certSample;

                if (service.UpadteCertificate(model) > 0)
                {
                    bc.MessageBox("修改成功！", "CertificateList.aspx");
                }
                else
                {
                    bc.MessageBox1("修改失败！");
                }
            }
            else
            {
                Certificate model = new Certificate();
                model.Name = name;
                model.SearchType = searchType;
                model.Picture = picture;
                model.IDNumber = idNumber;
                model.CertNo = certNo;
                model.JobTitle = jobTitle;
                model.CertSample = certSample;
                model.AddDateTime = addDateTime;
                model.UserID = userid;
                model.ActiveFlag = activeFlag;

                if (service.AddCertificate(model) > 0)
                {
                    bc.MessageBox("添加成功！", "CertificateList.aspx");
                }
                else
                {
                    bc.MessageBox1("添加失败！");
                }
            }
        }
    }
}