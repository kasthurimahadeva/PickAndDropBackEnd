using System;
using System.ComponentModel.DataAnnotations;

namespace PickAndDropBackEnd.Models
{
    public class Complaints
    {
        [Key] 
        public long ComplaintId { get; set; }
        [Required]
        public string ComplaintContent { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }
        [Required]
        public string Status { get; set; }
    }
}