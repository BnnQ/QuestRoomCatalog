namespace Homework.Models
{
    public class SearchParameterSet
    {
        public string? Name { get; init; }
        public string? Genre { get; init;  }
        public int? MinutesDuration { get; init; }
        public int? MinimumNumberOfPlayers { get; init; }
        public byte? MinimumAge { get; init;  }
        public byte? MinimumDifficultyLevel { get; init; }
        public byte? MaximumDifficultyLevel { get; init; }
        public string? CompanyName { get; init; }
        public byte? Rating { get; init; }
        public byte? FearLevel { get; init; }

        public SearchParameterSet()
        {
            //empty
        }
        public SearchParameterSet(string? name, string? genre, int? minutesDuration, int? minimumNumberOfPlayers, byte? minimumAge, byte? minimumDifficultyLevel, byte? maximumDifficultyLevel, string? companyName, byte? rating, byte? fearLevel)
        {
            Name = name;
            Genre = genre;
            MinutesDuration = minutesDuration;
            MinimumNumberOfPlayers = minimumNumberOfPlayers;
            MinimumAge = minimumAge;
            MinimumDifficultyLevel = minimumDifficultyLevel;
            MaximumDifficultyLevel = maximumDifficultyLevel;
            CompanyName = companyName;
            Rating = rating;
            FearLevel = fearLevel;
        }

    }
}