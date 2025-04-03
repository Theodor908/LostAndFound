using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LostAndFound.Entities;
using LostAndFound.Models;
using AutoMapper;

namespace LostAndFound.Controllers;

public class AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper) : Controller
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

            var user = await userManager.FindByEmailAsync(model.UsernameOrEmail) ?? await userManager.FindByNameAsync(model.UsernameOrEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            
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

            var existingUser = await userManager.FindByNameAsync(registerDTO.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "Username already in use.");
                return View(registerDTO);
            }

            existingUser = await userManager.FindByEmailAsync(registerDTO.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email already in use.");
                return View(registerDTO);
            }

            var user = new AppUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email,
                City = registerDTO.City,
                Country = registerDTO.Country
            };

            var result = await userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                await signInManager.PasswordSignInAsync(user, registerDTO.Password, isPersistent: true, lockoutOnFailure: false);
                return RedirectToAction("Profile", "Account", new { id = user.Id });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerDTO);
        }

        [HttpGet]
        public IActionResult Profile(int id)
        {
            var user = userManager.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var model = mapper.Map<UserDTO>(user);
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

}
