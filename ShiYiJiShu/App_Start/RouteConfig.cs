using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShiYiJiShu
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "NewsDetail",
             url: "News/Detail/{newsid}",
             defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "NewsList",
                url: "List/{classid}/{currentpage}",
                defaults: new { controller = "News", action = "List", currentpage = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Info",
               url: "Info/{classid}",
               defaults: new { controller = "Info", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "DoctorIndex",
               url: "Doctor/Index/{provinceid}/{currentpage}",
               defaults: new { controller = "Doctor", action = "Index", currentpage = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "DoctorList",
               url: "Doctor/List/{classid}/{currentpage}",
               defaults: new { controller = "Doctor", action = "List", currentpage = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "DoctorDetail",
              url: "Doctor/Detail/{doctorid}",
              defaults: new { controller = "Doctor", action = "Detail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "CourseDetail",
             url: "Course/Detail/{courseid}",
             defaults: new { controller = "Course", action = "Detail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "CourseList",
            url: "Course/List/{classid}",
            defaults: new { controller = "Course", action = "List", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "VideoDetail",
            url: "Video/Detail/{videoid}",
            defaults: new { controller = "Video", action = "Detail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "VideoList",
            url: "Video/List/{classid}/{currentpage}",
            defaults: new { controller = "Video", action = "List", currentpage = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "ProjectList",
            url: "Project/List/{classid}/{currentpage}",
            defaults: new { controller = "Project", action = "Index", currentpage = UrlParameter.Optional }
            );

            routes.MapRoute(
           name: "ProjectDetail",
           url: "Project/Detail/{projectid}",
           defaults: new { controller = "Project", action = "Detail", projectid = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "JiDiList",
            url: "JiDi/List/{classid}/{provinceid}/{currentpage}",
            defaults: new { controller = "JiDi", action = "List", provinceid = UrlParameter.Optional, currentpage = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "JiDiDetail",
            url: "JiDi/Detail/{jidiid}",
            defaults: new { controller = "JiDi", action = "Detail", jidiid = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "VoteList",
            url: "Vote/List/{classid}/{currentPage}",
            defaults: new { controller = "Vote", action = "List", currentpage = UrlParameter.Optional }
            );

            routes.MapRoute(
          name: "VoteDetail",
          url: "Vote/Detail/{staffid}",
          defaults: new { controller = "Vote", action = "Detail", staffid = UrlParameter.Optional }
           );

            routes.MapRoute(
          name: "Technology",
          url: "Tech/List/{classid}/{currentpage}",
          defaults: new { controller = "Tech", action = "List", currentpage = UrlParameter.Optional }
          );

            routes.MapRoute(
            name: "TechnologyDetail",
            url: "Tech/Info/{classid}",
            defaults: new { controller = "Tech", action = "Info", id = UrlParameter.Optional }
         );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
