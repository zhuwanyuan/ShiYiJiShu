using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Common;
using ShiYiJiShu.Data;
 
namespace ShiYiJiShu.Web_Master
{
    public partial class AdminAdd : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService service = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                if (Request.QueryString["userid"] != null)
                {
                    int userid = Convert.ToInt32(Request.QueryString["userid"].ToString());

                    AdminUser model = service.GetAdminUserByID(userid);

                    this.txtUserName.Text = model.UserName;
                    this.txtPassword.Text = model.Password;
                 
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string userName = this.txtUserName.Text;
            string password = this.txtPassword.Text;
 
            if (Request.QueryString["userid"] != null)
            {
                int userid = Convert.ToInt32(Request.QueryString["userid"].ToString());

                AdminUser model = service.GetAdminUserByID(userid);
                model.UserName = userName;
                model.Password = password;
                model.UpdateDateTime = DateTime.Now;

                if (service.UpdateAdminUser(model) > 0)
                {
                    bc.MessageBox("修改成功！", "AdminList.aspx");
                }
                else
                {
                    bc.MessageBox("修改失败，请重试！", "AdminAdd.aspx?userid=" + userid);
                }
            }

            else
            {
                AdminUser model = service.GetAdminUser(userName);

                if (model != null)
                {
                    bc.MessageBox("该管理员用户已存在，请输入一个不同的用户名！", "AdminAdd.aspx");
                }
                else
                {
                    AdminUser user = new AdminUser();

                    user.UserName = userName;
                    user.Password = this.txtPassword.Text;
                    user.AdminLevel = 2;
                    user.UpdateDateTime = DateTime.Now;

                    if (service.AddAdminUser(user) > 0)
                    {
                        bc.MessageBox("添加成功！", "AdminList.aspx");
                    }
                    else
                    {
                        bc.MessageBox("添加失败，请重试！", "AdminAdd.aspx");
                    }
                }
            }
           
        }
    }
}