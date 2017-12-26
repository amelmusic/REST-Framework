using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $rootnamespace$.Core //DD
{
    /// <summary>
    /// Base class for all Triggers that start a transition between states.
    /// </summary>
    public partial class TriggerBase
    {
        public int TriggerId { get; set; }

        public virtual void UpdateEntity(object entity)
        {

        }
    }
}
