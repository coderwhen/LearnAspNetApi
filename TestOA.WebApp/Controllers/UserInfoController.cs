using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;
using TestOA.IBLL;
using TestOA.Model;
using TestOA.WebApp.Models;
using System.Drawing;

namespace TestOA.WebApp.Controllers
{
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
        [HttpGet]
        [Route("AddUserInfo")]
        public object AddUserInfo()
        {
            Bitmap bmp = new Bitmap(@"C:\Users\满目山河\Pictures\ScreenCapture\capture_20200501114718544.bmp");
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var base64 = Convert.ToBase64String(ms.GetBuffer());
            bmp.Dispose();
            ms.Dispose();
            return Json(new
            {
                code = 200,
                imgBase = base64
            });
        }

        [HttpGet]
        [Route("DeleteUserInfo")]
        public object DeleteUserInfo()
        {
            List<long> list = new List<long>()
            {
                100,
                101,
                102,
                103,
                104,
                105,
                106
            };
            var result = UserInfoService.DeleteUserInfo(list);
            return result;
        }

        [HttpGet]
        [Route("Connect")]
        public HttpResponseMessage Connect(string nickName)
        {
            HttpContext.Current.AcceptWebSocketRequest(ProcessRequest); //在服务器端接受Web Socket请求，传入的函数作为Web Socket的处理函数，待Web Socket建立后该函数会被调用，在该函数中可以对Web Socket进行消息收发
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols); //构造同意切换至Web Socket的Response.
        }

        public async Task ProcessRequest(AspNetWebSocketContext context)
        {
            var socket = context.WebSocket;//传入的context中有当前的web socket对象
            while (true)
            {
                var buffer = new ArraySegment<byte>(new byte[1024]);
                var receivedResult = await socket.ReceiveAsync(buffer, CancellationToken.None);//对web socket进行异步接收数据
                if (receivedResult.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.Empty, string.Empty, CancellationToken.None);//如果client发起close请求，对client进行ack
                    break;
                }

                if (socket.State == WebSocketState.Open)
                {
                    string recvMsg = Encoding.UTF8.GetString(buffer.Array, 0, receivedResult.Count);
                    var recvBytes = Encoding.UTF8.GetBytes(recvMsg);
                    var sendBuffer = new ArraySegment<byte>(recvBytes);
                    await socket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
