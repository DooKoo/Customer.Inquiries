using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Customer.Inquiries.DataAccess.Base;

namespace Customer.Inquiries.DataAccess.Models
{
    public class Transaction : BaseEntity
    {
        [Key]
        public long TransactionId { get; set; }

        public DateTimeOffset TransactionDateTime { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; }

        public ETransactionStatus Status { get; set; }

        [ForeignKey("Customer")]
        public long CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }

    public enum ETransactionStatus
    {
        [Description("Success")]
        Success,

        [Description("Failed")]
        Failed,

        [Description("Canceled")]
        Canceled
    }
}
