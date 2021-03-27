using StackExchange.Redis;
using System;
using System.Linq;

namespace RedisService
{
    internal class RedisConnection
    {
        private static volatile Lazy<RedisConnection> lazyRedis = new Lazy<RedisConnection>();
        public static RedisConnection Instance { get { return lazyRedis.Value; } }

        private volatile Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            var con = ConnectionMultiplexer.Connect(Instance.ConfigurationOption);
            con.PreserveAsyncOrder = true;
            return con;
        });
        public ConnectionMultiplexer Connection { get { return lazyConnection.Value; } }

        private ConfigurationOptions ConfigurationOption
        {
            get
            {
                var configruation = new ConfigurationOptions();
                configruation.EndPoints.Add("127.0.0.1", 6379);
                configruation.SyncTimeout = int.MaxValue;
                configruation.AllowAdmin = true;
                configruation.KeepAlive = 100;
                return configruation;
            }
        }

        public IDatabase Database { get { return Connection.GetDatabase(); } }

        public IServer Server
        {
            get
            {
                var endpoints = Connection.GetEndPoints();
                var connectedEndpPoints = endpoints.Where(x => Connection.GetServer(x).IsConnected);
                if (connectedEndpPoints.Count() > 1)
                {
                    var slave = connectedEndpPoints.First(endpoint => Connection.GetServer(endpoint).IsSlave);
                    return Connection.GetServer(slave);
                }
                else
                    return Connection.GetServer(connectedEndpPoints.FirstOrDefault());
            }
        }
    }
}