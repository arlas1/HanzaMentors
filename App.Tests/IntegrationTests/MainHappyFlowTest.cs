using System.Net;
using System.Text.Json;
using AngleSharp.Html.Dom;
using App.BLL.DTO;
using Base.Tests.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace App.Tests.IntegrationTests;

[Collection("NonParallel")]
public class MainHappyFlowTest : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    public MainHappyFlowTest(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _testOutputHelper = testOutputHelper;
        
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = false,
            CookieContainer = new CookieContainer()
        };
        
        _client = new HttpClient(handler)
        {
            BaseAddress = new Uri("http://localhost:5191")
        };
        // -------------------------

        // _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        // _testOutputHelper = testOutputHelper;
        //
        // _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        // {
        //     AllowAutoRedirect = false,
        //     HandleCookies = true
        // });
        
    }

    [Fact]
    public async Task LoginUserAsync()
    {
        const string loginPage = "/Account11/Login1";
        var loginPageResponse = await _client.GetAsync(loginPage);
        loginPageResponse.EnsureSuccessStatusCode();

        var loginContent = await HtmlHelpers.GetDocumentAsync(loginPageResponse);
        var loginForm = (IHtmlFormElement)loginContent.QuerySelector("#loginForm")!;

        var loginFormValues = new Dictionary<string, string>
        {
            ["Email"] = "lasimer0406@gmail.com",
            ["Password"] = "Qwe.qwe1",
            ["RememberMe"] = "false"
        };

        var postLoginResponse = await _client.SendAsync(loginForm, loginFormValues);
        Assert.Equal(HttpStatusCode.Found, postLoginResponse.StatusCode);

        // -----------------------------------------------------------------------------------

        const string addMentorPage = "/Add/Mentor";
        var addMentorPageResponse = await _client.GetAsync(addMentorPage);
        addMentorPageResponse.EnsureSuccessStatusCode();
        
        var addMentorPageContent = await HtmlHelpers.GetDocumentAsync(addMentorPageResponse);
        var mentorForm = (IHtmlFormElement) addMentorPageContent.QuerySelector("#mentorForm")!;
        if (mentorForm == null) throw new ArgumentNullException(nameof(mentorForm));
        
        var mentorFormValues = new Dictionary<string, string>
        {
            ["FirstName"] = "testFirstName1",
            ["LastName"] = "testLastName1",
            ["Email"] = "tasfqwf11@gm.com",
            ["PersonalCode"] = "123",
            ["Profession"] = "testProfession1",
            ["IsTest"] = "true"
        };
        
        var postAddMentorResponse = await _client.SendAsync(mentorForm, mentorFormValues);
        Assert.Equal(HttpStatusCode.OK, postAddMentorResponse.StatusCode);
        
        var mentorContent = await postAddMentorResponse.Content.ReadAsStringAsync();
        var mentor = JsonSerializer.Deserialize<Mentor>(mentorContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        Assert.NotNull(mentor);
        Assert.Equal("testFirstName1", mentor.FirstName);
        Assert.Equal("testLastName1", mentor.LastName);
        Assert.Equal("testProfession1", mentor.Profession);
        
        var mentorId = mentor.Id;
        // -----------------------------------------------------------------------------------
        
        const string addSupervisorPage = "/Add/Supervisor";
        var addSupervisorPageResponse = await _client.GetAsync(addSupervisorPage);
        addSupervisorPageResponse.EnsureSuccessStatusCode();
        
        var addSupervisorPageContent = await HtmlHelpers.GetDocumentAsync(addSupervisorPageResponse);
        var addSupervisorForm = (IHtmlFormElement) addSupervisorPageContent.QuerySelector("#supervisorForm")!;
        if (addSupervisorForm == null) throw new ArgumentNullException(nameof(addSupervisorForm));
        
        var addSupervisorFormValues = new Dictionary<string, string>
        {
            ["FullName"] = "testFullName1",
            ["SupervisorType"] = "Factory",
            ["IsTest"] = "true"
        };
        
        var postAddSupervisorResponce = await _client.SendAsync(addSupervisorForm, addSupervisorFormValues);
        Assert.Equal(HttpStatusCode.OK, postAddSupervisorResponce.StatusCode);
        
        var suprevisorContent = await postAddSupervisorResponce.Content.ReadAsStringAsync();
        var supervisor = JsonSerializer.Deserialize<FactorySupervisor>(suprevisorContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        Assert.NotNull(supervisor);
        Assert.Equal("testFullName1", supervisor.FullName);
        
        var supervisorId = supervisor.Id;
        
        // -----------------------------------------------------------------------------------
        
        const string addMenteePage = "/Add/Mentee";
        var addMenteePageResponse = await _client.GetAsync(addMenteePage);
        addMenteePageResponse.EnsureSuccessStatusCode();
        
        var addMenteePageContent = await HtmlHelpers.GetDocumentAsync(addMenteePageResponse);
        var addMenteeForm = (IHtmlFormElement) addMenteePageContent.QuerySelector("#menteeForm")!;
        if (addMenteeForm == null) throw new ArgumentNullException(nameof(addMenteeForm));
        
        var addMenteeFormValues = new Dictionary<string, string>
        {
            ["MenteeType"] = "Employee",
            ["InternType"] = "a",
            ["InternSupervisorId"] = Guid.Empty.ToString(),
            ["InternFactorySupervisorId"] = Guid.Empty.ToString(),
            ["EmployeeType"] = "Full-time",
            ["EmployeeFactorySupervisorId"] = supervisorId.ToString(),
            ["FirstName"] = "testMenteeFirstName1",
            ["LastName"] = "testMenteeLastName1",
            ["Email"] = "testmenteeemail@gm.co",
            ["PersonalCode"] = "123",
            ["Profession"] = "testMenteeProfession1",
            ["MenteeFromDate"] = "2000-12-12",
            ["MenteeUntilDate"] = "2000-12-12",
            ["MenteeTotalHours"] = "12",
            ["ChosenMentorId"] = mentorId.ToString(),
            ["MentorFromDate"] = "2000-12-12",
            ["MentorUntilDate"] = "2000-12-12",
            ["MentorTotalHours"] = "12",
            ["IsTest"] = "true"
        };
        
        var postAddMenteeResponce = await _client.SendAsync(addMenteeForm, addMenteeFormValues);
        Assert.Equal(HttpStatusCode.OK, postAddMenteeResponce.StatusCode);
        
        var menteeContent = await postAddMenteeResponce.Content.ReadAsStringAsync();
        var mentee = JsonSerializer.Deserialize<Employee>(menteeContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        Assert.NotNull(mentee);
        Assert.Equal("testMenteeFirstName1", mentee.FirstName);
        Assert.Equal("testMenteeProfession1", mentee.Profession);
        var menteeId = mentee.Id;
        
        // -----------------------------------------------------------------------------------
        
        const string addDocumentSamplePage = "/Add/DocumentSample";
        var addDocumentSamplePageResponse = await _client.GetAsync(addDocumentSamplePage);
        addDocumentSamplePageResponse.EnsureSuccessStatusCode();
        
        var addDocumentSamplePageContent = await HtmlHelpers.GetDocumentAsync(addDocumentSamplePageResponse);
        var documentSampleForm = (IHtmlFormElement)addDocumentSamplePageContent.QuerySelector("#documentSampleForm")!;
        if (documentSampleForm == null) throw new ArgumentNullException(nameof(documentSampleForm));
        
        var filePath = "C:\\Users\\lasim\\Desktop\\sampleMentee.pdf";
        byte[] fileBytes;
        using (var fileStream = File.OpenRead(filePath))
        {
            fileBytes = new byte[fileStream.Length];
            await fileStream.ReadAsync(fileBytes, 0, (int)fileStream.Length);
        }
        
        string base64String = Convert.ToBase64String(fileBytes);
        
        var documentSampleFormValues = new Dictionary<string, string>
        {
            ["Title"] = "sampleMentee1",
            ["IsTest"] = "true",
            ["TestBase64Code"] = base64String
        };
        
        var postDocumentSampleResponse = await _client.SendAsync(documentSampleForm, documentSampleFormValues);
        Assert.Equal(HttpStatusCode.OK, postDocumentSampleResponse.StatusCode);
        
        var documentSampleContent = await postDocumentSampleResponse.Content.ReadAsStringAsync();
        var documentSample = JsonSerializer.Deserialize<DocumentSample>(documentSampleContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        Assert.NotNull(documentSample);
        Assert.Equal("sampleMentee1", documentSample.Title);
        var documentSampleId = documentSample.Id;
        
        // ------------------------------------------------------------------------------------
        
        var addDocumentEmployeePage = $"/Add/DocumentEmployee?menteeId={menteeId}";
        var addDocumentemployeePageResponse = await _client.GetAsync(addDocumentEmployeePage);
        addDocumentemployeePageResponse.EnsureSuccessStatusCode();
        
        var addDocumentEmployeePageContent = await HtmlHelpers.GetDocumentAsync(addDocumentemployeePageResponse);
        var addDocumentEmployeeForm = (IHtmlFormElement) addDocumentEmployeePageContent.QuerySelector("#generateDocumentForm")!;
        if (addDocumentEmployeeForm == null) throw new ArgumentNullException(nameof(addDocumentEmployeeForm));
        
        var addDocumentEmployeeFormValues = new Dictionary<string, string>
        {
            ["IsTest"] = "true",
            ["TestMenteeId"] = menteeId.ToString(),
            ["TestSigningTimes"] = "12 may 13.00,12 may 14.00",
            ["TestSelectedSampleId"] = documentSampleId.ToString(),
            ["TestSelectedMentorId"] = mentorId.ToString()
        };
        
        var postAddDocumentEmployeeResponse = await _client.SendAsync(addDocumentEmployeeForm, addDocumentEmployeeFormValues);
        Assert.Equal(HttpStatusCode.OK, postAddDocumentEmployeeResponse.StatusCode);
        
        var generatedDocumentContent = await postAddDocumentEmployeeResponse.Content.ReadAsStringAsync();
        var generatedDocument = JsonSerializer.Deserialize<EmployeeMentorshipDocument>(generatedDocumentContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        Assert.NotNull(generatedDocument);
        Assert.Equal(documentSampleId, generatedDocument.DocumentSampleId);
        
        // ------------------------------------------------------------------------------------
        
        var logoutForm = (IHtmlFormElement) addDocumentEmployeePageContent.QuerySelector("#logout")!;
        var submitElement = Assert.Single(logoutForm.QuerySelectorAll("[type=submit]"));
        var submitButton = Assert.IsAssignableFrom<IHtmlElement>(submitElement);
        
        var logoutAction = await _client.SendLogoutAsync(logoutForm, submitButton);
        Assert.Equal(HttpStatusCode.Redirect, logoutAction.StatusCode);

        var testPage = "/Add/Supervisor";
        var testPageResponse = await _client.GetAsync(testPage);
        Assert.Equal(HttpStatusCode.Redirect, testPageResponse.StatusCode);
    }
}
