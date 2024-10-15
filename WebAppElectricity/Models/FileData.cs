using System;

namespace WebAppElectricity.Models
{
    public class FileData
    {
        public int FileDataId { get; set; }

        public int FileUploadId { get; set; }

        public byte[] Data { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        // Navigation property
        public FileUpload FileUpload { get; set; }
    }

}