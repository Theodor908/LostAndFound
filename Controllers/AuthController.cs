using Microsoft.AspNetCore.Mvc;
using LostAndFound.Models;
using AutoMapper;
using LostAndFound.Interfaces;

namespace LostAndFound.Controllers;

public class AccountController(IAuthService authService, IUserService userService) : Controller
{

         [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid) return View(model);

            var (succeeded, errorMessage) = await authService.LoginAsync(
                model.UsernameOrEmail, 
                model.Password, 
                model.RememberMe);

            if (succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            
            ModelState.AddModelError("", errorMessage!);
            return View(model);
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDTO);
            }

            var (succeeded, errors, user) = await authService.RegisterAsync(registerDTO);



            if (succeeded)
            {
                // The service has already saved changes
                return RedirectToAction("Profile", "Account", new { id = user!.Id });
            }

            foreach (var error in errors!)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }

            return View(registerDTO);
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

}
