using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    /// <summary>
    /// This class maps our DTOs with the Models using Automapper.
    /// 
    /// </summary>
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            /*
            * Source -> Target
            * ----------------
            * When the property names between the source and the destination
            * are exactly the same, the automapper will do the mapping 
            * automatically for us.
            * In this case, it does it automatically.
            
            * Note: In the future, if we need to map Target -> Source,
            * need to tell the automapper explicitely how to do the mapping
            * as the inverse mapping is not automatic.
            */
            CreateMap<Platform, PlatformReadDto>();

            /*
            * Source -> Target
            */
            CreateMap<PlatformCreateDto, Platform>();
        }
    }
}