using System;
using System.Threading.Tasks;
using Xero.Api;
using Xero.Api.Infrastructure.Authenticators;
using Xero.Api.Infrastructure.Exceptions;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth;

namespace Invoice.Integration.Xero.XeroAuthenticators
{
    public class PublicAuthenticator : PublicAuthenticatorBase, AuthenticatorFacade
    {
        private readonly IConsumer consumer;
        private readonly RequestTokenStore requestTokenStore;

        public PublicAuthenticator(
            IXeroApiSettings applicationSettings,
            AccessTokenStore accessTokenStore,
            RequestTokenStore requestTokenStore) 
            : base(accessTokenStore, applicationSettings)
        {
            this.requestTokenStore = requestTokenStore;
            consumer = new Consumer(
                applicationSettings.ConsumerKey,
                applicationSettings.ConsumerSecret);
        }

        protected override string AuthorizeUser(IToken token, string scope = null, bool redirectOnError = false)
        {
            throw new NotSupportedException();
        }

        protected override Task<IToken> RenewTokenAsync(IToken sessionToken, IConsumer consumer)
        {
            throw new RenewTokenException();
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
            var existingAccessToken =  Store.FindAsync(userId).Result;
            if (existingAccessToken != null)
            {
                if (!existingAccessToken.HasExpired)
                    return new TokenAdapter(existingAccessToken);
                else
                    Store.DeleteAsync(existingAccessToken);
            }

            var requestToken =  requestTokenStore.FindAsync(userId).Result;
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