using System;
using System.Collections.Generic;
using System.Text;
using Invoice.Integration.Xero.XeroAuthenticators;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Specifications.AutoMocking;
using Machine.Specifications.AutoMocking.Rhino;
using Moq;
using NSubstitute;
using Rhino.Mocks;
using Xero.Api;
using It = Machine.Specifications.It;
using MockRepository = Rhino.Mocks.MockRepository;

namespace Invoice.Integration.Xero.Tests
{   
    class AuthenticatorFacadeImplSpecs 
        :WithSubject<AuthenticatorFacadeImpl>
    {
           
    }

    class When_setting_app_type_is_public 
        : AuthenticatorFacadeImplSpecs
    {
        private Establish context = () =>
            The<IXeroApiSettings>().AppType.Returns(XeroApiAppType.Public);

        private It should_call_method_of_get_public_authenticator = () =>
            The<IServiceProvider>().Received().GetService(typeof(PublicAuthenticator));

        private It should_not_call_method_of_get_partner_authenticator = () =>
            The<IServiceProvider>().DidNotReceive().GetService(typeof(PartnerAuthenticator));
    }

    class When_setting_app_type_is_partner
        : AuthenticatorFacadeImplSpecs
    {
        private Establish context = () =>
            The<IXeroApiSettings>().AppType.Returns(XeroApiAppType.Partner);

        private It should_call_method_of_get_partner_authenticator = () =>
            The<IServiceProvider>().Received().GetService(typeof(PartnerAuthenticator));

        private It should_not_call_method_of_get_public_authenticator = () =>
            The<IServiceProvider>().DidNotReceive().GetService(typeof(PublicAuthenticator));
    }
}
