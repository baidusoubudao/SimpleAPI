using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Common.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command,CommandReadDto>();
            CreateMap<CommandCreateDto,Command>();
            //CreateMap<CommandCreateDto,CommandReadDto>();
             CreateMap<Command,CommandCreateDto>();

        }
    }

}