# WebApi搭建WebSocket

### 一、创建控制器和方法（Get）

```C#
 	[RoutePrefix("api/Chat")]
    public class ChatController : ApiController
    {
        //
        public static List<WebSocket> _socketList = new List<WebSocket>();
        [HttpGet]
        [Route("Connect")]
        public HttpResponseMessage Connect(string nickName)
        {
            HttpContext.Current.AcceptWebSocketRequest(ProcessRequest); //在服务器端接受Web Socket请求，传入的函数作为Web Socket的处理函数，待Web Socket建立后该函数会被调用，在该函数中可以对Web Socket进行消息收发
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols); //构造同意切换至Web Socket的Response.
        }

      
    }
```

### 二、指定一个方法用于与客户端长连接

```C#
  public async Task ProcessRequest(AspNetWebSocketContext context)
        {
            var socket = context.WebSocket;//传入的context中有当前的web socket对象
            _socketList.Add(socket);
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
                    foreach (var _socket in _socketList)
                    {
                        await _socket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }

                Thread.Sleep(1000);
            }
        }
```

### 三、客户端代码（Vue）

```Vue
<template>
    <div id="app">
        <p>
            <label for="nickName">用户名</label>
            <input type="text" id="nickName" name="nickName" v-model="nickName">
            <button @click="onStart">启动</button>
        </p>
        <p>
            <label for="message">消息</label>
            <input type="text" id="message" name="message" v-model="message" @keydown.enter="onSendMessage">
            <button @click="onSendMessage">发送</button>
        </p>
        <p>
            <button @click="onClose">关闭</button>
        </p>
        <ul>
            <li v-for="(item,index) in messageList" :key="index">
                {{item}}
            </li>
        </ul>
    </div>
</template>

<script>
    import HelloWorld from './components/HelloWorld.vue'

    const _data = {}
    export default {
        name: 'App',
        data() {
            return {
                nickName: '',
                message: '',
                isStart: false,
                messageList: []
            }
        },
        components: {
            HelloWorld
        },
        methods: {
            init() {
                let self = this
                let webSocket = new WebSocket("ws://192.168.1.104/api/chat/Connect?nickName=" + this.nickName);
                webSocket.onopen = function () {
                    console.log("opened");
                    self.isStart = true
                }
                webSocket.onerror = function () {
                    console.log("web socket error");
                }

                webSocket.onmessage = function (event) {
                    let data = JSON.parse(event.data)
                    console.log("web socket error", event);
                    self.messageList.push(data.message)
                }
                _data.webSocket = webSocket
            },
            onStart() {
                let isStart = this.isStart
                if (isStart) {
                    return alert('启动')
                }
                this.init()
            },
            onSendMessage() {
                let message = this.message
                if (message.trim().length <= 0) {
                    return console.log('发送信息为空')
                }
                _data.webSocket.send(JSON.stringify({
                    message
                }))
                this.message = ''
            },
            onClose() {
                let res = _data.webSocket.close()
                console.log(res)
            }
        }
    }
</script>

<style lang="scss">

</style>V
```



