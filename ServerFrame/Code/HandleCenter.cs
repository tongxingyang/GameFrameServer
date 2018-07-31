using ServerFrame.Encode;

namespace ServerFrame.Code
{
    public abstract class HandleCenter
    {
        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="token"></param>
        public abstract void ClientConnect(UserToken token);
        /// <summary>
        /// 接收客户端的消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        /// <typeparam name="T"></typeparam>
        public abstract void MessageReceive(UserToken token, SocketModel model);
        /// <summary>
        /// 客户端断开连接
        /// </summary>
        /// <param name="tokeb"></param>
        /// <param name="errotmsg"></param>
        public abstract void ClientClose(UserToken tokeb, string errotmsg);
    }
}