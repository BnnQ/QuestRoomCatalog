using AutoMapper;
using Homework.Data.Entities;
using Homework.Models;

namespace Homework.Services.MapperProfiles
{
    public class QuestRoomProfile : Profile
    {
        public QuestRoomProfile() 
        {
            CreateMap<QuestRoom, QuestRoomDto>().ReverseMap();
        }
    }
}