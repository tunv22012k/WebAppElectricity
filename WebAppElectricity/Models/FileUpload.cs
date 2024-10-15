using System;
using System.Collections.Generic;

namespace WebAppElectricity.Models
{
    public class FileUpload
    {
        public int FileUploadId { get; set; }

        public int CategoryId { get; set; }

        public int Step { get; set; }

        public string FileName { get; set; }

        public string TypeFile { get; set; }

        public string FileExtension { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        // Navigation properties
        public Category Category { get; set; }
        public ICollection<FileData> FileData { get; set; }
    }

}