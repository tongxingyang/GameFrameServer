using System;
using Common.Model;
using Common.Protocol;
using GameFrameServer.Cache;
using ServerFrame;
using ServerFrame.Encode;

namespace GameFrameServer.Handle
{
    public class AccountHandle:HandleInterface
    {
        public void ClientClose(UserToken userToken, string error)
        {
        }

        public void MessageReceive(UserToken userToken, SocketModel socketModel)
        {
           AccountModel accountModel = CacheHelper.AccountCache.GetAccountModel("wuwei");
            Console.WriteLine("username    "+accountModel.AccountName);
            Console.WriteLine("passwd      "+accountModel.Passworld);
            Console.WriteLine("time      "+accountModel.RegisterTime);
            Console.WriteLine("id      "+accountModel.ID);
        }
    }
}