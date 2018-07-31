using System.Collections.Generic;
using System.Net.Sockets;

namespace ServerFrame.Code
{
    public class CodingDelegate
    {
        public delegate byte[] LengthEncode(byte[] value);
        public delegate byte[] LengthDecode(ref List<byte> value);
        public delegate void SendProcess(SocketAsyncEventArgs e);
        public delegate void CloseProcess(UserToken token, string error);
    }
}