using System;
using System.Net;
using Common.Protocol;
using ServerFrame;
using ServerFrame.Code;
using ServerFrame.Encode;

namespace GameFrameServer.Handle
{
    public class Center:HandleCenter
    {
        private AccountHandle Account;
        private PlayerInfoHandle PlayerInfo;
        public Center()
        {
            Account = new AccountHandle();
            PlayerInfo = new PlayerInfoHandle();
        }
      
        public override void ClientConnect(UserToken token)
        {
            IPEndPoint clientipe = (IPEndPoint)token.conn.RemoteEndPoint;
            Console.WriteLine("接收到客户端连接  IP:  "+clientipe.Address+"   Port: "+clientipe.Port);
        }

        public override void MessageReceive(UserToken token, SocketModel model)
        {
            switch (model.type) { 
                case Protocol.Account:
                    Account.MessageReceive(token, model);
                    break;
                case Protocol.PlayerInfo:
                    PlayerInfo.MessageReceive(token, model);
                    break;
                default:
                    //未知模块  可能是客户端作弊了 无视
                    break;
            }
        }

        public override void ClientClose(UserToken tokeb, string errotmsg)
        {
            Console.WriteLine("有客户端断开连接了");
        }
    }
}