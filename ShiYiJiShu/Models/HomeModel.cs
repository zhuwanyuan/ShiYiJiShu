using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class HomeModel
    {
        public List<News> ZuiXinDongTai { get; set; }
        public List<News> WoHuiDongTai { get; set; }
        public IEnumerable<News> FocusPicList { get; set; }
        public List<News> TongZhiGongGao { get; set; }
        public List<Project> ProjectList { get; set; }
        public List<VoteStaff> WangLuoTouPiao { get; set; }
        public List<News> XueShuDongTai { get; set; }
        public News XueShuDongTai_Pic { get; set; }
        public List<JiDiJianShe> JiDiJianShe { get; set; }
        public JiDiJianShe JiDiJianShe_Pic { get; set; }
        public List<SelectProject> RuXuanXiangMu { get; set; }
        public SelectProject RuXuanXiangMu_Pic { get; set; }
        public List<News> ZhengCeFaGui { get; set; }
        public List<News> HuiYuanZhongXin { get; set; }
        public List<Video> ShiPinZhongXin { get; set; }
        public IEnumerable<Doctor> DoctorList { get; set; }
        public List<FriendLink> FriendLinks { get; set; }
 
    }
 
}