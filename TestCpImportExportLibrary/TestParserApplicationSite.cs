using NUnit.Framework;
using CpExportImport;
using System;
using Newtonsoft.Json.Linq;

namespace TestCpImportExportLibrary
{
    public class TestParserApplicationSite
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Parse_Test()
        {
            string applicationSite1 = $@"{{
      ""uid"": ""00fa9e3c-35b8-0f65-e053-08241dc22da2"",
      ""name"": ""OCSP Protocol"",
      ""type"": ""application-site"",
      ""domain"": {{
        ""uid"": ""8bf4ac51-2df7-40e1-9bce-bedbedbedbed"",
        ""name"": ""APPI Data"",
        ""domain-type"": ""data domain""
      }},
      ""application-id"": 10075086,
      ""primary-category"": ""Network Protocols"",
      ""description"": ""The Online Certificate Status Protocol (OCSP) is an Internet protocol used for obtaining the revocation status of an X.509 digital certificate. It was created as an alternative to certificate revocation lists."",
      ""risk"": ""Very Low"",
      ""user-defined"": false,
      ""additional-categories"": [[
        ""Very Low Risk"",
        ""Encrypts communications"",
        ""Network Protocols""
      ]],
      ""comments"": """",
      ""color"": ""black"",
      ""icon"": ""@app/10075086_2"",
      ""tags"": [[]],
      ""meta-info"": {{
        ""lock"": ""unlocked"",
        ""validation-state"": ""ok"",
        ""last-modify-time"": {{
          ""posix"": 1585056368214,
          ""iso-8601"": ""2020-03-24T14:26+0100""
        }},
        ""last-modifier"": ""System"",
        ""creation-time"": {{
          ""posix"": 1619444108190,
          ""iso-8601"": ""2021-04-26T15:35+0200""
        }},
        ""creator"": ""System""
      }},
      ""read-only"": false
    }}";
            JObject o = JObject.Parse(applicationSite1);   
            Parser parser = new ParserApplicationSite();
            dynamic result = parser.parse(o, null);
            Assert.AreEqual(result, o);
        }
    }
}