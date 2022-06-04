using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;

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

        /// <summary>
        /// Constructor dependency injection pattern
        /// </summary>
        /// <param name="repository">PlatformService.Data.IPlatformRepo</param>
        /// <param name="mapper">AutoMapper.IMapper</param>
        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms...");

            var platformItem = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }
    }
}