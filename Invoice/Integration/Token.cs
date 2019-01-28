using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Integration
{
    public interface Token
    {
        string UserId { get; }
      
        string ConsumerKey { get; }
        string ConsumerSecret { get; }
        string TokenKey { get; }
        string TokenSecret { get; }
        string Session { get; }

        DateTime? ExpiresAt { get; }
        DateTime? SessionExpiresAt { get; }

        bool HasExpired { get; }
    }
}
