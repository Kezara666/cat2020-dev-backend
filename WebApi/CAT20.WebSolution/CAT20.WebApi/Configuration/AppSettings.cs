namespace CAT20.WebApi.Configuration
{
    public class AppSettings
    {
        public string? UploadsFolder { get; set; }
        public string? BackupFolder { get; set; }
        public string? FontFolder { get; set; }
        public bool? fontResolverSet { get; set; } = false;
    }
}
