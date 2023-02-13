namespace Homework.ViewModels.Admin
{
    public class EditQuestRoomViewModel
    {
        public Data.Entities.QuestRoom QuestRoom { get; init; }
        public IEnumerable<string> Genres { get; init; }
        public IEnumerable<string> CompanyNames { get; init; }

        public EditQuestRoomViewModel(Data.Entities.QuestRoom questRoom, IEnumerable<string> genres, IEnumerable<string> companyNames)
        {
            QuestRoom = questRoom;
            Genres = genres;
            CompanyNames = companyNames;
        }
    }
}