using System;
using System.Collections.Generic;
using System.Net.Sockets;
using ServerFrame.Code;
using ServerFrame.Encode;

namespace ServerFrame
{
    /// <summary>
    /// 用户连接信息
    /// </summary>
    public class UserToken
    {
        public Socket conn;
        //用户异步接收网络数据对象
        public SocketAsyncEventArgs receiveSAEA;
        //用户异步发送网络数据对象
        public SocketAsyncEventArgs sendSAEA;
        public CodingDelegate.LengthEncode LE;
        public CodingDelegate.LengthDecode LD;
        public CodingDelegate.SendProcess sendProcess;
        public CodingDelegate.CloseProcess closeProcess;
        public HandleCenter center;
        List<byte> cache = new List<byte>();
        private bool isReading = false;
        private bool isWriting = false;
        Queue<byte[]> writeQueue = new Queue<byte[]>();

        public UserToken()
        {
            receiveSAEA = new SocketAsyncEventArgs();
            sendSAEA = new SocketAsyncEventArgs();
            receiveSAEA.UserToken = this;
            sendSAEA.UserToken = this;
            //设置接收对象的缓冲区大小
            receiveSAEA.SetBuffer(new byte[1024], 0, 1024);
        }
        //网络消息到达
        public void receive(byte[] buff) {
            //将消息写入缓存
            cache.AddRange(buff);
            if (!isReading)
            {
                isReading = true;
                onData();
            }
        }
        //缓存中有数据处理
        void onData() {
            //解码消息存储对象
            byte[] buff = null;
            //当粘包解码器存在的时候 进行粘包处理
            if (LD != null)
            {
                buff = LD(ref cache);
                //消息未接收全 退出数据处理 等待下次消息到达
                if (buff == null) { isReading = false; return; }
            }
            else {
                //缓存区中没有数据 直接跳出数据处理 等待下次消息到达
                if (cache.Count == 0) { isReading = false; return; }
                buff = cache.ToArray();
                cache.Clear();
            }
            //进行消息反序列化
            SocketModel message = MessageEncoding.Decode(buff);
            center.MessageReceive(this, message);
            onData();
        }
        public void write(byte[] value) {
            if (conn == null) {
                //此连接已经断开了
                closeProcess(this, "调用已经断开的连接");
                return;
            }
            writeQueue.Enqueue(value);
            if (!isWriting) {
                isWriting = true;
                onWrite();
            }
        }

        public void onWrite() {
            //判断发送消息队列是否有消息
            if (writeQueue.Count == 0) { isWriting = false; return; }
            //取出第一条待发消息
            byte[] buff = writeQueue.Dequeue();
            //设置消息发送异步对象的发送数据缓冲区数据
            sendSAEA.SetBuffer(buff, 0, buff.Length);
            //开启异步发送
            bool result = conn.SendAsync(sendSAEA);
            //是否挂起
            if (!result) {
                sendProcess(sendSAEA);
            }
        }

        public void writed() {
            //与onData尾递归同理
            onWrite();
        }
        public void Close() {
            try
            {
                writeQueue.Clear();
                cache.Clear();
                isReading = false;
                isWriting = false;
                conn.Shutdown(SocketShutdown.Both);
                conn.Close();
                conn = null;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }

}