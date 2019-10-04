using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Customer.Inquiries.DataAccess.Base;

namespace Customer.Inquiries.DataAccess.Models
{
    public class Customer : BaseEntity
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(30)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(25)]
        public string ContactEmail { get; set; }

        [Required]
        public int MobileNumber { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
