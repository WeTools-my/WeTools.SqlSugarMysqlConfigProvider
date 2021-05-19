using System.Collections.Generic;
using System.Text.Json;

namespace WeTools.SqlSugarMysqlConfigProvider
{
    static class ConfigProviderExtensions
    {
        public static IDictionary<string, string> Clone(this IDictionary<string, string> dict)
        {
            IDictionary<string, string> newDict = new Dictionary<string, string>();
            foreach (var kv in dict)
            {
                newDict[kv.Key] = kv.Value;
            }
            return newDict;
        }

        public static bool IsChanged(IDictionary<string, string> oldDict,
                                     IDictionary<string, string> newDict)
        {
            if (oldDict.Count != newDict.Count)
            {
                return true;
            }

            foreach (var oldKV in oldDict)
            {
                var oldKey = oldKV.Key;
                var oldValue = oldKV.Value;
                var newValue = newDict[oldKey];
                if (oldValue != newValue)
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetValueForConfig(this JsonElement e)
        {
            if (e.ValueKind == JsonValueKind.String)
            {
                //remove the quotes, "ab"-->ab
                return e.GetString();
            }
            else if (e.ValueKind == JsonValueKind.Null
                     || e.ValueKind == JsonValueKind.Undefined)
            {
                //remove the quotes, "null"-->null
                return null;
            }
            else
            {
                return e.GetRawText();
            }
        }
    }
}
