using System;
using MediatR;
using Customer.Inquiries.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using Customer.Inquiries.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Customer.Inquiries.Core.Extensions;

namespace Customer.Inquiries.Core.Queries
{
    public class GetCustomerQuery : IRequest<CustomerProfileDto>
    {
        [Range(0, 9999999999, ErrorMessage = "Invalid Customer ID")]
        public long? CustomerId { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email")]
        [RequiredIfNull(nameof(GetCustomerQuery.CustomerId), "No inquiry criteria")]
        public string Email { get; set; }
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerProfileDto>
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public GetCustomerQueryHandler(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CustomerProfileDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            if (request.CustomerId == null && string.IsNullOrEmpty(request.Email)) return null;

            var items = repository.FindAll<DataAccess.Models.Customer>(x =>
            (string.IsNullOrEmpty(request.Email) || request.Email == x.ContactEmail) &&
            (request.CustomerId == null || request.CustomerId.Value == x.CustomerId));

            return mapper.Map<CustomerProfileDto>(await items.Include(x => x.Transactions).FirstOrDefaultAsync());
        }
    }
}
