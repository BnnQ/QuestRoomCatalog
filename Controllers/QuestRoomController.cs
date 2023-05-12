using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework.Data;
using Homework.ViewModels;
using System.Diagnostics;
using Homework.ViewModels.QuestRoom;
using Homework.Data.Entities;
using Homework.Services;
using Homework.Models;

namespace Homework.Controllers
{
    public class QuestRoomController : Controller
    {
        private readonly QuestRoomContext context;
        private readonly ILogger logger;

        public QuestRoomController(QuestRoomContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            logger = loggerFactory.CreateLogger<QuestRoomController>();
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromServices] DatabaseDataFilterBuilder<QuestRoom> dataFilterBuilder, SearchParameterSet? searchParameterSet)
        {
            if (context.QuestRooms is null)
            {
                logger.LogWarning("[GET] Index: failed to return view because database set 'QuestRooms' is null");
                return NotFound();
            }
            if (searchParameterSet is not null)
            {
                var hasMinimumDifficultyLevel = searchParameterSet.MinimumDifficultyLevel > 0;
                var hasMaximumDifficultyLevel = searchParameterSet.MaximumDifficultyLevel > 0;
                var dataFilter = dataFilterBuilder.SetItems(context.QuestRooms)
                    .AddRuleIf(rule: questRoom => questRoom.Name.Contains(searchParameterSet.Name!), condition: !string.IsNullOrWhiteSpace(searchParameterSet.Name))
                    .AddRuleIf(questRoom => questRoom.Genre.Equals(searchParameterSet.Genre), !string.IsNullOrWhiteSpace(searchParameterSet.Genre))
                    .AddRuleIf(questRoom => questRoom.MinutesDuration == searchParameterSet.MinutesDuration, searchParameterSet.MinutesDuration is > 0)
                    .AddRuleIf(questRoom => questRoom.MinimumNumberOfPlayers >= searchParameterSet.MinimumNumberOfPlayers, searchParameterSet.MinimumNumberOfPlayers is > 0)
                    .AddRuleIf(questRoom => questRoom.MinimumAge >= searchParameterSet.MinimumAge, searchParameterSet.MinimumAge is > 0)
                    .AddRuleIf(questRoom => questRoom.DifficultyLevel >= searchParameterSet.MinimumDifficultyLevel, hasMinimumDifficultyLevel && searchParameterSet.MinimumDifficultyLevel > 0 && (!hasMaximumDifficultyLevel || searchParameterSet.MinimumDifficultyLevel <= searchParameterSet.MaximumDifficultyLevel))
                    .AddRuleIf(questRoom => questRoom.DifficultyLevel <= searchParameterSet.MaximumDifficultyLevel, searchParameterSet.MaximumDifficultyLevel is > 0 && (!hasMinimumDifficultyLevel || searchParameterSet.MaximumDifficultyLevel >= searchParameterSet.MinimumDifficultyLevel))
                    .AddRuleIf(questRoom => questRoom.CompanyName.Equals(searchParameterSet.CompanyName), !string.IsNullOrWhiteSpace(searchParameterSet.CompanyName))
                    .AddRuleIf(questRoom => questRoom.Rating == searchParameterSet.Rating, searchParameterSet.Rating is > 0)
                    .AddRuleIf(questRoom => questRoom.FearLevel == searchParameterSet.FearLevel, searchParameterSet.FearLevel is > 0)
                    .Build();

                logger.LogInformation("[GET] Index: returned view with filtered QuestRooms");
                return View(new IndexViewModel(await dataFilter.FilterItems().ToListAsync(), context.QuestRooms, searchParameterSet));
            }
            else
            {
                IEnumerable<QuestRoom> questRooms = await context.QuestRooms.ToListAsync();
                logger.LogInformation("[GET] Index: returned view without filtering QuestRooms");
                return View(new IndexViewModel(questRooms, questRooms));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || context.QuestRooms is null)
            {
                logger.LogError("[GET] Details: failed to request details page (passed ID is null or database set 'QuestRooms' is null)");
                return NotFound();
            }

            var questRoom = await context.QuestRooms.FindAsync(id);
            if (questRoom is null)
            {
                logger.LogError("[GET] Details: failed to request details page (QuestRoom with ID {QuestRoomId} not found)", id);
                return NotFound();
            }

            return View(questRoom);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}