﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rafy;
using Rafy.ComponentModel;
using Rafy.DbMigration;
using Rafy.Domain;
using Rafy.Domain.ORM.DbMigration;
using Rafy.MetaModel;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            //启动领域项目
            new ConsoleHostApp().Startup();

            //不能在 Main 方法中直接使用领域相关的类型，而是需要封装额外的方法（MainDomainProcess）。
            //这会导致 User 类型的静态构造函数在 ConsoleHostApp.Startup() 方法之前运行，
            //从而引发 System.Reflection.TargetInvocationException 异常。
            //var repo = RF.ResolveInstance<UserRepository>();

            MainDomainProcess();

            Console.WriteLine("执行完毕，回车退出。");
            Console.ReadLine();
        }

        private static void MainDomainProcess()
        {
            ////本项目中使用了一个 RBAC 的插件，
            ////同时在应用层引用 RBAC.dll 即可使用。
            ////（注意，只是简单引用，不拷贝到根目录，还放在插件目录。即引用的 CopyLocal = False。）
            //var users = RF.ResolveInstance<UserRepository>().GetAll();
        }
    }

    class ConsoleHostApp : DomainApp
    {
        protected override void InitEnvironment()
        {
            RafyEnvironment.Provider.IsDebuggingEnabled = ConfigurationHelper.GetAppSettingOrDefault("IsDebuggingEnabled", false);

            base.InitEnvironment();
        }

        //protected override void OnRuntimeStarting()
        //{
        //    base.OnRuntimeStarting();

        //    if (ConfigurationHelper.GetAppSettingOrDefault("ConsoleHost_AutoUpdateDb", true))
        //    {
        //        var svc = ServiceFactory.Create<MigrateService>();
        //        svc.Options = new MigratingOptions
        //        {
        //            //ReserveHistory = true,//ReserveHistory 表示是否需要保存所有数据库升级的历史记录
        //            RunDataLossOperation = DataLossOperation.All,//要支持数据库表、字段的删除操作，取消本行注释。
        //            Databases = new string[] { "FundMng" }
        //        };
        //        svc.Invoke();
        //    }
        //}
    }
}