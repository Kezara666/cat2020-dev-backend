using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Common.Enums
{
    public enum ServiceStatus
    {
        Success = 0,
        AuthenticationFailed = 1,
        DatabaseFailure = 2,
        DataNotAvailable = 3,
        InputParameterOutOfRange = 4,
        DataDuplicateError = 5,
        SessionExpired = 6,
        ConcurrencyError = 7,
        DataValidationError = 8,
        Unknown = 9,
        ArgumentNullException = 10,
        NotImplemented = 11,
        NotSet = 12,
        LicenceError = 13,
        Error = 14
    }
}
