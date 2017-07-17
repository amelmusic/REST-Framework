using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Attributes
{
    /// <summary>
    /// Represents attribute that marks entity suitable for generating additional source code based upon it
    /// </summary>
    public class EntityAttribute : Attribute
    {
        public EntityAttribute() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="copyAttributesToRequests">If this is [true] all attributes from model will be copied to request object, except attributes in A.Core.Attributes</param>
        /// <param name="mapTo">If populated, it will automatically create AutomMappers profile for mapping. This should be used when we have separated database from model</param>
        public EntityAttribute(bool copyAttributesToRequests, string mapTo) {}
        public EntityAttribute(bool copyAttributesToRequests) { }
    }
}
