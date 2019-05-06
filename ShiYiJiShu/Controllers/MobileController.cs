using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiYiJiShu.Models;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Controllers
{
    public class MobileController : Controller
    {
        DataService _dateService = new DataService();

        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.FocusPicList = _dateService.GetFocusPicList(5);
            model.ZuiXinDongTai = _dateService.GetNewsListByClassID(21, 1, 1);
            model.WoHuiDongTai = _dateService.GetNewsListByClassID(21, 0, 11);
            model.TongZhiGongGao = _dateService.GetNewsListByClassID(3, 10);
            model.ProjectList = _dateService.GetProjectsByCount(4);

            model.WangLuoTouPiao = _dateService.GetVoteStaffsByCount(12);

             model.XueShuDongTai = _dateService.GetNewsListByClassID(4, 6);

            //model.JiDiJianShe = _dateService.GetNewsListByClassID(5, 6);

             model.RuXuanXiangMu = _dateService.GetSelectProjectsByCount(6);

            model.ZhengCeFaGui = _dateService.GetNewsListByClassID(8, 9);
            model.HuiYuanZhongXin = _dateService.GetNewsListByBigClassID(9, 9);

            model.ShiPinZhongXin = _dateService.GetVideos(4);

            model.DoctorList = _dateService.GetDoctorsByTuiJian(6);
 
            return View(model);
        }
    }
}