using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using X.Core.Generator.Attributes;

namespace Common.Model
{
    [ModelGenerator(Behaviour = EntityBehaviourEnum.CRUD)]
    public partial class AspNetUsers
    {
        [Filter(Filter = FilterEnum.Equal)]
        [Key]
        public string Id { get; set; }
        [RequestField(RequestName = "Insert")]
        [RequestField(RequestName = "Update")]
        [StringLength(250)]
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [RequestField(RequestName = "Insert")]
        [RequestField(RequestName = "Update")]
        [StringLength(250)]
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [RequestField(RequestName = "Insert")]
        [StringLength(256)]
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [StringLength(256)]
        public string NormalizedUserName { get; set; }
        [RequestField(RequestName = "Insert")]
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string NormalizedEmail { get; set; }
        [RequestField(RequestName = "Insert")]
        [RequestField(RequestName = "Update")]
        public bool EmailConfirmed { get; set; }
        public string ConcurrencyStamp { get; set; }
        [RequestField(RequestName = "Insert")]
        [RequestField(RequestName = "Update")]
        public string PhoneNumber { get; set; }
        [RequestField(RequestName = "Insert")]
        [RequestField(RequestName = "Update")]
        public bool PhoneNumberConfirmed { get; set; }
        [RequestField(RequestName = "Insert")]
        [RequestField(RequestName = "Update")]
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        [RequestField(RequestName = "Insert")]
        [RequestField(RequestName = "Update")]
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> AspNetUserClaims { get; set; }
        public virtual ICollection<IdentityUserRole<string>> AspNetUserRoles { get; set; }
    }
}
