using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Invoice.Integration.Xero.XeroAuthenticators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xero.Api;
using Xero.Api.Infrastructure.Authenticators;

namespace Invoice.Integration.Xero
{
    public class AuthenticatorFacadeImpl :AuthenticatorFacade
    {
        private AuthenticatorFacade authenticator;

        public AuthenticatorFacadeImpl(
            IXeroApiSettings settings, 
            IServiceProvider serviceProvider)
        {
            switch (settings.AppType)
            {
                case XeroApiAppType.Public:
                    authenticator =
                        serviceProvider.GetService<PublicAuthenticator>();
                    break;
                case XeroApiAppType.Partner:
                    authenticator =
                        serviceProvider.GetService<PartnerAuthenticator>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        typeof(XeroApiAppType).Name,
                        settings.AppType,
                        $"Not supported yet.");
            }
        }

        public string GetRequestTokenAuthorizeUrl(string userId)
        {
            return authenticator.GetRequestTokenAuthorizeUrl(userId);
        }

        public Token RetrieveAndStoreAccessToken(string userId, string tokenKey, string verifier)
        {
            return authenticator.RetrieveAndStoreAccessToken(userId, tokenKey, verifier);
        }
    }
}
