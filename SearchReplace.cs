using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CpExportImport
{
    class SearchReplace
    {
        public dynamic RemoveProperties(List<string> keys, dynamic json)
        {
            foreach(var item in keys)
            {
                json.Remove(item);
            }

            return json;
        }

        public dynamic RemovePropertiesExcept(List<string> notToRemove, dynamic json)
        {
            JObject clonedJson = (JObject) json.DeepClone();
            foreach(var item in clonedJson.Properties())
            {
                if(!notToRemove.Contains(item.Name))
                {
                    json.Remove(item.Name);
                }
            }
            return json;
        }

        public void ReplaceAllUidsInAccessRulesByName(dynamic rulebase, dynamic objectsDictionary)
        {
            foreach(dynamic item  in rulebase)
            {
                if(item["type"] == "access-section")
                {
                    ReplaceAllUidsInAccessRulesByName(item["rulebase"], objectsDictionary);
                }else
                {
                    //List<string> itemsToRemove = new List<String>() {"domain", "icon", "meta-info", "read-only", "install-on", "custom-fields", "time"};
                    //RemoveProperties(itemsToRemove, item);
                    //Console.WriteLine(item);
                }
            }
        }
    }
}