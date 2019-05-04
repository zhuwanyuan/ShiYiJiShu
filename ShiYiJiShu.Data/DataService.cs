using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShiYiJiShu.Model;

namespace ShiYiJiShu.Data
{
    public class DataService
    {
        ShiYiJiShuEntities context = new ShiYiJiShuEntities();

        #region >>>Admin<<<
        //检查是否管理员
        public bool CheckAdmin(string username, string password)
        {
            bool returnValue = false;

            var userExist = from s in context.AdminUsers
                            where s.UserName == username && s.Password == password
                            select s;

            if (userExist.Count() > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        //根据用户名获取用户
        public AdminUser GetAdminUser(string userName)
        {
            AdminUser user = context.AdminUsers.Where(s => s.UserName == userName).FirstOrDefault();
            return user;
        }

        public AdminUser GetAdminUserByID(int userid)
        {
            AdminUser user = context.AdminUsers.Where(s => s.UserID == userid).FirstOrDefault();
            return user;
        }


        public List<AdminUser> GetAdminUserByLevel(int adminLevel)
        {
            List<AdminUser> admins = context.AdminUsers.Where(s => s.AdminLevel == adminLevel).ToList<AdminUser>();
            return admins;
        }

        //修改密码
        public int EditPassword(AdminUser adminUser)
        {
            AdminUser user = adminUser;
            return context.SaveChanges();
        }

        //添加文章
        public int AddAdminUser(AdminUser adminUser)
        {
            context.AdminUsers.Add(adminUser);
            return context.SaveChanges();
        }

        //修改文章
        public int UpdateAdminUser(AdminUser adminUser)
        {
            return context.SaveChanges();
        }

        public int DeleteAdminUser(int userid)
        {
            AdminUser model = GetAdminUserByID(userid);
            context.AdminUsers.Remove(model);

            return context.SaveChanges();
        }

   
        #endregion

        #region >>>NewsClass<<<
        //添加文章
        public int AddNewsClass(NewsClass newsClass)
        {
            context.NewsClasses.Add(newsClass);
            return context.SaveChanges();
        }

        //修改文章
        public int UpdateNewsClass(NewsClass newsClass)
        {
            return context.SaveChanges();
        }


        public int DeleteNewsClass(int classid)
        {
            NewsClass model = GetNewsClassByClassID(classid);
            context.NewsClasses.Remove(model);

            return context.SaveChanges();
        }

        //获取文章子类别
        public IEnumerable<NewsClass> GetSubClasses(int classID)
        {
            List<NewsClass> list = (from s in context.NewsClasses
                                    where s.ParentClassID == classID
                                    select s).ToList<NewsClass>();

            return list;
        }

        public IEnumerable<NewsClass> GetSubClasses(int classID, int classLevel)
        {
            List<NewsClass> list = (from s in context.NewsClasses
                                    where s.ParentClassID == classID && s.ClassLevel == classLevel
                                    select s).ToList<NewsClass>();

            return list;
        }

        //获取父类ClassID
        public int GetParentClassID(int classID)
        {
            int parentClassID = (int)context.NewsClasses.FirstOrDefault(c => c.ClassID == classID).ParentClassID;
            return parentClassID;
        }

        //根据classid获取父类别
        public NewsClass GetParentClass(int classID)
        {
            int parentID = GetParentClassID(classID);

            NewsClass list = (from s in context.NewsClasses
                              where s.ClassID == parentID
                              select s).FirstOrDefault();
            return list;
        }

        //根据ClassID获取类别
        public NewsClass GetNewsClassByClassID(int classID)
        {
            NewsClass newsClass = context.NewsClasses.FirstOrDefault(c => c.ClassID == classID);
            return newsClass;
        }
        #endregion

        #region >>>News<<<
        //根据类别分页获取新闻
        public List<News> GetNewsListByClassID(int classID, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }
            var list = context.News.Where(x => x.SmallClassID == classID && x.ActiveFlag == 1).OrderByDescending(c => c.ZhiDing).ThenByDescending(c => c.ModifyDate).Skip(startNo).Take(pageCount).ToList<News>();

            return list;
        }

        public int GetNewsTotalCount(int classid)
        {
            return context.News.Where(x => x.SmallClassID == classid).Count();
        }

        //根据类别获取图文文章
        public News GetPicNewsByClassID(int classID)
        {
            var news = context.News.Where(x => x.SmallClassID == classID && x.NewsPic != "" && x.ActiveFlag == 1).OrderByDescending(x => x.ModifyDate).Take(1).FirstOrDefault();
            return news;
        }

        //根据类别获取文章
        public List<News> GetNewsListByClassID(int classID, int newsCount)
        {
            var list = context.News.Where(c => c.SmallClassID == classID && c.ActiveFlag == 1).OrderByDescending(c => c.CreateDate).Take(newsCount).ToList<News>();

            return list;
        }

        public List<News> GetNewsListByBigClassID(int classID, int newsCount)
        {
            var list = context.News.Where(c => c.BigClassID == classID && c.ActiveFlag == 1).OrderByDescending(c => c.CreateDate).Take(newsCount).ToList<News>();

            return list;
        }

        //根据类别获取文章
        public List<News> GetNewsListByClassID(int classID, int tuijian, int newsCount)
        {
            var list = context.News.Where(c => c.SmallClassID == classID && c.ZhiDing == tuijian && c.ActiveFlag == 1).OrderByDescending(c => c.CreateDate).Take(newsCount).ToList<News>();
            return list;
        }

        //根据类别获取文章
        public List<News> GetNewsListByClickNum(int classID, int newsCount)
        {
            var list = context.News.Where(c => c.SmallClassID == classID && c.ActiveFlag == 1).OrderByDescending(c => c.HitCount).Take(newsCount).ToList<News>();

            return list;
        }


        //根据类别获取文章-图文
        public IEnumerable<News> GetNewsList2ByClassID(int classID, int newsCount)
        {
            IEnumerable<News> list = null;
            var firstPicNews = context.News.Where(x => x.NewsPic != "" && x.ActiveFlag == 1).OrderByDescending(x => x.ModifyDate).FirstOrDefault();
            if (firstPicNews != null)
            {
                list = context.News.Where(c => c.SmallClassID == classID && c.NewsID != firstPicNews.NewsID && c.ActiveFlag == 1).OrderByDescending(c => c.ZhiDing).ThenByDescending(c => c.ModifyDate).Take(newsCount);
            }
            else
            {
                list = context.News.Where(c => c.SmallClassID == classID && c.ActiveFlag == 1).OrderByDescending(c => c.ZhiDing).ThenByDescending(c => c.ModifyDate).Take(newsCount);
            }
            
            return list;
       }

        public List<News> GetNewsListByAdminID(int adminid, int classid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }

            List<News> newslist = new List<News>();

            if (adminid == 1) //高级管理员
            {
                newslist = context.News.Where(c => c.SmallClassID == classid).OrderBy(x => x.ActiveFlag).ThenByDescending(c => c.NewsID).Skip(startNo).Take(pageCount).ToList<News>();
            }
            else //普通管理员
            {
                newslist = context.News.Where(c => c.SmallClassID == classid && c.UserID == adminid).OrderBy(x => x.ActiveFlag).ThenByDescending(c => c.NewsID).Skip(startNo).Take(pageCount).ToList<News>();
            }
 
            return newslist;
        }


        public int GetNewsTotalCountByAdminID(int adminid, int classid)
        {
            int count = 0;

            if (adminid == 1) //高级管理员
            {
                count = context.News.Where(c => c.SmallClassID == classid).Count();
            }
            else //普通管理员
            {
                count = context.News.Where(c => c.SmallClassID == classid && c.UserID == adminid).Count();
            }

            return count;
        }

        //根据ID获取新闻
        public News GetNewsByNewsID(int newsID)
        {
            News news = context.News.FirstOrDefault(c => c.NewsID == newsID);
            return news;
        }

        //添加文章
        public int AddNews(News news)
        {
            context.News.Add(news);
            return context.SaveChanges();
        }

        //修改文章
        public int UpadteNews(News news)
        {
            return context.SaveChanges();
        }

        //点击次数+1
        public void AddNewsHitCount(int newsID)
        {
            News news = context.News.Where(s => s.NewsID == newsID).FirstOrDefault();
            news.HitCount = news.HitCount + 1;
            context.SaveChanges();
        }

        public IEnumerable<News> SearchNews(string keyword)
        {
            IEnumerable<News> list = context.News.Where(c => c.NewsTitle.Contains(keyword)).OrderByDescending(c => c.ZhiDing).ThenByDescending(c => c.ModifyDate);

            return list;
        }

        public IEnumerable<News> GetFocusPicList(int count)
        {
            IEnumerable<News> list = context.News.Where(c => c.ZhiDing == 1 && c.NewsPic != "").OrderByDescending(c => c.CreateDate).Take(count);

            return list;
        }
        #endregion

        #region >>>Info<<<
        //根据InfoID获取文章
        public Info GetInfoByInfoID(int infoID)
        {
            Info info = context.Infoes.FirstOrDefault(c => c.ClassID == infoID);
            return info;
        }

        //检查info存在
        public bool CheckInfoExist(int classID)
        {
            return context.Infoes.Any(c => c.ClassID == classID);
        }

        //修改文章
        public int UpadteInfo(Info info)
        {
            Info model = context.Infoes.Where(c => c.ClassID == info.ClassID).FirstOrDefault();
            model.Keyword = info.Keyword;
            model.Description = info.Description;
            model.InfoContent = info.InfoContent;

            return context.SaveChanges();
        }

        //保存文章
        public int AddInfo(Info info)
        {
            context.Infoes.Add(info);
            return context.SaveChanges();
        }

        //点击次数+1
        public void AddInfoHitCount(int classID)
        {
            Info model = context.Infoes.Where(s => s.ClassID == classID).FirstOrDefault();
            model.HitCount = model.HitCount + 1;
            context.SaveChanges();
        }
        #endregion

        #region >>>Doctor<<<
        //添加案例
        public int AddDoctor(Doctor model)
        {
            context.Doctors.Add(model);
            return context.SaveChanges();
        }

        //修改案例
        public int UpadteDoctor(Doctor model)
        {
            Doctor projectModel = model;
            return context.SaveChanges();
        }

        public int GetDoctorTotalCount()
        {
            return context.Doctors.Count();
        }

        public int GetDoctorTotalCountByClassID(int classid)
        {
            return context.Doctors.Where(x => x.SecondClassID == classid && x.ActiveFlag == 1).Count();
        }

        //根据案例ID获取案例
        public Doctor GetDoctorByDoctorID(int doctorID)
        {
            Doctor model = context.Doctors.FirstOrDefault(c => c.DoctorID == doctorID);
            return model;
        }

        public Doctor GetPreDoctor(int classid, int currentDoctorID)
        {
            Doctor model = context.Doctors.Where(x=>x.SecondClassID==classid && x.DoctorID< currentDoctorID && x.ActiveFlag == 1).FirstOrDefault();
            return model;
        }

        public Doctor GetNextDoctor(int classid, int currentDoctorID)
        {
            Doctor model = context.Doctors.Where(x => x.SecondClassID == classid && x.DoctorID > currentDoctorID && x.ActiveFlag == 1).FirstOrDefault();
            return model;
        }

        //根据类别获取文章
        public IEnumerable<Doctor> GetDoctorsByPageNum(int classid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }

            var list = context.Doctors.Where(x => x.SecondClassID == classid && x.ActiveFlag == 1).OrderByDescending(x => x.TuiJian).ThenByDescending(x => x.DoctorID).Skip(startNo).Take(pageCount);
            return list;
        }

        //根据类别获取文章
        public IEnumerable<Doctor> GetDoctorsByTuiJian(int count)
        {
            var list = context.Doctors.Where(x => x.TuiJian == 1 && x.ActiveFlag == 1).OrderByDescending(x => x.DoctorID).Take(count);
            return list;
        }

        public IEnumerable<Doctor> GetDoctorsByType(int type, int count)
        {
            var list = context.Doctors.Where(x => x.DoctorType == type && x.ActiveFlag == 1).OrderByDescending(x => x.TuiJian).ThenByDescending(x => x.DoctorID).Take(count);
            return list;
        }

        public IEnumerable<Doctor> GetTypeDoctorsByPageNum(int type, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }

            var list = context.Doctors.Where(x => x.DoctorType == type && x.ActiveFlag == 1).OrderByDescending(x => x.TuiJian).ThenByDescending(x => x.DoctorID).Skip(startNo).Take(pageCount);
            return list;
        }

        public int GetDoctorTotalCountByProvinceID(string provinceid)
        {
            int totalcount = 0;
 
            if (provinceid == "000000") //全部省份
            {
                totalcount = context.Doctors.Count();
            }
            else
            {
                totalcount = context.Doctors.Where(x => x.ProvinceID == provinceid && x.ActiveFlag == 1).Count();
            }

            return totalcount;
        }

        public int GetDoctorTotalCountByType(int type)
        {
            int totalcount = context.Doctors.Where(x => x.DoctorType == type && x.ActiveFlag == 1).Count();

            return totalcount;
        }

        public IEnumerable<Doctor> GetDoctorsByProvinceID(string provinceid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }

            IEnumerable<Doctor> doctors = null;

            if (provinceid == "000000") //全部省份
            {
                doctors = context.Doctors.Where(x=> x.ActiveFlag == 1).OrderByDescending(x => x.TuiJian).ThenByDescending(x => x.DoctorID).Skip(startNo).Take(pageCount);
            }
            else 
            {
                doctors = context.Doctors.Where(x => x.ProvinceID == provinceid && x.ActiveFlag == 1).OrderByDescending(x => x.TuiJian).ThenByDescending(x => x.DoctorID).Skip(startNo).Take(pageCount);
            }

            return doctors;
        }

        //点击次数+1
        public void AddDoctorHitCount(int doctorID)
        {
            Doctor model = context.Doctors.Where(s => s.DoctorID == doctorID).FirstOrDefault();
            model.HitCount = model.HitCount + 1;
            context.SaveChanges();
        }

        public List<Doctor> GetDoctorListByAdminID(int adminid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }

            List<Doctor> doclist = new List<Doctor>();

            if (adminid == 1) //高级管理员
            {
                doclist = context.Doctors.OrderBy(x => x.ActiveFlag).ThenByDescending(c => c.DoctorID).Skip(startNo).Take(pageCount).ToList<Doctor>();
            }
            else //普通管理员
            {
                doclist = context.Doctors.Where(c => c.UserID == adminid).OrderBy(x => x.ActiveFlag).ThenByDescending(c => c.DoctorID).Skip(startNo).Take(pageCount).ToList<Doctor>();
            }

            return doclist;
        }
 
        public int GetDoctorTotalCountByAdminID(int adminid)
        {
            int count = 0;

            if (adminid == 1) //高级管理员
            {
                count = context.Doctors.Count();
            }
            else //普通管理员
            {
                count = context.Doctors.Where(c => c.UserID == adminid).Count();
            }

            return count;
        }
        #endregion

        #region >>>Video<<<
        //添加客户
        public int AddVideo(Video model)
        {
            context.Videos.Add(model);
            return context.SaveChanges();
        }

        //修改客户
        public int UpadteVideo(Video model)
        {
            Video videoModel = model;
            return context.SaveChanges();
        }

        //根据客户ID获取客户
        public Video GetVideoByVideoID(int videoID)
        {
            Video model = context.Videos.FirstOrDefault(c => c.VideoID == videoID);
            return model;
        }

        //获取课程总数
        public int GetVideoTotalCount(int classid)
        {
            int totalCount = 0;

            if (classid == 0)
            {
                totalCount = context.Videos.Count();
            }
            else
            {
                totalCount = context.Videos.Where(x => x.SecondClassID == classid).Count();
            }

            return totalCount;
        }

        public List<Video> GetVideos(int count)
        {
            var list = context.Videos.Where( x=> x.ActiveFlag == 1).OrderByDescending(x => x.ZhiDing).ThenByDescending(x => x.VideoID).Take(count).ToList<Video>();
            return list;
        }

        //根据类别获取文章
        public IEnumerable<Video> GetVideosByPageNum(int classid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }
            List<Video> list = new List<Video>();

            if (classid == 0)
            {
                list = context.Videos.Where(x => x.ActiveFlag == 1).OrderByDescending(x => x.ZhiDing).ThenByDescending(x => x.VideoID).Skip(startNo).Take(pageCount).ToList<Video>();
            }
            else
            {
                list = context.Videos.Where(x => x.ActiveFlag == 1 && x.SecondClassID == classid).OrderByDescending(x => x.ZhiDing).ThenByDescending(x => x.VideoID).Skip(startNo).Take(pageCount).ToList<Video>();
            }
 
            return list;
        }

        //点击次数+1
        public void AddVideoHitCount(int videoID)
        {
            Video model = context.Videos.Where(s => s.VideoID == videoID).FirstOrDefault();
            model.HitCount = model.HitCount + 1;
            context.SaveChanges();
        }


        public List<Video> GetVideoListByAdminID(int adminid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }

            List<Video> vodlist = new List<Video>();

            if (adminid == 1) //高级管理员
            {
                vodlist = context.Videos.OrderBy(x => x.ActiveFlag).ThenByDescending(c => c.VideoID).Skip(startNo).Take(pageCount).ToList<Video>();
            }
            else //普通管理员
            {
                vodlist = context.Videos.Where(c => c.UserID == adminid).OrderBy(x => x.ActiveFlag).ThenByDescending(c => c.VideoID).Skip(startNo).Take(pageCount).ToList<Video>();
            }

            return vodlist;
        }

        public int GetVideoTotalCountByAdminID(int adminid)
        {
            int count = 0;

            if (adminid == 1) //高级管理员
            {
                count = context.Videos.Count();
            }
            else //普通管理员
            {
                count = context.Videos.Where(c => c.UserID == adminid).Count();
            }

            return count;
        }
        #endregion

        #region >>>Project<<<
        //添加案例
        public int AddProject(Project model)
        {
            context.Projects.Add(model);
            return context.SaveChanges();
        }

        //修改案例
        public int UpadteProject(Project model)
        {
            Project projectModel = model;
            return context.SaveChanges();
        }

        public int GetProjectTotalCount(int classid)
        {
            int totalCount = 0;
            if (classid == 0)
            {
                totalCount = context.Projects.Count();
            }
            else
            {
                totalCount = context.Projects.Where(x => x.SecondClassID == classid).Count();
            }

            return totalCount;
        }

        //根据案例ID获取案例
        public Project GetProjectByProjectID(int projectID)
        {
            Project model = context.Projects.FirstOrDefault(c => c.ProjectID == projectID);
            return model;
        }

        //根据类别获取文章
        public List<Project> GetProjectsByPageNum(int classid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }

            List<Project> list = new List<Project>();

            if (classid == 0)
            {
                list = context.Projects.Where(x => x.ActiveFlag == 1).OrderByDescending(x => x.ProjectID).Skip(startNo).Take(pageCount).ToList<Project>();
            }
            else
            {
                list = context.Projects.Where(x => x.SecondClassID == classid && x.ActiveFlag == 1).OrderByDescending(x => x.ProjectID).Skip(startNo).Take(pageCount).ToList<Project>();
            }

            return list;
        }

        //根据类别获取文章
        public List<Project> GetProjectsByCount(int count)
        {
            var list = context.Projects.Where(x => x.TuiJian == 1 && x.ActiveFlag == 1).OrderByDescending(x => x.ProjectID).Take(count).ToList<Project>();
            return list;
        }

        //点击次数+1
        public void AddProjectHitCount(int projectID)
        {
            Project model = context.Projects.Where(s => s.ProjectID == projectID).FirstOrDefault();
            model.HitCount = model.HitCount + 1;
            context.SaveChanges();
        }

        public List<Project> GetProjectListByAdminID(int adminid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }

            List<Project> doclist = new List<Project>();

            if (adminid == 1) //高级管理员
            {
                doclist = context.Projects.OrderBy(x => x.ActiveFlag).ThenByDescending(c => c.ProjectID).Skip(startNo).Take(pageCount).ToList<Project>();
            }
            else //普通管理员
            {
                doclist = context.Projects.Where(c => c.UserID == adminid).OrderBy(x => x.ActiveFlag).ThenByDescending(c => c.ProjectID).Skip(startNo).Take(pageCount).ToList<Project>();
            }

            return doclist;
        }

        public int GetProjectTotalCountByAdminID(int adminid)
        {
            int count = 0;

            if (adminid == 1) //高级管理员
            {
                count = context.Projects.Count();
            }
            else //普通管理员
            {
                count = context.Projects.Where(c => c.UserID == adminid).Count();
            }

            return count;
        }
        #endregion

        #region >>>ProjectRegister<<<
        //添加案例
        public int AddProjectRegister(ProjectRegister model)
        {
            context.ProjectRegisters.Add(model);
            return context.SaveChanges();
        }

        public List<ProjectRegister> GetProjectRegisterList(int projectid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }

            List<ProjectRegister> list = context.ProjectRegisters.Where(c => c.ProjectID == projectid).OrderByDescending(x => x.RegDateTime).Skip(startNo).Take(pageCount).ToList<ProjectRegister>();
 
            return list;
        }

        public int GetProjectRegisterTotalCount(int projectid)
        {
            int count = context.ProjectRegisters.Where(c => c.ProjectID == projectid).Count();
            return count;
        }
        #endregion

        #region >>>Technology<<<
        //添加品牌
        public int AddTechnology(Technology model)
        {
            context.Technologies.Add(model);
            return context.SaveChanges();
        }

        //修改品牌
        public int UpadteTechnology(Technology model)
        {
            Technology tecModel = model;
            return context.SaveChanges();
        }

        //根据品牌ID获取品牌
        public Technology GetTechnologyByTechnologyID(int technologyID)
        {
            Technology model = context.Technologies.FirstOrDefault(c => c.TechID == technologyID);
            return model;
        }

        public int GetTechnologyTotalCount()
        {
            return context.Technologies.Count();
        }

        //获取品牌
        public IEnumerable<Technology> GetTechnologiesByPageNum(int classid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }
            var list = context.Technologies.Where(x => x.ClassID == classid).OrderByDescending(x => x.TechID).Skip(startNo).Take(pageCount);

            return list;
        }

        public IEnumerable<Technology> GetTechnologiesByCount(int count)
        {
            var list = context.Technologies.OrderByDescending(x => x.ModifyDate).Take(count);
            return list;
        }

        public int GetTechnologyCountByClassid(int classid)
        {
            var count = context.Technologies.Where(x => x.ClassID == classid).Count();
            return count;
        }

        #endregion

        #region >>>FriendLink<<<
         //添加友情链接
        public int AddFriendLink(FriendLink friendLink)
        {
            context.FriendLinks.Add(friendLink);
            return context.SaveChanges();
        }

        //修改友情链接
        public int UpadteFriendLink(FriendLink friendLink)
        {
            FriendLink model = friendLink;
            return context.SaveChanges();
        }

        //删除友情链接
        public int DeleteFriendLink(int linkid)
        {
            FriendLink model = GetFriendLinkByLinkID(linkid);
            context.FriendLinks.Remove(model);

            return context.SaveChanges();
        }

        //根据FriendLinkID获取职位
        public FriendLink GetFriendLinkByLinkID(int linkID)
        {
            FriendLink friendLink = context.FriendLinks.FirstOrDefault(c => c.LinkID == linkID);
            return friendLink;
        }

        //获取友情链接列表
        public List<FriendLink> GetFriendLinkList(int count)
        {
            List<FriendLink> friendLinks = context.FriendLinks.OrderByDescending(x => x.LinkID).Take(count).ToList<FriendLink>();
            return friendLinks;
        }

        public List<FriendLink> GetAllFriendLinks()
        {
            var friendLinks = context.FriendLinks.ToList<FriendLink>();
            return friendLinks;
        }
        #endregion
 
        #region >>>Certificate<<<
        //添加证书
        public int AddCertificate(Certificate cert)
        {
            context.Certificates.Add(cert);
            return context.SaveChanges();
        }

        //修改证书
        public int UpadteCertificate(Certificate cert)
        {
            Certificate model = cert;
            return context.SaveChanges();
        }

        //根据证书ID获取证书
        public Certificate GetCertificateByCertID(int certID)
        {
            Certificate info = context.Certificates.FirstOrDefault(c => c.CertID == certID);
            return info;
        }

        //证书查询
        public Certificate SearchCertificate(string searchType, string name, string certNo)
        {
            Certificate cert = context.Certificates.Where(s => s.SearchType == searchType && s.ActiveFlag==1).Where(s => s.Name == name).Where(s => s.CertNo == certNo).FirstOrDefault();
            return cert;
        }

        //证书查询
        public Certificate SearchCertificate(string name, string certNo)
        {
            Certificate cert = context.Certificates.Where(s => s.Name == name && s.CertNo == certNo && s.ActiveFlag == 1).FirstOrDefault();
            return cert;
        }


        public List<Certificate> GetCertificateListByAdminID(int adminid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }

            List<Certificate> crtlist = new List<Certificate>();

            if (adminid == 1) //高级管理员
            {
                crtlist = context.Certificates.OrderBy(x => x.ActiveFlag).ThenByDescending(c => c.CertID).Skip(startNo).Take(pageCount).ToList<Certificate>();
            }
            else //普通管理员
            {
                crtlist = context.Certificates.Where(c => c.UserID == adminid).OrderBy(x => x.ActiveFlag).ThenByDescending(c => c.CertID).Skip(startNo).Take(pageCount).ToList<Certificate>();
            }

            return crtlist;
        }

        public int GettCertificateTotalCountByAdminID(int adminid)
        {
            int count = 0;

            if (adminid == 1) //高级管理员
            {
                count = context.Certificates.Count();
            }
            else //普通管理员
            {
                count = context.Certificates.Where(c => c.UserID == adminid).Count();
            }

            return count;
        }
        #endregion

        #region >>>FocusPic<<<
        //添加证书
        public int AddFocusPic(FocusPic focusPic)
        {
            context.FocusPics.Add(focusPic);
            return context.SaveChanges();
        }

        //修改证书
        public int UpadteFocusPic(FocusPic focusPic)
        {
            FocusPic model = focusPic;
            return context.SaveChanges();
        }

        //根据证书ID获取证书
        public FocusPic GetFocusPicByPicID(int picID)
        {
            FocusPic info = context.FocusPics.FirstOrDefault(c => c.PicID == picID);
            return info;
        }

        //public List<FocusPic> GetFocusPicList()
        //{
        //    return context.FocusPics.OrderByDescending(x=>x.PicID).ToList<FocusPic>();
        //}
        #endregion

        #region >>>VoteStaff<<<
        //添加证书
        public int AddVoteStaff(VoteStaff voteStaff)
        {
            context.VoteStaffs.Add(voteStaff);
            return context.SaveChanges();
        }

        //修改证书
        public int UpdateVoteStaff(VoteStaff voteStaff)
        {
            VoteStaff model = voteStaff;
            return context.SaveChanges();
        }

        //根据证书ID获取证书
        public VoteStaff GetVoteStaffByID(int staffid)
        {
            VoteStaff model = context.VoteStaffs.Where(x => x.StaffID == staffid).FirstOrDefault();
            return model;
        }

        public List<VoteStaff> GetVoteStaffsByPageNum(int classid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }
 
            List<VoteStaff> list = new List<VoteStaff>();

            if (classid == 0)
            {
                list = context.VoteStaffs.OrderByDescending(x => x.VoteCount).Skip(startNo).Take(pageCount).ToList<VoteStaff>();
            }
            else
            {
                list = context.VoteStaffs.Where(x => x.SecondClassID == classid).OrderByDescending(x => x.VoteCount).Skip(startNo).Take(pageCount).ToList<VoteStaff>();
            }

            return list;
        }

        public List<VoteStaff> GetVoteStaffsByClassID(int classid)
        {
            List<VoteStaff> list = list = context.VoteStaffs.Where(x => x.SecondClassID == classid).OrderByDescending(x => x.VoteCount).ToList<VoteStaff>();
 
            return list;
        }

        public List<VoteStaff> GetVoteStaffsByClassID(int classid, int count)
        {
            List<VoteStaff> list = list = context.VoteStaffs.Where(x => x.SecondClassID == classid).OrderByDescending(x => x.VoteCount).Take(count).ToList<VoteStaff>();

            return list;
        }

        public List<VoteStaff> GetVoteStaffsByCount(int count)
        {
            var list = context.VoteStaffs.OrderByDescending(x => x.VoteCount).Take(count).ToList<VoteStaff>();
            return list;
        }

        public List<VoteStaff> GetVoteStaffList()
        {
            return context.VoteStaffs.ToList<VoteStaff>();
        }

        public bool CheckVoteLog(string macAddress, int staffid)
        {
            bool returnValue = false;
            DateTime currentTime = DateTime.Now;

            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var day = DateTime.Now.Day;
            DateTime start = new DateTime(year, month, day, 0, 0, 0);
            DateTime end = new DateTime(year, month, day, 23, 59, 59);

            IEnumerable<VoteLog> votelogs = context.VoteLogs.Where(x => x.StaffID == staffid && x.VoteMacAddress == macAddress).Where(x => x.VoteDateTime <= end && x.VoteDateTime >= start);

            if (votelogs.Count() > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        public int GetVoteStaffTotalCount()
        {
            return context.VoteStaffs.Count();
        }

        public int GetVoteStaffCountByClassID(int classid)
        {
            int totalCount = 0;
            if (classid == 0)
            {
                totalCount = context.Projects.Count();
            }
            else
            {
                totalCount = context.Projects.Where(x => x.SecondClassID == classid).Count();
            }

            return totalCount;
        }
 
        #endregion

        #region >>>VoteLog<<<
        public int AddVoteLog(VoteLog log)
        {
            context.VoteLogs.Add(log);
            return context.SaveChanges();
        }
        #endregion

        #region >>>Province<<<
        public List<Province> GetProvinceList()
        {
            List<Province> provinces = context.Provinces.ToList<Province>();
            return provinces;
        }

        public Province GetProvinceByID(string provinceid)
        {
            Province province = context.Provinces.Where(x => x.Code == provinceid).FirstOrDefault();
            return province;
        }
        #endregion

        #region >>>JiDiJianShe<<<
        //添加案例
        public int AddJiDi(JiDiJianShe model)
        {
            context.JiDiJianShes.Add(model);
            return context.SaveChanges();
        }

        //修改案例
        public int UpdateJiDi(JiDiJianShe model)
        {
            JiDiJianShe jdModel = model;
            return context.SaveChanges();
        }

        public int GetJiDiTotalCount(int classid)
        {
            int totalCount = 0;

            if (classid == 0)
            {
                totalCount = context.JiDiJianShes.Count();
            }
            else
            {
                totalCount = context.JiDiJianShes.Where(x => x.SecondClassID == classid).Count();
            }

            return totalCount;
        }

        //根据案例ID获取案例
        public JiDiJianShe GetJiDiByID(int jiDiId)
        {
            JiDiJianShe model = context.JiDiJianShes.FirstOrDefault(c => c.JiDiId == jiDiId);
            return model;
        }

        //根据案例ID获取案例
        public JiDiJianShe GetJiDiByTuiJian()
        {
            JiDiJianShe model = context.JiDiJianShes.OrderByDescending(x => x.AddDateTime).FirstOrDefault(c => c.TuiJian == 1);
            return model;
        }

        //根据类别获取文章
        public IEnumerable<JiDiJianShe> GetJiDisByPageNum(int classid, string provinceid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }

            IEnumerable<JiDiJianShe> list = new List<JiDiJianShe>();

            if (classid == 0)
            {
                list = context.JiDiJianShes.Where(x => x.ActiveFlag == 1).OrderByDescending(x => x.JiDiId).Skip(startNo).Take(pageCount).ToList<JiDiJianShe>();
            }
            else
            {
                list = context.JiDiJianShes.Where(x => x.SecondClassID == classid && x.ActiveFlag == 1).OrderByDescending(x => x.JiDiId).Skip(startNo).Take(pageCount).ToList<JiDiJianShe>();
            }

            if (!string.IsNullOrEmpty(provinceid) && provinceid != "000000")
            {
                list = list.Where(x => x.ProvinceID == provinceid);
            }

            return list;
        }

        //根据类别获取文章
        public List<JiDiJianShe> GetJiDisByCount(int count)
        {
            var list = context.JiDiJianShes.Where(x => x.ActiveFlag == 1).OrderByDescending(x => x.JiDiId).Take(count).ToList<JiDiJianShe>();
            return list;
        }

        //点击次数+1
        public void AddJiDiHitCount(int jiDiId)
        {
            JiDiJianShe model = context.JiDiJianShes.Where(s => s.JiDiId == jiDiId).FirstOrDefault();
            model.HitCount = model.HitCount + 1;
            context.SaveChanges();
        }

        public List<JiDiJianShe> GetJiDisByAdminID(int adminid, int pageCount, int? currentPage)
        {
            int pageIndex = 1;
            if (currentPage != null)
            {
                pageIndex = (int)currentPage;
            }

            int startNo = (pageIndex - 1) * pageCount;
            if (startNo < 0)
            {
                startNo = 0;
            }

            List<JiDiJianShe> jidilist = new List<JiDiJianShe>();

            if (adminid == 1) //高级管理员
            {
                jidilist = context.JiDiJianShes.OrderBy(x => x.ActiveFlag).ThenByDescending(c => c.JiDiId).Skip(startNo).Take(pageCount).ToList<JiDiJianShe>();
            }
            else //普通管理员
            {
                jidilist = context.JiDiJianShes.Where(c => c.UserID == adminid).OrderBy(x => x.ActiveFlag).ThenByDescending(c => c.JiDiId).Skip(startNo).Take(pageCount).ToList<JiDiJianShe>();
            }

            return jidilist;
        }

        public int GetJiDiTotalCountByAdminID(int adminid)
        {
            int count = 0;

            if (adminid == 1) //高级管理员
            {
                count = context.JiDiJianShes.Count();
            }
            else //普通管理员
            {
                count = context.JiDiJianShes.Where(c => c.UserID == adminid).Count();
            }

            return count;
        }
        #endregion
    }
}
