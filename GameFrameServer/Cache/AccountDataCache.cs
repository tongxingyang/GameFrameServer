using System.Collections.Generic;
using Common.Model;
using GameFrameServer.Data;
using ServerFrame;

namespace GameFrameServer.Cache
{
    /// <summary>
    /// AccountDataCache
    /// </summary>
    public class AccountDataCache
    {
        private Dictionary<string,AccountModel> AccountModelDic = new Dictionary<string, AccountModel>();
        /// <summary>
        /// 查询数据库的实例
        /// </summary>
        public AccountDataHandle AccountDataHandle = new AccountDataHandle();
        /// <summary>
        /// 根据用户名获取AccountModel
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AccountModel GetAccountModel(string name)
        {
            AccountModel accountModel = null;
            //在缓存中查找
            if (AccountModelDic.ContainsKey(name))
            {
                accountModel = AccountModelDic[name];
                return accountModel;
            }
            //在数据库中查找
            accountModel = AccountDataHandle.GetByUsername(name);
            if (accountModel != null)
            {
                AccountModelDic[name] = accountModel;
            }
            return accountModel;
        }
        /// <summary>
        /// 判断是否包含Account
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasAccountModel(string name)
        {
            bool ret = AccountModelDic.ContainsKey(name);
            if (ret == false)
            {
                AccountModel accountModel = AccountDataHandle.GetByUsername(name);
                if (accountModel != null)
                {
                    AccountModelDic[name] = accountModel;
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            return ret;
        }
        /// <summary>
        /// 验证Account的账号密码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="passworld"></param>
        /// <returns></returns>
        public bool AccountMatch(string name,string passworld)
        {
            AccountModel accountModel = GetAccountModel(name);
            if (accountModel.Passworld == passworld)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 更新Account数据
        /// </summary>
        /// <param name="accountModel"></param>
        public void UpdateAccount(AccountModel accountModel)
        {
            //先写入数据库
            AccountDataHandle.Update(accountModel);
            //写入缓存
            AccountModelDic[accountModel.AccountName] = accountModel;
        }
        /// <summary>
        /// 移除Account缓存数据
        /// </summary>
        /// <param name="name"></param>
        public void RemoveAccountCache(string name)
        {
            if (AccountModelDic.ContainsKey(name))
            {
                AccountModelDic.Remove(name);
            }
        }

        #region 账号上线下线处理

        private Dictionary<UserToken,AccountModel> user2nameDic = new Dictionary<UserToken, AccountModel>();
        private Dictionary<AccountModel,UserToken> name2userDic = new Dictionary<AccountModel, UserToken>();
        /// <summary>
        /// 是否在线
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsOnline(AccountModel accountModel)
        {
            bool ret = name2userDic.ContainsKey(accountModel);
            return ret;
        }
        /// <summary>
        /// 上线处理
        /// </summary>
        /// <param name="name"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Online(AccountModel name, UserToken user)
        {
            if (!IsOnline(name))
            {
                user2nameDic[user] = name;
                name2userDic[name] = user;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 下线操作
        /// </summary>
        /// <param name="user"></param>
        public void Outline(UserToken user)
        {
            if (!user2nameDic.ContainsKey(user))
            {
                return;
            }
            AccountModel name = user2nameDic[user];
            if (user2nameDic.ContainsKey(user))
            {
                user2nameDic.Remove(user);
            }
            if (name2userDic.ContainsKey(name))
            {
                name2userDic.Remove(name);
            }
            //移除Account的缓存数据
            RemoveAccountCache(name.AccountName);
        }

        public UserToken GetUserByName(AccountModel accountModel)
        {
            if (name2userDic.ContainsKey(accountModel))
            {
                return name2userDic[accountModel];
            }
            return null;
        }
        #endregion
    }
}