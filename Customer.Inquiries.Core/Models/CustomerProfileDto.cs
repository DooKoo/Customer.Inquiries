using System;
using System.Collections.Generic;

namespace Customer.Inquiries.Core.Models
{
    public class CustomerProfileDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public IEnumerable<TransactionDto> Transactions { get; set; }

        public CustomerProfileDto()
        {
        }
    }
}
