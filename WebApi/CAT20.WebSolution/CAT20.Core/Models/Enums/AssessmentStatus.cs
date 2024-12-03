using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Enums
{
    public enum AssessmentStatus
    {
        Delete,
        Active,
        ActiveSubdivide,
        ActiveAmalgamated,
        NextYearInactive,
        NextYearActive,
        NextYearSubdivision,
        NextYearAmalgamation,

        NextYearSubdivisionInactive,
        NextYearAmalgamationInactive,

        NextQuarterSubdivision,
        NextQuarterAmalgamation,

        NextQuarterSubdivisionInactive,
        NextQuarterAmalgamationInactive,

        Temporary,


        Inactive = -1,
        InactiveSubdivide = -2,
        InactiveAmalgamated = -3

  //[AssessmentStatus.Delete]: 'Deleted',
  //      [AssessmentStatus.Active]: 'Active',
  //      [AssessmentStatus.ActiveSubdivide]: 'Active and Subdivided',
  //      [AssessmentStatus.ActiveAmalgamated]: 'Active and Amalgamated',
  //      [AssessmentStatus.NextYearInactive]: 'Inactive for Next Year',
  //      [AssessmentStatus.NextYearActive]: 'Active for Next Year',
  //      [AssessmentStatus.NextYearSubdivision]: 'Keep this assessment active, with a subdivision planned for the next year.',
  //      [AssessmentStatus.NextYearAmalgamation]: 'Keep this assessment active, with an amalgamation planned for the next year.',
  //      [AssessmentStatus.NextYearSubdivisionInactive]: 'Mark this assessment inactive, with a subdivision planned for the next year.',
  //      [AssessmentStatus.NextYearAmalgamationInactive]: 'Mark this assessment inactive, with an amalgamation planned for the next year.',

  //      [AssessmentStatus.NextQuarterSubdivision]: 'Keep this assessment active, with a subdivision planned for the next quarter.',
  //      [AssessmentStatus.NextQuarterAmalgamation]: 'Keep this assessment active, with an amalgamation planned for the next quarter.',
  //      [AssessmentStatus.NextQuarterSubdivisionInactive]: 'Mark this assessment inactive, with a subdivision planned for the next quarter.',
  //      [AssessmentStatus.NextQuarterAmalgamationInactive]: 'Mark this assessment inactive, with an amalgamation planned for the next quarter.',

  //      [AssessmentStatus.Temporary]: 'Temporary',
  //      // Descriptions for negative values
  //      [AssessmentStatus.Inactive]: 'Inactive',
  //      [AssessmentStatus.InactiveSubdivide]: 'Inactive and Subdivided',
  //      [AssessmentStatus.InactiveAmalgamated]: 'Inactive and Amalgamated',
    }
}
