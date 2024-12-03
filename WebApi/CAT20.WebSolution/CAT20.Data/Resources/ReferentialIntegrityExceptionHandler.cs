using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.AuditTrails;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Interfaces;
using KellermanSoftware.CompareNetObjects;
using CAT20.Core.Models.Common;
using CAT20.Common.Enums;
using CAT20.Common.Envelop;
using System.Reflection;
using System.Text.RegularExpressions;
using CAT20.Data.Resources;

namespace CAT20.Data.ExceptionHandler
{
    /// <summary>
    /// Handlers ReferentialIntegrity Exception when try to delete one entity
    /// </summary>
    public class ReferentialIntegrityExceptionHandler
    {
        #region Property

        private string _message;
        private TransferObject<bool> TransObject;
        public string ReferentialIntegrity { get; set; }
        public Exception Exception { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex">ReferentialIntegrity Exception</param>
        /// <param name="entityName">Deleteting Entity Name</param>
        public ReferentialIntegrityExceptionHandler(Exception ex)
        {
            this.Exception = ex;
            this.TransObject = new TransferObject<bool>();
        }

        /// <summary>
        /// Generates Data Transfer Object
        /// </summary>
        /// <returns></returns>
        public TransferObject<bool> GetTransferObject()
        {
            var exceptionName = this.Exception.GetType().Name;
            switch (exceptionName)
            {
                case "DbUpdateConcurrencyException":
                    TransObject.StatusInfo.Message = Resources.MessageDictionary.CONCURRECNCY_UPDATE_ERROR;
                    TransObject.StatusInfo.Status = ServiceStatus.ConcurrencyError;
                    break;
                case "DbUpdateException":
                    TransObject = GenerateReferentialIntegrityError();
                    break;
                default:
                    TransObject.StatusInfo.Message = this.Exception.Message;
                    break;
            }
            return TransObject;
        }

        /// <summary>
        ///Generate Referential Integrity Error Message
        /// </summary>
        /// <returns></returns>
        private TransferObject<bool> GenerateReferentialIntegrityError()
        {
            try
            {
                TransObject.StatusInfo.Status = ServiceStatus.DataValidationError;
                if (this.Exception.InnerException != null && this.Exception.InnerException.InnerException != null)
                {
                    _message = Exception.InnerException.InnerException.Message;
                    string[] splitMessages = _message.Split(',');
                    var reg = new Regex("\".*?\"");
                    MatchCollection matches = reg.Matches(splitMessages[1]);
                    foreach (var item in matches)
                    {
                        ReferentialIntegrity = item.ToString().Replace("\"", "").Remove(0, 4);
                        break;
                    }
                }

                FieldInfo fieldInfo = typeof(ReferentialIntegrityDictionary).GetFields().SingleOrDefault(p => p.Name == ReferentialIntegrity.ToUpper());
                if (fieldInfo != null)
                    TransObject.StatusInfo.Message = fieldInfo.GetValue(null).ToString();
                else
                    TransObject.StatusInfo.Message = ReferentialIntegrity;
            }
            catch (Exception)
            {
                TransObject.StatusInfo.Status = ServiceStatus.Unknown;
                TransObject.StatusInfo.Message = ReferentialIntegrityDictionary.DEFAULT;
            }
            return TransObject;
        }
    }
}

