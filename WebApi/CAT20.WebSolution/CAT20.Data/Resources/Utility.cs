using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.AuditTrails;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Interfaces;
using KellermanSoftware.CompareNetObjects;

namespace CAT20.Data.Resources
{
        public class Utility
        {
            //private SystemConfiguration systemConfiguration;

            //internal Utility(SystemConfiguration _systemConfiguration)
            //{
            //    systemConfiguration = _systemConfiguration;
            //}

            public static AuditTrail CreateAuditTrail(object dbObject, object newObject, AuditTrailAction action, List<string> listNames, int userID)
            {
                var returnVal = new AuditTrail();
                returnVal.DetailList = new List<AuditTrailDetail>();

                string fullEntityTypeString = newObject.GetType().ToString();
                returnVal.EntityType = GetEntityType(fullEntityTypeString);
            
                returnVal.ClaimedUserID = userID;
                returnVal.ClaimedSabhaID = int.Parse(newObject.GetType().GetProperty("ClaimedSabhaID").GetValue(newObject, null).ToString());
                returnVal.ClaimedOfficeID = int.Parse(newObject.GetType().GetProperty("ClaimedOfficeID").GetValue(newObject, null).ToString());
                returnVal.Date = DateTime.Now;
                returnVal.Action = action;
                returnVal.State = State.Added;

                if (action == AuditTrailAction.Insert)
                {
                    var detail = new AuditTrailDetail();
                    detail.EntityType = returnVal.EntityType;
                    detail.NewValue = newObject.GetType().GetProperty("AuditReference").GetValue(newObject, null).ToString();
                    detail.Action = action;
                    detail.State = State.Added;
                    detail.UserID = userID;
                    returnVal.DetailList.Add(detail);
                }
                else if (action == AuditTrailAction.Delete)
                {
                    returnVal.EntityID = (int)newObject.GetType().GetProperty("Id").GetValue(newObject, null);
                    var detail = new AuditTrailDetail();
                    detail.EntityType = returnVal.EntityType;
                    detail.EntityID = returnVal.EntityID;
                    detail.NewValue = newObject.GetType().GetProperty("AuditReference").GetValue(newObject, null).ToString();
                    detail.Action = action;
                    detail.State = State.Added;
                    detail.UserID = userID;
                    returnVal.DetailList.Add(detail);
                }
                else if (action == AuditTrailAction.Update)
                {
                    returnVal.EntityID = (int)newObject.GetType().GetProperty("Id").GetValue(newObject, null);

                    foreach (var diff in Compare(dbObject, newObject).Differences)
                    {
                    var detail = new AuditTrailDetail();

                        detail.Property = diff.PropertyName;  //diff.PropertyName.Substring(1, diff.PropertyName.Length - 1);
                        if (detail.Property != "TimeStamp" && detail.Property != "State" && detail.Property != "CreatedAt" && detail.Property != "UpdatedAt" && detail.Property != "AuditReference" && detail.Property != "UpdatedBy" && detail.Property != "ClaimedUserID" && detail.Property != "ClaimedOfficeID" && detail.Property != "ClaimedSabhaID")
                        { 
                        detail.EntityType = returnVal.EntityType;
                        detail.EntityID = returnVal.EntityID;
                        detail.PreviousValue = diff.Object1Value != "(null)" ? diff.Object1Value : string.Empty;
                        detail.NewValue = diff.Object2Value;
                        detail.Action = action;
                        detail.State = State.Added;
                        detail.UserID = userID;
                        returnVal.DetailList.Add(detail);
                        }
                    }

                    foreach (var child in listNames)
                    {
                    var dbChildList = (IList<object>)dbObject.GetType().GetProperty(child).GetValue(dbObject, null);
                    var newChildList = (IList<object>)newObject.GetType().GetProperty(child).GetValue(newObject, null);

                    foreach (var newChild in newChildList)
                        {
                            int? id = (int?)newChild.GetType().GetProperty("Id").GetValue(newChild, null);

                            if (id == null)
                            {
                                var detail = new AuditTrailDetail();
                                string newChildEntityTypeString = newChild.GetType().ToString();
                                returnVal.EntityType = GetEntityType(newChildEntityTypeString);
                                //detail.EntityType = newChild.GetType().ToString().Split(new[] { "CAT20.Core.Models." }, StringSplitOptions.None)[1];
                                detail.NewValue = newChild.GetType().GetProperty("AuditReference").GetValue(newChild, null).ToString();
                                detail.Action = AuditTrailAction.Insert;
                                detail.State = State.Added;
                                detail.UserID = userID;
                                returnVal.DetailList.Add(detail);
                            }
                            else
                            {
                                foreach (var dbChild in dbChildList)
                                {
                                    int dbChildId = (int)dbChild.GetType().GetProperty("Id").GetValue(dbChild, null);
                                    if (id.Value == dbChildId)
                                    {
                                        foreach (var diff in Compare(dbChild, newChild).Differences)
                                        {
                                            var detail = new AuditTrailDetail();
                                            string newChildEntityType = newChild.GetType().ToString();
                                            returnVal.EntityType = GetEntityType(newChildEntityType);
                                            //detail.EntityType = newChild.GetType().ToString().Split(new[] { "CAT20.Core.Models." }, StringSplitOptions.None)[1];
                                            detail.EntityID = id;
                                            detail.Property = diff.PropertyName.Substring(1, diff.PropertyName.Length - 1);
                                            detail.PreviousValue = diff.Object1Value != "(null)" ? diff.Object1Value : string.Empty;
                                            detail.NewValue = diff.Object2Value;
                                            detail.Action = AuditTrailAction.Update;
                                            detail.State = State.Added;
                                            detail.UserID = userID;
                                            returnVal.DetailList.Add(detail);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        //Manage deleted list
                        foreach (var dbChild in dbChildList)
                        {
                            int? dbChildId = (int?)dbChild.GetType().GetProperty("Id").GetValue(dbChild, null);
                            bool hasFound = false;
                            foreach (var newChild in newChildList)
                            {
                                int? newChildId = (int?)newChild.GetType().GetProperty("Id").GetValue(newChild, null);
                                if (dbChildId == newChildId)
                                {
                                    hasFound = true;
                                    break;
                                }
                            }
                            if (!hasFound)
                            {
                                var detail = new AuditTrailDetail();
                                string dbChildEntityType = dbChild.GetType().ToString();
                                returnVal.EntityType = GetEntityType(dbChildEntityType);
                                //detail.EntityType = dbChild.GetType().ToString().Split(new[] { "CAT20.Core.Models." }, StringSplitOptions.None)[1];
                                detail.EntityID = dbChildId;
                                detail.NewValue = dbChild.GetType().GetProperty("AuditReference").GetValue(dbChild, null).ToString();
                                detail.Action = AuditTrailAction.Delete;
                                detail.State = State.Added;
                                detail.UserID = userID;
                                returnVal.DetailList.Add(detail);
                            }
                        }
                    }
                }

                return returnVal;
            }

            public static ComparisonResult Compare(object dbObject, object newObject)
            {
                CompareLogic compareLogic = new CompareLogic();
                compareLogic.Config.MaxDifferences = 100;
                compareLogic.Config.CompareChildren = false;

                return compareLogic.Compare(dbObject, newObject);
            }

        public static CAT20.Core.Models.Enums.EntityType GetEntityType(string fullEntityTypeString)
        {
            string[] parts = fullEntityTypeString.Split(new[] { "CAT20.Core.Models." }, StringSplitOptions.None);
            if (parts.Length > 1)
            {
                string entityName = parts[1].Substring(parts[1].LastIndexOf('.') + 1);
                if (Enum.TryParse<CAT20.Core.Models.Enums.EntityType>(entityName, out var entityType))
                {
                    return entityType;
                }
            }
            // Default to an unknown entity type
            return CAT20.Core.Models.Enums.EntityType.Unknown;
        }
    }
    }

