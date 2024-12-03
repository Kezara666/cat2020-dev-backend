using CAT20.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.User
{
    public class LoginResource
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string? OperatingSystem { get; set; }
        public string? Browser { get; set; }
        public string? Device { get; set; }
        public string? OSVersion { get; set; }
        public string? BrowserVersion { get; set; }
        public string? DeviceType { get; set; }
        public string? Orientation { get; set; }
        public string? IpAddress { get; set; }

    }
}
