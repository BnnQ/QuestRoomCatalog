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
            if (context.QuestRooms is null)
            {
                logger.LogWarning("[GET] Index: failed to return view because database set 'QuestRooms' is null");
                return NotFound();
            }
            
            logger.LogInformation("[GET] Index: returning view");
            return View(await context.QuestRooms.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            logger.LogInformation("[GET] Create: returning view");
            return View(new CreateQuestRoomViewModel(new QuestRoomDto(), GetGenres() ?? new List<string>(), GetCompanyNames() ?? new List<string>()));
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
                    logger.LogInformation("[POST] Create: saved logo \"\"{LogoFileNameme}\"\" for created QuestRoom", logo.FileName);
                }

                try
                {
                    await context.AddAsync(createdQuestRoom);
                    await context.SaveChangesAsync();
                    logger.LogInformation("[POST] Create: successfully created QuestRoom with ID {QuestRoomId}", createdQuestRoom.Id);
                }
                catch (Exception exception)
                {
                    logger.LogError("[POST] Create: failed to create QuestRoom: {ExceptionMessage}", exception.Message);
                    throw;
                }

                return RedirectToIndex();
            }
            else
            {
                var errorMessage = $"The entered data contains {ModelState.ErrorCount} errors";
                logger.LogError("[POST] Create: failed to create QuestRoom ({ErrorMessage}), returning 404", errorMessage);
                return Problem(errorMessage, statusCode: 404);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null || context.QuestRooms is null)
            {
                logger.LogError("[GET] Edit: failed to request edit page (passed ID is null or DB QuestRooms table is empty)");
                return NotFound();
            }

            var questRoom = await context.QuestRooms.FindAsync(id);
            if (questRoom is null)
            {
                logger.LogError("[GET] Edit: failed to return requested edit page (QuestRoom by ID {QuestRoomId} not found in database)", id);
                return NotFound();
            }

            logger.LogInformation("[GET] Edit: successfully requested edit page for QuestRoom with ID {QuestRoomId}", id);
            return View(new EditQuestRoomViewModel(questRoom, GetGenres() ?? new List<string>(), GetCompanyNames() ?? new List<string>()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(QuestRoom questRoom, IFormFile? logo, [FromServices] IFormImageProcessor imageProcessor)
        {
            if (ModelState.IsValid)
            {
                if (logo is not null)
                {
                    if (IsLogoExists(questRoom.PathToLogo))
                    {
                        DeleteLogo(questRoom.PathToLogo!);
                        logger.LogInformation("[POST] Edit: deleted QuestRoom logo \"\"{QuestRoomPathToLogo}\"\"", questRoom.PathToLogo);
                    }

                    questRoom.PathToLogo = imageProcessor.Process(logo);
                    logger.LogInformation("[POST] Edit: saved new logo \"\"{LogoFileName}\"\" for QuestRoom with ID {QuestRoomId}", logo.FileName, questRoom.Id);
                }

                try
                {
                    context.Update(questRoom);
                    await context.SaveChangesAsync();
                    logger.LogInformation("[POST] Edit: successfully edited QuestRoom with ID {QuestRoomId}", questRoom.Id);
                }
                catch (DbUpdateConcurrencyException exception)
                {
                    logger.LogError("[POST] Edit: failed to update QuestRoom with ID {QuestRoomId}: {ExceptionMessage}", questRoom.Id, exception.Message);
                    if (!IsQuestRoomExists(questRoom.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                return RedirectToIndex();
            }
            
            return View(new EditQuestRoomViewModel(questRoom: questRoom, genres: GetGenres() ?? new List<string>(), companyNames: GetCompanyNames() ?? new List<string>()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (context.QuestRooms is null)
            {
                const string ErrorMessage = "Entity set 'QuestRoomContext.QuestRooms' is null.";
                logger.LogError("[POST] Delete: failed to delete QuestRoom with ID {QuestRoomId} ({ErrorMessage})", id, ErrorMessage);
                return Problem(ErrorMessage);
            }
            
            var questRoom = await context.QuestRooms.FindAsync(id);
            if (questRoom is not null)
            {
                if (IsLogoExists(questRoom.PathToLogo))
                {
                    DeleteLogo(questRoom.PathToLogo!);
                    logger.LogInformation("[POST] Delete: deleted QuestRoom logo \"\"{QuestRoomPathToLogo}\"\"", questRoom.PathToLogo);
                }

                try
                {
                    context.QuestRooms.Remove(questRoom);
                    await context.SaveChangesAsync();
                    logger.LogInformation("[POST] Delete: successfully deleted QuestRoom with ID {QuestRoomId}", id);
                }
                catch (Exception exception)
                {
                    logger.LogError("[POST] Delete: failed to delete QuestRoom with ID {QuestRoomId}: {ExceptionMessage}", id, exception.Message);
                    throw;
                }
            }

            return RedirectToIndex();
        }

        #region Utils

        private IActionResult RedirectToIndex()
        {
            return RedirectToAction(nameof(Index));
        }
        
        private bool IsQuestRoomExists(int id)
        {
            return context.QuestRooms is not null && context.QuestRooms.Any(e => e.Id == id);
        }

        private static bool IsLogoExists(string? relativePathToLogo)
        {
            return !string.IsNullOrWhiteSpace(relativePathToLogo) && !Path.GetFileNameWithoutExtension(relativePathToLogo).Equals("stub");
        }

        private void DeleteLogo(string relativePathToLogo)
        {
            System.IO.File.Delete(Path.Combine(environment.WebRootPath, relativePathToLogo));
        }

        #endregion

        #region General data selectors
        private IEnumerable<string>? GetGenres()
        {
            return context.QuestRooms?.Select(questRoom => questRoom.Genre)
                                     .AsEnumerable();
        }

        private IEnumerable<string>? GetCompanyNames()
        {
            return context.QuestRooms?.Select(questRoom => questRoom.CompanyName)
                                     .AsEnumerable();
        }
        #endregion
    }
}
