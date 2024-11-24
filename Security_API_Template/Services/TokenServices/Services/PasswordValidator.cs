using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API_Template.Api.Services.TokenServices.Services
{
    //public class PasswordValidator : IResourceOwnerPasswordValidator
    //{
    //    private Elect_Alex_Data.Interfaces.Token.IToken token;

    //    public PasswordValidator(Elect_Alex_Data.Interfaces.Token.IToken token)
    //    {
    //        this.token = token;
    //    }


    //    public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    //    {

    //        int id = Convert.ToInt32(context.Request.Raw["username"]);
    //        string hashpass = context.Request.Raw["password"];
    //        int responseCheck = token.CheckUser(id, hashpass);

    //        if (responseCheck == 1)
    //        {
    //            context.Result.IsError = false;
    //            context.Result.Subject = GetClaimsPrincipal(id.ToString(), id);

    //            return Task.CompletedTask;
    //        }
    //        else
    //        {
    //            return Task.CompletedTask;
    //        }
    //    }

    //    private static ClaimsPrincipal GetClaimsPrincipal(string userName, int id)
    //    {
    //        var issued = DateTimeOffset.Now.ToUnixTimeSeconds();

    //        var claims = new List<Claim>
    //        {
    //            new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()),
    //            new Claim(JwtClaimTypes.AuthenticationTime, issued.ToString()),
    //            new Claim(JwtClaimTypes.IdentityProvider, "10.3.0.66:4439"),
    //            new Claim("UserName", userName),
    //            new Claim(JwtClaimTypes.Id, id.ToString())
    //        };

    //        return new ClaimsPrincipal(new ClaimsIdentity(claims));
    //    }
    //}

}
