using System;
using System.Collections.Generic;
using System.Linq;
using Invoice.Domain;
using NUnit.Framework.Constraints;
using Xero.Api;
using Xero.Api.Core;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth;

namespace Invoice.Integration.Xero
{
    public class XeroRepository :ExternalRepository
    {
        private IXeroCoreApi xeroCoreApi;

        public XeroRepository(
            AuthenticatorFacadeImpl authenticatorFacadeImpl, 
            IXeroApiSettings settings, User currentUser)
        {
            xeroCoreApi = new XeroCoreApi(
                authenticatorFacadeImpl.XeroAuthenticator, 
                settings, 
                new ApiUser{Identifier =currentUser.Identifier} );
        }        

        public T Get<T>(ExternalQuery<T> query)
        {
            var xeroQuery = query as XeroApiQuery<T>;
            if (xeroQuery !=null)
            {
                return xeroQuery.execute(xeroCoreApi).Single();                 
            }
            throw  new ArgumentException($"Not support query type {query.GetType()}");
        }

        public IEnumerable<T> Search<T>(ExternalQuery<T> query)
        {
            var xeroQuery = query as XeroApiQuery<T>;
            if (xeroQuery != null)
            {
                return (query as XeroApiQuery<T>).execute(xeroCoreApi);                               
            }
            throw new ArgumentException($"Not support query type {query.GetType()}");
        }
    }
}