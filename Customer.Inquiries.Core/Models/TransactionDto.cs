using System;
namespace Customer.Inquiries.Core.Models
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }

        public TransactionDto()
        {
        }
    }
}
