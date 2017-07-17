using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Attributes
{
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = true,
     AllowMultiple = false)]
    public class FilterAttribute : Attribute
    {
        public FilterEnum Filter { get; set; }
        /// <summary>
        /// If this is true, only filter in searchObject will be added. You would have to implement it in service yourself.
        /// </summary>
        public bool DisableCodeGenerationOnService { get; set; }

        public FilterAttribute(FilterEnum filter)
        {
            Filter = filter;
        }
        public FilterAttribute(FilterEnum filter, bool disableCodeGenerationOnService)
        {
            Filter = filter;
            DisableCodeGenerationOnService = disableCodeGenerationOnService;
        }
    }

    [Flags]
    public enum FilterEnum
    {
        None = 0,
        Equal = 1 << 0,       // 1
        // now that value 1 is available, start shifting from there
        NotEqual = Equal << 1,     // 2
        GreatherThan = NotEqual << 1,     // 4
        GreatherThanOrEqual = GreatherThan << 1,   // 8
        LowerThan = GreatherThanOrEqual << 1,
        LowerThanOrEqual = LowerThan << 1,
        List = LowerThanOrEqual << 1,
        EqualOrNull = List << 1,
        NotEqualOrNull = EqualOrNull << 1,
        GreatherThanOrEqualOrNull = NotEqualOrNull << 1,
        LowerThanOrEqualOrNull = GreatherThanOrEqualOrNull << 1,
        Null = LowerThanOrEqualOrNull << 1,
        GreatherThanOrNull = Null << 1,
        LowerThanOrNull = GreatherThanOrNull << 1,
        ListOrNull = LowerThanOrNull << 1,
        ListNotEqualOrNull = ListOrNull << 1,
        ListNotEqual = ListNotEqualOrNull << 1,
    }
}
