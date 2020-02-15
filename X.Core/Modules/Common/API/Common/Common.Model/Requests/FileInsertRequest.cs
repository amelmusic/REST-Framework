using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Model.Requests
{
    partial class FileInsertRequest
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
