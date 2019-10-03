using System;
using MediatR;
using Customer.Inquiries.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using Customer.Inquiries.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;

namespace Customer.Inquiries.Core.Commands
{
    public class GetInquiryCommand : IRequest<CustomerProfileDto>
    {
        public int? CustomerId { get; set; }
        public string Email { get; set; }
    }

    public class GetInquiryCommandHandler : IRequestHandler<GetInquiryCommand, CustomerProfileDto>
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public GetInquiryCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CustomerProfileDto> Handle(GetInquiryCommand request, CancellationToken cancellationToken)
        {
            return mapper.Map<CustomerProfileDto>(repository.FindAll<DataAccess.Models.Customer>(x =>
            (!string.IsNullOrEmpty(request.Email) ? request.Email == x.ContactEmail : true) &&
            (request.CustomerId.HasValue ? request.CustomerId.Value == x.CustomerId : true))
                .Include(x => x.Transactions));
        }
    }
}
