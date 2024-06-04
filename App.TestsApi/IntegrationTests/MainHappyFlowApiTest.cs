using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Base.Helpers;
using WebApp.DTO;
using WebAppApi.Models;
using Xunit.Abstractions;

namespace App.TestsApi.IntegrationTests;

[Collection("NonParallel")]
public class MainHappyFlowApiTest : IClassFixture<CustomWebAppApiFactory<ProgramApi>>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppApiFactory<ProgramApi> _factory;
    private readonly ITestOutputHelper _testOutputHelper;


    public MainHappyFlowApiTest(CustomWebAppApiFactory<ProgramApi> factory, ITestOutputHelper testOutputHelper)
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
            BaseAddress = new Uri("http://localhost:5232")
        };
    }

    [Fact]
    public async Task IndexWithUser()
    {
        // --------------------------1-Login---------------------------------------------------------
        
        var loginPayload = new StringContent(JsonSerializer.Serialize(new LoginDTO
        {
            Email = "lasimer0406@gmail.com",
            Password = "Qwe.qwe1",
            RememberMe = false
        }), Encoding.UTF8, "application/json");

        var postLogin = await _client.PostAsync("/api/v1.0/AccountApi/Login", loginPayload);
        var loginContent = await postLogin.Content.ReadAsStringAsync();
        postLogin.EnsureSuccessStatusCode();
        
        var loginData = JsonSerializer.Deserialize<JWTResponse>(loginContent, JsonHelper.CamelCase);

        Assert.NotNull(loginData);
        Assert.NotNull(loginData.Jwt);
        Assert.True(loginData.Jwt.Length > 0);
        
        // ------------------------------2-Check-table-with-jwt-----------------------------------------------------
        
        var msg = new HttpRequestMessage(HttpMethod.Post, "/api/v1.0/MenteeApi/EmployeeMentee");
        msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.Jwt);
        msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        msg.Content = new StringContent(JsonSerializer.Serialize(loginData.Jwt), Encoding.UTF8, "application/json");
        
        var response1 = await _client.SendAsync(msg);
        response1.EnsureSuccessStatusCode();

        // ------------------------------3-Add-mentor---------------------------------------------------------------
        
        var addMentorPayload = new StringContent(JsonSerializer.Serialize(new MentorDTO
        {
            FirstName = "testMentorFN",
            LastName = "testMentorLN",
            Email = "testmentorasd@gm.co",
            PersonalCode = 123,
            Profession = "keevitaja"
        }), Encoding.UTF8, "application/json");
        
        var postAddMentor = await _client.PostAsync("/api/v1.0/AddApi/AddMentor", addMentorPayload);
        var addMentorContent = await postAddMentor.Content.ReadAsStringAsync();
        postAddMentor.EnsureSuccessStatusCode();
        
        var responseMentor = JsonSerializer.Deserialize<Public.DTO.v1.Mentor>(addMentorContent, JsonHelper.CamelCase);
        Assert.NotNull(responseMentor);
        Assert.Equal("testMentorFN", responseMentor.FirstName);
        Assert.Equal("testMentorLN", responseMentor.LastName);
        var mentorId = responseMentor.Id;
        
        // ------------------------------3-Add-supervisor-----------------------------------------------------
        
        var addSupervisorPayload = new StringContent(JsonSerializer.Serialize(new SupervisorDTO
        {
            FullName = "testMentorFN",
            Type = "Factory",
        
        }), Encoding.UTF8, "application/json");
        
        var postAddSupervisor = await _client.PostAsync("/api/v1.0/AddApi/AddSupervisor", addSupervisorPayload);
        var addSupervisorContent = await postAddSupervisor.Content.ReadAsStringAsync();
        postAddSupervisor.EnsureSuccessStatusCode();
        
        var responseSupervisor = JsonSerializer.Deserialize<Public.DTO.v1.FactorySupervisor>(addSupervisorContent, JsonHelper.CamelCase);
        Assert.NotNull(responseSupervisor);
        Assert.Equal("testMentorFN", responseSupervisor.FullName);
        var supervisorId = responseSupervisor.Id;
        
        // ------------------------------4-Add-Document-sample-----------------------------------------------------
        
        var filePath = "C:\\Users\\lasim\\Desktop\\sampleMentee.pdf";
        byte[] fileBytes;
        using (var fileStream = File.OpenRead(filePath))
        {
            fileBytes = new byte[fileStream.Length];
            await fileStream.ReadAsync(fileBytes, 0, (int)fileStream.Length);
        }
        
        string base64String = Convert.ToBase64String(fileBytes);
        
        var addDocumentSamplePayload = new StringContent(JsonSerializer.Serialize(new DocumentSampleDTO
        {
            Title = "sampleMentee",
            FileBase64Code = base64String,
            IsTest = "true"
        }), Encoding.UTF8, "application/json");
        
        var postAddDocumentSample = await _client.PostAsync("/api/v1.0/AddApi/AddDocumentSample", addDocumentSamplePayload);
        var addDocumentSampleContent = await postAddDocumentSample.Content.ReadAsStringAsync();
        postAddDocumentSample.EnsureSuccessStatusCode();
        
        var responseDocumentSample = JsonSerializer.Deserialize<Public.DTO.v1.DocumentSample>(addDocumentSampleContent, JsonHelper.CamelCase);
        Assert.NotNull(responseDocumentSample);
        Assert.Equal("sampleMentee", responseDocumentSample.Title);
        
        var documentSampleId = responseDocumentSample.Id;
        
        // ------------------------------5-Add-Mentee-----------------------------------------------------
        
        var addMenteePayload = new StringContent(JsonSerializer.Serialize(new MenteeDTO
        {
            FirstName = "testEmployeefn",
            LastName = "testEmployeeln",
            Email = "testemail@gm.co",
            PersonalCode = 123,
            Profession = "testProfession",
            MenteeType = "Employee",
            EmployeeType = "Full-time",
            InternType = "",
            MenteeFromDate = new DateOnly(2000, 1, 1),
            MenteeUntilDate = new DateOnly(2000, 1, 1),
            MenteeTotalHours = 100,
            MentorFromDate = new DateOnly(3000, 1, 1),
            MentorUntilDate = new DateOnly(3000, 1, 1),
            MentorTotalHours = 100,
            ChosenMentorId = mentorId.ToString(),
            InternFactorySupervisorId = Guid.Empty.ToString(),
            EmployeeFactorySupervisorId = supervisorId.ToString(),
            InternSupervisorId = Guid.Empty.ToString(),
            IsTest = "true"
        }), Encoding.UTF8, "application/json");
        
        var postAddMentee = await _client.PostAsync("/api/v1.0/AddApi/AddMentee", addMenteePayload);
        var addMenteeContent = await postAddMentee.Content.ReadAsStringAsync();
        postAddMentee.EnsureSuccessStatusCode();
        
        var responseEmployee = JsonSerializer.Deserialize<Public.DTO.v1.Employee>(addMenteeContent, JsonHelper.CamelCase);
        Assert.NotNull(responseEmployee); 
        Assert.Equal("testEmployeefn", responseEmployee.FirstName);
        Assert.Equal("testEmployeeln", responseEmployee.LastName);
        var menteeId = responseEmployee.Id;
        
        // ------------------------------6-Assign-document-----------------------------------------------------
        
        var assignDocumentPayload = new StringContent(JsonSerializer.Serialize(new GenerateDoucmentDTO
        {
            MenteeId = menteeId.ToString(),
            SelectedSamples = new List<string>{ documentSampleId.ToString() },
            SelectedMentorId = mentorId.ToString(),
            SigningTimes = new List<string>{ "12 may 11.00", "12 may 12.00" },
            IsTest = "true"
        }), Encoding.UTF8, "application/json");
        
        var postassignDocument = await _client.PostAsync("/api/v1.0/AddApi/GenerateDocumentEmployee", assignDocumentPayload);
        var assignDocumentContent = await postassignDocument.Content.ReadAsStringAsync();
        postassignDocument.EnsureSuccessStatusCode();
        
        var responseDocument = JsonSerializer.Deserialize<Public.DTO.v1.EmployeeMentorshipDocument>(assignDocumentContent, JsonHelper.CamelCase);
        Assert.NotNull(responseDocument);
        Assert.Equal(documentSampleId, responseDocument.DocumentSampleId);
        var responseDocumentId = responseDocument.Id;

        // ------------------------------7-Logout------------------------------------------------------
        
        
        var logout = new HttpRequestMessage(HttpMethod.Post, "/api/v1.0/AccountApi/Logout");
        logout.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.Jwt);
        logout.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        var logoutResponse = await _client.SendAsync(logout);
        logoutResponse.EnsureSuccessStatusCode();
    }
}

