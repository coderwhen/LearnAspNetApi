using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestOA.WebApp.Controllers
{

    [RoutePrefix("api/userinfo")]
    public class UserInfoController : ApiController
    {
        IBLL.IUserService bll = new BLL.UserInfoService();

        [Route("getuserinfo")]
        public object GetUserInfo()
        {
            var res = bll.LoadEntities(c => true);
            return Json(res);
        }
    }
}
