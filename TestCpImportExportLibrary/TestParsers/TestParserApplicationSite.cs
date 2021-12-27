using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using CpImportExportLibrary.src.Parser;

namespace TestCpImportExportLibrary.TestParsers
{
    /// <summary>Class <c>TestParserApplicationSite</c> tests the ParserApplicationSite to parse an application site object </summary>
    class TestParserApplicationSite
    {

        [Test]
        /// <summary>method <c>Parse_Test</c> test the parser of a application site object</summary>
        public void Parse_Test()
        {
            string applicationSiteInput = $@"{{
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

            string applicationSiteExpected = $@"{{
                ""name"": ""OCSP Protocol"",
                ""type"": ""application-site"",
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
                ""tags"": [[]]
            }}";
            JObject input = JObject.Parse(applicationSiteInput);
            IParser parser = new ParserApplicationSite();
            parser.Parse(input, null);
            JObject expected = JObject.Parse(applicationSiteExpected);
            Assert.AreEqual(expected.Count, input.Count);
            Assert.AreEqual(expected, input); // does not validate keys of the Json Object

            var expectedToDict = expected.ToObject<Dictionary<string, object>>();
            var expectedKeys = (from r in expectedToDict
                                let key = r.Key
                                select key).ToList();

            var resultToDict = input.ToObject<Dictionary<string, object>>();
            var resultKeys = (from r in resultToDict
                              let key = r.Key
                              select key).ToList();

            CollectionAssert.AreEqual(expectedKeys, resultKeys);
        }

        [Test]
        /// <summary>method <c>ParserFailWithEmptyInput_Test</c> test the parser of an ApplicationSiteObject if it could handle empty input</summary>
        public void ParserFailWithEmptyInput_Test()
        {
            string emptyInput = $@"{{}}";
            JObject input = JObject.Parse(emptyInput);
            IParser parser = new ParserApplicationSite();
            parser.Parse(input, null);
            JObject expected = JObject.Parse(emptyInput);
            Assert.AreEqual(expected.Count, input.Count);
            Assert.AreEqual(expected, input);
        }
    }
}