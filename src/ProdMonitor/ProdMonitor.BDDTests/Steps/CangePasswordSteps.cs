using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using ProdMonitor.Web.Dto.Auth;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.BDDTests.Steps;

[Binding]
[Scope(Tag = "change-password")]
public class CangePasswordSteps
{
    private readonly HttpClient _client;
    private string _userId;
    private string _email;
    private string _password;
    private string _oldPassword;
    private string _newPassword;
    private string _generated2FACode;
    private string _input2FACode;
    private HttpResponseMessage _response;

    public CangePasswordSteps()
    {
        _client = new HttpClient { BaseAddress = new Uri("http://localhost:5091/") };
        _email = Environment.GetEnvironmentVariable("TEST_EMAIL_2");
        _password = Environment.GetEnvironmentVariable("TEST_PASSWORD");
        _newPassword = Environment.GetEnvironmentVariable("TEST_PASSWORD_NEW");
    }

    [Given(@"a registered user")]
    public async Task GivenARegisteredUser()
    {
        var technicalUser = new
        {
            Name = "Technical2",
            Surname = "User2",
            Fathername = "Father2",
            Department = "IT2",
            Email = _email,
            Password = _password,
            BirthDate = new DateOnly(1990, 1, 1),
            Sex = "Male"
        };
        
        var contentRegister = new StringContent(JsonConvert.SerializeObject(technicalUser), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/v1/Auth/register", contentRegister);

        if (response.StatusCode != HttpStatusCode.BadRequest)
        {
            response.EnsureSuccessStatusCode();
        }
        else
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Registration failed: {responseContent}");
        }

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [When(@"the user enters valid credentials")]
    public async Task WhenTheUserEntersValidCredentials()
    {
        var technicalUser = new
        {
            Login = _email,
            Password = _password
        };
        
        var contentRegister = new StringContent(JsonConvert.SerializeObject(technicalUser), Encoding.UTF8, "application/json");

        var _response = await _client.PostAsync("api/v1/Auth/login", contentRegister);

        var responseContent = await _response.Content.ReadAsStringAsync();

        if (_response.StatusCode != HttpStatusCode.BadRequest)
        {
            _response.EnsureSuccessStatusCode();
        }
        else
        {
            throw new Exception($"Registration failed: {responseContent}");
        }
        
        _response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var json = JsonDocument.Parse(responseContent);
        _userId = json.RootElement.GetProperty("id").GetString();
    }

    [When(@"the system sends a 2FA code to their email")]
    public async Task WhenTheSystemSendsA2FACodeToTheirEmail()
    {
        _response = await _client.GetAsync($"api/v1/Users/{_userId}");
        _response.EnsureSuccessStatusCode();
        
        var responseContent = await _response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(responseContent); 
        _generated2FACode = json.RootElement.GetProperty("twoFactorCode").GetString();
    }

    [When(@"the user enters the correct 2FA code")]
    public async Task WhenTheUserEntersTheCorrect2FACode()
    {
        var requestData = new
        {
            UserId = _userId,
            TwoFactorCode = _generated2FACode
        };
        
        var contentVerification = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

        var _response = await _client.PostAsync("api/v1/Auth/verify-2fa", contentVerification);

        var responseContent = await _response.Content.ReadAsStringAsync();

        if (_response.StatusCode != HttpStatusCode.BadRequest)
        {
            _response.EnsureSuccessStatusCode();
        }
        else
        {
            throw new Exception($"Verivication failed: {responseContent}");
        }
        
        _response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Then(@"the user should be successfully authenticated")]
    public void ThenTheUserShouldBeSuccessfullyAuthenticated()
    {
        _response.StatusCode.Should().Be(HttpStatusCode.OK);
    }


    [When(@"the user enters a new password")]
    public async Task WhenTheUserEntersANewPassword()
    {
        var newCredentials = new
        {
            OldPassword = _password,
            NewPassword = _newPassword
        };
        
        var contentRegister = new StringContent(JsonConvert.SerializeObject(newCredentials), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync($"api/v1/Auth/change-password/{_userId}", contentRegister);

        if (response.StatusCode != HttpStatusCode.BadRequest)
        {
            response.EnsureSuccessStatusCode();
        }
        else
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Registration failed: {responseContent}");
        }

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
    }

    [Then(@"the user should be able to log in with the new password")]
    public async Task ThenTheUserShouldBeAbleToLogInWithTheNewPassword()
    {
        _password = _newPassword;
        
        var technicalUser = new
        {
            Login = _email,
            Password = _password
        };
        
        var contentRegister = new StringContent(JsonConvert.SerializeObject(technicalUser), Encoding.UTF8, "application/json");

        var _response = await _client.PostAsync("api/v1/Auth/login", contentRegister);

        var responseContent = await _response.Content.ReadAsStringAsync();

        if (_response.StatusCode != HttpStatusCode.BadRequest)
        {
            _response.EnsureSuccessStatusCode();
        }
        else
        {
            throw new Exception($"Registration failed: {responseContent}");
        }
        
        _response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [AfterScenario]
    public async Task AfterScenario()
    {
        if (!string.IsNullOrEmpty(_userId))
        {
            var deleteResponse = await _client.DeleteAsync($"api/v1/Users/{_userId}");
            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}