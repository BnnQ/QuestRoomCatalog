using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework.Data;
using Homework.Data.Entities;
using Homework.ViewModels.Admin;
using Homework.Services.Abstractions;
using AutoMapper;
using Homework.Models;

namespace Homework.Controllers
{
    public class AdminController : Controller
    {
        private readonly QuestRoomContext context;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment environment;
        private readonly ILogger logger;

        public AdminController(QuestRoomContext context, IMapper mapper, IWebHostEnvironment environment, ILoggerFactory loggerFactory)
        {
            this.context = context;
            this.mapper = mapper;
            this.environment = environment;
            logger = loggerFactory.CreateLogger<AdminController>();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            logger.LogInformation("Index page requested");
            return View(await context.QuestRooms.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateQuestRoomViewModel(new QuestRoomDto(), GetGenres(), GetCompanyNames()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateQuestRoomViewModel questRoomVideModel, IFormFile? logo, [FromServices] IFormImageProcessor imageProcessor)
        {
            if (ModelState.IsValid)
            {
                QuestRoom createdQuestRoom = mapper.Map<QuestRoom>(questRoomVideModel.QuestRoom);
                if (logo is not null)
                {
                    createdQuestRoom.PathToLogo = imageProcessor.Process(logo);
                    logger.LogInformation($@"Saved logo ""{logo.FileName}"" for created QuestRoom");
                }

                try
                {
                    await context.AddAsync(createdQuestRoom);
                    await context.SaveChangesAsync();
                    logger.LogInformation($"Successfully created QuestRoom with ID {createdQuestRoom.Id}");
                }
                catch (Exception exception)
                {
                    logger.LogError($"Failed to create QuestRoom: {exception.Message}");
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                string errorMessage = $"The entered data contains {ModelState.ErrorCount} errors";
                logger.LogError($"Failed to create QuestRoom: {errorMessage}", ModelState);
                return Problem(errorMessage, statusCode: 404);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null || context.QuestRooms is null)
            {
                logger.LogError("Failed to request edit page: passed ID is null or DB QuestRooms table is empty");
                return NotFound();
            }

            var questRoom = await context.QuestRooms.FindAsync(id);
            if (questRoom is null)
            {
                logger.LogError($"Failed to return requested edit page: QuestRoom by ID {id} not found in DB");
                return NotFound();
            }

            logger.LogInformation($"Successfully requested edit page for QuestRoom with ID {id}");
            return View(new EditQuestRoomViewModel(questRoom, GetGenres(), GetCompanyNames()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(QuestRoom questRoom, IFormFile? logo, [FromServices] IFormImageProcessor imageProcessor)
        {
            if (ModelState.IsValid)
            {
                if (logo is not null)
                {
                    if (LogoExists(questRoom.PathToLogo))
                    {
                        DeleteLogo(questRoom.PathToLogo!);
                        logger.LogInformation($@"Deleted QuestRoom logo ""{questRoom.PathToLogo}""");
                    }

                    questRoom.PathToLogo = imageProcessor.Process(logo);
                    logger.LogInformation($@"Saved new logo ""{logo.FileName}"" for QuestRoom with ID {questRoom.Id}");
                }

                try
                {
                    context.Update(questRoom);
                    await context.SaveChangesAsync();
                    logger.LogInformation($"Successfully updated QuestRoom with ID {questRoom.Id}");
                }
                catch (DbUpdateConcurrencyException exception)
                {
                    logger.LogError($"Failed to update QuestRoom with ID {questRoom.Id}: {exception.Message}");
                    if (!QuestRoomExists(questRoom.Id))
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
            return View(questRoom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (context.QuestRooms is null)
            {
                string errorMessage = "Entity set 'QuestRoomContext.QuestRooms' is null.";
                logger.LogError($"Failed to delete QuestRoom with ID {id}: {errorMessage}");
                return Problem(errorMessage);
            }
            var questRoom = await context.QuestRooms.FindAsync(id);
            if (questRoom is not null)
            {
                if (LogoExists(questRoom.PathToLogo))
                {
                    DeleteLogo(questRoom.PathToLogo!);
                    logger.LogInformation($@"Deleted QuestRoom logo ""{questRoom.PathToLogo}""");
                }

                try
                {
                    context.QuestRooms.Remove(questRoom);
                    await context.SaveChangesAsync();
                    logger.LogInformation($"Successfully deleted QuestRoom with ID {id}");
                }
                catch (Exception exception)
                {
                    logger.LogError($"Failed to delete QuestRoom with ID {id}: {exception.Message}");
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool QuestRoomExists(int id)
        {
            return context.QuestRooms.Any(e => e.Id == id);
        }

        private bool LogoExists(string? relativePathToLogo)
        {
            return !string.IsNullOrWhiteSpace(relativePathToLogo) && !Path.GetFileNameWithoutExtension(relativePathToLogo).Equals("stub");
        }

        private void DeleteLogo(string relativePathToLogo)
        {
            System.IO.File.Delete(Path.Combine(environment.WebRootPath, relativePathToLogo));
        }

        #region General data selectors
        private IEnumerable<string> GetGenres()
        {
            return context.QuestRooms.Select(questRoom => questRoom.Genre)
                                     .AsEnumerable();
        }

        private IEnumerable<string> GetCompanyNames()
        {
            return context.QuestRooms.Select(questRoom => questRoom.CompanyName)
                                     .AsEnumerable();
        }
        #endregion
    }
}
