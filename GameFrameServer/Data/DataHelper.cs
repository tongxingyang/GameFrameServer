using NHibernate;
using NHibernate.Cfg;

namespace GameFrameServer.Data
{
    /// <summary>
    /// DataHelper
    /// </summary>
    public class DataHelper
    {
        private static ISessionFactory iSessionFactory;

        public static ISessionFactory ISessionFactory
        {
            get
            {
                if (iSessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly(Define.ServerName);
                    iSessionFactory = configuration.BuildSessionFactory();
                }
                return iSessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return ISessionFactory.OpenSession();
        }
    }
}