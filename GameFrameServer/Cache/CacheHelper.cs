namespace GameFrameServer.Cache
{
    /// <summary>
    /// CacheManager
    /// </summary>
    public class CacheHelper
    {
        public static AccountDataCache AccountCache;
        public static PlayerInfoDataCache PlayerInfoCache;

        static CacheHelper()
        {
            AccountCache = new AccountDataCache();
            PlayerInfoCache = new PlayerInfoDataCache();
        }
    }
}