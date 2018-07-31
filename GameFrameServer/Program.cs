using System;
using System.Collections.Generic;
using GameFrameServer.Handle;
using ServerFrame.Code;
using ServerFrame.Encode;

namespace GameFrameServer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Define.ServerName+"............");
            ServerStart ss = new ServerStart(9000);
            ss.center = new Center();
            ss.LD = LengthEncoding.Decode;
            ss.LE = LengthEncoding.Encode;
            ss.Start(6650);
            Console.WriteLine("服务器启动成功");
            while (true) { }
        }
    }
}