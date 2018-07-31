using System.Collections.Generic;
using Common.Tools;

namespace ServerFrame.Encode
{
    /// <summary>
    /// socketmodel
    /// </summary>
    public class SocketModel
    {
        public byte type { get; set; }//一级协议
        public int area { get; set; }//子模块
        public int command { get; set; }//逻辑功能
        public byte[] message { get; set; }//消息体

        public SocketModel(byte t,int a,int c,byte[] o)
        {
            type = t;
            area = a;
            command = c;
            message = o;
        }

        public SocketModel()
        {
            
        }

        public T GetMessage<T>()
        {
            return CommonTool.Deserialize<T>(message);
        }
        
        public byte[] GetMessage()
        {
            return message;
        }
    }
}