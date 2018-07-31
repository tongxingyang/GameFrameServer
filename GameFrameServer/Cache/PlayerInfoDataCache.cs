using System.Collections.Generic;
using Common.Model;
using GameFrameServer.Data;
using ServerFrame;

namespace GameFrameServer.Cache
{
    /// <summary>
    /// PlayerInfoDataCache
    /// </summary>
    public class PlayerInfoDataCache
    {
        //playerid对应playerinfo
        private Dictionary<int,PlayerInfoModel> id2playerinfo = new Dictionary<int, PlayerInfoModel>();
        //name对用playerinfo
        private Dictionary<string,PlayerInfoModel> name2playerinfo = new Dictionary<string, PlayerInfoModel>();
        //accountid对应playerinfoid
        private Dictionary<int,int> accid2playerid = new Dictionary<int, int>();
        /// <summary>
        /// PlayerInfo数据库操作实例
        /// </summary>
        public PlayerInfoDataHandle PlayerInfoDataHandle = new PlayerInfoDataHandle();
        /// <summary>
        /// 账号ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlayerInfoModel GetPlayerInfoModel(int accountid)
        {
            //从缓存中找
            if (accid2playerid.ContainsKey(accountid))
            {
                return id2playerinfo[accid2playerid[accountid]];
            }
            //在数据库中找
            PlayerInfoModel playerInfoModel = PlayerInfoDataHandle.GetPlayerInfoByAccountID(accountid);
            if (playerInfoModel != null)
            {
                accid2playerid.Add(playerInfoModel.AccountID,playerInfoModel.ID);
                name2playerinfo.Add(playerInfoModel.RoleName,playerInfoModel);
                id2playerinfo.Add(playerInfoModel.ID,playerInfoModel);
            }
            return playerInfoModel;
        }
        /// <summary>
        /// Player的RoleName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PlayerInfoModel GetPlayerInfoModel(string name)
        {
            if (name2playerinfo.ContainsKey(name))
            {
                return name2playerinfo[name];
            }
            PlayerInfoModel playerInfoModel = PlayerInfoDataHandle.GetPlayerInfoByRoleName(name);
            if (playerInfoModel != null)
            {
                accid2playerid.Add(playerInfoModel.AccountID,playerInfoModel.ID);
                id2playerinfo.Add(playerInfoModel.ID,playerInfoModel);
                name2playerinfo.Add(playerInfoModel.RoleName,playerInfoModel);
            }
            return playerInfoModel;
        }
        /// <summary>
        /// 判断缓存是否包含PlayerInfo 账号ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HasPlayerInfoModel(int id)
        {
            if (accid2playerid.ContainsKey(id))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 添加PlayerInfo缓存
        /// </summary>
        /// <param name="playerInfoModel"></param>
        public void AddPlayerInfoCache(PlayerInfoModel playerInfoModel)
        {
            if (!HasPlayerInfoModel(playerInfoModel.AccountID))
            {
                accid2playerid.Add(playerInfoModel.AccountID,playerInfoModel.ID);
                id2playerinfo.Add(playerInfoModel.ID,playerInfoModel);
                name2playerinfo.Add(playerInfoModel.RoleName,playerInfoModel);
            }
        }
        /// <summary>
        /// PlayerID
        /// </summary>
        /// <param name="id"></param>
        public void RemovePlayerInfoCache(int id)
        {
            if (id2playerinfo.ContainsKey(id))
            {
                PlayerInfoModel playerInfoModel = id2playerinfo[id];
                id2playerinfo.Remove(id);
                name2playerinfo.Remove(playerInfoModel.RoleName);
                accid2playerid.Remove(playerInfoModel.AccountID);
            }
        }

        #region PlayerInfo 上线下线

        private Dictionary<UserToken, int> clientIdDict = new Dictionary<UserToken, int>();
        private Dictionary<int, UserToken> idClientDict = new Dictionary<int, UserToken>();
        
        public void Online(UserToken client,int playerid)
        {
            if (IsOnline(playerid) == false)
            {
                clientIdDict.Add(client, playerid);
                idClientDict.Add(playerid, client);
            }
          
        }

        public bool IsOnline(int playerid)
        {
            if (idClientDict.ContainsKey(playerid))
            {
                return true;
            }
            return false;
        }

        public void Outline(UserToken client)
        {
            int playerid;
            clientIdDict.TryGetValue(client, out playerid);
            if (IsOnline(playerid) == true)
            {
                clientIdDict.Remove(client);
                idClientDict.Remove(playerid);
                RemovePlayerInfoCache(playerid);
            }
        }

        #endregion
    }
}