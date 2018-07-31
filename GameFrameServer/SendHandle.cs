using Common.Tools;
using ServerFrame;
using ServerFrame.Encode;

namespace GameFrameServer
{
    public class SendHandle
    {
        public static void OnceSend<T>(UserToken token,byte type, int area, int command, T message)
        {
            SocketModel socketModel = new SocketModel();
            socketModel.type = type;
            socketModel.area = area;
            socketModel.command = command;
            socketModel.message = CommonTool.Serialize<T>(message);
            byte[] value = MessageEncoding.Encode(socketModel);
            value = LengthEncoding.Encode(value);
            token.write(value);
        }
    }
}