using System.Text;

namespace Homework.ViewModels.QuestRoom
{
    public class QuestRoomViewModel
    {
        public int Id { get; init; }
        public string PathToLogo { get; init; }
        public string Description { get; init; }
        public int MinutesDuration { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string RawRating { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Genre { get; set; }
        public string Price { get; set; }
        public string MinimumAge { get; set; }
        public string RawDifficultyLevel { get; set; }
        public string NumberOfPlayers { get; set; }

        public string ProgressRating { get; set; }
        public string ProgressDifficulty { get; set; }
        public string ProgressFear { get; set; }

        public QuestRoomViewModel(Data.Entities.QuestRoom questRoom)
        {
            Id = questRoom.Id;
            PathToLogo = questRoom.PathToLogo;
            Description = questRoom.Description;
            MinutesDuration = questRoom.MinutesDuration;
            Name = questRoom.Name;
            FullName = $"{questRoom.Name} ({questRoom.CompanyName})";

            RawRating = Enumerable.Range(0, 5).Aggregate(new StringBuilder(), (builderAccumulator, i) =>
            {
                if (i <= questRoom.Rating)
                {
                    return builderAccumulator.AppendLine(@"<i class=""fa fa-star""></i>");
                }
                else
                {
                    return builderAccumulator.AppendLine(@"<i class=""fa fa-star-o""></i>");
                }
            }, resultBuilder => resultBuilder.ToString());

            Address = questRoom.Address;
            PhoneNumber = questRoom.PhoneNumber;
            Email = questRoom.Email;
            CompanyName = questRoom.CompanyName;
            Genre = questRoom.Genre;
            Price = $"{questRoom.Price}грн.";
            MinimumAge = $"{questRoom.MinimumAge}+";

            RawDifficultyLevel = Enumerable.Range(0, questRoom.DifficultyLevel).Aggregate(new StringBuilder(), (builderAccumulator, i) =>
            {
                return builderAccumulator.AppendLine(@"<i class=""fa fa-lock""></i>");
            }, resultBuilder => resultBuilder.ToString());

            NumberOfPlayers = $"{questRoom.MinimumNumberOfPlayers}-{questRoom.MaximumNumberOfPlayers}";

            const int ProgressBarMaximumValue = 100;
            const int RatingMaximumValue = 5;
            const int DifficultyLevelMaximumValue = 5;
            const int FearLevelMaximumValue = 5;

            ProgressRating = $"{questRoom.Rating * (ProgressBarMaximumValue / RatingMaximumValue)}%";
            ProgressDifficulty = $"{questRoom.DifficultyLevel * (ProgressBarMaximumValue / DifficultyLevelMaximumValue)}%";
            ProgressFear = $"{questRoom.FearLevel * (ProgressBarMaximumValue / FearLevelMaximumValue)}%";
        }

    }
}