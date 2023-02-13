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
            if (searchParameterSet is not null)
            {
                bool hasMinimumDifficultyLevel = searchParameterSet.MinimumDifficultyLevel > 0;
                bool hasMaximumDifficultyLevel = searchParameterSet.MaximumDifficultyLevel > 0;
                DatabaseDataFilter<QuestRoom> dataFilter = dataFilterBuilder.SetItems(context.QuestRooms)
                    .AddRuleIf(rule: questRoom => questRoom.Name.Contains(searchParameterSet.Name!), condition: !string.IsNullOrWhiteSpace(searchParameterSet.Name))
                    .AddRuleIf(questRoom => questRoom.Genre.Equals(searchParameterSet.Genre), !string.IsNullOrWhiteSpace(searchParameterSet.Genre))
                    .AddRuleIf(questRoom => questRoom.MinutesDuration == searchParameterSet.MinutesDuration, searchParameterSet.MinutesDuration.HasValue && searchParameterSet.MinutesDuration.Value > 0)
                    .AddRuleIf(questRoom => questRoom.MinimumNumberOfPlayers >= searchParameterSet.MinimumNumberOfPlayers, searchParameterSet.MinimumNumberOfPlayers.HasValue && searchParameterSet.MinimumNumberOfPlayers.Value > 0)
                    .AddRuleIf(questRoom => questRoom.MinimumAge >= searchParameterSet.MinimumAge, searchParameterSet.MinimumAge.HasValue && searchParameterSet.MinimumAge.Value > 0)
                    .AddRuleIf(questRoom => questRoom.DifficultyLevel >= searchParameterSet.MinimumDifficultyLevel, hasMinimumDifficultyLevel && searchParameterSet.MinimumDifficultyLevel > 0 && (!hasMaximumDifficultyLevel || (searchParameterSet.MinimumDifficultyLevel <= searchParameterSet.MaximumDifficultyLevel)))
                    .AddRuleIf(questRoom => questRoom.DifficultyLevel <= searchParameterSet.MaximumDifficultyLevel, searchParameterSet.MaximumDifficultyLevel.HasValue && searchParameterSet.MaximumDifficultyLevel.Value > 0 && (!hasMinimumDifficultyLevel || (searchParameterSet.MaximumDifficultyLevel >= searchParameterSet.MinimumDifficultyLevel)))
                    .AddRuleIf(questRoom => questRoom.CompanyName.Equals(searchParameterSet.CompanyName), !string.IsNullOrWhiteSpace(searchParameterSet.CompanyName))
                    .AddRuleIf(questRoom => questRoom.Rating == searchParameterSet.Rating, searchParameterSet.Rating.HasValue && searchParameterSet.Rating.Value > 0)
                    .AddRuleIf(questRoom => questRoom.FearLevel == searchParameterSet.FearLevel, searchParameterSet.FearLevel.HasValue && searchParameterSet.FearLevel.Value > 0)
                    .Build();

                logger.LogInformation("Filtered QuestRooms using passed search parameters", searchParameterSet);
                logger.LogInformation("Successfully requested index page");
                return View(new IndexViewModel(await dataFilter.FilterItems().ToListAsync(), context.QuestRooms, searchParameterSet));
            }
            else
            {
                IEnumerable<QuestRoom> questRooms = await context.QuestRooms.ToListAsync();
                logger.LogInformation("Successfully requested index page");
                return View(new IndexViewModel(questRooms, questRooms));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.QuestRooms == null)
            {
                logger.LogError("Failed to request details page: passed ID is null or DB QuestRooms table is empty");
                return NotFound();
            }

            var questRoom = await context.QuestRooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questRoom == null)
            {
                logger.LogError($"Failed to request details page: QuestRoom with ID {id} not found");
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