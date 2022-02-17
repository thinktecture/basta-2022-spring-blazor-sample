using AutoMapper;
using TT.ConfTool.Api.Models;

namespace TT.ConfTool.Api.Mappers
{
    public class ConftoolMapper : Profile
    {
        public ConftoolMapper()
        {
            CreateMap<Conference, Shared.DTO.ConferenceOverview>();
            CreateMap<Shared.DTO.ConferenceOverview, Conference>();
            CreateMap<Conference, Shared.DTO.ConferenceDetails>();
            CreateMap<Shared.DTO.ConferenceDetails, Conference>();
        }
    }
}

