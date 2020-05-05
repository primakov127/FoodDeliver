using FoodDelivery.Domain.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace FoodDelivery.WebUI.Models
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = await HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>().FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            
            // Add roles to claim
            var roleManager = HttpContext.Current.GetOwinContext().GetUserManager<AppRoleManager>();
            foreach (var role in user.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleManager.FindByIdAsync(role.RoleId).Result.Name));
            }

            //identity.AddClaim(new Claim("Email", user.UserEmailID));
            context.Validated(identity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();

            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            context.AdditionalResponseParameters.Add("userName", context.Identity.GetUserName());
            context.AdditionalResponseParameters.Add("userId", userManager.FindByName(context.Identity.GetUserName()).Id);
            
            return Task.FromResult<object>(null);
        }
    }
}