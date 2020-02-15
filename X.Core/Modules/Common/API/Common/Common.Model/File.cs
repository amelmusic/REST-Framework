using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using X.Core.Generator.Attributes;

namespace Common.Model
{
    [ModelGenerator(Behaviour = EntityBehaviourEnum.CRUDAsUpload)]
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

        [RequestField(RequestName = "Insert")]
        [StringLength(250)]
        public string StorageType { get; set; }

        //TODO: Sharing
    }
}
