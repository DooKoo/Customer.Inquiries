using System;
using Microsoft.AspNetCore.Authentication;

namespace Customer.Inquiries.Web.Security
{
    public class ApiKeyAuthorizationOptions : AuthenticationSchemeOptions
    {
        public string[] ApiKeys
        {
            get; set;
        }
    }
}
