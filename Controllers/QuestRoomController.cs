using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework.Data;
using Homework.ViewModels;
using System.Diagnostics;
using Homework.ViewModels.QuestRoom;
using Homework.Data.Entities;
using Homework.Services;

namespace Homework.Controllers
{
    public class QuestRoomController : Controller
    {
        private readonly QuestRoomContext context;

        public QuestRoomController(QuestRoomContext context)
        {
            this.context = context;
        }

        // GET: QuestRoom
        [HttpGet]
        public async Task<IActionResult> Index([FromServices] DatabaseDataFilterBuilder<QuestRoom> dataFilterBuilder, string? searchName, string? searchGenre, int? searchMinutesDuration, int? searchMinimumNumberOfPlayers, int? searchMinimumAge, int? searchMinimumDifficultyLevel, int? searchMaximumDifficultyLevel, string? searchCompanyName, int? searchRating, int? searchFearLevel)
        {
            DatabaseDataFilter<QuestRoom> dataFilter = dataFilterBuilder.SetItems(context.QuestRooms)
                .AddRuleIf(rule: questRoom => questRoom.Name.Contains(searchName!), condition: !string.IsNullOrWhiteSpace(searchName))
                .AddRuleIf(questRoom => questRoom.Genre.Equals(searchGenre), !string.IsNullOrWhiteSpace(searchGenre))
                .AddRuleIf(questRoom => questRoom.MinutesDuration == searchMinutesDuration, searchMinutesDuration.HasValue && searchMinutesDuration.Value > 0)
                .AddRuleIf(questRoom => questRoom.MinimumNumberOfPlayers >= searchMinimumNumberOfPlayers, searchMinimumNumberOfPlayers.HasValue && searchMinimumNumberOfPlayers.Value > 0)
                .AddRuleIf(questRoom => questRoom.MinimumAge >= searchMinimumAge, searchMinimumAge.HasValue && searchMinimumAge.Value > 0)
                .AddRuleIf(questRoom => questRoom.DifficultyLevel >= searchMinimumDifficultyLevel, searchMinimumDifficultyLevel.HasValue && searchMinimumDifficultyLevel.Value > 0 && searchMinimumDifficultyLevel <= searchMaximumDifficultyLevel)
                .AddRuleIf(questRoom => questRoom.DifficultyLevel <= searchMaximumDifficultyLevel, searchMaximumDifficultyLevel.HasValue && searchMaximumDifficultyLevel.Value > 0 && searchMaximumDifficultyLevel >= searchMinimumDifficultyLevel)
                .AddRuleIf(questRoom => questRoom.CompanyName.Equals(searchCompanyName), !string.IsNullOrWhiteSpace(searchCompanyName))
                .AddRuleIf(questRoom => questRoom.Rating == searchRating, searchRating.HasValue && searchRating.Value > 0)
                .AddRuleIf(questRoom => questRoom.FearLevel == searchFearLevel, searchFearLevel.HasValue && searchFearLevel.Value > 0)
                .Build();

            return View(new IndexViewModel(await dataFilter.FilterItems().ToListAsync()));
        }

        // GET: QuestRoom/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.QuestRooms == null)
            {
                return NotFound();
            }

            var questRoom = await context.QuestRooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questRoom == null)
            {
                return NotFound();
            }

            return View(new QuestRoomViewModel(questRoom));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //// GET: QuestRoom/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: QuestRoom/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Description,MinutesDuration,MinimumNumberOfPlayers,MaximumNumberOfPlayers,MinimumAge,Address,PhoneNumber,Email,CompanyName,Rating,FearLevel,DifficultyLevel,Price,PathToLogo")] QuestRoom questRoom)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        context.Add(questRoom);
        //        await context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(questRoom);
        //}

        //// GET: QuestRoom/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || context.QuestRooms == null)
        //    {
        //        return NotFound();
        //    }

        //    var questRoom = await context.QuestRooms.FindAsync(id);
        //    if (questRoom == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(questRoom);
        //}

        //// POST: QuestRoom/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,MinutesDuration,MinimumNumberOfPlayers,MaximumNumberOfPlayers,MinimumAge,Address,PhoneNumber,Email,CompanyName,Rating,FearLevel,DifficultyLevel,Price,PathToLogo")] QuestRoom questRoom)
        //{
        //    if (id != questRoom.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            context.Update(questRoom);
        //            await context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!QuestRoomExists(questRoom.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(questRoom);
        //}

        //// GET: QuestRoom/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || context.QuestRooms == null)
        //    {
        //        return NotFound();
        //    }

        //    var questRoom = await context.QuestRooms
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (questRoom == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(questRoom);
        //}

        //// POST: QuestRoom/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (context.QuestRooms == null)
        //    {
        //        return Problem("Entity set 'QuestRoomContext.QuestRooms'  is null.");
        //    }
        //    var questRoom = await context.QuestRooms.FindAsync(id);
        //    if (questRoom != null)
        //    {
        //        context.QuestRooms.Remove(questRoom);
        //    }

        //    await context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool QuestRoomExists(int id)
        //{
        //  return context.QuestRooms.Any(e => e.Id == id);
        //}
    }
}
