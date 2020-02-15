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
        public long Id { get; set; }

        //depending on storage type, this will be different

        [StringLength(250)]
        [Required]
        public string Title { get; set; }

        public long Size { get; set; }
        /// <summary>
        /// Same as ContentType
        /// </summary>
        [Required]
        [StringLength(400)]
        public string MimeType { get; set; }

        [StringLength(250)]
        public string StorageType { get; set; }

        public long FileContentId { get; set; }
        public FileContent FileContent { get; set; }
        //TODO: Sharing
    }
}
