using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class Account11Controller(SignInManager<AppUser> signInManager) : Controller
{
    [HttpGet]
    public IActionResult Login1()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login11(LoginViewModel loginViewModel)
    {
        var signIn = await signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);

        return RedirectToAction("Index", "Home");
    }
}