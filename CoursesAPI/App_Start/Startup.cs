﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Owin;
using Owin;
using System.IdentityModel.Tokens;
using System.Linq;
using Thinktecture.IdentityModel;
using Thinktecture.IdentityModel.Owin.ScopeValidation;
using Thinktecture.IdentityModel.Tokens;
using Thinktecture.IdentityServer.v3.AccessTokenValidation;


[assembly: OwinStartup(typeof(CoursesAPI.App_Start.Startup))]

namespace CoursesAPI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            JwtSecurityTokenHandler.InboundClaimTypeMap = Thinktecture.IdentityModel.Tokens.ClaimMappings.None;

            // for self contained tokens
            app.UseIdentitiyServerJwt(new JwtTokenValidationOptions
            {
                Authority = "http://dispatch.ru.is/auth/core",
                //AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Passive
            });

            // for reference tokens
            app.UseIdentitiyServerReferenceToken(new ReferenceTokenValidationOptions
            {
                Authority = "http://dispatch.ru.is/auth/core",
                //AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Passive
            });

            // require read OR write scope
            app.RequireScopes(new ScopeValidationOptions
            {
                AllowAnonymousAccess = true,
                Scopes = new[] { "read", "write" }
            });

            app.UseWebApi(WebApiConfig.Register());
        }
    }
}
