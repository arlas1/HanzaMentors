using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using App.DAL.Contracts;
using WebApp.Models;

namespace WebApp.Controllers;

public class InternController(IAppBLL bll, UserManager<AppUser> userManager) : Controller
{
    // GET: Intern
    public async Task<IActionResult> Index()
    {
        var userId = Guid.Parse(userManager.GetUserId(User)!);
        
        var interns = await bll.Interns.GetAllAsync(userId);
        return View(interns);
    }

    // GET: Intern/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var intern = await bll.Interns
            .FirstOrDefaultAsync(id.Value);
        if (intern == null)
        {
            return NotFound();
        }

        return View(intern);
    }

    // GET: Intern/Create
    public async Task<IActionResult> Create()
    {
        var interns = await bll.Interns.GetAllAsync();

        var appUserIds = interns.Select(e => e.AppUserId).ToList();

        var viewModel = new InternCreateEditViewModel()
        {
            AppUserSelectList = new SelectList(appUserIds)
        };

        return View(viewModel);
    }

    // POST: Intern/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InternCreateEditViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            bll.Interns.Add(viewModel.Intern);
            await bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.AppUserSelectList = new SelectList(await bll.Users.GetAllAsync(), nameof(AppUser.Id),
            nameof(AppUser.Email), viewModel.Intern.AppUserId);
        
        return View(viewModel);
    }

    // GET: Intern/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var intern = await bll.Interns.FirstOrDefaultAsync(id.Value);
        if (intern == null)
        {
            return NotFound();
        }

        var interns = await bll.Interns.GetAllAsync();
        var appUserIds = interns.Select(e => e.AppUserId).ToList();

        var viewModel = new InternCreateEditViewModel()
        {
            Intern = intern,
            AppUserSelectList = new SelectList(appUserIds)
        };

        return View(viewModel);
    }

    // POST: Intern/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, InternCreateEditViewModel viewModel)
    {
        if (id != viewModel.Intern.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                bll.Interns.Update(viewModel.Intern);
                await bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await bll.Interns.ExistsAsync(viewModel.Intern.Id))
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

        var interns = await bll.Interns.GetAllAsync();
        var appUserIds = interns.Select(e => e.AppUserId).ToList();

        viewModel.AppUserSelectList = new SelectList(appUserIds);
        return View(viewModel);
    }

    // GET: Intern/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var intern = await bll.Interns.FirstOrDefaultAsync(id.Value);
        if (intern == null)
        {
            return NotFound();
        }

        return View(intern);
    }

    // POST: Intern/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await bll.Interns.RemoveAsync(id);
        await bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}