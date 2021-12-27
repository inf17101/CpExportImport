using System;
using CpImportExportLibrary.src.Helpers;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace TestCpImportExportLibrary.TestHelpers
{
    class TestSearchReplace
    {
        /// <summary>Class <c>TestSearchReplace</c> tests if SearchReplace does correct replace operations on json objects</summary>
        [Test]
        /// <summary>method <c>RemoveProperties_Test</c> tests if unnecessary items inside json object are removed</summary>
        public void RemoveProperties_Test()
        {
            List<string> itemsToRemove = new List<string>() { "uid", "tags", "domain", 
                                                                "icon", "meta-info", "read-only", 
                                                                    "install-on", "custom-fields", "time" };
            JObject input = new JObject();
            input.Add("uid", "xxxxxx-xxxx-xxxxxx");
            input.Add("install-on", "test-gw");
            input.Add("read-only", false);
            input.Add("item-not-to-remove", "test test");

            SearchReplace.RemoveProperties(itemsToRemove, input);
            Assert.AreEqual(1, input.Count);
            Assert.IsTrue(input.ContainsKey("item-not-to-remove"));
            Assert.AreEqual("test test", Convert.ToString(input.GetValue("item-not-to-remove")));
        }

        [Test]
        /// <summary>method <c>RemovePropertiesWithEmptyInput_Test</c> tests if empty input is handled correctly</summary>
        public void RemovePropertiesWithEmptyInput_Test()
        {
            List<string> itemsToRemove = new List<string>() { "uid", "tags", "domain",
                                                                "icon", "meta-info", "read-only",
                                                                    "install-on", "custom-fields", "time" };
            JObject input = new JObject();

            SearchReplace.RemoveProperties(itemsToRemove, input);
            Assert.AreEqual(0, input.Count);
        }

        [Test]
        /// <summary>method <c>RemovePropertiesExcept_Test</c> tests if all keys besides a list of valid keys is removed of the json object</summary>
        public void RemovePropertiesExcept_Test()
        {
            List<string> itemsNotToRemove = new List<string>() { "not-to-remove1", "not-to-remove2" };
            JObject input = new JObject();
            input.Add("uid", "xxxxxx-xxxx-xxxxxx");
            input.Add("install-on", "test-gw");
            input.Add("read-only", false);
            input.Add("not-to-remove1", "test test");
            input.Add("not-to-remove2", false);

            SearchReplace.RemovePropertiesExcept(itemsNotToRemove, input);
            Assert.AreEqual(2, input.Count);
            Assert.IsTrue(input.ContainsKey("not-to-remove1"));
            Assert.IsTrue(input.ContainsKey("not-to-remove2"));
            Assert.AreEqual("test test", Convert.ToString(input.GetValue("not-to-remove1")));
            Assert.AreEqual(false, Convert.ToBoolean(input.GetValue("not-to-remove2")));
        }
    }
}
