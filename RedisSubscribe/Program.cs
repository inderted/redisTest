using System;
using Nop.Core.Caching;

namespace RedisSubscribe
{
    class Program
    {
        static void Main(string[] args)
        {
            var redisConnectionWrapper = new RedisConnectionWrapper("***:6379,defaultDatabase=0,password=***");

            #region 订阅信道a

            //Console.WriteLine("开始监听...");
            //using (var redisManager = new RedisCacheManager(redisConnectionWrapper))
            //{
            //    redisManager.SubscribeAsync("a", (redisChannel, redisValue) =>
            //    {
            //        Console.WriteLine($"接收到订阅：{redisValue}");
            //    }).Wait();
            //}

            #endregion


            #region 订阅

            /*
   * http://redisdoc.com/key/del.html#del、http://redisdoc.com/pub_sub/publish.html#publish
   * 对于每个修改数据库的操作，键空间通知都会发送两种不同类型的事件：键空间通知（key-space）和键事件通知（key-event）。
   * * 当 del mykey 命令执行时：
   * * *   键空间频道的订阅者将接收到被执行的事件的名字，在这个例子中，就是 del
   * * *   键事件频道的订阅者将接收到被执行事件的键的名字，在这个例子中，就是 mykey
   * * *   比如说，对 0 号数据库的键 mykey 执行del命令时， 系统将分发两条消息， 相当于执行以下两个 publish命令:
   * * *   PUBLISH __keyspace@0__:sampleKey del
   * * *   PUBLISH __keyevent@0__:del sampleKey
   * * *   订阅第一个频道 __keyspace@0__:mykey 可以接收 0 号数据库中所有修改键 mykey 的事件
   * * *   订阅第二个频道 __keyevent@0__:del 则可以接收 0 号数据库中所有执行 del 命令的键
   *
   * 事件是用  __keyspace@DB__:KeyPattern 或者  __keyevent@DB__:OpsType 的格式来发布消息的
   * *   DB表示在第几个库；
   * *   KeyPattern则是表示需要监控的键模式（可以用通配符）；
   * *   OpsType则表示操作类型。因此，如果想要订阅特殊的Key上的事件，应该是订阅keyspace。
   */
            Console.WriteLine("开始监听...");
            using (var redisManager = new RedisCacheManager(redisConnectionWrapper))
            {
                redisManager.SubscribeAsync("__keyspace@0__:*", (redisChannel, redisValue) =>
                {
                    Console.WriteLine($"接收到键mykey的更改：{redisValue}");
                }).Wait();
            }

            #endregion



            Console.ReadKey();
        }
    }
}
