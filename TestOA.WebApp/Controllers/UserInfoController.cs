using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using TestOA.IBLL;
using TestOA.Model;
using TestOA.BLL;
using System.IO;

namespace TestOA.WebApp.Controllers
{
    [RoutePrefix("api/UserInfo")]
    public class UserInfoController : ApiController
    {
        IUserInfoService UserInfoService { get; set; }
        [HttpGet]
        [Route("GetUserInfo")]
        public HttpResponseMessage GetUserInfo()
        {
            var res = UserInfoService.LoadEntities(c => true);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
        [HttpPost]
        [Route("AddUserInfo")]
        public object AddUserInfo([FromBody] UserInfo user)
        {
            var res = UserInfoService.AddUserInfo(user);
            return res;
        }
        [HttpGet]
        [Route("DeleteUserInfo")]
        public object DeleteUserInfo()
        {
            var result = UserInfoService.DeleteUserInfo(null);
            return result;
        }
    }
}
