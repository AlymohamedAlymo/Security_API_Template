using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Template.Api.Services.TokenServices.Models;

namespace API_Template.Api.Services.TokenServices.Interfaces
{
    public interface ITokenProvider
    {
        Task<TokenResponse> GetToken(TokenRequest request, string Scope);
    }
}
