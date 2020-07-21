# WebApi上传与下载文件

### 一、上传文件

```C#
 		[HttpPost]
        [Route("UpLoad")]
        public object UpLoad()
        {
            var formData = HttpContext.Current.Request.Form;
            var data = formData["FirstName2"];//获取携带的key的值
            var files = HttpContext.Current.Request.Files.GetMultiple("list");//获取标识的文件可为多个
            foreach (var item in files)
            {
                //保存图片
                item.SaveAs(HttpContext.Current.Server.MapPath("~/App_Data/") + item.FileName);
            }
            return Json(formData);
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



