using Newtonsoft.Json;
using publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publisher
{
    public class Publisher : IPublisher
    {
        readonly StackExchange.Redis.IConnectionMultiplexer _redisConnection;
        const string key = "publish";
        public Publisher(StackExchange.Redis.IConnectionMultiplexer redisConnection)
        {
            this._redisConnection = redisConnection;
        }

        public async Task Publish(object data)
        {
            var serialized = JsonConvert.SerializeObject(data);
            await _redisConnection.GetDatabase().ListRightPushAsync(key, serialized);
            await _redisConnection.GetDatabase().PublishAsync("publish", "request 1");
            Console.Write(DateTime.Now + " " + serialized);

        }

    }
}
