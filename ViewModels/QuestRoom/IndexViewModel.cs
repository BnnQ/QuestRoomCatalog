using Homework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.ViewModels.QuestRoom
{
    public class IndexViewModel
    {
        public IEnumerable<Data.Entities.QuestRoom> QuestRooms { get; init; } = null!;
        public int NumberOfQuests => QuestRooms.Count();

        public SelectList GenreSelectList { get; init; } = null!;
        public SelectList MinutesDurationSelectList { get; init; } = null!;
        public SelectList MinimumNumberOfPlayersSelectList { get; init; } = null!;
        public SelectList MinimumAgeSelectList { get; init; } = null!;
        public SelectList CompanyNameSelectList { get; init; } = null!;

        public SearchParameterSet SearchParameterSet { get; private set; }

        
        public IndexViewModel()
        {
            SearchParameterSet = new();
        }
        public IndexViewModel(IEnumerable<Data.Entities.QuestRoom> questRooms, IEnumerable<Data.Entities.QuestRoom> initialQuestRooms) : this()
        {
            QuestRooms = questRooms;

            GenreSelectList = new(initialQuestRooms.Select(questRoom => questRoom.Genre)
                                            .Distinct()
                                            .OrderBy(genre => genre));
            MinutesDurationSelectList = new(initialQuestRooms.Select(questRoom => questRoom.MinutesDuration)
                                                      .Distinct()
                                                      .OrderBy(minutesDuration => minutesDuration));
            MinimumNumberOfPlayersSelectList = new(initialQuestRooms.Select(questRoom => questRoom.MinimumNumberOfPlayers)
                                                             .Distinct()
                                                             .OrderBy(minimumNumberOfPlayers => minimumNumberOfPlayers));
            MinimumAgeSelectList = new(initialQuestRooms.Select(questRoom => questRoom.MinimumAge)
                                                 .Distinct()
                                                 .OrderBy(minimumAge => minimumAge));
            CompanyNameSelectList = new(initialQuestRooms.Select(questRoom => questRoom.CompanyName)
                                                  .Distinct()
                                                  .OrderBy(companyName => companyName));
        }
        public IndexViewModel(IEnumerable<Data.Entities.QuestRoom> questRooms, IEnumerable<Data.Entities.QuestRoom> initialQuestRooms, SearchParameterSet searchParameterSet) : this(questRooms, initialQuestRooms)
        {
            SearchParameterSet = searchParameterSet;
        }

    }
}