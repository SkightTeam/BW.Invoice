using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Invoice.Domain;
using Xero.Api.Core.Model;
using Account = Xero.Api.Core.Model.Account;

namespace Invoice.Integration.Xero
{
    public class XeroMappingProfile : Profile
    {
        public XeroMappingProfile()
        {
            CreateMap<Account, Domain.Account>()
                .ForMember(x => x.Organization, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Contact, Vendor>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Organization, opt => opt.Ignore());
        }
    }
}
