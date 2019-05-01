using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using Common;

namespace ShiYiJiShu.Web_Master
{
    public partial class UploadPic : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile.FileName != "")
            {
                string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);

                if (fileExt.ToLower() == ".gif" || fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".jpeg" || fileExt.ToLower() == ".png")
                {
                    string smallPic = System.DateTime.Now.ToString("yyyyMMddHHmmss").ToString() + fileExt;

                    string ImagePath = "~\\UploadPic\\";

                    FileUpload1.SaveAs(Server.MapPath(ImagePath) + smallPic);

                    //bc.CreateThumbnailImage(Server.MapPath(ImagePath) + bigpic, Server.MapPath(ImagePath) + smallpic_50, 50, 50);

                    string imgurl = ConfigurationManager.AppSettings["DomainName"].ToString() + "/UploadPic/" + smallPic;

                    StringBuilder strScript = new StringBuilder();
                    strScript.Append("var origin = artDialog.open.origin;");

                    strScript.Append("var img = origin.document.getElementById('img');");
                    strScript.Append("img.src='" + imgurl + "';");

                    strScript.Append("var hidPic = origin.document.getElementById('hidSmallPic');");
                    strScript.Append("hidPic.value='" + smallPic + "';");
                    strScript.Append("art.dialog.close(); ");


                    Page.ClientScript.RegisterStartupScript(typeof(Page), "", "<script>" + strScript + "</script>");
                }
                else
                {
                    lbMessage.Text = "<font color='red'>文件格式错误,必须为JPG、GIF、BMP或PNG格式的图片！</font>";

                }
            }
            else
            {
                lbMessage.Text = "<font color='red'>请选择上传文件！</font>";
            }
        }
 
        public string ReplaceNum(int colorid)
        {
            string result = colorid.ToString();
            if (colorid < 10)
            {
                result = "0" + colorid;
            }

            return result;
        }
    }
}