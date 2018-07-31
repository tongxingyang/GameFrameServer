using System;

namespace Common.Model
{
    /// <summary>
    /// 账号信息传输模型
    /// </summary>
    [ProtoBuf.ProtoContract]
    public class AccountModel
    {
        [ProtoBuf.ProtoMember(1)]
        public virtual int ID { get; set; }
        [ProtoBuf.ProtoMember(2)]
        public virtual string AccountName { get; set; }
        [ProtoBuf.ProtoMember(3)]
        public virtual string Passworld { get; set; }
        [ProtoBuf.ProtoMember(4)]
        public virtual int ServerID { get; set; }
        [ProtoBuf.ProtoMember(5)]
        public virtual DateTime RegisterTime { get; set; }
    }
}