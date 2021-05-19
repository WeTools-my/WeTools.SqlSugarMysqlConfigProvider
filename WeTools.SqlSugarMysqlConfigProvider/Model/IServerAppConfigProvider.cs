using System.Collections.Generic;

namespace WeTools.SqlSugarMysqlConfigProvider.Model
{
    /// <summary>
    /// 后端服务配置
    /// </summary>
    public interface IServerAppConfigProvider
    {
        /// <summary>
        /// 获取应用的所有配置
        /// </summary>
        /// <param name="appName">应用名称</param>
        /// <returns></returns>
        List<ServerAppConfigModel> GetConfigsByAppName(string appName);

        /// <summary>
        /// 获取应用的所有配置
        /// </summary>
        /// <returns></returns>
        List<ServerAppConfigModel> GetConfigs();
    }
}
