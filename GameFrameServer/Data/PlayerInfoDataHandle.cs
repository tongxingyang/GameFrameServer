using System.Collections.Generic;
using Common.Model;
using NHibernate;
using NHibernate.Criterion;

namespace GameFrameServer.Data
{
    /// <summary>
    /// PlayerInfoDataHandle
    /// </summary>
    public class PlayerInfoDataHandle
    {
        /// <summary>
        /// 添加PlayerInfoData
        /// </summary>
        /// <param name="info"></param>
        public void AddPlayerInfo(PlayerInfoModel info)
        {
            using (ISession session = DataHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(info);
                    transaction.Commit();
                }
            }            
        }
        /// <summary>
        /// 更新PlayerInfo
        /// </summary>
        /// <param name="info"></param>
        public void Update(PlayerInfoModel info)
        {
            using (ISession session = DataHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(info);
                    transaction.Commit();
                }
            }
        }
        /// <summary>
        /// 根据ID获取PLayerInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlayerInfoModel GetPlayerInfoByID(int id)
        {
            using (ISession session = DataHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    PlayerInfoModel info = session.Get<PlayerInfoModel>(id);
                    transaction.Commit();
                    return info;
                }
            }
        }
        /// <summary>
        /// 根据用户名获取PlayerInfo
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public PlayerInfoModel GetPlayerInfoByRoleName(string username)
        {
            using (ISession session = DataHelper.OpenSession())
            {
                PlayerInfoModel info = session.CreateCriteria<PlayerInfoModel>().Add(Restrictions.Eq("RoleName", username)).UniqueResult<PlayerInfoModel>();
                return info;
            }
        }
        /// <summary>
        /// 根据AccountID获取PlayerInfo
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public PlayerInfoModel GetPlayerInfoByAccountID(int accountID)
        {
            using (ISession session = DataHelper.OpenSession())
            {
                PlayerInfoModel info = session.CreateCriteria(typeof(PlayerInfoModel)).Add(Restrictions.Eq("AccountID", accountID)).UniqueResult<PlayerInfoModel>();
                return info;  
            }
        }
        /// <summary>
        /// 获取所有的玩家角色信息
        /// </summary>
        /// <returns></returns>
        public ICollection<PlayerInfoModel> GetAllUsers()
        {
            using (ISession session = DataHelper.OpenSession())
            {
                IList<PlayerInfoModel> users = session.CreateCriteria(typeof(PlayerInfoModel)).List<PlayerInfoModel>();
                return users;
            }
        }
    }
}