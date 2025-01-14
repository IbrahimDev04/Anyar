﻿using Anyar.DataAccessLayer;
using Anyar.Enums;
using Anyar.Models;
using Anyar.ViewModels.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Anyar.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManger) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {

            if(!ModelState.IsValid) return View(vm);

            AppUser user = new AppUser
            {
                Name = vm.Name,
                Email = vm.Email,
                Surname = vm.Surname,
                UserName = vm.Username
            };

            IdentityResult result = await _userManager.CreateAsync(user, vm.Password);

            if(!result.Succeeded) 
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
                return View(vm);
            }

            await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());

            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if(!ModelState.IsValid) return View(vm);

            AppUser user = await _userManager.FindByNameAsync(vm.UserNameOrEmail);

            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(vm.UserNameOrEmail);
                if(user == null)
                {
                    ModelState.AddModelError("", "Username or Password is wrong.");
                }
                
            }

            

            if (!ModelState.IsValid) return View(vm);

            await _signInManager.CheckPasswordSignInAsync(user, vm.Password,true);
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "You try more time. In that case, you must wait for " + user.LockoutEnd.Value.ToString("HH:mm:ss"));
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> CreateRoles()
        {


            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                if (!await _roleManger.RoleExistsAsync(role.ToString()))
                {
                    await _roleManger.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
            }

            return Content("Ok");


        }
    }
}
