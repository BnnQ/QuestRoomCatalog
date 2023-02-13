using Homework.Models;

namespace Homework.ViewModels.Admin
{
    public class CreateQuestRoomViewModel
    {
        public QuestRoomDto QuestRoom { get; init; } = null!;
        public IEnumerable<string> Genres { get; init; }
        public IEnumerable<string> CompanyNames { get; init; }

        public CreateQuestRoomViewModel()
        {
            Genres = new List<string>();
            CompanyNames = new List<string>();
        }
        public CreateQuestRoomViewModel(QuestRoomDto questRoom, IEnumerable<string> genres, IEnumerable<string> companyNames)
        {
            QuestRoom = questRoom;
            Genres = genres;
            CompanyNames = companyNames;
        }
    }
}