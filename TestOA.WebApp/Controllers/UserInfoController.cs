using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestOA.IBLL;
using TestOA.Model;

namespace TestOA.WebApp.Controllers
{

    [RoutePrefix("api/userinfo")]
    public class UserInfoController : ApiController
    {
        IUserInfoService UserInfoService { get; set; }
        [HttpGet]
        [Route("getuserinfo")]
        public object GetUserInfo()
        {
            var res = UserInfoService.LoadEntities(c => true);
            return Json(res);
        }

        [HttpPost]
        [Route("adduserinfo")]
        public object AddUserInfo([FromBody]UserInfo userInfo)
        {
            var res = UserInfoService.AddUserInfo(userInfo);
            return Json(res);
        }

        [HttpPost]
        [Route("deleteuserinfo")]
        public object DeleteUserInfo([FromBody]List<long> ids)
        {
            if (ids != null)
                return Json(new
                {
                    ids,
                    ids.Count
                });
            return new
            {
                ids
            };
        }
    }
}
