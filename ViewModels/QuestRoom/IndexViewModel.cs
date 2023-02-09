using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.ViewModels.QuestRoom
{
    public class IndexViewModel
    {
        public IEnumerable<Data.Entities.QuestRoom> QuestRooms { get; init; }
        public string NumberOfQuests => $"Найдено квестов {QuestRooms.Count()}";

        public byte MinimumRating => 1;
        public byte MaximumRating => 5;
        public byte MinimumDifficultyLevel => 1;
        public byte MaximumDifficultyLevel => 5;
        public byte MinimumFearLevel => 1;
        public byte MaximumFearLevel => 5;


        public SelectList GenreSelectList { get; init; }
        public SelectList MinutesDurationSelectList { get; init; }
        public SelectList MinimumNumberOfPlayersSelectList { get; init; }
        public SelectList MinimumAgeSelectList { get; init; }
        public SelectList CompanyNameSelectList { get; init; }
        public string? SearchName { get; }
        public string? SearchGenre { get; }
        public int? SearchMinutesDuration { get; }
        public int? SearchMinimumNumberOfPlayers { get; }
        public byte? SearchMinimumAge { get; }
        public byte? SearchMinimumDifficultyLevel { get; }
        public byte? SearchMaximumDifficultyLevel { get; }
        public string? SearchCompanyName { get; }
        public byte? SearchRating { get; }
        public byte? SearchFearLevel { get; }
        
        public IndexViewModel(IEnumerable<Data.Entities.QuestRoom> questRooms)
        {
            QuestRooms = questRooms;

            GenreSelectList = new(questRooms.Select(questRoom => questRoom.Genre)
                                            .Distinct()
                                            .OrderBy(genre => genre));
            MinutesDurationSelectList = new(questRooms.Select(questRoom => questRoom.MinutesDuration)
                                                      .Distinct()
                                                      .OrderBy(minutesDuration => minutesDuration));
            MinimumNumberOfPlayersSelectList = new(questRooms.Select(questRoom => questRoom.MinimumNumberOfPlayers)
                                                             .Distinct()
                                                             .OrderBy(minimumNumberOfPlayers => minimumNumberOfPlayers));
            MinimumAgeSelectList = new(questRooms.Select(questRoom => questRoom.MinimumAge)
                                                 .Distinct()
                                                 .OrderBy(minimumAge => minimumAge));
            CompanyNameSelectList = new(questRooms.Select(questRoom => questRoom.CompanyName)
                                                  .Distinct()
                                                  .OrderBy(companyName => companyName));
        }
        public IndexViewModel(IEnumerable<Data.Entities.QuestRoom> questRooms, string? searchName, string? searchGenre, int? searchMinutesDuration, int? searchMinimumNumberOfPlayers, byte? searchMinimumAge, byte? searchMinimumDifficultyLevel, byte? searchMaximumDifficultyLevel, string? searchCompanyName, byte? searchRating, byte? searchFearLevel)
            : this(questRooms)
        {
            SearchName = searchName;
            SearchGenre = searchGenre;
            SearchMinutesDuration = searchMinutesDuration;
            SearchMinimumNumberOfPlayers = searchMinimumNumberOfPlayers;
            SearchMinimumAge = searchMinimumAge;
            SearchMinimumDifficultyLevel = searchMinimumDifficultyLevel;
            SearchMaximumDifficultyLevel = searchMaximumDifficultyLevel;
            SearchCompanyName = searchCompanyName;
            SearchRating = searchRating;
            SearchFearLevel = searchFearLevel;
        }
    }
}