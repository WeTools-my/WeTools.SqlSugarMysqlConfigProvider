using Microsoft.Extensions.Options;
using SqlSugar;
using System;
using System.Collections.Generic;
using WeTools.SqlSugarMysqlConfigProvider.Model;
using WeTools.SqlSugarMysqlConfigProvider.Options;

namespace WeTools.SqlSugarMysqlConfigProvider
{
    public class ServerAppConfigProvider : DbContext<ServerAppConfigModel>, IServerAppConfigProvider
    {
        public ServerAppConfigProvider(IOptionsMonitor<DbOption> db, InitKeyType keyType = InitKeyType.SystemTable) : base(db,keyType) { }
        public ServerAppConfigProvider(DbOption db, InitKeyType keyType = InitKeyType.SystemTable) : base(db, keyType) { }

        public List<ServerAppConfigModel> GetConfigs()
        {
            return db.Queryable<ServerAppConfigModel>().ToList();
        }

        //public List<ServerAppConfigView> GetConfigsByAppName(string appName)
        //{
        //    var data = db.Queryable<ServerAppConfigModel, ServerAppModel>((ac, app) => ac.AppId == app.Id)
        //        .Where((ac, app) => app.Name.Contains(appName))
        //        .Select((ac, app) => new ServerAppConfigView
        //        {
        //            Id = app.Id,
        //            Name = app.Name,
        //            ServerIp = app.ServerIp,
        //            ConfigId = ac.Id,
        //            ConfigName = ac.Name,
        //            ConfigValue = ac.Value
        //        }).ToList();

        //    return data;
        //}

        List<ServerAppConfigModel> IServerAppConfigProvider.GetConfigsByAppName(string appName)
        {
            throw new NotImplementedException();
        }

        public void InitTable()
        {
            db.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(ServerAppConfigModel));
        }
    }
}
