using Library_Management_System.Entities;
using Library_Management_System.Models;
using Library_Management_System.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library_Management_System.Controllers
{
    public class AuthController : Controller
    {
        // Static list to simulate user data storage.
        private static List<UserEntity> _users = new List<UserEntity>()
        {
            new UserEntity { Id = 1, Email=".", Password=".", FullName=".", JoinDate= new DateTime(1999,01,01), PhoneNumber= "." }
        };

        private readonly IDataProtector _dataProtector;

        // Constructor to initialize data protector for password encryption.
        public AuthController(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }

        // Displays the sign-up form.
        [HttpGet]
        public IActionResult SignUp()
        {
            // Redirects to home page if user is already logged in.
            if (@User.Claims.FirstOrDefault(x => x.Type == "id")?.Value != null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            return View();
        }

        // Handles the sign-up form submission.
        [HttpPost]
        public IActionResult SignUp(AuthViewModel formData)
        {
            // Validates form data; if invalid, re-displays the form.
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            // Checks if the user already exists.
            var user = _users.FirstOrDefault(x => x.Email.ToLower() == formData.Email.ToLower());

            if (user is not null)
            {
                ModelState.AddModelError(string.Empty, "User already exists with this email.");
                return View(formData);
            }

            // Creates a new user entity and encrypts the password.
            var newUser = new UserEntity()
            {
                Id = _users.Max(x => x.Id) + 1,
                Email = formData.Email.ToLower(),
                Password = _dataProtector.Protect(formData.Password),
                FullName = formData.FullName,
                PhoneNumber = formData.PhoneNumber
            };

            // Adds the new user to the list.
            _users.Add(newUser);

            return RedirectToAction("LogIn");
        }

        // Displays the login form.
        [HttpGet]
        public IActionResult LogIn()
        {
            // Redirects to home page if user is already logged in.
            if (@User.Claims.FirstOrDefault(x => x.Type == "id")?.Value != null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            return View();
        }

        // Handles the login form submission.
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel formData)
        {
            // Validates form data; if invalid, re-displays the form.
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            // Finds the user by email.
            var user = _users.FirstOrDefault(x => x.Email.ToLower() == formData.Email.ToLower());

            if (user is null)
            {
                ViewBag.Error = "Wrong username or password";
                return View();
            }

            // Decrypts the password and checks if it matches.
            var rawPassword = _dataProtector.Unprotect(user.Password);

            if (rawPassword == formData.Password)
            {
                // Creates a list of claims for the authenticated user.
                var claims = new List<Claim>
                {
                    new Claim("email", user.Email),
                    new Claim("id", user.Id.ToString()),
                    new Claim("FullName", user.FullName.ToUpper()),
                    new Claim("joinDate", user.JoinDate.ToShortDateString())
                };

                // Creates claims identity and authentication properties.
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48))
                };

                // Signs in the user.
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);

                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                ViewBag.Error = "Wrong username or password";
                return View();
            }
        }

        // Handles user sign-out.
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
