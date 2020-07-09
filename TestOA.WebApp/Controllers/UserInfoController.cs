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
        IBLL.IUserInfoService UserInfoService = new BLL.UserInfoService();

        [Route("getuserinfo")]
        public object GetUserInfo()
        {
            var res = UserInfoService.LoadEntities(c => true);
            return Json(res);
        }

        [Route("adduserinfo")]
        public object AddUserInfo([FromBody]UserInfo userInfo)
        {
            var res = UserInfoService.AddUserInfo(userInfo);
            return Json(res);
        }

        [Route("deleteuserInfo")]
        public object DeleteUserInfo([FromBody]List<long> ids)
        {
            if (ids.Count > 0)
                return UserInfoService.DeleteUserInfo(ids);
            else
                return Json(new { message = "没有数据" });
        }
    }
}
