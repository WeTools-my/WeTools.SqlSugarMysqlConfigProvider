# WeTools.SqlSugarMysqlConfigProvider

## ����
Name�е�ֵ��ѭ.NET�����õġ���㼶���ݵı�ƽ����
���¶��ǺϷ���Name�е�ֵ��
```
Api:Jwt:Audience
Age
Api:Names:0
Api:Names:1
```
Value�е�ֵ��������Name���Ӧ�����õ�ֵ��
Value��ֵ��������ͨ��ֵ��Ҳ����ʹ��json���飬Ҳ������json����
�������涼�ǺϷ���Valueֵ��
```
["a","d"]
{"Secret": "afd3","Issuer": "wetools","Ids":[3,5,8]} 
33
ccc
```

## ʹ��
1.��vs ����Ӱ�
WeTools.SqlSugarMysqlConfigProvider
����ʹ��pm
```
 Install-Package WeTools.SqlSugarMysqlConfigProvider -Version 1.0.1
 ```
2.��չ����
```
AddSqlSugarMysqlConfiguration(this IConfigurationBuilder builder)  ʹ�ô˷�����Ҫ�������ļ������̶��ڵ� WeTools ��������Զ������ӽڵ㡣
AddSqlSugarMysqlConfiguration(this IConfigurationBuilder builder, ConfigProviderOption option) ʹ�ô˷�����Ҫ�Լ�����Options���󣬽����ô����ȥ��
```
3.���ýڵ�
```
"WeTools":

{
"ConnectionString": "host=localhost;port=3306;user id=root;password=;database=sugar;",//mysql �����ַ����������޸�
"ReloadOnChange": true, //�Ƿ��������� Ĭ��false ���˽ڵ� ���Բ�����
"ReloadInterval": 3, //����ʱ��������λ�룬�������0���������0 ʹ��Ĭ��ֵ3��
"InitTable": true //�Ƿ��ʼ���������û��Զ��������ñ�Ĭ��true,������Ѵ��ڣ�������и��ǣ��˽ڵ� ���Բ�����
}
```
# ��л
����Ŀ�ο������п���ʦ�� Zack.AnyDBConfigProvider(https://github.com/yangzhongke/Zack.AnyDBConfigProvider) 