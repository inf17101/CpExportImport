using System;
using System.Threading;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

/* mcs /reference:Newtonsoft.Json.dll *.cs -r:System.Net.Http /out:programm.exe */
namespace CpExportImport
{
    class Programm
    {
        public static bool isDataCenterObject(dynamic item)
        {
            foreach (var key in item.Properties())
            {
                if (Convert.ToString(key).Contains("data-center"))
                    return true;
            }
            return false;
        }
        static void Main(string[] args)
        {
            //base directory for exported items
            string rootDir = "../../../output/";
            // read in all predefined objects of check point
            var predefinedObjects = PredefinedObjectsReader.ReadPredefinedObjects("../../../predefined_objects/objects_R81.csv", delemiter: ';');

            var checkPointObjectMappings = new Dictionary<string, string>()
            {
                { "SBL-MG-01", "ccsa-mgmt-01" },
                { "SBL-GW-01", "ccsa-gw-01"}
            };

            // parse all data and make it ready for export
            string ip = "1.16.2.230";
            string port = "443";
            Session session = new Session(ip, port, "oklapper", "hallo123");
            session.Login();

            List<string> itemsToRemove = new List<string>() { "uid", "tags", "domain", "icon", "meta-info", "read-only", "install-on", "custom-fields", "time" };
            List<string> ignoredBuiltInTypes = new List<string>() { "RulebaseAction", "CpmiAnyObject", "Track", "Global", "CpmiClusterMember", "simple-cluster", "checkpoint-host", "simple-gateway" };
            var alreadyAddedItems = new Dictionary<string, string>();
            ObjectExporter exporter = new ObjectExporter(
                session, predefinedObjects,
                alreadyAddedItems, ignoredBuiltInTypes,
                itemsToRemove, rootDir);
            PolicyPackageExporter policyPackageExporter = new PolicyPackageExporter(session, exporter);
            policyPackageExporter.ExportPolicyPackage("Lotto-Hessen", rootDir);
            policyPackageExporter.ExportPolicyPackage("Standard", rootDir);
            session.Logout();
            

        }
    }
}
