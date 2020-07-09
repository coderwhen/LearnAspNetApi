using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestOA.Model;

namespace TestOA.WebApp.Controllers
{

    [RoutePrefix("api/userinfo")]
    public class UserInfoController : ApiController
    {
        IBLL.IUserInfoService UserService = new BLL.UserInfoService();

        [Route("getuserinfo")]
        public object GetUserInfo()
        {
            var res = UserService.LoadEntities(c => true);
            return Json(res);
        }

        [Route("adduserinfo")]
        public object AddUserInfo([FromBody]UserInfo userInfo)
        {
            var res = UserService.AddUserInfo(userInfo);
            return Json(res);
        }

        [Route("deleteuserInfo")]
        public object DeleteUserInfo([FromBody]List<long> ids)
        {
            if (ids.Count > 0)
                return UserService.DeleteUserInfo(ids);
            else
                return Json(new { message = "没有数据" });
        }
    }
}
