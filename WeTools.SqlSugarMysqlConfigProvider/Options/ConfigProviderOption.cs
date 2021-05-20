namespace WeTools.SqlSugarMysqlConfigProvider.Options
{
    /// <summary>
    /// 配置提供者选项
    /// </summary>
    public class ConfigProviderOption
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 后台配置的应用名称，根据此项查询配置
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 是否重载
        /// </summary>
        public bool ReloadOnChange { get; set; } = false;
        /// <summary>
        /// 重载间隔
        /// </summary>
        public int ReloadInterval { get; set; }

        /// <summary>
        /// 配置源 0 json 文件，1 mysql数据库
        /// </summary>
        public int ConfigSource { get; set; } = 1;

        /// <summary>
        /// 初始化表
        /// </summary>
        public bool InitTable { get; set; } = true;
    }
}
