using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftPortalBot.Model.DataBaseContext;
using SoftPortalBot.Model.DataBaseTable;

namespace SoftPortalBot.Client.Controllers
{
    /// <summary>
    /// Контроллер связи между пользователем и ответственной группой.
    /// </summary>
    public class UserResponsibleGroupsController : Controller
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly Context _context;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public UserResponsibleGroupsController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Веб-страница с записями таблицы.
        /// </summary>
        /// <returns>Веб-страница с записями таблицы.</returns>
        public async Task<IActionResult> Index()
        {
            var context = _context.UserResponsibleGroup.Include(
                u => u.ResponsibleGroup).Include(u => u.User);
            return View(await context.ToListAsync());
        }

        /// <summary>
        /// Создание связи.
        /// </summary>
        /// <returns>Веб-страница создания связи.</returns>
        public IActionResult Create()
        {
            ViewBag.ConstraintError = "";
            ViewData["ResponsibleGroupId"] = new SelectList(_context.ResponsibleGroups, "Id", "Name");
            var list = new SelectList(_context.Users, "Id", "Surname");
            var userId = 0;
            foreach (var item in list)
            {
                userId = Convert.ToInt32(item.Value);
                var user = _context.Users.First(u => u.Id == userId);
                item.Text += " " + user.Name + " " + user.Patronymic;
            }
            ViewData["UserId"] = list;
            return View();
        }

        /// <summary>
        /// Создание связи.
        /// </summary>
        /// <param name="userResponsibleGroup">Новая связь.</param>
        /// <returns>Веб-страница с записями таблицы.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,UserId,ResponsibleGroupId")] UserResponsibleGroup userResponsibleGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(userResponsibleGroup);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ViewBag.ConstraintError = "Повторная запись!";
            }

            ViewData["ResponsibleGroupId"] =
                new SelectList(_context.ResponsibleGroups, "Id", "Name", 
                userResponsibleGroup.ResponsibleGroupId);
            var list = new SelectList(_context.Users, "Id", "Surname", 
                userResponsibleGroup.UserId);
            var userId = 0;
            foreach (var item in list)
            {
                userId = Convert.ToInt32(item.Value);
                var user = _context.Users.First(u => u.Id == userId);
                item.Text += " " + user.Name + " " + user.Patronymic;
            }
            ViewData["UserId"] = list;
            return View(userResponsibleGroup);
        }

        /// <summary>
        /// Редактирование связи.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Веб-страница редактирования связи.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ConstraintError = "";
            if (id == null || _context.UserResponsibleGroup == null)
            {
                return NotFound();
            }

            var userResponsibleGroup = await _context.UserResponsibleGroup.FindAsync(id);
            if (userResponsibleGroup == null)
            {
                return NotFound();
            }
            ViewData["ResponsibleGroupId"] = 
                new SelectList(_context.ResponsibleGroups, "Id", "Name", 
                userResponsibleGroup.ResponsibleGroupId);
            var list = new SelectList(_context.Users, "Id", "Surname", userResponsibleGroup.UserId);
            var userId = 0;
            foreach (var item in list)
            {
                userId = Convert.ToInt32(item.Value);
                var user = _context.Users.First(u => u.Id == userId);
                item.Text += " " + user.Name + " " + user.Patronymic;
            }
            ViewData["UserId"] = list;
            return View(userResponsibleGroup);
        }

        /// <summary>
        /// Редактирование связи.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <param name="userResponsibleGroup">Новая связь.</param>
        /// <returns>Веб-страница с записями таблицы.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,UserId,ResponsibleGroupId")] 
            UserResponsibleGroup userResponsibleGroup)
        {
            if (id != userResponsibleGroup.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userResponsibleGroup);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewBag.ConstraintError = "Повторная запись!";
                }
            }
            ViewData["ResponsibleGroupId"] = 
                new SelectList(_context.ResponsibleGroups, "Id", "Name", 
                userResponsibleGroup.ResponsibleGroupId);
            var list = new SelectList(_context.Users, "Id", "Surname", 
                userResponsibleGroup.UserId);
            var userId = 0;
            foreach (var item in list)
            {
                userId = Convert.ToInt32(item.Value);
                var user = _context.Users.First(u => u.Id == userId);
                item.Text += " " + user.Name + " " + user.Patronymic;
            }
            ViewData["UserId"] = list;
            return View(userResponsibleGroup);
        }

        /// <summary>
        /// Удаление связи.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Веб-страница удалением связи.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserResponsibleGroup == null)
            {
                return NotFound();
            }

            var userResponsibleGroup = await _context.UserResponsibleGroup
                .Include(u => u.ResponsibleGroup)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userResponsibleGroup == null)
            {
                return NotFound();
            }
            return View(userResponsibleGroup);
        }

        /// <summary>
        /// Удаление связи.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Веб-страница с записями таблицы.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserResponsibleGroup == null)
            {
                return Problem("Entity set 'Context.UserResponsibleGroup'  is null.");
            }
            var userResponsibleGroup = await _context.UserResponsibleGroup.FindAsync(id);
            if (userResponsibleGroup != null)
            {
                _context.UserResponsibleGroup.Remove(userResponsibleGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверка на существование записи.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Наличие записи в таблице.</returns>
        private bool UserResponsibleGroupExists(int id)
        {
          return (_context.UserResponsibleGroup?.Any(
              e => e.Id == id)).GetValueOrDefault();
        }
    }
}
