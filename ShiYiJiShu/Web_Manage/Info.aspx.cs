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
    public partial class Info : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService service = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                int classid = int.Parse(Request.QueryString["classid"].ToString());

                this.lbName.Text = service.GetNewsClassByClassID(classid).ClassName + " 添加";

               ShiYiJiShu.Data.Info model = service.GetInfoByInfoID(classid);
                if (model != null)
                {
                    this.txtKeyword.Text = model.Keyword;
                    this.txtDescription.Text = model.Description;
                    this.txtContent.Value = model.InfoContent;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string keyword = this.txtKeyword.Text;
            string description = this.txtDescription.Text;
            string infoContent = this.txtContent.Value.Trim().ToString();

            int classid = Convert.ToInt32(Request.QueryString["classid"].ToString());

            ShiYiJiShu.Data.Info model = new ShiYiJiShu.Data.Info();
            model.Keyword = keyword;
            model.Description = description;
            model.InfoContent = infoContent;
            model.ClassID = classid;

            if (Request.QueryString["classid"] != null)
            {
                if (service.CheckInfoExist(classid))
                {
                    if (service.UpadteInfo(model) > 0)
                    {
                        bc.MessageBox("修改成功！", "Info.aspx?classid=" + classid);
                    }
                    else
                    {
                        bc.MessageBox1("修改失败！");
                    }
                }
                else
                {
                    if (service.AddInfo(model) > 0)
                    {
                        bc.MessageBox("保存成功！", "Info.aspx?classid=" + classid);
                    }
                    else
                    {
                        bc.MessageBox1("保存失败！");
                    }
                }
            }
        }
    }
}