using RedisService;
using StackExchange.Redis;

namespace Service
{
    public class Repository
    {
        public static bool UpdateValue(string serviceNum)
        {
            var serviceName = $"Service{serviceNum}";
            if(RedisCache.Instance.SExist(serviceName))
            {
                var value = RedisCache.Instance.Get(serviceName);
                value = value + 1;
                RedisCache.Instance.Add(serviceName, value.ToString(), When.Always);
                return true;
            }
            RedisCache.Instance.Add(serviceName, "1", When.Always);
            return true;
        }
    }
}