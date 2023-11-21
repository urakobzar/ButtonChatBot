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
    /// Контроллер ответа по проблеме.
    /// </summary>
    public class ProblemResponsesController : Controller
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly Context _context;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public ProblemResponsesController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Веб-страница с записями таблицы.
        /// </summary>
        /// <returns>Веб-страница с записями таблицы.</returns>
        public async Task<IActionResult> Index()
        {
            var context 
                = _context.ProblemResponses.Include(k => k.Application);
            return View(await context.ToListAsync());
        }

        /// <summary>
        /// Создание ответа.
        /// </summary>
        /// <returns>Веб-страница создания ответа.</returns>
        public IActionResult Create()
        {
            ViewData["ApplicationId"] 
                = new SelectList(_context.Applications, "Id", "Name");
            return View();
        }

        /// <summary>
        /// Создание ответа.
        /// </summary>
        /// <param name="knowledgeBaseResponse">Новый ответ.</param>
        /// <returns>Веб-страница с записями таблицы.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,ApplicationId,Name,Description,Content")] 
            ProblemResponse knowledgeBaseResponse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knowledgeBaseResponse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationId"] = 
                new SelectList(_context.Applications, "Id", "Name", 
                knowledgeBaseResponse.ApplicationId);
            return View(knowledgeBaseResponse);
        }

        /// <summary>
        /// Редактирование ответа.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Веб-страница редактирования ответа.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProblemResponses == null)
            {
                return NotFound();
            }

            var knowledgeBaseResponse = 
                await _context.ProblemResponses.FindAsync(id);
            if (knowledgeBaseResponse == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "Id", "Name", 
                knowledgeBaseResponse.ApplicationId);
            return View(knowledgeBaseResponse);
        }

        /// <summary>
        /// Редактирование ответа.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <param name="knowledgeBaseResponse">Новый ответ.</param>
        /// <returns>Веб-страница с записями таблицы.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,ApplicationId,Name,Description,Content")] 
            ProblemResponse knowledgeBaseResponse)
        {
            if (id != knowledgeBaseResponse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knowledgeBaseResponse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnowledgeBaseResponseExists(knowledgeBaseResponse.Id))
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
            ViewData["ApplicationId"] = new SelectList(_context.Applications, 
                "Id", "Name", knowledgeBaseResponse.ApplicationId);
            return View(knowledgeBaseResponse);
        }

        /// <summary>
        /// Удаление ответа.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Веб-страница удалением ответа.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProblemResponses == null)
            {
                return NotFound();
            }

            var knowledgeBaseResponse = await _context.ProblemResponses
                .Include(k => k.Application)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowledgeBaseResponse == null)
            {
                return NotFound();
            }

            return View(knowledgeBaseResponse);
        }

        /// <summary>
        /// Удаление ответа.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Веб-страница с записями таблицы.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProblemResponses == null)
            {
                return Problem("Entity set 'Context.KnowledgeBaseResponses'  is null.");
            }
            var knowledgeBaseResponse = await _context.ProblemResponses.FindAsync(id);
            if (knowledgeBaseResponse != null)
            {
                _context.ProblemResponses.Remove(knowledgeBaseResponse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверка на существование записи.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Наличие записи в таблице.</returns>
        private bool KnowledgeBaseResponseExists(int id)
        {
          return (_context.ProblemResponses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
