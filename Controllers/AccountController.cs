using Microsoft.AspNetCore.Mvc;
using Examination_System.Repos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Examination_System.Models;


namespace Examination_System.Controllers
{
	public class AccountController : Controller
	{
		readonly IUserRepo userRepo; //user repository

		public AccountController(IUserRepo _userRepo) //constructor
		{
			userRepo = _userRepo;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel login)
		{
			if (ModelState.IsValid)
			{
				var user = userRepo.GetUser(login.UserName, login.UserPass);
				if (user != null)
				{
					ClaimsIdentity identity = new(new[]
					{
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(ClaimTypes.Role, user.UserRole),
					new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()) //add the user id to the claims
				}, CookieAuthenticationDefaults.AuthenticationScheme);
					ClaimsPrincipal principal = new(identity);
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    if(user.UserRole == "Student")
					{
                        return RedirectToAction("Index", "Student");
                    }
                    else if(user.UserRole == "Instructor")
					{
                        return RedirectToAction("EnrollStudent", "Instructor");
                    }
                    else
					{
                        return RedirectToAction("Login");
                    }
				}
				else
				{
					ModelState.AddModelError("", "Invalid Email or Password");
				}
			}

			return View(login);

		}
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Login");
		}
	}
}
