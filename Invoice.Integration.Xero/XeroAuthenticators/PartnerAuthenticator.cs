using System;
using Xero.Api;
using Xero.Api.Infrastructure.Authenticators;
using Xero.Api.Infrastructure.Interfaces;

namespace Invoice.Integration.Xero.XeroAuthenticators
{
    public class PartnerAuthenticator : PartnerAuthenticatorBase, AuthenticatorFacade
    {
        private readonly IConsumer consumer;
        private readonly ITokenStoreAsync requestTokenStore;

        public PartnerAuthenticator(ITokenStoreAsync store, IXeroApiSettings applicationSettings) : base(store, applicationSettings)
        {
        }

        protected override string AuthorizeUser(IToken oauthToken, string scope = null, bool redirectOnError = false)
        {
            throw new System.NotImplementedException();
        }

        public string GetRequestTokenAuthorizeUrl(string userId)
        {
            var requestToken = GetRequestTokenAsync(consumer).Result;
            requestToken.UserId = userId;

            var existingToken = requestTokenStore.FindAsync(userId).Result;
            if (existingToken != null)
                requestTokenStore.DeleteAsync(requestToken);

            requestTokenStore.AddAsync(requestToken);

            return GetAuthorizeUrl(requestToken);
        }

        public Token RetrieveAndStoreAccessToken(string userId, string tokenKey, string verifier)
        {
            var existingAccessToken = Store.FindAsync(userId).Result;
            if (existingAccessToken != null)
            {
                if (!existingAccessToken.HasExpired)
                    return new TokenAdapter(existingAccessToken);
                else
                    Store.DeleteAsync(existingAccessToken);
            }

            var requestToken = requestTokenStore.FindAsync(userId).Result;
            if (requestToken == null)
                throw new ApplicationException("Failed to look up request token for user");

            //Delete the request token from the _requestTokenStore as the next few lines will render it useless for the future.
            requestTokenStore.DeleteAsync(requestToken);

            if (requestToken.TokenKey != tokenKey)
                throw new ApplicationException("Request token key does not match");

            var accessToken = Tokens.GetAccessTokenAsync(
                    requestToken,
                    GetAuthorization(requestToken, "POST", Tokens.AccessTokenEndpoint, null, verifier))
                .Result;

            accessToken.UserId = userId;

            Store.AddAsync(accessToken);

            return new TokenAdapter(accessToken);

        }
    }
}