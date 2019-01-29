using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Invoice.Domain;
using Invoice.Integration.Queries;
using Xero.Api.Core;

namespace Invoice.Integration.Xero.Queries
{
    public class XeroGetCurrentOrganization : XeroApiQuery<Organization>, GetCurrentOrganization
    {
        private IMapper mapper;

        public XeroGetCurrentOrganization(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<Organization> execute(IXeroCoreApi xeroCoreApi)
        {
            return new List<Organization>
            {
                mapper.Map<Organization>(xeroCoreApi.FindOrganisationAsync().Result)
            };
        }
    }
}
