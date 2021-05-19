using System.ComponentModel.DataAnnotations;

namespace WeTools.SqlSugarMysqlConfigProvider.Options
{
    public class DbOption
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "缺少数据库连接字符串，请检查ConnectionString节点")]
        public string ConnectionString { get; set; }
    }
}
