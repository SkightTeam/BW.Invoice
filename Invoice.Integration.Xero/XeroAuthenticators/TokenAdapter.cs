using System;
using Xero.Api.Infrastructure.Interfaces;

namespace Invoice.Integration.Xero.XeroAuthenticators
{
    public class TokenAdapter:Token
    {
        public TokenAdapter(IToken xeroToken)
        {
            UserId = xeroToken.UserId;
            ConsumerKey = xeroToken.ConsumerKey;
            ConsumerSecret = xeroToken.ConsumerSecret;
            TokenKey = xeroToken.TokenKey;
            TokenSecret = xeroToken.TokenSecret;
            Session = xeroToken.Session;
            ExpiresAt = xeroToken.ExpiresAt;
            SessionExpiresAt = xeroToken.SessionExpiresAt;
            HasExpired = xeroToken.HasExpired;
        }
        public string UserId { get; set; }
        public string ConsumerKey { get; }
        public string ConsumerSecret { get; }
        public string TokenKey { get; }
        public string TokenSecret { get; }
        public string Session { get; }
        public DateTime? ExpiresAt { get; }
        public DateTime? SessionExpiresAt { get; }
        public bool HasExpired { get; }
    }
}
