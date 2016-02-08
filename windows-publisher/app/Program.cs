using Autofac;
using publisher;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = new Runner();
            Console.ReadLine();
        }
    }

    class Runner
    {
        Timer timer;
        Publisher publisher;
        public Runner()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            publisher = new Publisher(redis);
            timer = new Timer(OnTimer, null, 1000, Timeout.Infinite);
        }

        private void OnTimer(object state)
        {
            publisher.Publish(Guid.NewGuid()).Wait();
            timer.Change(1000, Timeout.Infinite);
        }
    }
}
