using CommonTestUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace WebApi.Test.Users.Register;
public class RegisterUserTest : IClassFixture<WebApplicationFactory<Program>>
{
    private const string METHOD = "api/User";

    private readonly HttpClient _httpClient;
    public RegisterUserTest(WebApplicationFactory<Program> webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }

    [Fact(DisplayName = nameof(Register_User_Success))]
    [Trait("Integration", "Register User")]
    public async Task Register_User_Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        HttpResponseMessage result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
