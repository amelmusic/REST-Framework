using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Services.Database
{
    [Table("File", Schema = "common")]
    public class File
    {
        [Key]
        public int Id { get; set; }

        //depending on storage type, this will be different
        [Required]
        [StringLength(4000)]
        public string Path { get; set; }

        [StringLength(250)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Same as ContentType
        /// </summary>
        [Required]
        [StringLength(400)]
        public string MimeType { get; set; }

        [StringLength(250)]
        public string StorageType { get; set; }
        //TODO: Sharing
    }
}
