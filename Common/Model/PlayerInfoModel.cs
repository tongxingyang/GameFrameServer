namespace Common.Model
{
    /// <summary>
    /// 玩家角色信息
    /// </summary>
    [ProtoBuf.ProtoContract]
    public class PlayerInfoModel
    {
        [ProtoBuf.ProtoMember(1)]
        public virtual int ID { get; set; }
        [ProtoBuf.ProtoMember(2)]
        public virtual int AccountID { get; set; }
        [ProtoBuf.ProtoMember(3)]
        public virtual string RoleName { get; set; }
        [ProtoBuf.ProtoMember(4)]
        public virtual int LV { get; set; }
        [ProtoBuf.ProtoMember(5)]
        public virtual int EXP { get; set; }
        [ProtoBuf.ProtoMember(6)]
        public virtual int Power { get; set; }
        [ProtoBuf.ProtoMember(7)]
        public virtual string HeroID { get; set; }
        [ProtoBuf.ProtoMember(8)]
        public virtual string FriendID { get; set; }
        [ProtoBuf.ProtoMember(9)]
        public virtual int CoinNum { get; set; }
        [ProtoBuf.ProtoMember(10)]
        public virtual int CouponNum { get; set; }
        [ProtoBuf.ProtoMember(11)]
        public virtual int WinNum { get; set; }
        [ProtoBuf.ProtoMember(12)]
        public virtual int LostNum { get; set; }
        [ProtoBuf.ProtoMember(13)]
        public virtual int RunNum { get; set; }
        [ProtoBuf.ProtoMember(14)]
        public virtual string HeadImg { get; set; }
    }
}