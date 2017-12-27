using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Attributes
{
    public enum HistoryType
    {
        /// <summary>
        /// For eg. for table Contract, data will be stored in ContractHistory table
        /// </summary>
        SeparateTable = 1,
        /// <summary>
        /// Everything is in same table with few additional columns like IsActive, ActiveId, ValidFrom and ValidTo
        /// </summary>
        SameTable = 2
    }
    public class HistoryAttribute : Attribute
    {
        public HistoryType HistoryType { get; set; }
        public string EntityName { get; set; }
        public string ParentTableIdName { get; set; }

        /// <summary>
        /// Info about historization type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parentTableIdName">Name of the column in parent table</param>
        /// <param name="entityName">Required only if history type is SeparateTable</param>
        public HistoryAttribute(HistoryType type, string entityName = null, string parentTableIdName = null)
        {
            HistoryType = type;
            EntityName = entityName;
            ParentTableIdName = parentTableIdName;

            if (type == HistoryType.SeparateTable && string.IsNullOrWhiteSpace(entityName))
            {
                throw  new ApplicationException("For HistoryType.SeparateTable, entity name is required");
            }
        }
    }
}
