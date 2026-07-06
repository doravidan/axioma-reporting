using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

// מסקנות כיתה — class-level conclusion/recommendation values for a report row.
// Kept distinct from SchoolClass (actual class 1..15) so the two dropdowns don't mix.
public class ClassConclusion : LookupEntity { }
