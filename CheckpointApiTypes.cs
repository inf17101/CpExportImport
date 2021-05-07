// AccessRuleBase myDeserializedClass = JsonConvert.DeserializeObject<AccessRuleBase>(myJsonResponse);
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
    public class Domain
    {
        public string uid { get; set; }
        public string name { get; set; }

        [JsonProperty("domain-type")]
        public string DomainType { get; set; }
    }

    public class Track
    {
        public string type { get; set; }

        [JsonProperty("per-session")]
        public bool PerSession { get; set; }

        [JsonProperty("per-connection")]
        public bool PerConnection { get; set; }
        public bool accounting { get; set; }

        [JsonProperty("enable-firewall-session")]
        public bool EnableFirewallSession { get; set; }
        public string alert { get; set; }
    }

    public class ActionSettings
    {
        [JsonProperty("enable-identity-captive-portal")]
        public bool EnableIdentityCaptivePortal { get; set; }
    }

    public class CustomFields
    {
        [JsonProperty("field-1")]
        public string Field1 { get; set; }

        [JsonProperty("field-2")]
        public string Field2 { get; set; }

        [JsonProperty("field-3")]
        public string Field3 { get; set; }
    }

    public class LastModifyTime
    {
        public object posix { get; set; }

        [JsonProperty("iso-8601")]
        public string Iso8601 { get; set; }
    }

    public class CreationTime
    {
        public object posix { get; set; }

        [JsonProperty("iso-8601")]
        public string Iso8601 { get; set; }
    }

    public class MetaInfo
    {
        public string @lock { get; set; }

        [JsonProperty("validation-state")]
        public string ValidationState { get; set; }

        [JsonProperty("last-modify-time")]
        public LastModifyTime LastModifyTime { get; set; }

        [JsonProperty("last-modifier")]
        public string LastModifier { get; set; }

        [JsonProperty("creation-time")]
        public CreationTime CreationTime { get; set; }
        public string creator { get; set; }
    }

    public class Rulebase
    {
        public string uid { get; set; }
        public string type { get; set; }
        public Domain domain { get; set; }

        [JsonProperty("rule-number")]
        public int RuleNumber { get; set; }
        public Track track { get; set; }
        public List<string> source { get; set; }

        [JsonProperty("source-negate")]
        public bool SourceNegate { get; set; }
        public List<string> destination { get; set; }

        [JsonProperty("destination-negate")]
        public bool DestinationNegate { get; set; }
        public List<string> service { get; set; }

        [JsonProperty("service-negate")]
        public bool ServiceNegate { get; set; }
        public List<string> vpn { get; set; }
        public string action { get; set; }

        [JsonProperty("action-settings")]
        public ActionSettings ActionSettings { get; set; }
        public List<string> content { get; set; }

        [JsonProperty("content-negate")]
        public bool ContentNegate { get; set; }

        [JsonProperty("content-direction")]
        public string ContentDirection { get; set; }
        public List<string> time { get; set; }

        [JsonProperty("custom-fields")]
        public CustomFields CustomFields { get; set; }

        [JsonProperty("meta-info")]
        public MetaInfo MetaInfo { get; set; }
        public string comments { get; set; }
        public bool enabled { get; set; }

        [JsonProperty("install-on")]
        public List<string> InstallOn { get; set; }
        public string name { get; set; }
    }

    public class TopologySettings
    {
        [JsonProperty("ip-address-behind-this-interface")]
        public string IpAddressBehindThisInterface { get; set; }

        [JsonProperty("interface-leads-to-dmz")]
        public bool InterfaceLeadsToDmz { get; set; }
    }

    public class SecurityZoneSettings
    {
        [JsonProperty("auto-calculated")]
        public bool AutoCalculated { get; set; }

        [JsonProperty("specific-zone")]
        public string SpecificZone { get; set; }
    }

    public class Interface
    {
        public string name { get; set; }

        [JsonProperty("ipv4-address")]
        public string Ipv4Address { get; set; }

        [JsonProperty("ipv4-network-mask")]
        public string Ipv4NetworkMask { get; set; }

        [JsonProperty("ipv4-mask-length")]
        public int Ipv4MaskLength { get; set; }

        [JsonProperty("ipv6-address")]
        public string Ipv6Address { get; set; }
        public string comments { get; set; }
        public string color { get; set; }
        public string icon { get; set; }
        public string topology { get; set; }

        [JsonProperty("topology-settings")]
        public TopologySettings TopologySettings { get; set; }

        [JsonProperty("anti-spoofing")]
        public bool AntiSpoofing { get; set; }

        [JsonProperty("security-zone")]
        public bool SecurityZone { get; set; }

        [JsonProperty("security-zone-settings")]
        public SecurityZoneSettings SecurityZoneSettings { get; set; }
    }

    public class FirewallSettings
    {
        [JsonProperty("auto-maximum-limit-for-concurrent-connections")]
        public bool AutoMaximumLimitForConcurrentConnections { get; set; }

        [JsonProperty("maximum-limit-for-concurrent-connections")]
        public int MaximumLimitForConcurrentConnections { get; set; }

        [JsonProperty("auto-calculate-connections-hash-table-size-and-memory-pool")]
        public bool AutoCalculateConnectionsHashTableSizeAndMemoryPool { get; set; }

        [JsonProperty("connections-hash-size")]
        public int ConnectionsHashSize { get; set; }

        [JsonProperty("memory-pool-size")]
        public int MemoryPoolSize { get; set; }

        [JsonProperty("maximum-memory-pool-size")]
        public int MaximumMemoryPoolSize { get; set; }
    }

    public class VpnSettings
    {
        [JsonProperty("maximum-concurrent-ike-negotiations")]
        public int MaximumConcurrentIkeNegotiations { get; set; }

        [JsonProperty("maximum-concurrent-tunnels")]
        public int MaximumConcurrentTunnels { get; set; }
    }

    public class LogsSettings
    {
        [JsonProperty("rotate-log-by-file-size")]
        public bool RotateLogByFileSize { get; set; }

        [JsonProperty("rotate-log-file-size-threshold")]
        public int RotateLogFileSizeThreshold { get; set; }

        [JsonProperty("rotate-log-on-schedule")]
        public bool RotateLogOnSchedule { get; set; }

        [JsonProperty("alert-when-free-disk-space-below-metrics")]
        public string AlertWhenFreeDiskSpaceBelowMetrics { get; set; }

        [JsonProperty("alert-when-free-disk-space-below")]
        public bool AlertWhenFreeDiskSpaceBelow { get; set; }

        [JsonProperty("alert-when-free-disk-space-below-threshold")]
        public int AlertWhenFreeDiskSpaceBelowThreshold { get; set; }

        [JsonProperty("alert-when-free-disk-space-below-type")]
        public string AlertWhenFreeDiskSpaceBelowType { get; set; }

        [JsonProperty("delete-when-free-disk-space-below-metrics")]
        public string DeleteWhenFreeDiskSpaceBelowMetrics { get; set; }

        [JsonProperty("delete-when-free-disk-space-below")]
        public bool DeleteWhenFreeDiskSpaceBelow { get; set; }

        [JsonProperty("delete-when-free-disk-space-below-threshold")]
        public int DeleteWhenFreeDiskSpaceBelowThreshold { get; set; }

        [JsonProperty("before-delete-keep-logs-from-the-last-days")]
        public bool BeforeDeleteKeepLogsFromTheLastDays { get; set; }

        [JsonProperty("before-delete-keep-logs-from-the-last-days-threshold")]
        public int BeforeDeleteKeepLogsFromTheLastDaysThreshold { get; set; }

        [JsonProperty("before-delete-run-script")]
        public bool BeforeDeleteRunScript { get; set; }

        [JsonProperty("before-delete-run-script-command")]
        public string BeforeDeleteRunScriptCommand { get; set; }

        [JsonProperty("stop-logging-when-free-disk-space-below-metrics")]
        public string StopLoggingWhenFreeDiskSpaceBelowMetrics { get; set; }

        [JsonProperty("stop-logging-when-free-disk-space-below")]
        public bool StopLoggingWhenFreeDiskSpaceBelow { get; set; }

        [JsonProperty("stop-logging-when-free-disk-space-below-threshold")]
        public int StopLoggingWhenFreeDiskSpaceBelowThreshold { get; set; }

        [JsonProperty("reject-connections-when-free-disk-space-below-threshold")]
        public bool RejectConnectionsWhenFreeDiskSpaceBelowThreshold { get; set; }

        [JsonProperty("reserve-for-packet-capture-metrics")]
        public string ReserveForPacketCaptureMetrics { get; set; }

        [JsonProperty("reserve-for-packet-capture-threshold")]
        public int ReserveForPacketCaptureThreshold { get; set; }

        [JsonProperty("delete-index-files-when-index-size-above-metrics")]
        public string DeleteIndexFilesWhenIndexSizeAboveMetrics { get; set; }

        [JsonProperty("delete-index-files-when-index-size-above")]
        public bool DeleteIndexFilesWhenIndexSizeAbove { get; set; }

        [JsonProperty("delete-index-files-when-index-size-above-threshold")]
        public int DeleteIndexFilesWhenIndexSizeAboveThreshold { get; set; }

        [JsonProperty("delete-index-files-older-than-days")]
        public bool DeleteIndexFilesOlderThanDays { get; set; }

        [JsonProperty("delete-index-files-older-than-days-threshold")]
        public int DeleteIndexFilesOlderThanDaysThreshold { get; set; }

        [JsonProperty("forward-logs-to-log-server")]
        public bool ForwardLogsToLogServer { get; set; }

        [JsonProperty("perform-log-rotate-before-log-forwarding")]
        public bool PerformLogRotateBeforeLogForwarding { get; set; }

        [JsonProperty("update-account-log-every")]
        public int UpdateAccountLogEvery { get; set; }

        [JsonProperty("detect-new-citrix-ica-application-names")]
        public bool DetectNewCitrixIcaApplicationNames { get; set; }

        [JsonProperty("turn-on-qos-logging")]
        public bool TurnOnQosLogging { get; set; }
    }

    public class NatSettings
    {
        [JsonProperty("auto-rule")]
        public bool AutoRule { get; set; }
    }

    public class CenterGateway
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public Domain domain { get; set; }
        public string color { get; set; }
        public string natSummaryText { get; set; }

        [JsonProperty("meta-info")]
        public MetaInfo MetaInfo { get; set; }
        public List<object> tags { get; set; }
        public string icon { get; set; }
        public string comments { get; set; }

        [JsonProperty("display-name")]
        public string DisplayName { get; set; }
        public object customFields { get; set; }

        [JsonProperty("ipv4-address")]
        public string Ipv4Address { get; set; }

        [JsonProperty("ipv6-address")]
        public string Ipv6Address { get; set; }
    }

    public class SatelliteGateway
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public Domain domain { get; set; }
        public List<Interface> interfaces { get; set; }

        [JsonProperty("ipv4-address")]
        public string Ipv4Address { get; set; }

        [JsonProperty("dynamic-ip")]
        public bool DynamicIp { get; set; }
        public string version { get; set; }

        [JsonProperty("os-name")]
        public string OsName { get; set; }
        public string hardware { get; set; }

        [JsonProperty("sic-name")]
        public string SicName { get; set; }

        [JsonProperty("sic-state")]
        public string SicState { get; set; }
        public bool firewall { get; set; }

        [JsonProperty("firewall-settings")]
        public FirewallSettings FirewallSettings { get; set; }
        public bool vpn { get; set; }

        [JsonProperty("vpn-settings")]
        public VpnSettings VpnSettings { get; set; }

        [JsonProperty("application-control")]
        public bool ApplicationControl { get; set; }

        [JsonProperty("url-filtering")]
        public bool UrlFiltering { get; set; }
        public bool ips { get; set; }

        [JsonProperty("content-awareness")]
        public bool ContentAwareness { get; set; }

        [JsonProperty("anti-bot")]
        public bool AntiBot { get; set; }

        [JsonProperty("anti-virus")]
        public bool AntiVirus { get; set; }

        [JsonProperty("threat-emulation")]
        public bool ThreatEmulation { get; set; }

        [JsonProperty("threat-extraction")]
        public bool ThreatExtraction { get; set; }

        [JsonProperty("save-logs-locally")]
        public bool SaveLogsLocally { get; set; }

        [JsonProperty("send-alerts-to-server")]
        public List<string> SendAlertsToServer { get; set; }

        [JsonProperty("send-logs-to-server")]
        public List<string> SendLogsToServer { get; set; }

        [JsonProperty("send-logs-to-backup-server")]
        public List<object> SendLogsToBackupServer { get; set; }

        [JsonProperty("logs-settings")]
        public LogsSettings LogsSettings { get; set; }
        public string comments { get; set; }
        public string color { get; set; }
        public string icon { get; set; }
        public List<object> tags { get; set; }

        [JsonProperty("meta-info")]
        public MetaInfo MetaInfo { get; set; }

        [JsonProperty("read-only")]
        public bool ReadOnly { get; set; }
    }

    public class ExternalGateway
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public Domain domain { get; set; }
        public string color { get; set; }
        public string natSummaryText { get; set; }

        [JsonProperty("meta-info")]
        public MetaInfo MetaInfo { get; set; }
        public List<object> tags { get; set; }
        public string icon { get; set; }
        public string comments { get; set; }

        [JsonProperty("display-name")]
        public string DisplayName { get; set; }
        public object customFields { get; set; }

        [JsonProperty("ipv4-address")]
        public string Ipv4Address { get; set; }

        [JsonProperty("ipv6-address")]
        public string Ipv6Address { get; set; }
    }

    public class SharedSecret
    {
        [JsonProperty("external-gateway")]
        public ExternalGateway ExternalGateway { get; set; }
    }

    public class IkePhase1
    {
        [JsonProperty("encryption-algorithm")]
        public string EncryptionAlgorithm { get; set; }

        [JsonProperty("diffie-hellman-group")]
        public string DiffieHellmanGroup { get; set; }

        [JsonProperty("data-integrity")]
        public string DataIntegrity { get; set; }
    }

    public class IkePhase2
    {
        [JsonProperty("encryption-algorithm")]
        public string EncryptionAlgorithm { get; set; }

        [JsonProperty("data-integrity")]
        public string DataIntegrity { get; set; }
    }

    public class ObjectsDictionary
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public Domain domain { get; set; }
        public string color { get; set; }

        [JsonProperty("meta-info")]
        public MetaInfo MetaInfo { get; set; }
        public List<object> tags { get; set; }
        public string icon { get; set; }
        public string comments { get; set; }

        [JsonProperty("display-name")]
        public string DisplayName { get; set; }
        public object customFields { get; set; }
        public List<Interface> interfaces { get; set; }

        [JsonProperty("ipv4-address")]
        public string Ipv4Address { get; set; }

        [JsonProperty("dynamic-ip")]
        public bool? DynamicIp { get; set; }
        public string version { get; set; }

        [JsonProperty("os-name")]
        public string OsName { get; set; }
        public string hardware { get; set; }

        [JsonProperty("sic-name")]
        public string SicName { get; set; }

        [JsonProperty("sic-state")]
        public string SicState { get; set; }
        public bool? firewall { get; set; }

        [JsonProperty("firewall-settings")]
        public FirewallSettings FirewallSettings { get; set; }
        public bool? vpn { get; set; }

        [JsonProperty("vpn-settings")]
        public VpnSettings VpnSettings { get; set; }

        [JsonProperty("application-control")]
        public bool? ApplicationControl { get; set; }

        [JsonProperty("url-filtering")]
        public bool? UrlFiltering { get; set; }
        public bool? ips { get; set; }

        [JsonProperty("content-awareness")]
        public bool? ContentAwareness { get; set; }

        [JsonProperty("anti-bot")]
        public bool? AntiBot { get; set; }

        [JsonProperty("anti-virus")]
        public bool? AntiVirus { get; set; }

        [JsonProperty("threat-emulation")]
        public bool? ThreatEmulation { get; set; }

        [JsonProperty("threat-extraction")]
        public bool? ThreatExtraction { get; set; }

        [JsonProperty("save-logs-locally")]
        public bool? SaveLogsLocally { get; set; }

        [JsonProperty("send-alerts-to-server")]
        public List<string> SendAlertsToServer { get; set; }

        [JsonProperty("send-logs-to-server")]
        public List<string> SendLogsToServer { get; set; }

        [JsonProperty("send-logs-to-backup-server")]
        public List<object> SendLogsToBackupServer { get; set; }

        [JsonProperty("logs-settings")]
        public LogsSettings LogsSettings { get; set; }

        [JsonProperty("read-only")]
        public bool? ReadOnly { get; set; }

        [JsonProperty("nat-settings")]
        public NatSettings NatSettings { get; set; }

        [JsonProperty("center-gateways")]
        public List<CenterGateway> CenterGateways { get; set; }

        [JsonProperty("satellite-gateways")]
        public List<SatelliteGateway> SatelliteGateways { get; set; }

        [JsonProperty("mesh-center-gateways")]
        public bool? MeshCenterGateways { get; set; }

        [JsonProperty("use-shared-secret")]
        public bool? UseSharedSecret { get; set; }

        [JsonProperty("shared-secrets")]
        public List<SharedSecret> SharedSecrets { get; set; }

        [JsonProperty("encryption-method")]
        public string EncryptionMethod { get; set; }

        [JsonProperty("encryption-suite")]
        public string EncryptionSuite { get; set; }

        [JsonProperty("ike-phase-1")]
        public IkePhase1 IkePhase1 { get; set; }

        [JsonProperty("ike-phase-2")]
        public IkePhase2 IkePhase2 { get; set; }
    }

    public class AccessRuleBase
    {
        public string uid { get; set; }
        public string name { get; set; }
        public List<Rulebase> rulebase { get; set; }

        [JsonProperty("objects-dictionary")]
        public List<ObjectsDictionary> ObjectsDictionary { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public int total { get; set; }
    }
