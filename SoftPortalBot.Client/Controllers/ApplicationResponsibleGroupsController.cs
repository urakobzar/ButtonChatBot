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
    /// Контроллер связи между приложением и ответственной группой.
    /// </summary>
    public class ApplicationResponsibleGroupsController : Controller
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly Context _context;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public ApplicationResponsibleGroupsController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Веб-страница с записями таблицы.
        /// </summary>
        /// <returns>Веб-страница с записями таблицы.</returns>
        public async Task<IActionResult> Index()
        {
            var context = 
                _context.ApplicationResponsibleGroups.Include(
                    a => a.Application).Include(a => a.ResponsibleGroup);
            return View(await context.ToListAsync());
        }        

        /// <summary>
        /// Создание связи.
        /// </summary>
        /// <returns>Веб-страница создания связи.</returns>
        public IActionResult Create()
        {
            ViewData["ApplicationId"] = 
                new SelectList(_context.Applications, "Id", "Name");
            ViewData["ResponsibleGroupId"] = 
                new SelectList(_context.ResponsibleGroups, "Id", "Name");
            return View();
        }

        /// <summary>
        /// Создание связи.
        /// </summary>
        /// <param name="applicationResponsibleGroup">Новая связь.</param>
        /// <returns>Веб-страница с записями таблицы.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,ApplicationId,ResponsibleGroupId")] 
            ApplicationResponsibleGroup applicationResponsibleGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(applicationResponsibleGroup);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ViewBag.ConstraintError = "Повторная запись!";
            }
            ViewData["ApplicationId"] = 
                new SelectList(_context.Applications, "Id", "Name", 
                applicationResponsibleGroup.ApplicationId);
            ViewData["ResponsibleGroupId"] = 
                new SelectList(_context.ResponsibleGroups, "Id", "Name", 
                applicationResponsibleGroup.ResponsibleGroupId);
            return View(applicationResponsibleGroup);
        }

        /// <summary>
        /// Редактирование связи.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Веб-страница редактирования связи.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ConstraintError = "";
            if (id == null || _context.ApplicationResponsibleGroups == null)
            {
                return NotFound();
            }

            var applicationResponsibleGroup = 
                await _context.ApplicationResponsibleGroups.FindAsync(id);
            if (applicationResponsibleGroup == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "Id", "Name", 
                applicationResponsibleGroup.ApplicationId);
            ViewData["ResponsibleGroupId"] = new SelectList(_context.ResponsibleGroups, "Id", "Name", 
                applicationResponsibleGroup.ResponsibleGroupId);
            return View(applicationResponsibleGroup);
        }

        /// <summary>
        /// Редактирование связи.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <param name="applicationResponsibleGroup">Новая связь.</param>
        /// <returns>Веб-страница с записями таблицы.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,ApplicationId,ResponsibleGroupId")] 
            ApplicationResponsibleGroup applicationResponsibleGroup)
        {
            if (id != applicationResponsibleGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationResponsibleGroup);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewBag.ConstraintError = "Повторная запись!";
                }
                
            }
            ViewData["ApplicationId"] = 
                new SelectList(_context.Applications, "Id", "Name", 
                applicationResponsibleGroup.ApplicationId);
            ViewData["ResponsibleGroupId"] = 
                new SelectList(_context.ResponsibleGroups, "Id", "Name", 
                applicationResponsibleGroup.ResponsibleGroupId);
            return View(applicationResponsibleGroup);
        }

        /// <summary>
        /// Удаление связи.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Веб-страница удалением связи.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ApplicationResponsibleGroups == null)
            {
                return NotFound();
            }

            var applicationResponsibleGroup = 
                await _context.ApplicationResponsibleGroups
                .Include(a => a.Application)
                .Include(a => a.ResponsibleGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationResponsibleGroup == null)
            {
                return NotFound();
            }

            return View(applicationResponsibleGroup);
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
            if (_context.ApplicationResponsibleGroups == null)
            {
                return Problem("Entity set 'Context.ApplicationResponsibleGroups'  is null.");
            }
            var applicationResponsibleGroup = 
                await _context.ApplicationResponsibleGroups.FindAsync(id);
            if (applicationResponsibleGroup != null)
            {
                _context.ApplicationResponsibleGroups.Remove(applicationResponsibleGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверка на существование записи.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Наличие записи в таблице.</returns>
        private bool ApplicationResponsibleGroupExists(int id)
        {
          return (_context.ApplicationResponsibleGroups?.Any(
              e => e.Id == id)).GetValueOrDefault();
        }
    }
}
