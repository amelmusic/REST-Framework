using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace X.Core.Test.Database
{
    public partial class Channels
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int AccountId { get; set; }
    }
}
