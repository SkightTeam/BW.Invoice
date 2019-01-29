using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Invoice.Domain;
using Invoice.Integration.Queries;
using Xero.Api.Core;

namespace Invoice.Integration.Xero.Queries
{
    public class XeroSerachVendorInOrganization : XeroApiQuery<Vendor>, SearchVendorsInOrganization
    {
        private IMapper mapper;

        public XeroSerachVendorInOrganization(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<Vendor> execute(IXeroCoreApi xeroCoreApi)
        {
            return xeroCoreApi.Contacts
                .Where("IsSupplier=true").FindAsync().Result
                .Select(x => mapper.Map<Vendor>(x))
                .ToList();
        }
    }
}