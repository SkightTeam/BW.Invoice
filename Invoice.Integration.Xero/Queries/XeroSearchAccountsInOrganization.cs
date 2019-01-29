using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Invoice.Domain;
using Invoice.Integration.Queries;
using Xero.Api.Core;

namespace Invoice.Integration.Xero.Queries
{
    public class XeroSearchAccountsInOrganization : XeroApiQuery<Account>, SearchAccountsInOrganization
    {
        private IMapper mapper;

        public XeroSearchAccountsInOrganization(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<Account> execute(IXeroCoreApi xeroCoreApi)
        {
            return xeroCoreApi.Accounts.FindAsync().Result
                .Select(x=> mapper.Map<Account>(x))
                .ToList();
        }
    }
}