using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Integration
{
    public interface AuthenticatorFacade
    {
        string GetRequestTokenAuthorizeUrl(string userId);
        Token RetrieveAndStoreAccessToken(string userId, string tokenKey, string verifier);
    }
}
