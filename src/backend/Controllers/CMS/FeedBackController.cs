using backend.DTOs.CMS.Request;
using backend.Helper.Untils;
using backend.Services.CMS.FeedBack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace backend.Controllers.CMS
{
    [Route("api/feed-backs")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackServices _feedBackServices;
        private readonly ILogger<FeedBackController> _logger;
        public FeedBackController(IFeedBackServices feedBackServices, ILogger<FeedBackController> logger)
        {
            _feedBackServices = feedBackServices;
            _logger = logger;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateFeedBackAsync([FromBody] CreateFeedBackRequest request)
        {
            _logger.LogInformation("Running function create feedback");
            try
            {
                await _feedBackServices.CreateFeedBackASync(request);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetFeedBacksAsync()
        {
            _logger.LogInformation("Running function get feedback");
            try
            {
                var search = SearchQueryStringUntil.ConvertQueryStringToOSearch(HttpContext);
                var result = await _feedBackServices.SearchFeedBackAsync(search);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpGet("pagination/{page}/{pageSize}")]
        public async Task<IActionResult> GetFeedBacksWithPaginationAsync(int page, int pageSize)
        {
            _logger.LogInformation("Running function get feedback");
            try
            {
                var search = SearchQueryStringUntil.ConvertQueryStringToOSearch(HttpContext);
                var result = await _feedBackServices.SearchFeedBackAsync(search,true,page,pageSize);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
    }
}
