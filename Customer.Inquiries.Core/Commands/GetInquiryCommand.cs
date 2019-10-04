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

namespace Customer.Inquiries.Core.Commands
{
    public class GetInquiryCommand : IRequest<CustomerProfileDto>
    {
        [Range(0, 99999, ErrorMessage = "Invalid Customer ID")]
        public int? CustomerId { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email")]
        [RequiredIfNull(nameof(GetInquiryCommand.CustomerId), "No inquiry criteria")]
        public string Email { get; set; }
    }

    public class GetInquiryCommandHandler : IRequestHandler<GetInquiryCommand, CustomerProfileDto>
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public GetInquiryCommandHandler(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CustomerProfileDto> Handle(GetInquiryCommand request, CancellationToken cancellationToken)
        {
            var items = repository.FindAll<DataAccess.Models.Customer>(x =>
            (string.IsNullOrEmpty(request.Email) || request.Email == x.ContactEmail) &&
            (request.CustomerId == null || request.CustomerId.Value == x.CustomerId));

            return mapper.Map<CustomerProfileDto>(await items.Include(x => x.Transactions).FirstOrDefaultAsync());
        }
    }
}
