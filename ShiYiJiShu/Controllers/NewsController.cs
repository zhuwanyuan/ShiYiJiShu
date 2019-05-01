using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiYiJiShu.Data;
using ShiYiJiShu.Models;
using Common;

namespace ShiYiJiShu.Controllers
{
    public class NewsController : Controller
    {
        DataService _dateService = new DataService();
        BaseClass bc = new BaseClass();

        public ActionResult List(int classid, int? currentpage)
        {
            int pageCount = 20;

            NewsClass newsClass = _dateService.GetNewsClassByClassID(classid);

            NewsListModel model = new NewsListModel();
            //model.PaiHang = _dateService.GetNewsListByClickNum(classid, pageCount);
            model.List = _dateService.GetNewsListByClassID(classid, pageCount, currentpage);
            model.ClassName = newsClass.ClassName;
 
            model.ClassID = classid;
            model.ParentClassID = _dateService.GetParentClassID(classid);
            model.TopClassID= _dateService.GetParentClassID(model.ParentClassID);

            if (model.ParentClassID == 139 || model.ParentClassID == 140 || model.ParentClassID == 177)
            {
                model.HasSubClass = true;
            }

            int totalCount = _dateService.GetNewsTotalCount(classid);
 
            if (bc.CheckMobile())
            {
                if (totalCount > pageCount)
                {
                    model.PageLink = bc.GetMobilePageLink(classid, pageCount, totalCount, currentpage, "List");
                }

                return View("~/Views/Mobile/NewsList.cshtml", model);
            }
            else
            {             
                if (totalCount > pageCount)
                {
                    model.PageLink = bc.GetPageLink(classid, pageCount, totalCount, currentpage, "List");
                }
 
                return View(model);
            }           
        }
        
        public ActionResult Detail(int newsid)
        {
            _dateService.AddNewsHitCount(newsid);
            News news = _dateService.GetNewsByNewsID(newsid);
            NewsClass newsClass = _dateService.GetNewsClassByClassID((int)news.SmallClassID);

            NewsDetailModel model = new NewsDetailModel();
            model.News = news;
            model.ClassID = newsClass.ClassID;
            model.ClassName = newsClass.ClassName;
            model.ParentClassID = _dateService.GetParentClassID(newsClass.ClassID);

            if (bc.CheckMobile())
            {
                return View("~/Views/Mobile/NewsDetail.cshtml", model);
            }
            else
            {
                return View(model);
            }
        }

        public PartialViewResult GetShiYiJiShuMenuPartial(int currentClassID)
        {
            ShiYiJiShuMenuModel model = new ShiYiJiShuMenuModel();
            model.GongZuoJieShao = _dateService.GetSubClasses(139, 3);
            model.RuXuanJiShu = _dateService.GetSubClasses(140, 3);
            model.CurrentClassID = currentClassID;

            if (bc.CheckMobile())
            {
                return PartialView("~/Views/Mobile/_MobileShiYiJiShuMenuPartial.cshtml", model);
            }
            else
            {
                return PartialView("_ShiYiJiShuMenuPartial", model);
            }

            
        }

        public PartialViewResult GetNewsMenuPartial(int topClassID, int currentClassID, string routeName)
        {
            NewsMenuModel model = new NewsMenuModel();

            NewsClass topClass = _dateService.GetNewsClassByClassID(topClassID);
            NewsClass newsClass = _dateService.GetNewsClassByClassID(currentClassID);

 
            IEnumerable<NewsClass> newsclasses = _dateService.GetSubClasses(topClassID, 2);

            List<NewsSubMenuModel> NewsSubMenuList = new List<NewsSubMenuModel>();

            foreach (var newsclass in newsclasses)
            {
                NewsSubMenuModel newsSubMenuModel = new NewsSubMenuModel();

                int classid = newsclass.ClassID;

                IEnumerable<NewsClass> subclasses = _dateService.GetSubClasses(classid, 3);
                newsSubMenuModel.NewsClass = newsclass;
                newsSubMenuModel.SubNewsClassList = subclasses;
 
                NewsSubMenuList.Add(newsSubMenuModel);
            }
 
            model.ClassList = NewsSubMenuList;
            model.CurrentClassID = currentClassID;
            model.TopClassName = topClass.ClassName;
            model.RouteName = routeName;

            if (bc.CheckMobile())
            {
                return PartialView("~/Views/Mobile/_MobileNewsMenuPartial.cshtml", model);
            }
            else
            {
                return PartialView("_NewsMenuPartial", model);
            } 
        }
    }
}
