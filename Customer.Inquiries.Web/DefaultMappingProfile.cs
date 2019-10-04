using System;
using AutoMapper;
using Customer.Inquiries.Core.Extensions;
using Customer.Inquiries.Core.Models;

namespace Customer.Inquiries.Web
{
    public class DefaultMappingProfile : Profile
    {
        public DefaultMappingProfile()
        {
            CreateMap<DataAccess.Models.Transaction, TransactionDto>()
                .ForMember(x => x.Date, cfg => cfg.MapFrom(x => x.TransactionDateTime))
                .ForMember(x => x.Status, cfg => cfg.MapFrom(x => x.Status.GetDescription()))
                .ForMember(x => x.Id, cfg => cfg.MapFrom(x => x.TransactionId));

            CreateMap<DataAccess.Models.Customer, CustomerProfileDto>()
                .ForMember(x => x.Name, cfg => cfg.MapFrom(x => x.CustomerName))
                .ForMember(x => x.Email, cfg => cfg.MapFrom(x => x.ContactEmail))
                .ForMember(x => x.Mobile, cfg => cfg.MapFrom(x => x.MobileNumber));
        }
    }
}
