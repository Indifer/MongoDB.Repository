﻿using MongoDB.Driver;
using MongoDB.Repository.Test;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.Repository.PerformanceTest
{
    class Program
    {
        static UserRepAsync userRepAsync = new UserRepAsync();
        static UserRep userRep = new UserRep();

        /// <summary>
        /// 
        /// </summary>
        //static int index;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int speed = 1000;
            //GetAsync().Wait();
            Stopwatch sw = new Stopwatch();

            //sw.Restart();
            //for (var i = 0; i < speed; i++)
            //{
            //    Insert2().Wait();
            //}
            //sw.Stop();
            //Console.WriteLine("for:async:" + sw.ElapsedMilliseconds);


            //sw.Restart();
            //for (var i = 0; i < speed; i++)
            //{
            //    Insert3();
            //}
            //sw.Stop();
            //Console.WriteLine("for:sync:" + sw.ElapsedMilliseconds);

            //sw.Restart();
            //Parallel.For(0, speed, x =>
            //{
            //    Insert2().Wait();
            //});
            //sw.Stop();
            //Console.WriteLine("parallel:async:" + sw.ElapsedMilliseconds);

            //sw.Restart();
            //Parallel.For(0, speed, x =>
            //{
            //    Insert3();
            //});
            //sw.Stop();
            //Console.WriteLine("parallel:sync:" + sw.ElapsedMilliseconds);

            //Expression<Func<User>> fieldsExp = () => new User { Age = 12, Name = "1111" };



            Console.WriteLine("over...");
            Console.ReadLine();
        }


        public static void Insert3()
        {
            var user = new User();
            user.Name = "cc";
            userRep.Insert(user);
        }

        public static async Task Insert2()
        {
            var user = new User();
            user.Name = "cc";
            await userRepAsync.InsertAsync(user).ConfigureAwait(false);
        }

        public static async Task GetAsync()
        {
            Console.WriteLine("GetAsync begin");

            UserRepAsync userRepAsync = new UserRepAsync();
            User user = null;

            long lambda, builders, buildersFun;
            int speed = 10000;

            user = await userRepAsync.GetAsync(x => x.Name == "aa");

            user = await userRepAsync.GetAsync(UserRepAsync.Filter.Eq<string>(nameof(User.Name), "aa"));
            //user = await userRep.Get(x => x.Eq<string>(nameof(User.Name), "aa"));
            Stopwatch sw = new Stopwatch();

            sw.Reset();
            sw.Start();
            for (var i = 0; i < speed; i++)
            {
                user = await userRepAsync.GetAsync(x => x.Name == "aa");
            }
            sw.Stop();
            lambda = sw.ElapsedMilliseconds;



            sw.Reset();
            sw.Start();
            for (var i = 0; i < speed; i++)
            {
                //user = await userRep.Get(x => x.Eq<string>(nameof(User.Name), "aa"));
                //user = await userRep.Get(x => x.Eq<string>(nameof(User.Name), "aa"));
            }
            sw.Stop();
            buildersFun = sw.ElapsedMilliseconds;



            sw.Reset();
            sw.Start();
            for (var i = 0; i < speed; i++)
            {
                user = await userRepAsync.GetAsync(Builders<User>.Filter.Eq<string>(nameof(User.Name), "aa"));
            }
            sw.Stop();
            builders = sw.ElapsedMilliseconds;

            Console.WriteLine("lambda:{0},builders:{1},buildersFun:{2}", lambda, builders, buildersFun);
            Console.WriteLine("GetAsync end");

            ;
        }
    }
}