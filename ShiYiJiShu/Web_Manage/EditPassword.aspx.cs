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
    public partial class EditPassword : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService service = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string userName = Request.Cookies["AdminUser"].Value.ToString();
            string oldPassword = this.txtOldPassword.Text;
            string newPassword = this.txtNewPassword.Text;

            AdminUser model = service.GetAdminUser(userName);

            if (oldPassword == model.Password)
            {
                model.Password = newPassword;
                model.UpdateDateTime = DateTime.Now;

                if (service.EditPassword(model) > 0)
                {
                    bc.MessageBox("密码修改成功！", "EditPassword.aspx");
                }
                else
                {
                    bc.MessageBox("抱歉，密码修改失败！", "EditPassword.aspx");
                }
            }
            else
            {
                bc.MessageBox("对不起，原密码输入错误！", "EditPassword.aspx");
            }
        }
    }
}