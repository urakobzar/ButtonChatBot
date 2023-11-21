using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Web;
using System.Drawing.Drawing2D;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SoftPortalBot.Model.DataBaseContext;
using SoftPortalBot.Model.DataBaseTable;
using SoftPortalBot.Client.Models;

namespace SoftPortalBot.Client.Controllers
{
    /// <summary>
    /// Контроллер домашней страницы.
    /// </summary>
    public class HomeController : Controller
    { 
        /// <summary>
        /// Объект логов.
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly Context _context = new Context();

        /// <summary>
        /// Объект веб-окружения.
        /// </summary>
        private readonly IWebHostEnvironment _appEnvironment;

        /// <summary>
        /// Название директории с файлами.
        /// </summary>
        private readonly string _filesDirectoryName = "Files";

        /// <summary>
        /// Название временной директории.
        /// </summary>
        private readonly string _tempDirectoryName = "Temp";

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="logger">Объект логов.</param>
        /// <param name="appEnvironment">Объект веб-окружения.</param>
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
        }

        /// <summary>
        /// Домашняя веб-страница.
        /// </summary>
        /// <returns>Домашняя веб-страница.</returns>
        [Authorize]
        public IActionResult Index()
        {
            if (User != null)
            {
                var filesPath = _appEnvironment.WebRootPath + $"\\{_filesDirectoryName}\\";
                if (!Directory.Exists(filesPath))
                {
                    Directory.CreateDirectory(filesPath);
                }
                FindExistResponses();
                ViewData["ApplicationId"] = 
                    new SelectList(_context.Applications, "Id", "Name");
                return View();
            }
            else
            {
                return Content("Не авторизованный пользователь");
            }
        }

        /// <summary>
        /// Создание заявки.
        /// </summary>
        /// <param name="request">Заявка.</param>
        /// <returns>Веб-страница с виртуальным собеседником.</returns>
        [HttpPost]
        public IActionResult Create(Request request)
        {
            var user = _context.Users.First(u => u.Login == User.Identity.Name);
            request.CreatorId = user.Id;
            request.StatusId = _context.RequestStatus.First(u => u.Name == "На рассмотрении").Id;
            request.CreateTime = DateTime.UtcNow.AddHours(7);
            var pathTemp
                = _appEnvironment.WebRootPath +
                $"\\{_filesDirectoryName}\\{_tempDirectoryName}{user.Login}\\";
            try
            {
                var files = Directory.GetFiles(pathTemp);
                var pathFiles = _appEnvironment.WebRootPath + $"\\{_filesDirectoryName}\\";
                var dateTime = $"{request.CreateTime}";
                var correctTime = dateTime.Replace(':', '\'');
                var newPath = pathFiles + $"{user.Login}_{correctTime}\\";
                Directory.CreateDirectory(newPath);
                for (var i = 0; i < files.Length; i++)
                {
                    var file = Path.GetFileName(files[i]);
                    System.IO.File.Move(files[i], Path.Combine(newPath, file));
                }
                Directory.Delete(pathTemp);
                request.AttachmentsLink = newPath;
            }
            catch
            {
                // ignored
            }

            _context.Requests.Add(request);
            _context.SaveChanges();

            // Рассылка по почте
            //EmailServices.SendEmailCustom(request);

            FindExistResponses();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Загрузка файлов на сервер.
        /// </summary>
        /// <param name="files">Файлы пользователя.</param>
        /// <returns>Веб-страница с виртуальным собеседником.</returns>
        [HttpPost]
        public IActionResult Index(IList<IFormFile> files)
        {
            if (files.Count == 0)
            {
                FindExistResponses();
                return View();
            }
            var user = _context.Users.First(u => u.Login == User.Identity.Name);
            var path =
                this._appEnvironment.WebRootPath +
                $"\\{_filesDirectoryName}\\{_tempDirectoryName}{user.Login}\\";
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }
            else
            {
                Directory.CreateDirectory(path);
            }
            foreach (var source in files)
            {
                var filename =
                    ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');

                filename = this.EnsureCorrectFilename(filename);

                using (var output = System.IO.File.Create(path + filename))
                    source.CopyTo(output);
            }
            FindExistResponses();
            return this.View();
        }

        /// <summary>
        /// Создание корректного имени файла.
        /// </summary>
        /// <param name="filename">Имя файла.</param>
        /// <returns>Корректное имя файла.</returns>
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        /// <summary>
        /// Найти существующие ответы на вопросы.
        /// </summary>
        private void FindExistResponses()
        {
            var listResponses = new List<ProblemResponse>();
            var listAppIds = new List<int>();
            foreach (var item in _context.Applications)
            {
                listAppIds.Add(item.Id);
            }
            foreach (var id in listAppIds)
            {
                foreach (var response in _context.ProblemResponses)
                {

                    if (response.ApplicationId == id)
                    {
                        listResponses.Add(response);
                        break;
                    }
                }

            }

            var listApps = new List<Application>();
            foreach (var item in _context.Applications)
            {
                foreach (var res in listResponses)
                {
                    if (item.Id == res.ApplicationId)
                    {


                        listApps.Add(item);
                        break;
                    }
                }
            }

            ViewBag.AppWithResponse = listApps;
        }

        /// <summary>
        /// Приватность.
        /// </summary>
        /// <returns>Веб-страница приватности.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Ошибка.
        /// </summary>
        /// <returns>Веб-страница ошибки.</returns>
        [ResponseCache(Duration = 0, 
            Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Поиск в базе данных ответов по приложению.
        /// </summary>
        /// <param name="id">ID приложения.</param>
        /// <returns>Ответы по приложению.</returns>
        [HttpPost("api/Chat/{id}")]
        public async Task<JsonResult> Chat(string id)
        {
            try
            {
                var context = new Context();
                var appId = Convert.ToInt32(id);
                var responses = 
                    context.ProblemResponses.Where(item => 
                        item.ApplicationId == appId).ToList();
                return Json(responses);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Поиск в базе данных ответов по приложению из выпадающего списка.
        /// </summary>
        /// <param name="name">Название приложения.</param>
        /// <returns>Ответы по приложению.</returns>
        [HttpPost("api/ChatByList/'{name}'")]
        public async Task<JsonResult> ChatByList(string name)
        {
            try
            {
                var context = new Context();
                var appId = 0;
                foreach (var application in context.Applications)
                {
                    if (application.Name == name)
                    {
                        appId = application.Id;
                        break;
                    }
                }

                var responses =
                    context.ProblemResponses.Where(item =>
                        item.ApplicationId == appId).ToList();
                return Json(responses);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Получить объект ответа по названию.
        /// </summary>
        /// <param name="responseId">ID ответа.</param>
        /// <returns>Объект ответа.</returns>
        [HttpPost("api/Response/{responseId}")]
        public async Task<JsonResult> BotResponse(string responseId)
        {
            try
            {
                var context = new Context();
                var id = Convert.ToInt32(responseId);
                
                var response =
                    context.ProblemResponses.Where(item =>
                        item.Id == id).ToList();
                return Json(response);
            }
            catch
            {
                return null;
            }
        }
    }
}