using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Common.Enums;

namespace CAT20.Common
{
    public class StatusInfo
    {
        #region Constructors

        public StatusInfo() : this(ServiceStatus.NotSet) { }
        public StatusInfo(ServiceStatus status) { this.Status = status; }

        #endregion

        #region Members

        public ServiceStatus Status { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        /// 
        public string Message { get; set; }
        /// <summary>
        /// CodeBase
        /// </summary>
        /// 
        public string CodeBase { get; set; }
        /// <summary>
        /// StackTrace
        /// </summary>
        /// 
        public string StackTrace { get; set; }

        #endregion
    }
}
