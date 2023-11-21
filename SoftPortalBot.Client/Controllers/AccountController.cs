using System.Collections.Generic;
using SoftPortalBot.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using SoftPortalBot.Model.DataBaseContext;
using SoftPortalBot.Model.DataBaseTable;
using Microsoft.EntityFrameworkCore;
using SoftPortalBot.Client.Models;

namespace SoftPortalBot.Client.Controllers
{
    /// <summary>
    /// Контроллер аккаунта.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly Context _context;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public AccountController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <returns>Веб-страница авторизации.</returns>
        [HttpGet]
        public async Task<ViewResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Веб-страница авторизации.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = 
                    await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        /// <summary>
        /// Аутентификация пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>Аутентификация.</returns>
        private async Task Authenticate(User user)
        {
            RoleUser roleUser = 
                await _context.RoleUsers.FirstOrDefaultAsync(u => u.UserId == user.Id);
            Role role =
                await _context.Roles.FirstOrDefaultAsync(u => u.Id == roleUser.RoleId);

            // Создаем один claim.
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name)
            };

            // Создаем объект ClaimsIdentity.
            ClaimsIdentity id = new(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            // Установка аутентификационных кук.
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        /// <summary>
        /// Выйти из аккаунта.
        /// </summary>
        /// <returns>Веб-страница авторизации.</returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
