using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TestOA.IBLL;
using TestOA.Model;
using TestOA.WebApp.Models;

namespace TestOA.WebApp.Controllers
{
    //[MyActionFilter]
    //[RoutePrefix("api/UserInfo")]
    public class UserInfoController : ApiController
    {
        IUserInfoService UserInfoService { get; set; }
        [HttpGet]
        //[Route("GetUserInfo")]
        public object GetUserInfo()
        {
            var res = UserInfoService.LoadEntities(c => true).Select(m => new
            {
                uId = m.Uid,
                uPwd = m.UPwd,
                uName = m.UName,
                cartList = m.Cart
            });
            return Json(res);
        }
        [HttpPost]
        //[Route("AddUserInfo")]
        public object AddUserInfo([FromBody]UserInfo userInfo)
        {
            var res = UserInfoService.AddUserInfo(userInfo);
            IndexManager.GetInstance().AddQueue(res);
            return Json(res);
        }
        [HttpPost]
        public object DeleteUserInfo(UserInfo userInfo)
        {
            var res = UserInfoService.DeleteUserInfo(userInfo);
            return Json(res);
        }
        [HttpGet]
        //[Route("GetIpAddress")]
        public object GetIpAddress()
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            return Json(new { ip });
        }
    }
}
