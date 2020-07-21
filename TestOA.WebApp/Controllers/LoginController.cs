using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TestOA.Common;

namespace TestOA.WebApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public void CheckLogin()
        {
            HttpContext.AcceptWebSocketRequest(Login.ProcessRequest);
        }
        public ActionResult GetCode()
        {
            return Json(new
            {
                code = 200,
                data = new
                {
                    code = QrCodeHelper.GetQRCodeImageAsBase64("http://192.168.1.104:/Login/Yes?id=" + Guid.NewGuid())
                }
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Yes()
        {
            Login.status = 1;
            Login.message = "请确定登录";
            ViewBag.ID = Request["id"].ToString();
            return View();
        }
        [HttpPost]
        public ActionResult Yes(string id)
        {
            Login.status = 2;
            Login.message = id + "登录成功";
            return Content(id + "登录成功" + Login.message);
        }
    }
}