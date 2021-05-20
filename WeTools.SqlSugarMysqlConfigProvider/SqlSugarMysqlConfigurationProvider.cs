using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using WeTools.SqlSugarMysqlConfigProvider.Model;
using WeTools.SqlSugarMysqlConfigProvider.Options;

namespace WeTools.SqlSugarMysqlConfigProvider
{
    public class SqlSugarMysqlConfigurationProvider : ConfigurationProvider, IDisposable
    {
        private readonly ReaderWriterLockSlim _lockObj = new ReaderWriterLockSlim();
        private bool _isDisposed = false;

        private readonly ConfigProviderOption _option;
        private readonly IServerAppConfigProvider _appConfig;

        public SqlSugarMysqlConfigurationProvider(ConfigProviderOption option)
        {
            _option = option;

            _appConfig = new ServerAppConfigProvider(new DbOption { ConnectionString = option.ConnectionString },_option.InitTable?SqlSugar.InitKeyType.Attribute:SqlSugar.InitKeyType.SystemTable);


            if (_option.InitTable)
            {
                _appConfig.InitTable();
            }

            TimeSpan interval = _option.ReloadInterval == 0 ? TimeSpan.FromSeconds(3) : TimeSpan.FromSeconds(_option.ReloadInterval);

            if (_option.ReloadOnChange)
            {
                _ = ThreadPool.QueueUserWorkItem(obj =>
                {
                    while (!_isDisposed)
                    {
                        Thread.Sleep(interval);

                        Load();
                    }
                });
            }
        }

        public override IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
        {
            _lockObj.EnterReadLock();
            try
            {
                return base.GetChildKeys(earlierKeys, parentPath);
            }
            finally
            {
                _lockObj.ExitReadLock();
            }
        }

        public override bool TryGet(string key, out string value)
        {
            _lockObj.EnterReadLock();
            try
            {
                return base.TryGet(key, out value);
            }
            finally
            {
                _lockObj.ExitReadLock();
            }
        }

        public override void Load()
        {
            base.Load();
            var clonedData = Data.Clone();

            try
            {
                _lockObj.EnterWriteLock();
                Data.Clear();

                var data = string.IsNullOrWhiteSpace(_option.AppName)? _appConfig.GetConfigs(): _appConfig.GetConfigsByAppName(_option.AppName);

                if (data is null || data.Count <= 0)
                {
                    Console.WriteLine("配置提供程序未获取到配置数据");
                    return;
                }

                data.ForEach(c => {
                    var name = c.Name;
                    var value = c.Value;
                    Console.WriteLine("name:"+ name);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        Data[name] = value;
                    }
                    else
                    {
                        var configValue = value.Trim();

                        //json格式
                        if (configValue.StartsWith("[") && configValue.EndsWith("]") || configValue.StartsWith("{") && configValue.EndsWith("}"))
                        {
                            TryLoadAsJson(name, value);
                        }
                        else
                        {
                            Data[name] = value;
                        }
                    }

                });

            }
            catch (Exception ex)
            {
                Console.WriteLine("配置提供程序加载数据时出错，{msg}-{stack}", ex.Message, ex.StackTrace);
                //if DbException is thrown, restore to the original data.
                Data = clonedData;
            }
            finally
            {
                _lockObj.ExitWriteLock();
            }

            //OnReload cannot be between EnterWriteLock and ExitWriteLock, or "A read lock may not be acquired with the write lock held in this mode" will be thrown.
            if (ConfigProviderExtensions.IsChanged(clonedData, Data))
            {
                OnReload();
            }
        }

        private void TryLoadAsJson(string name, string value)
        {
            var jsonOptions = new JsonDocumentOptions { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip };
            try
            {
                var jsonRoot = JsonDocument.Parse(value, jsonOptions).RootElement;
                LoadJsonElement(name, jsonRoot);
            }
            catch (JsonException ex)
            {
                //if it is not valid json, parse it as plain string value
                Data[name] = value;
                Debug.WriteLine($"When trying to parse {value} as json object, exception was thrown. {ex}");
            }
        }

        private void LoadJsonElement(string name, JsonElement jsonRoot)
        {
            if (jsonRoot.ValueKind == JsonValueKind.Array)
            {
                int index = 0;
                foreach (var item in jsonRoot.EnumerateArray())
                {
                    //https://andrewlock.net/creating-a-custom-iconfigurationprovider-in-asp-net-core-to-parse-yaml/
                    //parse as "a:b:0"="hello";"a:b:1"="world"
                    string path = name + ConfigurationPath.KeyDelimiter + index;
                    LoadJsonElement(path, item);
                    index++;
                }
            }
            else if (jsonRoot.ValueKind == JsonValueKind.Object)
            {
                foreach (var jsonObj in jsonRoot.EnumerateObject())
                {
                    string pathOfObj = name + ConfigurationPath.KeyDelimiter + jsonObj.Name;
                    LoadJsonElement(pathOfObj, jsonObj.Value);
                }
            }
            else
            {
                //if it is not json array or object, parse it as plain string value
                Data[name] = jsonRoot.GetValueForConfig();
            }
        }

        public void Dispose() => _isDisposed = true;
    }
}
