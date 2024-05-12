using System.IO.Compression;
using App.BLL.Contracts;
using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;
using AppUser = App.Domain.Identity.AppUser;
using App.Helpers.EmailService;
using Spire.Pdf;
using Spire.Pdf.Texts;

namespace WebApp.Controllers;

public class AddController(IAppBLL bll, UserManager<AppUser> userManager, IEmailService emailService) : Controller
{
    // private const string emailSubject = "HANZA Mentors account created";
    
    // GET: Add/Mentee
    public IActionResult Mentee()
    {
        var menteeViewModel = new AddMenteeViewModel();
        menteeViewModel.Mentors = bll.Mentors.GetAll();
        menteeViewModel.FactorySupervisors = bll.FactorySupervisors.GetAll();
        menteeViewModel.InternSupervisors = bll.InternSupervisors.GetAll();
        
        return View(menteeViewModel);
    }
        
    // GET: Add/Mentor
    public IActionResult Mentor()
    {
        return View();
    }
    
    // GET: Add/Supervisor
    public IActionResult Supervisor()
    {
        return View();
    }
    
    // GET: Add/DocumentSample
    public IActionResult DocumentSample()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult DocumentIntern(Guid menteeId)
    {
        var documentSamples = bll.DocumentSamples.GetAll()
            .ToDictionary(sample => sample.Id, sample => sample.Title);
        
        var menteesViewModel = new MenteeViewModel
        {
            DocumentSamples = new Dictionary<Guid, string?>(),
            MenteeId = menteeId

        };
        menteesViewModel.DocumentSamples = documentSamples;

        var internMentorship1 = bll.InternMentorships.GetAll()
            .FirstOrDefault(internship => internship.InternId == menteeId);

        if (internMentorship1 != null)
        {
            var internMentorIds = bll.InternsMentors.GetAll()
                .Where(mentor => mentor.InternMentorshipId == internMentorship1.Id)
                .Select(mentor => mentor.MentorId)
                .ToList();

            var mentors = bll.Mentors.GetAll()
                .Where(mentor => internMentorIds.Contains(mentor.Id))
                .ToList();
            
            menteesViewModel.Mentors = mentors;
        }
  
        return View(menteesViewModel);
    }
    
    [HttpGet]
    public IActionResult DocumentEmployee(Guid menteeId)
    {
        var documentSamples = bll.DocumentSamples.GetAll()
            .ToDictionary(sample => sample.Id, sample => sample.Title);
        
        var menteesViewModel = new MenteeViewModel
        {
            DocumentSamples = new Dictionary<Guid, string?>(),
            MenteeId = menteeId

        };
        menteesViewModel.DocumentSamples = documentSamples;

        var employeeMentorship = bll.EmployeeMentorships.GetAll()
            .FirstOrDefault(internship => internship.EmployeeId == menteeId);

        if (employeeMentorship != null)
        {
            var employeeMentorIds = bll.EmployeesMentors.GetAll()
                .Where(mentor => mentor.EmployeeMentorshipId == employeeMentorship.Id)
                .Select(mentor => mentor.MentorId)
                .ToList();

            var mentors = bll.Mentors.GetAll()
                .Where(mentor => employeeMentorIds.Contains(mentor.Id))
                .ToList();
            
            menteesViewModel.Mentors = mentors;
        }
  
        return View(menteesViewModel);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> GenerateDocumentIntern(List<Guid> selectedSamples, Guid selectedMentorId, Guid menteeId, List<string> signingTimes)
    {
        var samples = (await bll.DocumentSamples.GetAllAsync()).Where(sample => selectedSamples.Contains(sample.Id));
        var mentor = await bll.Mentors.FirstOrDefaultAsync(selectedMentorId);
        var mentee = await bll.Interns.FirstOrDefaultAsync(menteeId);
        var menteesMentor = (await bll.InternsMentors.GetAllAsync()).FirstOrDefault(ms => ms.MentorId.Equals(mentor!.Id));
        var mentorship = (await bll.InternMentorships.GetAllAsync()).FirstOrDefault(ms => ms.InternId.Equals(mentee!.Id));

        if (mentor == null || mentee == null || menteesMentor == null || mentorship == null)
        {
            return NotFound();
        }

        var placeholders = new Dictionary<string, string>
        {
            { "%mentorFirstName%", mentor.FirstName! },
            { "%mentorLastName%", mentor.LastName! },
            { "%menteeFirstName%", mentee.FirstName! },
            { "%menteeLastName%", mentee.LastName! },
            { "%mentorFromDate%", menteesMentor.FromDate!.ToString()! },
            { "%mentorUntilDate%", menteesMentor.UntilDate!.ToString()! }
        };

        var tempFolderPath = Path.GetTempPath();
        var randomFileName = $"{Path.GetRandomFileName()}.zip";
        var zipFilePath = Path.Combine(tempFolderPath, randomFileName);

        using (var zipArchive = new ZipArchive(System.IO.File.Create(zipFilePath), ZipArchiveMode.Create))
        {
            foreach (var sampleDocument in samples)
            {
                var decodedDocument = Convert.FromBase64String(sampleDocument.Base64Code!);

                using (var memoryStream = new MemoryStream(decodedDocument))
                {
                    var pdfDocument = new PdfDocument();
                    pdfDocument.LoadFromStream(memoryStream);

                    for (var i = 0; i < pdfDocument.Pages.Count; i++)
                    {
                        var page = pdfDocument.Pages[i];
                        var textReplacer = new PdfTextReplacer(page);
                        var textReplaceOptions = new PdfTextReplaceOptions
                        {
                            ReplaceType = PdfTextReplaceOptions.ReplaceActionType.IgnoreCase |
                                           PdfTextReplaceOptions.ReplaceActionType.WholeWord |
                                           PdfTextReplaceOptions.ReplaceActionType.AutofitWidth
                        };
                        textReplacer.Options = textReplaceOptions;

                        foreach (var placeholder in placeholders)
                        {
                            textReplacer.ReplaceAllText(placeholder.Key, placeholder.Value);
                        }
                    }

                    var pdfFileName = $"{sampleDocument.Title}-{Guid.NewGuid()}.pdf";
                    var pdfPath = Path.Combine(tempFolderPath, pdfFileName);
                    pdfDocument.SaveToFile(pdfPath, FileFormat.PDF);

                    var pdfBytes = await System.IO.File.ReadAllBytesAsync(pdfPath);
                    string base64String = Convert.ToBase64String(pdfBytes);
                    var internsDocument = new InternMentorshipDocument
                    {
                        Id = Guid.NewGuid(),
                        InternMentorshipId = mentorship.Id,
                        DocumentSampleId = sampleDocument.Id,
                        ReceiverId = mentee.Id,
                        Base64Code = base64String,
                        DocumentStatus = "Not signed",
                        ChoosenSigningTime = "Not chosen",
                        WayOfSigning = "Not chosen"
                    };
                    
                    bll.InternMentorshipDocuments.Add(internsDocument);
                    await bll.SaveChangesAsync();
                    
                    foreach (var time in signingTimes)
                    {
                        var signingTime = new DoucmentSigningTime
                        {
                            Id = Guid.NewGuid(),
                            EmployeeMentorshipDocumentId = null,
                            InternMentorshipDocumentId = internsDocument.Id,
                            Time = time
                        };
                        bll.DocumentSigningTimes.Add(signingTime);
                    }
                    
                    await bll.SaveChangesAsync();

                    zipArchive.CreateEntryFromFile(pdfPath, pdfFileName);
                }
            }
        }

        byte[] zipFileBytes = System.IO.File.ReadAllBytes(zipFilePath);
        return File(zipFileBytes, "application/zip", randomFileName);
    }

    [HttpPost]
    public async Task<IActionResult> GenerateDocumentEmployee(List<Guid> selectedSamples, Guid selectedMentorId, Guid menteeId, List<string> signingTimes)
    {
        var samples = (await bll.DocumentSamples.GetAllAsync()).Where(sample => selectedSamples.Contains(sample.Id));
        var mentor = await bll.Mentors.FirstOrDefaultAsync(selectedMentorId);
        var mentee = await bll.Employees.FirstOrDefaultAsync(menteeId);
        var menteesMentor = (await bll.EmployeesMentors.GetAllAsync()).FirstOrDefault(ms => ms.MentorId.Equals(mentor!.Id));
        var mentorship = (await bll.EmployeeMentorships.GetAllAsync()).FirstOrDefault(ms => ms.EmployeeId.Equals(mentee!.Id));

        if (mentor == null || mentee == null || menteesMentor == null || mentorship == null)
        {
            return NotFound();
        }

        var placeholders = new Dictionary<string, string>
        {
            { "%mentorFirstName%", mentor.FirstName! },
            { "%mentorLastName%", mentor.LastName! },
            { "%menteeFirstName%", mentee.FirstName! },
            { "%menteeLastName%", mentee.LastName! },
            { "%mentorFromDate%", menteesMentor.FromDate!.ToString()! },
            { "%mentorUntilDate%", menteesMentor.UntilDate!.ToString()! }
        };

        var tempFolderPath = Path.GetTempPath();
        var randomFileName = $"{Path.GetRandomFileName()}.zip";
        var zipFilePath = Path.Combine(tempFolderPath, randomFileName);

        using (var zipArchive = new ZipArchive(System.IO.File.Create(zipFilePath), ZipArchiveMode.Create))
        {
            foreach (var sampleDocument in samples)
            {
                var decodedDocument = Convert.FromBase64String(sampleDocument.Base64Code!);

                using (var memoryStream = new MemoryStream(decodedDocument))
                {
                    var pdfDocument = new PdfDocument();
                    pdfDocument.LoadFromStream(memoryStream);

                    for (var i = 0; i < pdfDocument.Pages.Count; i++)
                    {
                        var page = pdfDocument.Pages[i];
                        var textReplacer = new PdfTextReplacer(page);
                        var textReplaceOptions = new PdfTextReplaceOptions
                        {
                            ReplaceType = PdfTextReplaceOptions.ReplaceActionType.IgnoreCase |
                                           PdfTextReplaceOptions.ReplaceActionType.WholeWord |
                                           PdfTextReplaceOptions.ReplaceActionType.AutofitWidth
                        };
                        textReplacer.Options = textReplaceOptions;

                        foreach (var placeholder in placeholders)
                        {
                            textReplacer.ReplaceAllText(placeholder.Key, placeholder.Value);
                        }
                    }

                    var pdfFileName = $"{sampleDocument.Title}-{Guid.NewGuid()}.pdf";
                    var pdfPath = Path.Combine(tempFolderPath, pdfFileName);
                    pdfDocument.SaveToFile(pdfPath, FileFormat.PDF);

                    var pdfBytes = await System.IO.File.ReadAllBytesAsync(pdfPath);
                    string base64String = Convert.ToBase64String(pdfBytes);
                    var employeesDocument = new EmployeeMentorshipDocument()
                    {
                        Id = Guid.NewGuid(),
                        EmployeeMentorshipId = mentorship.Id,
                        DocumentSampleId = sampleDocument.Id,
                        ReceiverId = mentee.Id,
                        Base64Code = base64String,
                        DocumentStatus = "Not signed",
                        ChoosenSigningTime = "Not chosen",
                        WayOfSigning = "Not chosen"
                    };
                    
                    bll.EmployeeMentorshipDocuments.Add(employeesDocument);
                    await bll.SaveChangesAsync();
                    
                    foreach (var time in signingTimes)
                    {
                        var signingTime = new DoucmentSigningTime
                        {
                            Id = Guid.NewGuid(),
                            EmployeeMentorshipDocumentId = employeesDocument.Id,
                            InternMentorshipDocumentId = null,
                            Time = time
                        };
                        bll.DocumentSigningTimes.Add(signingTime);
                    }
                    
                    await bll.SaveChangesAsync();
                    
                    zipArchive.CreateEntryFromFile(pdfPath, pdfFileName);
                }
            }
        }

        byte[] zipFileBytes = System.IO.File.ReadAllBytes(zipFilePath);
        return File(zipFileBytes, "application/zip", randomFileName);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddDocumentSample(AddDocumentSampleViewModel documentSampleViewModel)
    {
        Console.WriteLine(documentSampleViewModel.Title);
        Console.WriteLine(documentSampleViewModel.File);

        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
            await documentSampleViewModel.File.CopyToAsync(memoryStream);
            fileBytes = memoryStream.ToArray();
        }

        string base64String = Convert.ToBase64String(fileBytes);

        var documentSample = new DocumentSample
        {
            Id = Guid.NewGuid(),
            Title = documentSampleViewModel.Title,
            Base64Code = base64String
        };

        bll.DocumentSamples.Add(documentSample);
        await bll.SaveChangesAsync();

        return RedirectToAction("Documents", "Document");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddSupervisor(AddSupervisorViewModel supervisorViewModel)
    {
        if (ModelState.IsValid)
        {
            switch (supervisorViewModel.SupervisorType)
            {
                case "Factory":
                    var factorySupervisor = new FactorySupervisor()
                    {
                        FullName = supervisorViewModel.FullName,
                    };
                    bll.FactorySupervisors.Add(factorySupervisor);
                    await bll.SaveChangesAsync();
                    break;
                
                case "Vocational school":
                    var vcSupervisor = new InternSupervisor()
                    {
                        FullName = supervisorViewModel.FullName
                    };
                    bll.InternSupervisors.Add(vcSupervisor);
                    await bll.SaveChangesAsync();
                    break;
            }
        }

        return RedirectToAction("Index", "Home");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddMentor(AddMentorViewModel mentorViewModel)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser()
            {
                Id = Guid.NewGuid(),
                FirstName = mentorViewModel.FirstName,
                LastName = mentorViewModel.LastName,
                Email = mentorViewModel.Email,
                UserName = mentorViewModel.Email,
                PersonalCode = mentorViewModel.PersonalCode,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            
            var userPassword = emailService.GenerateUserPassword();
            var result = await userManager.CreateAsync(user, userPassword);
            if (!result.Succeeded)
            {
                Console.WriteLine("asddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd");
            }
                
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Mentor");
                
                // var emailBody = emailService.GenerateEmailBody(mentorViewModel.FirstName!, mentorViewModel.Email!, userPassword);
                // await emailService.SendEmailAsync(mentorViewModel.Email!, emailSubject, emailBody);
                    
                var employee = new Employee()
                {
                    Id = Guid.NewGuid(),
                    AppUserId = user.Id,
                    FirstName = mentorViewModel.FirstName,
                    LastName = mentorViewModel.LastName,
                    EmployeeType = "Full-time",
                    Profession = mentorViewModel.Profession
                };
                
                bll.Employees.Add(employee);
                await bll.SaveChangesAsync();
        
                if (EmployeeExists(employee.Id))
                {
                    var mentor = new Mentor()
                    {
                        EmployeeId = employee.Id,
                        FirstName = mentorViewModel.FirstName,
                        LastName = mentorViewModel.LastName,
                        PaymentAmount = null,
                        PaymentOrderDate = null,
                        Profession = mentorViewModel.Profession
                    };
            
                    bll.Mentors.Add(mentor);
                    await bll.SaveChangesAsync();    
                }
            }
        }

        return RedirectToAction("Index", "Mentor");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddMentee(AddMenteeViewModel menteeViewModel)
    {
        var user = new AppUser()
        {
            Id = Guid.NewGuid(),
            FirstName = menteeViewModel.FirstName,
            LastName = menteeViewModel.LastName,
            Email = menteeViewModel.Email,
            UserName = menteeViewModel.Email,
            PersonalCode = menteeViewModel.PersonalCode,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        var userPassword = emailService.GenerateUserPassword();
        var result = await userManager.CreateAsync(user, userPassword);
        if (!result.Succeeded)
        {
            Console.WriteLine("Faaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaail");
        }
        
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Mentee");
        
            // var emailBody = emailService.GenerateEmailBody(menteeViewModel.FirstName!, menteeViewModel.Email!, userPassword);
            // await emailService.SendEmailAsync(menteeViewModel.Email!, emailSubject, emailBody);
        
            switch (menteeViewModel.MenteeType)
            {
                case "Intern":
                    var intern = new Intern()
                    {
                        Id = Guid.NewGuid(),
                        AppUserId = user.Id,
                        FirstName = menteeViewModel.FirstName,
                        LastName = menteeViewModel.LastName,
                        InternType = menteeViewModel.InternType,
                        StudyProgram = menteeViewModel.Profession
                    };
                    
                    bll.Interns.Add(intern);
                    await bll.SaveChangesAsync();
        
                    var internMentorship = new InternMentorship()
                    {
                        Id = Guid.NewGuid(),
                        InternId = intern.Id,
                        InternSupervisorId = menteeViewModel.InternSupervisorId,
                        FactorySupervisorId = menteeViewModel.InternFactorySupervisorId,
                        FromDate = menteeViewModel.MenteeFromDate,
                        UntilDate = menteeViewModel.MenteeUntilDate,
                        TotalHours = menteeViewModel.MenteeTotalHours
                    };
                    
                    bll.InternMentorships.Add(internMentorship);
                    await bll.SaveChangesAsync();

                    var internsMentor = new InternsMentor
                    {
                        Id = Guid.NewGuid(),
                        MentorId = menteeViewModel.ChosenMentorId,
                        InternMentorshipId = internMentorship.Id,
                        FromDate = menteeViewModel.MentorFromDate,
                        UntilDate = menteeViewModel.MentorUntilDate,
                        TotalHours = menteeViewModel.MentorTotalHours,
                        IsCurrentlyActive = true,
                        ChangeReason = null
                    };
                    
                    bll.InternsMentors.Add(internsMentor);
                    await bll.SaveChangesAsync();
                    
                    break;
                
                case "Employee":
                    var employee = new Employee
                    {
                        Id = Guid.NewGuid(),
                        AppUserId = user.Id,
                        FirstName = menteeViewModel.FirstName,
                        LastName = menteeViewModel.LastName,
                        EmployeeType = menteeViewModel.EmployeeType,
                        Profession = menteeViewModel.Profession,
                    };
                    
                    bll.Employees.Add(employee);
                    await bll.SaveChangesAsync();
                    
                    
                    var employeeMentorship = new EmployeeMentorship()
                    {
                        Id = Guid.NewGuid(),
                        EmployeeId = employee.Id,
                        FactorySupervisorId = menteeViewModel.EmployeeFactorySupervisorId,
                        FromDate = menteeViewModel.MenteeFromDate,
                        UntilDate = menteeViewModel.MenteeUntilDate,
                        TotalHours = menteeViewModel.MenteeTotalHours
                    };
                    
                    bll.EmployeeMentorships.Add(employeeMentorship);
                    await bll.SaveChangesAsync();

                    var employeesMentor = new EmployeesMentor
                    {
                        Id = Guid.NewGuid(),
                        MentorId = menteeViewModel.ChosenMentorId,
                        EmployeeMentorshipId = employeeMentorship.Id,
                        FromDate = menteeViewModel.MentorFromDate,
                        UntilDate = menteeViewModel.MentorUntilDate,
                        TotalHours = menteeViewModel.MentorTotalHours,
                        IsCurrentlyActive = true,
                        ChangeReason = null
                    };

                    bll.EmployeesMentors.Add(employeesMentor);
                    await bll.SaveChangesAsync();
                    
                    break;
            }
        }
        
        return RedirectToAction("Index", "Mentor");
    }
        
    private bool EmployeeExists(Guid id)
    {
        return bll.Employees.Exists(id);
    }
}