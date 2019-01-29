using Invoice.Domain;

namespace Invoice.Integration.Queries
{
    public interface GetCurrentOrganization : ExternalQuery<Organization>
    {
    }
}
