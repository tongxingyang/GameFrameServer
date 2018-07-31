namespace Common.Protocol
{
    public class PlayerInfoProtocal
    {
        public const int GETINFO = 0;
        public const int CREATE = 1;
        public const int ONLINE = 2;
        public const int GETFRIEND = 3;
        public const int ADDFRIENDTOSERVER = 4;//向服务器请求添加好友
        public const int ADDFRIENDTOCLIENT = 5;//向客户端返回添加好友请求
        public const int FRIENDONLINE = 6;
        public const int FRIENDOUTLINE = 7;
    }
}