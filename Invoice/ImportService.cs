using AutoMapper;
using Invoice.Domain;
using Invoice.Integration;
using Invoice.Integration.Queries;
using Invoice.Persistent;

namespace Invoice
{
    public class ImportService
    {
        private ExternalRepository externalRepository;
        private Repository repository;
        private GetCurrentOrganization getCurrentOrganization;
        private SearchVendorsInOrganization searchVendorsInOrganization;
        private SearchAccountsInOrganization searchAccountsInOrganization;
        private IMapper mapper;

        public ImportService(ExternalRepository externalRepository, GetCurrentOrganization getCurrentOrganization, SearchVendorsInOrganization searchVendorsInOrganization, SearchAccountsInOrganization searchAccountsInOrganization, Repository repository, IMapper mapper)
        {
            this.externalRepository = externalRepository;
            this.getCurrentOrganization = getCurrentOrganization;
            this.searchVendorsInOrganization = searchVendorsInOrganization;
            this.searchAccountsInOrganization = searchAccountsInOrganization;
            this.repository = repository;
            this.mapper = mapper;
        }
       
        public void importVendors()
        {
            var organization = externalRepository.Get(getCurrentOrganization);
            var externalVendors = externalRepository.Search(searchVendorsInOrganization);
            var externalAccounts = externalRepository.Search(searchAccountsInOrganization);
            //TODO: Need check if organization is existed in our repository
            repository.create(organization);
            foreach (var externalVendor in externalVendors)
            {
                //TODO: Need check if Vendor is existed in our repository
                var vendor = new Vendor(organization);
                mapper.Map(externalVendor, vendor);
                repository.create(vendor);
            }

            foreach (var externalAccount in externalAccounts)
            {
                //TODO: Need check if Account is existed in our repository
                var account =new Account(organization);
                mapper.Map(externalAccount, account);
                repository.create(account);
            }
        }
    }
}
