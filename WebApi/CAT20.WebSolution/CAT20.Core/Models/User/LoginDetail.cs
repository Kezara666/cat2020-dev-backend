using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.User
{
    public partial class LoginDetail
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string? OperatingSysten { get; set; }
        public string? Browser { get; set; }
        public string? Device { get; set; }
        public string? OSVersion { get; set; }
        public string? BrowserVersion { get; set; }
        public string? DeviceType { get; set; }
        public string? Orientation { get; set; }
        public string? IpAddress { get; set; }
    }
}