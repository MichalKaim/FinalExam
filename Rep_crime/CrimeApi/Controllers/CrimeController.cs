using AutoMapper;
using CrimeApi.Models;
using CrimeApi.Models.DTO;
using CrimeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrimeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrimeController : ControllerBase
    {
        private readonly IEventRepository _repository;
        private readonly ILogger<CrimeController> _logger; 
        private readonly IMapper _mapper;

        public CrimeController(IEventRepository repository, IMapper mapper, ILogger<CrimeController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrimeEvent>>> GetAllEvents()
        {
            try
            {
                var result = await _repository.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());   
            }

            return StatusCode(500 ,"MongoDb unreachable");
        }

        [HttpPost]
        public async Task CreateEvent(CreateCrimeEvent newEvent)
        {
            //Wyslij http do LawEnforcement
            //Dodaj rekord do bazy danych
            //Wyslij rabbitmq do notificatora v2
        }
    }
}
