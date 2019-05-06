using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiYiJiShu.Models;
using ShiYiJiShu.Data;
using Common;

namespace ShiYiJiShu.Controllers
{
    public class HomeController : Controller
    {
        DataService _dateService = new DataService();
        BaseClass bc = new BaseClass();

        public ActionResult Index()
        {
            if (bc.CheckMobile())
            {              
                return RedirectToAction("Index", "Mobile");
                //if (url.Contains("www"))
                //{
                //    return Redirect("http://m.syjswyh.com");
                //}
                //else
                //{

                //}
            }


            string domainName = System.Configuration.ConfigurationManager.AppSettings["DomainName"].ToString();

            string url = Request.Url.ToString();
            if (!url.Contains("www") && !url.Contains("192.168.31.100"))
            {
                return Redirect(domainName);
            }

            HomeModel model = new HomeModel();
            model.FocusPicList = _dateService.GetFocusPicList(5);
            model.ZuiXinDongTai = _dateService.GetNewsListByClassID(21, 1, 4);
            model.WoHuiDongTai = _dateService.GetNewsListByClassID(21, 0, 11);
            model.TongZhiGongGao = _dateService.GetNewsListByClassID(3, 10);
            model.ProjectList = _dateService.GetProjectsByCount(10);

            model.WangLuoTouPiao = _dateService.GetVoteStaffsByClassID(174, 10);

            model.XueShuDongTai_Pic = _dateService.GetPicNewsByClassID(4);
            model.XueShuDongTai = _dateService.GetNewsListByClassID(4, 6);

            model.JiDiJianShe_Pic = _dateService.GetJiDiByTuiJian();
            model.JiDiJianShe = _dateService.GetJiDisByCount(6);

            model.RuXuanXiangMu_Pic = _dateService.GetSelectProjectByTuiJian();
            model.RuXuanXiangMu = _dateService.GetSelectProjectsByCount(6);

            model.ZhengCeFaGui = _dateService.GetNewsListByClassID(8, 9);
            model.HuiYuanZhongXin = _dateService.GetNewsListByBigClassID(9, 9);

            model.ShiPinZhongXin = _dateService.GetVideos(10);

            model.DoctorList = _dateService.GetDoctorsByTuiJian(20);

            model.FriendLinks = _dateService.GetFriendLinkList(40);

            return View(model);
        }

        public ActionResult Mobile()
        {
            return View();
        }
 
    }
}
