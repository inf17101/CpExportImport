using CpImportExportLibrary.src.ApiOperations;
using CpImportExportLibrary.src.FileWriter;
using System;
using System.IO;

namespace CpImportExportLibrary.src.Export
{
    class PolicyPackageExporter
    {
        private ISession _session;
        private IObjectExporter _exporter;

        public PolicyPackageExporter(ISession session, IObjectExporter exporter)
        {
            _session = session;
            _exporter = exporter;
        }

        private void SafeMetaData(string path, string metadata)
        {
            JsonFileWriter writer = new JsonFileWriter();
            writer.Json2File($"{path}/metadata.json", metadata);
        }
        public void ExportPolicyPackage(string policyPackagename, string path)
        {
            string payloadPolicyPackage = $"{{\"name\" : \"{policyPackagename}\"}}";
            string policyPackagePath = path + $"PolicyPackage_{policyPackagename}";
            dynamic policyPackageMetaData = _session.ApiCall("show-package", payloadPolicyPackage);

            Directory.CreateDirectory(policyPackagePath);
            SafeMetaData(policyPackagePath, Convert.ToString(policyPackageMetaData));
            AccessControlPolicyExporter accessControlExporter = new AccessControlPolicyExporter(_session, _exporter, policyPackageMetaData, policyPackagePath);
            accessControlExporter.ExportRulebase();
        }
    }
}
