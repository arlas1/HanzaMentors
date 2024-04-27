using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.Controllers;

public class MentorController(IAppBLL bll, UserManager<AppUser> userManager) : Controller
{
    // GET: Mentor
    public async Task<IActionResult> Index()
    {
        var userId = Guid.Parse(userManager.GetUserId(User)!);
        
        var mentors = await bll.Mentors.GetAllAsync(userId);
        return View(mentors);
    }

    // GET: Mentor/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var mentor = await bll.Mentors
            .FirstOrDefaultAsync(id.Value);
        if (mentor == null)
        {
            return NotFound();
        }

        return View(mentor);
    }

    // GET: Mentor/Create
    public async Task<IActionResult> Create()
    {
        var viewModel = new MentorCreateEditViewModel()
        {
            EmployeeSelectList = new SelectList(await bll.Mentors.GetAllAsync(), nameof(Employee.Id)),
            InternMentorshipSelectList = new SelectList(await bll.Mentors.GetAllAsync(), nameof(InternMentorship.Id)),
            EmployeeMentorshipSelectList = new SelectList(await bll.Mentors.GetAllAsync(), nameof(EmployeeMentorship.Id))
        };

        return View(viewModel);
    }

    // POST: Mentor/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MentorCreateEditViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            bll.Mentors.Add(viewModel.Mentor);
            await bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // viewModel.AppUserSelectList = new SelectList(await bll.Users.GetAllAsync(), nameof(AppUser.Id),
        //     nameof(AppUser.Email), viewModel.Mentor.EmployeeId);
        viewModel.EmployeeSelectList = new SelectList(await bll.Mentors.GetAllAsync(), nameof(Employee.Id),
            nameof(AppUser.Email), viewModel.Mentor.EmployeeId);
        viewModel.InternMentorshipSelectList = new SelectList(await bll.Mentors.GetAllAsync(), nameof(InternMentorship.Id),
            nameof(AppUser.Email), viewModel.Mentor.EmployeeId);
        viewModel.EmployeeMentorshipSelectList = new SelectList(await bll.Mentors.GetAllAsync(), nameof(EmployeeMentorship.Id),
            nameof(AppUser.Email), viewModel.Mentor.EmployeeId);
        
        return View(viewModel);
    }

    // GET: Mentor/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var mentor = await bll.Mentors.FirstOrDefaultAsync(id.Value);
        if (mentor == null)
        {
            return NotFound();
        }

        var viewModel = new MentorCreateEditViewModel()
        {
            Mentor = mentor,
            EmployeeSelectList = new SelectList(await bll.Mentors.GetAllAsync(), nameof(Employee.Id),
                nameof(AppUser.Email), mentor.EmployeeId),
            InternMentorshipSelectList = new SelectList(await bll.Mentors.GetAllAsync(), nameof(InternMentorship.Id),
                nameof(AppUser.Email), mentor.EmployeeId),
            EmployeeMentorshipSelectList = new SelectList(await bll.Mentors.GetAllAsync(), nameof(EmployeeMentorship.Id),
                nameof(AppUser.Email), mentor.EmployeeId)
        };

        return View(viewModel);
    }

    // POST: Mentor/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id,MentorCreateEditViewModel viewModel)
    {
        if (id != viewModel.Mentor.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                bll.Mentors.Update(viewModel.Mentor);
                await bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await bll.Mentors.ExistsAsync(viewModel.Mentor.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));

        }
        viewModel.EmployeeSelectList = new SelectList(await bll.Mentors.GetAllAsync(), nameof(Employee.Id),
            nameof(AppUser.Email), viewModel.Mentor.EmployeeId);
        viewModel.InternMentorshipSelectList = new SelectList(await bll.Mentors.GetAllAsync(), nameof(InternMentorship.Id),
            nameof(AppUser.Email), viewModel.Mentor.EmployeeId);
        viewModel.EmployeeMentorshipSelectList = new SelectList(await bll.Mentors.GetAllAsync(), nameof(EmployeeMentorship.Id),
            nameof(AppUser.Email), viewModel.Mentor.EmployeeId);
        return View(viewModel);
    }

    // GET: Mentor/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await bll.Mentors.FirstOrDefaultAsync(id.Value);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // POST: Mentor/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await bll.Mentors.RemoveAsync(id);
        await bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}