using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestOA.Common;

namespace TestOA.WebApp.Controllers
{
    [RoutePrefix("api/QrCode")]
    public class QrCodeController : ApiController
    {
        [HttpGet]
        [Route("GetCode")]
        public object GetCode()
        {
            return new
            {
                code = 200,
                data = new
                {
                    code = QrCodeHelper.GetQRCodeImageAsBase64("http://localhost:59419/Login/Yes")
                }
            };
        }
    }
}
