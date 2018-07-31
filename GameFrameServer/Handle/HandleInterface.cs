using ServerFrame;
using ServerFrame.Encode;

namespace GameFrameServer.Handle
{
    public interface HandleInterface
    {
        void ClientClose(UserToken userToken, string error);
        void MessageReceive(UserToken userToken, SocketModel socketModel);
    }
}