using Microsoft.Extensions.Configuration;
using WeTools.SqlSugarMysqlConfigProvider.Options;

namespace WeTools.SqlSugarMysqlConfigProvider
{
    public static class MysqlConfigExtensions
    {
        /// <summary>
        /// 注册mysql数据配置源
        /// 需要appsettings配置WeTools节点
        /// 包含ConnectionString（string类型）、ReloadOnChange（bool类型）、ReloadInterval(int类型)、InitTable（bool类型）节点
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddSqlSugarMysqlConfiguration(this IConfigurationBuilder builder)
        {
            var configRoot = builder.Build();
            var option = new ConfigProviderOption();
            configRoot.Bind("WeTools", option);
            return builder.Add(new SqlSugarMysqlConfigurationSource(option));
        }

        /// <summary>
        /// 注册mysql数据配置源
        /// 需要appsettings配置节点
        /// 包含ConnectionString（string类型）、ReloadOnChange（bool类型）、ReloadInterval(int类型)、InitTable（bool类型）节点
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddSqlSugarMysqlConfiguration(this IConfigurationBuilder builder, ConfigProviderOption option)
        {
            return builder.Add(new SqlSugarMysqlConfigurationSource(option));
        }

    }
}
