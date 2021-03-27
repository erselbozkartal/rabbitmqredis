using StackExchange.Redis;
using System;

namespace RedisService
{
    public class RedisCache
    {
        private static volatile Lazy<RedisCache> lazyRedis = new Lazy<RedisCache>();
        public static RedisCache Instance { get { return lazyRedis.Value; } }

        public bool Add(string key, string value, When when = When.NotExists) =>
            RedisConnection.Instance.Database.StringSet(key, value, when: when);

        public long Get(string key) =>
            (long)RedisConnection.Instance.Database.StringGet(key);

        public bool Remove(string key) =>
            RedisConnection.Instance.Database.KeyDelete(key);

        public bool SExist(string key) =>
            RedisConnection.Instance.Database.KeyExists(key);
    }
}