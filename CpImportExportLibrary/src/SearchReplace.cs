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

        private void ReplaceUidWithUidOfObjectsDictionary(dynamic rulePart, dynamic objectsDictionary)
        {
            if (rulePart is JArray)
            {
                int amountOfRulePartItems = rulePart.Count;
                for(int i = 0; i < amountOfRulePartItems; ++i)
                {
                    foreach (dynamic objectItem in objectsDictionary)
                    {
                        if (Convert.ToString(rulePart[i]) == Convert.ToString(objectItem["uid"]))
                        {
                            rulePart[i].Replace(objectItem["name"]);
                        }
                    }
                }
            }else if(rulePart is JToken)
            {
                foreach (dynamic objectItem in objectsDictionary)
                {
                    if (Convert.ToString(rulePart) == Convert.ToString(objectItem["uid"]))
                    {
                        rulePart.Replace(objectItem["name"]);
                    }
                }
            }
        }

        public dynamic ReplaceAllUidsInAccessRulesByName(dynamic rulebase, dynamic objectsDictionary)
        {
            int amountOfRules = rulebase.Count;
            for (int i=0; i < amountOfRules; ++i)
            {
                List<string> itemsToRemove = new() { "uid", "domain", "icon", "meta-info", "read-only", "custom-fields", "rule-number", "from", "to" };
                RemoveProperties(itemsToRemove, rulebase[i]);
                if (rulebase[i]["type"] == "access-section")
                {
                    ReplaceAllUidsInAccessRulesByName(rulebase[i]["rulebase"], objectsDictionary);
                }else
                {
                    ReplaceUidWithUidOfObjectsDictionary(rulebase[i]["source"], objectsDictionary);
                    ReplaceUidWithUidOfObjectsDictionary(rulebase[i]["destination"], objectsDictionary);
                    ReplaceUidWithUidOfObjectsDictionary(rulebase[i]["service"], objectsDictionary);
                    ReplaceUidWithUidOfObjectsDictionary(rulebase[i]["vpn"], objectsDictionary);
                    ReplaceUidWithUidOfObjectsDictionary(rulebase[i]["time"], objectsDictionary);
                    ReplaceUidWithUidOfObjectsDictionary(rulebase[i]["content"], objectsDictionary);
                    ReplaceUidWithUidOfObjectsDictionary(rulebase[i]["install-on"], objectsDictionary);
                    ReplaceUidWithUidOfObjectsDictionary(rulebase[i]["track"]["type"], objectsDictionary);
                    ReplaceUidWithUidOfObjectsDictionary(rulebase[i]["action"], objectsDictionary);
                    if(rulebase[i].ContainsKey("user-check"))
                        ReplaceUidWithUidOfObjectsDictionary(rulebase[i]["user-check"]["interaction"], objectsDictionary);
                }
            }
            return rulebase;
        }
    }
}