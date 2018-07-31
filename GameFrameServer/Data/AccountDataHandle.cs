using System.Collections.Generic;
using Common.Model;
using NHibernate;
using NHibernate.Criterion;

namespace GameFrameServer.Data
{
    /// <summary>
    /// Account
    /// </summary>
    public class AccountDataHandle
    {
        /// <summary>
        /// 添加Account
        /// </summary>
        /// <param name="user"></param>
        public void AddAccount(AccountModel user)
        {
            using (ISession session = DataHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }
        }
        /// <summary>
        /// 更新Account
        /// </summary>
        /// <param name="user"></param>
        public void Update(AccountModel user)
        {
            using (ISession session = DataHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(user);
                    transaction.Commit();
                }
            }
        }
        /// <summary>
        /// 删除Account
        /// </summary>
        /// <param name="user"></param>
        public void Delete(AccountModel user)
        {
            using (ISession session = DataHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(user);
                    transaction.Commit();
                }
            }
        }
        /// <summary>
        /// 根据ID获取Account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccountModel GetById(int id)
        {
            using (ISession session = DataHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    AccountModel user = session.Get<AccountModel>(id);
                    transaction.Commit();
                    return user;
                }
            }
        }
        /// <summary>
        /// 根据UserName获取Account
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public AccountModel GetByUsername(string username)
        {
            using (ISession session = DataHelper.OpenSession())
            {
                AccountModel user = session.CreateCriteria(typeof(AccountModel)).Add(Restrictions.Eq("AccountName", username)).UniqueResult<AccountModel>();
                return user;
            }
        }
        /// <summary>
        /// 获取全部的Account
        /// </summary>
        /// <returns></returns>
        public ICollection<AccountModel> GetAllUsers()
        {
            using (ISession session = DataHelper.OpenSession())
            {
                IList<AccountModel> users = session.CreateCriteria(typeof(AccountModel)).List<AccountModel>();
                return users;
            }
        }
        /// <summary>
        /// 验证用户名和密码是否相同
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VerifyUser(string username, string password)
        {
            using (ISession session = DataHelper.OpenSession())
            {
                AccountModel user = session
                    .CreateCriteria(typeof(AccountModel))
                    .Add(Restrictions.Eq("AccountName", username))
                    .Add(Restrictions.Eq("Passworld", password))
                    .UniqueResult<AccountModel>();
                if (user == null) return false;
                return true;
            }
        }
    }
}