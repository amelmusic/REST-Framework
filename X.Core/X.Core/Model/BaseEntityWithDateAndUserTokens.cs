using System.ComponentModel.DataAnnotations;

namespace X.Core.Model
{
    public class BaseEntityWithDateAndUserTokens : BaseEntityWithDateTokens
    {
        [StringLength(50)]
        public string CreatedById { get; set; }
        [StringLength(50)]
        public string ModifiedById { get; set; }
    }
}
