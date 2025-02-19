using System;
namespace App.Domain
{
    public class BaseEntity
    {
        public long Pk_ID { get; set; }
        public string Description { get; set; } = string.Empty;
        public byte[] Version { get; set; }
        public bool Active { get; set; } = false;
        public string CreatedBy { get; set; } = string.Empty;    
        public string LastUpdateBy { get; set; } = string.Empty;
        public string ApprovedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public DateTime ApprovedDate { get; set; }
    }
}
