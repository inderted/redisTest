using System;
using System.Threading;
using Nop.Core.Caching;

namespace RedisPublish
{
    class Program
    {
        static void Main(string[] args)
        {

            var redisConnectionWrapper = new RedisConnectionWrapper("***,defaultDatabase=0,password=***");

            #region 手动发布-信道a

            //using (var redisManager = new RedisCacheManager(redisConnectionWrapper))
            //{
            //    for (int i = 0; i < 10000; i++)
            //    {
            //        Thread.Sleep(1000);
            //        redisManager.PublishAsync("a", i.ToString()).Wait();
            //        Console.WriteLine($"发布信道a...值：{i}");
            //    }
            //}

            #endregion

            #region 修改键mykey,测试键监听订阅

            //因为开启键空间通知功能需要消耗一些 CPU ， 所以在默认配置下， 该功能处于关闭状态。

            //可以通过修改 redis.conf 文件， 或者直接使用 CONFIG SET 命令来开启或关闭键空间通知功能：

            //当 notify-keyspace - events 选项的参数为空字符串时，功能关闭。
            //另一方面，当参数不是空字符串时，功能开启。
            //notify - keyspace - events 的参数可以是以下字符的任意组合， 它指定了服务器该发送哪些类型的通知：

            //\begin{ array}
            //            [b] {| c |} 
            //\hline 字符 &发送的通知  \\ 
            //\hline K  &键空间通知，所有通知以 `__keyspace@< db > __` 为前缀  \\ 
            //\hline E  &键事件通知，所有通知以 `__keyevent@< db > __` 为前缀  \\ 
            //\hline g  &DEL 、 EXPIRE 、 RENAME 等类型无关的通用命令的通知 \\ 
            //\hline $  &字符串命令的通知 \\ 
            //\hline l  &列表命令的通知 \\ 
            //\hline s  &集合命令的通知 \\ 
            //\hline h  &哈希命令的通知 \\ 
            //\hline z  &有序集合命令的通知 \\ 
            //\hline x  &过期事件：每当有过期键被删除时发送 \\ 
            //\hline e  &驱逐(evict)事件：每当有键因为 maxmemory 政策而被删除时发送 \\ 
            //\hline A  &参数 g$lshzxe 的别名，即all \\ 
            //\end{ array} \\
            //输入的参数中至少要有一个 K 或者 E ， 否则的话， 不管其余的参数是什么， 都不会有任何通知被分发。
            //举个例子， 如果只想订阅键空间中和列表相关的通知， 那么参数就应该设为 Kl ， 诸如此类。
            //将参数设为字符串 "AKE" 表示发送所有类型的通知。

            using (var redis = new RedisCacheManager(redisConnectionWrapper))
            {
                for (int i = 0; i < 100000; i++)
                {
                    Thread.Sleep(1000);
                    redis.Set("mykey", i, 1000);
                    Console.WriteLine($"写入mykey:{i}");
                }
            }

            #endregion

            Console.ReadKey();
        }
    }
}
