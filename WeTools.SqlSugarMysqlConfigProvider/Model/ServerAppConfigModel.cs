using SqlSugar;

namespace WeTools.SqlSugarMysqlConfigProvider.Model
{
    [SugarTable("we_server_app_config")]
    public class ServerAppConfigModel
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        [SugarColumn(ColumnDataType = "varchar(300)")]
        public string Name { get; set; }
        [SugarColumn(ColumnDataType = "varchar(3000)")]
        public string Value { get; set; }

        public int AppId { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string AppName { get; set; }

        public int AddTime { get; set; }
    }
}
