using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Common;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Web_Manage
{
    public partial class FriendLinkList : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        DataService _dataService = new DataService();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bc.CheckAdminLogin(this);

                if (Request.QueryString["LinkID"] != null)
                {
                    int linkid = Convert.ToInt32(Request.QueryString["LinkID"]);
                    FriendLink model = _dataService.GetFriendLinkByLinkID(linkid);

                    //for (int i = 0; i < DDLLinkType.Items.Count; i++)
                    //{
                    //    if (DDLLinkType.Items[i].Value == model.LinkType.ToString())
                    //    {
                    //        DDLLinkType.Items[i].Selected = true;
                    //    }
                    //}

                    this.txtWebName.Text = model.LinkName;
                    this.txtWebLink.Text = model.LinkUrl;
                    img.Src = ConfigurationManager.AppSettings["DomainName"].ToString() + "/UploadPic/" + model.LinkPic;
                    hidSmallPic.Value = model.LinkPic;
                }

                BindRepeater();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string webName = this.txtWebName.Text;
            string webLink = this.txtWebLink.Text;

            if (Request.QueryString["LinkID"] != null)
            {
                int linkid = Convert.ToInt32(Request.QueryString["LinkID"]);
                FriendLink model = _dataService.GetFriendLinkByLinkID(linkid);
                model.LinkName = webName;
                model.LinkUrl = webLink;
                model.LinkPic = hidSmallPic.Value;
                model.UpdateTime = DateTime.Now;
                //model.LinkType = Convert.ToInt32(DDLLinkType.SelectedItem.Value);

                if (_dataService.UpadteFriendLink(model) > 0)
                {
                    bc.MessageBox("修改成功！", "FriendLinkList.aspx");
                }
                else
                {
                    bc.MessageBox1("修改失败！");
                }

            }
            else
            {
                FriendLink model = new FriendLink();
                model.LinkName = webName;
                model.LinkUrl = webLink;
                model.LinkPic = hidSmallPic.Value;
                model.UpdateTime = DateTime.Now;
                //model.LinkType = Convert.ToInt32(DDLLinkType.SelectedItem.Value);

                if (_dataService.AddFriendLink(model) > 0)
                {
                    bc.MessageBox("添加成功！", "FriendLinkList.aspx");
                }
                else
                {
                    bc.MessageBox1("添加失败！");
                }
            }
        }


        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (e.CommandName == "delete")
                {
                    Label lbLinkID = (Label)e.Item.FindControl("lbLinkID");
                    int linkid = Convert.ToInt32(lbLinkID.Text);

                    if (_dataService.DeleteFriendLink(linkid)>0)
                    {
                        BindRepeater();
                    }
                }
            }
        }

        protected void BindRepeater()
        {
            IEnumerable<FriendLink> friendLinks = _dataService.GetAllFriendLinks().ToList<FriendLink>();
            Repeater1.DataSource = friendLinks;
            Repeater1.DataBind();
        }

    }
}