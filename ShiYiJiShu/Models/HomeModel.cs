﻿using System;
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
        public List<News> JiDiJianShe { get; set; }
        public News JiDiJianShe_Pic { get; set; }
        public List<News> RuXuanJiShu { get; set; }
        public News RuXuanJiShu_Pic { get; set; }
        public List<News> ZhengCeFaGui { get; set; }
        public List<News> HuiYuanZhongXin { get; set; }
        public List<Video> ShiPinZhongXin { get; set; }
        public IEnumerable<Doctor> DoctorList { get; set; }
        public List<FriendLink> FriendLinks { get; set; }
 
    }
 
}