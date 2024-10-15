using System;
using System.Collections.Generic;

namespace WebAppElectricity.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public int UserId { get; set; }

        public string CategoryName { get; set; }

        public int? StepAccept { get; set; }

        public string StepDeny { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        // Navigation properties
        public Users users { get; set; }
        public ICollection<FileUpload> FileUploads { get; set; }
    }
}