using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestOA.WebApp.Models;
using TestOA.Common;

namespace TestOA.WebApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            if (MemcachedHelper.Get("UserInfo") != null)
            {
                ViewBag.Message = "您当前已登录！";
                return View();
            }
            ViewBag.Message = "请登录！";
            return View();
        }

        [HttpPost]
        public ActionResult Login(dynamic agrs)
        {
            var UName = Request["UName"];
            var UPwd = Request["UPwd"];
            if (UName == "2250573213" && UPwd == "zychhazl99")
            {
                Response.Cookies.Add(new HttpCookie("Uid")
                {
                    Expires = DateTime.Now.AddDays(7)
                });
                ViewBag.Message = "登录成功！";
                MemcachedHelper.Set("UserInfo", "success", DateTime.Now.AddSeconds(30));
                return View();
            }
            else
            {
                ViewBag.Message = "登录失败！";
                return View();
            }
        }
    }
}