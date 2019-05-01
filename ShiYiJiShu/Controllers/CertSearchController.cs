using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiYiJiShu.Models;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Controllers
{
    public class CertSearchController : Controller
    {
        DataService _dateService = new DataService();

        public ActionResult Index()
        {
            CertSearchModel model = new CertSearchModel();
            model.ClassID = 53;
            model.ParentClassID = 0;

            return View(model);
        }

        public ActionResult Result()
        {
            //string id = System.Web.HttpContext.Current.Request.QueryString["ID"].ToString();
            string certNo = System.Web.HttpContext.Current.Request.QueryString["certNo"].ToString();
            string name = System.Web.HttpContext.Current.Request.QueryString["Name"].ToString();

            CertificateModel model = new CertificateModel();
            var cert = _dateService.SearchCertificate(name, certNo);
            model.ClassID = 53;
            model.ParentClassID = 0;

            if (cert != null)
            {
                model.CertID = cert.CertID;
                model.Name = cert.Name;
                model.SearchType = cert.SearchType;
                model.IDNumber = cert.IDNumber;
                model.CertNo = cert.CertNo;
                model.Picture = cert.Picture;
                model.CertSample = cert.CertSample;
                model.JobTitle = cert.JobTitle;
            }
            else
            {
                model.CertID = 0;
            }

            return View(model);
        }

        public ActionResult SearchResult()
        {
            string searchType = System.Web.HttpContext.Current.Request.QueryString["SearchType"].ToString();
            string certNo = System.Web.HttpContext.Current.Request.QueryString["CertNo"].ToString();
            string name = System.Web.HttpContext.Current.Request.QueryString["Name"].ToString();

            CertificateModel model = new CertificateModel();
            var cert = _dateService.SearchCertificate(searchType, name, certNo);
            model.ClassID = 53;

            if (cert != null)
            {
                model.CertID = cert.CertID;
                model.Name = cert.Name;
                model.SearchType = cert.SearchType;
                model.IDNumber = cert.IDNumber;
                model.CertNo = cert.CertNo;
                model.Picture = cert.Picture;
                model.CertSample = cert.CertSample;
                model.JobTitle = cert.JobTitle;
            }
            else
            {
                model.CertID = 0;
            }

            return View(model);
        }
    }
}
