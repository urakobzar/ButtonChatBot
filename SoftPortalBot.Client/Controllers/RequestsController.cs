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
    /// Контроллер заявки.
    /// </summary>
    public class RequestsController : Controller
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly Context _context;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public RequestsController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Веб-страница с записями таблицы.
        /// </summary>
        /// <returns>Веб-страница с записями таблицы.</returns>
        public async Task<IActionResult> Index()
        {
            var context = _context.Requests.Include(
                r => r.Application).Include(r => r.Creator).Include(
                r => r.Executor).Include(r => r.RequestReason).Include(
                r => r.RequestStatus);
            return View(await context.ToListAsync());
        }

        /// <summary>
        /// Редактирование заявки.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Веб-страница редактирования заявки.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ExecutorError = "";
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            var groupId =
                _context.ApplicationResponsibleGroups.First(group =>
                    group.ApplicationId == request.ApplicationId).ResponsibleGroupId;
            var userIds =
                (from item in _context.UserResponsibleGroup
                    where item.ResponsibleGroupId == groupId
                    select item.UserId).ToList();
            var userLogins =
                userIds.Select(i => _context.Users.First(user => user.Id == i));
            ViewData["ApplicationId"] = 
                new SelectList(_context.Applications, "Id", "Name", request.ApplicationId);
            ViewData["CreatorId"] = 
                new SelectList(_context.Users, "Id", "Login", request.CreatorId);
            ViewData["ExecutorId"] = 
                new SelectList(userLogins, "Id", "Login", request.ExecutorId);
            ViewData["RequestReasonId"] = 
                new SelectList(_context.RequestReasons, "Id", "Name", request.RequestReasonId);
            ViewData["StatusId"] = 
                new SelectList(_context.RequestStatus, "Id", "Name", request.StatusId);
            return View(request);
        }

        /// <summary>
        /// Редактирование заявки.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <param name="request">Новая заявка.</param>
        /// <returns>Веб-страница с записями таблицы.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,ApplicationId,CreatorId,ExecutorId,RequestReasonId,StatusId," +
            "CreateTime,ProblemDescription,AttachmentsLink,ComputerNumber")] 
            Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (request.ExecutorId == null)
            {
                ViewBag.ExecutorError = "Необходимо выбрать исполнителя!";
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.Id))
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
            var groupId =
                _context.ApplicationResponsibleGroups.First(group =>
                    group.ApplicationId == request.ApplicationId).ResponsibleGroupId;
            var userIds =
                (from item in _context.UserResponsibleGroup
                    where item.ResponsibleGroupId == groupId
                    select item.UserId).ToList();
            var userLogins =
                userIds.Select(i => _context.Users.First(user => user.Id == i));
            ViewData["ApplicationId"] = 
                new SelectList(_context.Applications, "Id", "Name", request.ApplicationId);
            ViewData["CreatorId"] = 
                new SelectList(_context.Users, "Id", "Login", request.CreatorId);
            ViewData["ExecutorId"] = 
                new SelectList(userLogins, "Id", "Login", request.ExecutorId);
            ViewData["RequestReasonId"] = 
                new SelectList(_context.RequestReasons, "Id", "Name", request.RequestReasonId);
            ViewData["StatusId"] = 
                new SelectList(_context.RequestStatus, "Id", "Name", request.StatusId);
            return View(request);
        }

        /// <summary>
        /// Удаление заявки.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Веб-страница удалением заявки.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Application)
                .Include(r => r.Creator)
                .Include(r => r.Executor)
                .Include(r => r.RequestReason)
                .Include(r => r.RequestStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        /// <summary>
        /// Удаление заявки.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Веб-страница с записями таблицы.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверка на существование записи.
        /// </summary>
        /// <param name="id">ID записи.</param>
        /// <returns>Наличие записи в таблице.</returns>
        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
