using System;
using System.Collections.Generic;
using System.Text;

namespace X.Core.Generator.Attributes
{
    public class ServiceAttribute : Attribute
    {
        public EntityBehaviourEnum Behaviour { get; set; }
        public string ResourceName { get; set; }
    }
}
