using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Customer.Inquiries.DataAccess.Base;

namespace Customer.Inquiries.DataAccess.Models
{
    public class Transaction : BaseEntity
    {
        [Key]
        public int TransactionId { get; set; }

        public DateTimeOffset TransactionDateTime { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        public ETransactionStatus Status { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public Transaction()
        {
        }
    }

    public enum ETransactionStatus
    {
        Success,
        Failed,
        Canceled
    }
}
