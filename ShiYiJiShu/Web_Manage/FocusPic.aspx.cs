using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.web_manage
{
    public partial class _FocusPic : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService _dataService = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                BindRepeater();

                if (Request.QueryString["PicID"] != null)
                {
                    int picID = int.Parse(Request.QueryString["PicID"].ToString());
                    FocusPic model = _dataService.GetFocusPicByPicID(picID);

                    this.txtPicTitle.Text = model.PicTitle;
                    this.txtPicLink.Text = model.PicLinks;
                    this.hidSmallPic.Value = model.PicName;

                    this.img.Src = "/uploadpic/" + model.PicName;
                    this.Button1.Text = "修改";
                }
            }
        }

        protected void BindRepeater()
        {
            //List<FocusPic> focusPics = _dataService.GetFocusPicList().ToList();
            //this.Repeater1.DataSource = focusPics;
            //this.Repeater1.DataBind();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (e.CommandName == "delete")
                {
                    Label lbPicID = (Label)e.Item.FindControl("lbPicID");
                    string sql = "Delete from FocusPic where PicID=" + Convert.ToInt32(lbPicID.Text);
                    bc.ExecuteSQL(sql);
                    BindRepeater();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string picTitle = this.txtPicTitle.Text;
            string picLink = this.txtPicLink.Text;
            string picName = this.hidSmallPic.Value;

            if (Request.QueryString["PicID"] != null)
            {
                int picID = int.Parse(Request.QueryString["PicID"].ToString());
                FocusPic model = _dataService.GetFocusPicByPicID(picID);

                model.PicTitle = picTitle;
                model.PicName = picName;
                model.PicLinks = picLink;

                if (_dataService.UpadteFocusPic(model)>0)
                {
                    bc.MessageBox("修改成功！", "FocusPic.aspx");
                }
                else
                {
                    bc.MessageBox1("修改失败！");
                }
            }
            else
            {
                FocusPic model = new FocusPic();
                model.PicTitle = picTitle;
                model.PicName = picName;
                model.PicLinks = picLink;

                if (_dataService.AddFocusPic(model) > 0)
                {
                    bc.MessageBox("添加成功！", "FocusPic.aspx");
                }
                else
                {
                    bc.MessageBox1("添加失败！");
                }
            }

        }
    }
}