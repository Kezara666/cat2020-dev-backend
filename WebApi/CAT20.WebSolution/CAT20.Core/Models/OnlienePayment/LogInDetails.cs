using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.OnlienePayment;

public class LogInDetails
{
    
    public int? logInID { get; set; }
    public string? NIC { get; set; }
    public string? MobileNo { get; set; }
    
    public string? IpAddress { get; set; }
    public string? Device { get; set; }
    public string? OperatingSystem { get; set; }
    public string? OsVersion { get; set; }
    public string? Browser { get; set; }
    public string? BrowserVersion { get; set; }
    public string? DeviceType { get; set; }
    public string? Orientation { get; set; }
    public DateTime? Time { get; set; }
}