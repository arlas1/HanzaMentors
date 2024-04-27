using App.BLL.Contracts;
using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.Controllers;

public class EmployeeController(IAppBLL bll, UserManager<AppUser> userManager) : Controller
{
    // GET: Employee
    public async Task<IActionResult> Index()
    {
        var userId = Guid.Parse(userManager.GetUserId(User)!);
        
        var employees = await bll.Employees.GetAllAsync(userId);
        return View(employees);
    }

    // GET: Employee/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await bll.Employees
            .FirstOrDefaultAsync(id.Value);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // GET: Employee/Create
    public async Task<IActionResult> Create()
    {
        var employees = await bll.Employees.GetAllAsync();

        var appUserIds = employees.Select(e => e.AppUserId).ToList();

        var viewModel = new EmployeeCreateEditViewModel()
        {
            AppUserSelectList = new SelectList(appUserIds)
        };

        return View(viewModel);
    }

    // POST: Employee/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeCreateEditViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            bll.Employees.Add(viewModel.Employee);
            await bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.AppUserSelectList = new SelectList(await bll.Users.GetAllAsync(), nameof(AppUser.Id),
            nameof(AppUser.Email), viewModel.Employee.AppUserId);
        
        return View(viewModel);
    }

    // GET: Employee/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await bll.Employees.FirstOrDefaultAsync(id.Value);
        if (employee == null)
        {
            return NotFound();
        }

        var employees = await bll.Employees.GetAllAsync();
        var appUserIds = employees.Select(e => e.AppUserId).ToList();

        var viewModel = new EmployeeCreateEditViewModel()
        {
            Employee = employee,
            AppUserSelectList = new SelectList(appUserIds)
        };

        return View(viewModel);
    }

    // POST: Employee/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, EmployeeCreateEditViewModel viewModel)
    {
        if (id != viewModel.Employee.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                bll.Employees.Update(viewModel.Employee);
                await bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await bll.Employees.ExistsAsync(viewModel.Employee.Id))
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

        var employees = await bll.Employees.GetAllAsync();
        var appUserIds = employees.Select(e => e.AppUserId).ToList();

        viewModel.AppUserSelectList = new SelectList(appUserIds);
        return View(viewModel);
    }

    // GET: Employee/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await bll.Employees.FirstOrDefaultAsync(id.Value);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // POST: Employee/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await bll.Employees.RemoveAsync(id);
        await bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}