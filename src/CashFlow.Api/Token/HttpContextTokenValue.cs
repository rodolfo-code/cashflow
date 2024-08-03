using CashFlow.Domain.Security.Tokens;

namespace CashFlow.Api.Token;

public class HttpContextTokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextTokenValue(IHttpContextAccessor httpContextAccessor)
    {
        _contextAccessor = httpContextAccessor;
    }

    public string TokenOnRequest()
    {
        var teste = _contextAccessor.HttpContext!.Request.Headers;
        var teste2 = _contextAccessor.HttpContext!.Request.Headers.Authorization;

        var authorization = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        return authorization["Bearer ".Length..].Trim();
    }
}