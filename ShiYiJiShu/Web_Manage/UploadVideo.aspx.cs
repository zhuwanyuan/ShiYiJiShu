using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using Common;

namespace ShiYiJiShu.Web_Manage
{
    public partial class UploadVideo : System.Web.UI.Page
    {
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

                if (fileExt.ToLower() == ".mp4")
                {
                    string fileName = System.DateTime.Now.ToString("yyyyMMddHHmmss").ToString() + fileExt;

                    string ImagePath = "~\\UploadFiles\\";

                    FileUpload1.SaveAs(Server.MapPath(ImagePath) + fileName);

                    string imgurl = Server.MapPath("~/UploadFiles/") + fileName;

                    StringBuilder strScript = new StringBuilder();
                    strScript.Append("var origin = artDialog.open.origin;");

                    strScript.Append("var spFileName = origin.document.getElementById('spFileName');");
                    strScript.Append("spFileName.innerText='" + fileName + "';");

                    strScript.Append("var hidFileName = origin.document.getElementById('hidFileName');");
                    strScript.Append("hidFileName.value='" + fileName + "';");
                    strScript.Append("art.dialog.close(); ");


                    Page.ClientScript.RegisterStartupScript(typeof(Page), "", "<script>" + strScript + "</script>");
                }
                else
                {
                    lbMessage.Text = "<font color='red'>文件格式错误，必须为doc、docx、ppt、pptx、pdf、xls、xlsx格式的文件！</font>";

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