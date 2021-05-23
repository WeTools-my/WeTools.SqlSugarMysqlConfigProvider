# WeTools.SqlSugarMysqlConfigProvider
支持多种数据库版本 https://github.com/WeTools-my/WeTools.SqlSugarDBConfigProvider

## 介绍
Name列的值遵循.NET中配置的“多层级数据的扁平化”
如下都是合法的Name列的值：
```
Api:Jwt:Audience
Age
Api:Names:0
Api:Names:1
```
Value列的值用来保存Name类对应的配置的值。
Value的值可以是普通的值，也可以使用json数组，也可以是json对象。
比如下面都是合法的Value值：
```
["a","d"]
{"Secret": "afd3","Issuer": "wetools","Ids":[3,5,8]} 
33
ccc
```

## 使用
1.在vs 中添加包
WeTools.SqlSugarMysqlConfigProvider
或者使用pm
```
 Install-Package WeTools.SqlSugarMysqlConfigProvider -Version 1.0.1
 ```
2.扩展方法
```
AddSqlSugarMysqlConfiguration(this IConfigurationBuilder builder)  使用此方法需要在配置文件新增固定节点 WeTools ，程序会自动解析子节点。
AddSqlSugarMysqlConfiguration(this IConfigurationBuilder builder, ConfigProviderOption option) 使用此方法需要自己创建Options对象，将配置传入进去。
```
3.配置节点
```
"WeTools":

{
"ConnectionString": "host=localhost;port=3306;user id=root;password=;database=sugar;",//mysql 连接字符串，自行修改
"ReloadOnChange": true, //是否重载配置 默认false ，此节点 可以不配置
"ReloadInterval": 3, //重载时间间隔，单位秒，必须大于0，如果设置0 使用默认值3秒
"InitTable": true //是否初始化表，此配置会自动生成配置表，默认true,如果表已存在，不会进行覆盖，此节点 可以不配置
}
```
# 感谢
此项目参考自杨中科老师的 Zack.AnyDBConfigProvider(https://github.com/yangzhongke/Zack.AnyDBConfigProvider) 
