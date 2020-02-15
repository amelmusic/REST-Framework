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
        [Key]
        public string Id { get; set; }
        [RequestField(RequestName = "Insert")]
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(256)]
        public string NormalizedUserName { get; set; }
        [RequestField(RequestName = "Insert")]
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string ConcurrencyStamp { get; set; }
        [RequestField(RequestName = "Insert")]
        public string PhoneNumber { get; set; }
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
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
    }
}
