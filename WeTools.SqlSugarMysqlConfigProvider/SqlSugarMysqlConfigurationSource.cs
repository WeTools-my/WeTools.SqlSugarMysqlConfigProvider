using Microsoft.Extensions.Configuration;
using WeTools.SqlSugarMysqlConfigProvider.Options;

namespace WeTools.SqlSugarMysqlConfigProvider
{
    class SqlSugarMysqlConfigurationSource : IConfigurationSource
    {
        ConfigProviderOption _option;
        public SqlSugarMysqlConfigurationSource(ConfigProviderOption option)
        {
            _option = option;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new SqlSugarMysqlConfigurationProvider(_option);
        }
    }
}
