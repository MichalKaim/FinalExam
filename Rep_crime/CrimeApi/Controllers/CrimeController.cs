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
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IRabbitMqSender _sender;

        public CrimeController(IEventRepository repository, IMapper mapper, ILogger<CrimeController> logger, IHttpClientFactory httpClient, IConfiguration configuration, IRabbitMqSender sender)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
            _sender = sender;
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
                _logger.LogError(ex.ToString());   
            }

            return StatusCode(500 ,"MongoDb unreachable");
        }

        [HttpPost]
        public async Task<ActionResult> CreateEvent(CreateCrimeEvent newEvent)
        {
            _logger.LogInformation("ENTER HTTP POST: v1");
            string id;

            try
            {
                id = await _repository.Create(_mapper.Map<CrimeEvent>(newEvent));
            }
            catch(Exception ex)
            {
                _logger.LogError("MONGODB ERROR: " + ex.ToString());
                return StatusCode(500, "MongoDb Error");
            }

            _logger.LogInformation($"Created event with id: {id}");

            try
            {
                var httpClient = _httpClient.CreateClient();
                HttpRequestMessage request = new(HttpMethod.Put, $"{_configuration["AddEventToLawEnforcement"]}");
                request.Content = JsonContent.Create(new AddEventToLaw{ eventId = id });
                var response = await httpClient.SendAsync(request);

                if(!response.IsSuccessStatusCode)
                    return StatusCode(500, $"Wrong response {response.StatusCode}");
                
                var LawEnforcementId = await response.Content.ReadFromJsonAsync<int>();
                _logger.LogInformation($"LawEnforcement id from HTTP: {LawEnforcementId}");
                
                await _repository.AddLawEnforcement(id, LawEnforcementId);
            }
            catch (Exception ex)
            {
                _logger.LogError("HTTP ERROR: " + ex.ToString());
                return StatusCode(500, "HTTP Error");
            }

            _sender.SendMessageToNotificator(newEvent.Email);

            return Ok();
        }
    }
}
