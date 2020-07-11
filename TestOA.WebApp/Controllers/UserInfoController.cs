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
    [RoutePrefix("api/UserInfo")]
    public class UserInfoController : ApiController
    {
        IUserInfoService UserInfoService { get; set; }
        [HttpGet]
        [Route("GetUserInfo")]
        public object GetUserInfo()
        {
            var res = UserInfoService.LoadEntities(c => true);
            return Json(res);
        }

        [HttpPost]
        [Route("AddUserInfo")]
        public object AddUserInfo([FromBody]UserInfo userInfo)
        {
            var res = UserInfoService.AddUserInfo(userInfo);
            return Json(res);
        }

        [HttpGet]
        [Route("DeleteUserInfo")]
        public object DeleteUserInfo([FromBody]List<long> ids)
        {
            int i = 0;
            return 0 / i;
        }

        [HttpGet]
        [Route("GetIpAddress")]
        public object GetIpAddress()
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            return Json(new { ip });
        }
    }
}
