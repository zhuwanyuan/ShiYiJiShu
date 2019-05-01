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
    public partial class Login : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService service = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (this.code.Text.ToString().ToLower() == Request.Cookies["CheckCode"].Value.ToString().ToLower())
            {
                string userName = this.txtUsername.Text.ToString();
                string password = this.txtPassword.Text.ToString();

                if (service.CheckAdmin(userName, password))
                {
                    //HttpCookie cookie = new HttpCookie("AdminUser");
                    //cookie.Value = userName;
                    //Response.AppendCookie(cookie);

                    AdminUser adminUser = service.GetAdminUser(userName);

                    HttpCookie cookie = new HttpCookie("AdminUser"); //初使化并设置Cookie的名称
                    DateTime dt = DateTime.Now;
                    TimeSpan ts = new TimeSpan(1, 0, 0, 0, 0);//过期时间为1分钟
                    cookie.Expires = dt.Add(ts); //设置过期时间
                    cookie.Values.Add("userid", adminUser.UserID.ToString());
                    cookie.Values.Add("username", adminUser.UserName);
                    cookie.Values.Add("usergrade", adminUser.AdminLevel.ToString());
                    Response.AppendCookie(cookie);

                    Response.Redirect("index.aspx");
                }
                else
                {
                    bc.MessageBox("登录失败！", "login.aspx");
                }
            }
            else
            {
                bc.MessageBox("验证码输入错误！", "login.aspx");
            }
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}