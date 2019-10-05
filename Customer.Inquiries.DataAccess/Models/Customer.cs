using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Customer.Inquiries.DataAccess.Base;

namespace Customer.Inquiries.DataAccess.Models
{
    public class Customer : BaseEntity
    {
        [Key]
        public long CustomerId { get; set; }

        [Required]
        [StringLength(30)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(25)]
        public string ContactEmail { get; set; }

        [Required]
        public long MobileNumber { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
