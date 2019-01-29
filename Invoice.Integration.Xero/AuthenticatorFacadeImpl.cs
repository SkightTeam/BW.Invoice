using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Invoice.Integration.Xero.XeroAuthenticators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xero.Api;
using Xero.Api.Infrastructure.Authenticators;
using Xero.Api.Infrastructure.Interfaces;

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
                     var publicAuthenticator=   serviceProvider.GetService<PublicAuthenticator>();
                    XeroAuthenticator = publicAuthenticator;
                    authenticator = publicAuthenticator;
                    break;
                case XeroApiAppType.Partner:
                    var partnerAuthenticator =
                        serviceProvider.GetService<PartnerAuthenticator>();
                    XeroAuthenticator = partnerAuthenticator;
                    authenticator = partnerAuthenticator;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        typeof(XeroApiAppType).Name,
                        settings.AppType,
                        $"Not supported yet.");
            }
        }

        public IAuthenticator XeroAuthenticator { get; }

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
