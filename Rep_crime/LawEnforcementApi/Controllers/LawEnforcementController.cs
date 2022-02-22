using AutoMapper;
using LawEnforcementApi.Model;
using LawEnforcementApi.Model.DTO;
using LawEnforcementApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LawEnforcementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LawEnforcementController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly ILogger<LawEnforcement> _logger;
        private readonly IMapper _mapper;

        public LawEnforcementController(IRepository repo, ILogger<LawEnforcement> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LawEnforcement>>> GetAll()
        {
            try
            {
                var result = await _repo.GetAll();
                if (result.Any())
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return StatusCode(500, "SQL error");
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateLawEnforcement entity)
        {
            try
            {
                await _repo.Add(_mapper.Map<LawEnforcement>(entity));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return StatusCode(500, "SQL error");
        }

        [HttpPut]
        public async Task<ActionResult<int>> AddEvent(string eventId)
        {
            try
            {
                var result = await _repo.GetAll();

                var index = new Random().Next(result.Count());
                await _repo.AddEventToEntiy(eventId, result.ToList()[index].Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return StatusCode(500, "SQL error");
        }
    }
}
