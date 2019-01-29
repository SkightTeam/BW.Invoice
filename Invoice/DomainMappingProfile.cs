using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Invoice.Domain;

namespace Invoice
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<Vendor, Vendor>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Organization, opt => opt.Ignore());
            CreateMap<Account, Account>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Organization, opt => opt.Ignore());

        }
    }
}
