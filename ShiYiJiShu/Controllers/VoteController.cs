using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiYiJiShu.Models;
using ShiYiJiShu.Data;
using Common;
using System.Management;  

namespace ShiYiJiShu.Controllers
{
    public class VoteController : Controller
    {
        BaseClass bc = new BaseClass();
        DataService _dataService = new DataService();
 
        public ActionResult List(int classid, int? currentPage)
        {
            VoteListModel model = new VoteListModel();

            int pageCount = 16;

            NewsClass topClass = _dataService.GetNewsClassByClassID(13);
            model.TopClassID = topClass.ClassID;
            model.TopClassName = topClass.ClassName;

            if (classid != 0)
            {
                NewsClass currentClass = _dataService.GetNewsClassByClassID(classid);
                model.ClassID = currentClass.ClassID;
                model.ClassName = currentClass.ClassName;
            }
 
            //int totalCount = _dataService.GetVoteStaffCountByClassID(classid);

            IEnumerable<VoteStaff> staffList = _dataService.GetVoteStaffsByClassID(classid);
            model.StaffList = staffList;
 
            if (bc.CheckMobile())
            {
                //if (totalCount > pageCount)
                //{
                //    model.PageLink = bc.GetMobilePageLink(classid, pageCount, totalCount, currentPage, "../Vote/List");
                //}
 
                return View("~/Views/Mobile/VoteList.cshtml", model);
            }
            else
            {
                //if (totalCount > pageCount)
                //{
                //    model.PageLink = bc.GetPageLink(pageCount, totalCount, currentPage, "../Vote/List/" + classid);
                //}

                return View(model);
            }
        }

        public ActionResult Detail(int staffid)
        {
            VoteDetailModel model = new VoteDetailModel();
            model.VoteStaff = _dataService.GetVoteStaffByID(staffid);

            if (bc.CheckMobile())
            {
                return View("~/Views/Mobile/VoteDetail.cshtml", model);
            }
            else
            {
                return View(model);
            }
        }

        public JsonResult AddVoteCount(int staffid)
        {
            int returnCode = -1;
            string message = "添加失败，请重试！";
            object data = null;
 
            try
            {
                VoteStaff model = _dataService.GetVoteStaffByID(staffid);

                string ipAddress = GetHostAddress();

                bool check = _dataService.CheckVoteLog(ipAddress, staffid);

                VoteLog log = new VoteLog();
                log.StaffID = staffid;
                log.VoteMacAddress = ipAddress;
                log.VoteDateTime = DateTime.Now;
 
                if (!check)
                {
                    if (_dataService.AddVoteLog(log) > 0)
                    {
                        //将投票次数+1
                        model.VoteCount += 1;
                        model.AddDateTime = DateTime.Now;
                        _dataService.UpdateVoteStaff(model);

                        data = true;
                        message = "投票成功！";
                    }
                    else
                    {
                        data = false;
                        message = "投票失败！";
                    }
                }
                else
                {
                    data = false;
                    message = "您今天已投票，请勿重复投票！";
                }

                returnCode = 0;
                
            }
            catch (Exception ex)
            {
                data = false;
                returnCode = -1;
                message = ex.Message;
            }

            var result = new
            {
                ReturnCode = returnCode,
                Data = data,
                Message = message
            };

            return Json(result);
        }

        /// <summary>  
        /// 获取本机MAC地址  
        /// </summary>  
        /// <returns>本机MAC地址</returns>  
        public string GetMacAddress()
        {
            try
            {
                string strMac = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        strMac = mo["MacAddress"].ToString();
                    }
                }
                moc = null;
                mc = null;
                return strMac;
            }
            catch
            {
                return "unknown";
            }
        }


        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetHostAddress()
        {
            string userHostAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(userHostAddress))
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    userHostAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
                }
                    
            }
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }

            return "127.0.0.1";
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

    }
}
