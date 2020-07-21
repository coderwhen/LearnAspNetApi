using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.WebSockets;

namespace TestOA.Common
{
    public class Login
    {
        private static int counter = 0;
        public static int status { get; set; } = 0;
        public static string message { get; set; } = "未登录";
        public static async Task ProcessRequest(AspNetWebSocketContext context)
        {
            var socket = context.WebSocket;//传入的context中有当前的web socket对象
            while (true)
            {
                if (socket.State == WebSocketState.Open)
                {
                    var recvBytes = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status,
                        message
                    }));
                    var sendBuffer = new ArraySegment<byte>(recvBytes);
                    await socket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                if (status == 2 && counter++ > 50) { break; }
                Thread.Sleep(3000);
            }
        }
    }
}
