﻿using Microsoft.Extensions.Options;
using SqlSugar;
using System.Collections.Generic;
using WeTools.SqlSugarMysqlConfigProvider.Options;

namespace WeTools.SqlSugarMysqlConfigProvider
{
    public class DbContext<T> where T : class, new()
    {
        public DbContext(DbOption option, InitKeyType keyType=InitKeyType.SystemTable)
        {

            db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = option.ConnectionString,
                DbType = DbType.MySql,
                InitKeyType = keyType,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
                //IsShardSameThread = true//设为true相同线程是同一个SqlConnection
            });
            //调式代码 用来打印SQL 
            //db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    Console.WriteLine(sql + "\r\n" +
            //        db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            //    Console.WriteLine();
            //};

        }

        public DbContext(IOptionsMonitor<DbOption> option, InitKeyType keyType = InitKeyType.SystemTable)
        {

            db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = option.CurrentValue.ConnectionString,
                DbType = DbType.MySql,
                InitKeyType = keyType,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
                //IsShardSameThread = true//设为true相同线程是同一个SqlConnection
            });

            //先不使用，稍后修改
            //option.OnChange(op =>
            //{
            //    Console.WriteLine("DbContext变了");
            //    db.Close();
            //    db = null;

            //    db = new SqlSugarClient(new ConnectionConfig()
            //    {
            //        ConnectionString = op.ConnectionString,
            //        DbType = DbType.MySql,
            //        InitKeyType = InitKeyType.SystemTable,//从特性读取主键和自增列信息
            //        IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            //    });
            //});
            //调式代码 用来打印SQL 
            //db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    Console.WriteLine(sql + "\r\n" +
            //        db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            //    Console.WriteLine();
            //};

        }
        //注意：不能写成静态的
        public SqlSugarClient db;//用来处理事务多表查询和复杂的操作
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(db); } }//用来处理T表的常用操作

        public string GetConstr()
        {
            return db.CurrentConnectionConfig.ConnectionString;
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(dynamic id)
        {
            return CurrentDb.Delete(id);
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(T obj)
        {
            return CurrentDb.Update(obj);
        }

    }
}
