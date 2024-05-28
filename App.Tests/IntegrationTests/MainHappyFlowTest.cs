using System.Net;
using AngleSharp.Html.Dom;
using App.Tests.IntegrationTests.Utils;
using Base.Tests.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace App.Tests.IntegrationTests;

[Collection("NonParallel")]
public class MainHappyFlowTest: IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;
    
    public MainHappyFlowTest(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
            
        });
    }
    
    [Fact]
    public async Task LoginUserAsync()
    {
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = false,
            CookieContainer = new CookieContainer()
        };

        // Create a new HttpClient with the handler
        var client = new HttpClient(handler)
        {
            BaseAddress = new Uri(_factory.Server.BaseAddress.ToString())
        };
        
        const string loginPage = "/Account11/Login1";
        var loginPageResponse = await client.GetAsync(loginPage);
        loginPageResponse.EnsureSuccessStatusCode();

        var loginContent = await HtmlHelpers.GetDocumentAsync(loginPageResponse);
        var loginForm = (IHtmlFormElement)loginContent.QuerySelector("#loginForm")!;

        var loginFormValues = new Dictionary<string, string>
        {
            ["Email"] = "lasimer0406@gmail.com",
            ["Password"] = "Qwe.qwe1",
            ["RememberMe"] = "false"
        };

        var postLoginResponse = await client.SendAsync(loginForm, loginFormValues);
        Assert.Equal(HttpStatusCode.Found, postLoginResponse.StatusCode);     
        
        // -----------------------------------------------------------------------------------
        
        // const string addMentorPage = "/Add/Mentor";
        // var addMentorPageResponse = await _client.GetAsync(addMentorPage);
        // addMentorPageResponse.EnsureSuccessStatusCode();
        
        
        // const string loginPage = "/Account11/Login1";
        // var loginPageResponse = await _client.GetAsync(loginPage);
        // loginPageResponse.EnsureSuccessStatusCode();
        //
        // var loginContent = await HtmlHelpers.GetDocumentAsync(loginPageResponse);
        // var loginForm = (IHtmlFormElement) loginContent.QuerySelector("#loginForm")!;
        //
        // var loginFormValues = new Dictionary<string, string>
        // {
        //     ["Email"] = "lasimer0406@gmail.com",
        //     ["Password"] = "Qwe.qwe1",
        //     ["RememberMe"] = "false",
        // };
        // var postLoginResponse = await _client.SendAsync(loginForm, loginFormValues);
        // Assert.Equal(HttpStatusCode.Redirect, postLoginResponse.StatusCode);       
        //
        // // -----------------------------------------------------------------------------------
        //
        // const string addMentorPage = "/Add/Mentor";
        // var addMentorPageResponse = await _client.GetAsync(addMentorPage);
        // addMentorPageResponse.EnsureSuccessStatusCode();
        
        // var addMentorPageContent = await HtmlHelpers.GetDocumentAsync(addMentorPageResponse);
        // var mentorForm = (IHtmlFormElement) addMentorPageContent.QuerySelector("#mentorForm")!;
        // if (mentorForm == null) throw new ArgumentNullException(nameof(mentorForm));
        //
        // var mentorFormValues = new Dictionary<string, string>
        // {
        //     ["FirstName"] = "testFirstName",
        //     ["LastName"] = "testLastName",
        //     ["Email"] = "testemail@gm.com",
        //     ["PersonalCode"] = "123",
        //     ["Profession"] = "testProfession",
        // };
        // var postAddMentorResponse = await _client.SendAsync(mentorForm, mentorFormValues);
        // Assert.Equal(HttpStatusCode.Redirect, postAddMentorResponse.StatusCode);    
    }

    // [Fact]
    // public async Task AddMentorAsync()
    // {
    //     const string addMentorPage = "/Add/Mentor";
    //     var addMentorPageResponse = await _client.GetAsync(addMentorPage);
    //     addMentorPageResponse.EnsureSuccessStatusCode();
    //     
    //     var addMentorPageContent = await HtmlHelpers.GetDocumentAsync(addMentorPageResponse);
    //     var mentorForm = (IHtmlFormElement) addMentorPageContent.QuerySelector("#mentorForm")!;
    //     
    //     var mentorFormValues = new Dictionary<string, string>
    //     {
    //         ["FirstName"] = "testFirstName",
    //         ["LastName"] = "testLastName",
    //         ["Email"] = "testemail@gm.com",
    //         ["PersonalCode"] = "123",
    //         ["Profession"] = "testProfession",
    //     };
    //     var postAddMentorResponse = await _client.SendAsync(mentorForm, mentorFormValues);
    //     Assert.Equal(HttpStatusCode.Redirect, postAddMentorResponse.StatusCode);  
    // }
}