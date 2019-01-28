using System;
using System.Collections.Generic;
using System.Text;
using Machine.Specifications;

namespace Invoice.Integration.Xero.Tests
{
    class MSpecSelfCheck
    {
        private It identity_equation_should_pass = () => 1.ShouldEqual(1);
    }
}
