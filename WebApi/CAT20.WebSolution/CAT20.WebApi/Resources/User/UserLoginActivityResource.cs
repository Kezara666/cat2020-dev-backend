using CAT20.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.User
{
    public class UserLoginActivityResource
    {
        public int? ID { get; set; }
        public string Username { get; set; }
        public int? UserId { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public int? IsSuccessLogin { get; set; }
        public int? SabhaID { get; set; }
        public int? OfficeID { get; set; }
        public string? OperatingSystem { get; set; }
        public string? Browser { get; set; }
        public string? Device { get; set; }
        public string? OSVersion { get; set; }
        public string? BrowserVersion { get; set; }
        public string? DeviceType { get; set; }
        public string? Orientation { get; set; }
        public string? IpAddress { get; set; }
        public string? RuleName { get; set; }
        public DateTime? LastActiveTime { get; set; }
    }
}
