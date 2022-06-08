using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    /// <summary>
    /// Class decoration with attributes
    /// 1. ApiController: Defines a API Controller
    /// 2. Route: Find this controller;
    ///     [controller]: Wild card approach which will take the prefix of the
    ///     controller name we have defined in the class.
    ///     In this case, [Platforms] is the prefix name of our controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        /// <summary>
        /// Constructor dependency injection pattern
        /// </summary>
        /// <param name="repository">PlatformService.Data.IPlatformRepo</param>
        /// <param name="mapper">AutoMapper.IMapper</param>
        /// <param name="commandDataClient"></param>
        public PlatformsController(
            IPlatformRepo repository,
            IMapper mapper,
            ICommandDataClient commandDataClient)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._commandDataClient = commandDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms...");

            var platformItem = this._repository.GetAllPlatforms();
            return Ok(this._mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }

        [HttpGet("{id}", Name = "GetPlaformById")]
        public ActionResult<PlatformReadDto> GetPlaformById(int id)
        {
            var platformItem = this._repository.GetPlatformById(id);
            if (platformItem != null)
                return Ok(this._mapper.Map<PlatformReadDto>(platformItem));

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            /**
            * The mapping is taking place using the following code in PlatformsProfile class
            * CreateMap<PlatformCreateDto, Platform>();
            **/
            var platformModel = this._mapper.Map<Platform>(platformCreateDto);
            this._repository.CreatePlatform(platformModel);
            this._repository.SaveChanges();

            var platformReadDto = this._mapper.Map<PlatformReadDto>(platformModel);

            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }
            /*
            * nameof(GetPlaformById): This will use the GetPlaformById endpoint to create
            * a location callback to this item back to the caller.
            * This is a Rest API standard.
            * Additionally we pass the ID and the created object reference back to the
            * caller.
            */
            return CreatedAtRoute(nameof(GetPlaformById), new { id = platformReadDto.Id }, platformReadDto);
        }
    }
}