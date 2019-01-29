using System;
using System.Collections.Generic;
using System.Text;
using Invoice.Integration;
using Invoice.Integration.Queries;

namespace Invoice.Domain
{
    public class ImportService
    {
        private ExternalRepository externalRepository;
        private GetCurrentOrganization getCurrentOrganization;
        private SearchVendorsInOrganization searchVendorsInOrganization;
        private SearchAccountsInOrganization searchAccountsInOrganization;

        public ImportService(ExternalRepository externalRepository, GetCurrentOrganization getCurrentOrganization, SearchVendorsInOrganization searchVendorsInOrganization, SearchAccountsInOrganization searchAccountsInOrganization)
        {
            this.externalRepository = externalRepository;
            this.getCurrentOrganization = getCurrentOrganization;
            this.searchVendorsInOrganization = searchVendorsInOrganization;
            this.searchAccountsInOrganization = searchAccountsInOrganization;
        }
       
        public void importVendors()
        {
            var organization = externalRepository.Get(getCurrentOrganization);
            var vendors = externalRepository.Search(searchVendorsInOrganization);
            var accounts = externalRepository.Search(searchAccountsInOrganization);
        }
    }
}
