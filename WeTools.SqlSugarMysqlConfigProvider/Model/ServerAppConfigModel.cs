using SqlSugar;

namespace WeTools.SqlSugarMysqlConfigProvider.Model
{
    [SugarTable("we_server_app_config")]
    public class ServerAppConfigModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public int AppId { get; set; }

        public string AppName { get; set; }

        public int AddTime { get; set; }
    }
}
