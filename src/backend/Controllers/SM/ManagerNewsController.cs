using backend.Core.ValueObject;
using backend.DTOs.CMS.Request;
using backend.Helper.Untils;
using backend.Services.SM.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using uc.api.cms.Infrastructure.Authorization;
using static UC.Core.Common.Consts;

namespace backend.Controllers.SM
{
    [Route("api/manager-news")]
    [ApiController]
    public class ManagerNewsController : ControllerBase
    {
        private readonly ISMNewsServices _newServices;
        private readonly ILogger<ManagerNewsController> _logger;
        private readonly int userId = 2;
        public ManagerNewsController(ISMNewsServices newServices, ILogger<ManagerNewsController> logger)
        {
            _newServices = newServices;
            _logger = logger;
        }
        [HttpGet("news-normal/detail")]
        public async Task<IActionResult> GetNewsNormalDetailByIdAsync([Required] int newsId)
        {
            _logger.LogInformation("Start running get news normal detail by id");
            try
            {
                var result = await _newServices.GetNewsNormalByIdAsync(userId, newsId);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpGet("news-services/detail")]
        public async Task<IActionResult> GetNewServicesDetailByIdAsync([Required] int newsId)
        {
            _logger.LogInformation("Start running get news services detail by id");
            try
            {
                var result = await _newServices.GetNewsServicesByIdAsync(userId, newsId);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPost("create-news-normal")]
        public async Task<IActionResult> CreateNewsNormalAsync([FromBody] CreateNewsNormalRequest request)
        {
            _logger.LogInformation("Start build function create news normal");
            try
            {
                var result = await _newServices.CreateNewsNormalAsync(userId, request);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPost("create-news-services")]
        public async Task<IActionResult> CreateNewsNormalAsync([FromBody] CreateNewsServicesRequest request)
        {
            _logger.LogInformation("Start build function create news services");
            try
            {
                var result = await _newServices.CreateNewsServicesAsync(userId, request);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPut("update-news-normal")]
        public async Task<IActionResult> UpdateNewsNormalAsync([Required] int newsId, [FromBody] UpdateNewsNormalRequest request)
        {
            _logger.LogInformation("Start build function update news normal");
            try
            {
                var result = await _newServices.UpdateNewsNormalAsync(userId, newsId, request);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPut("update-news-services")]
        public async Task<IActionResult> UpdateNewsServicesAsync([Required] int newsId, [FromBody] UpdateNewsServicesRequest request)
        {
            _logger.LogInformation("Start build function update news services");
            try
            {
                var result = await _newServices.UpdateNewsServicesAsync(userId, newsId, request);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteNewsAsync([Required] int newsId)
        {
            _logger.LogInformation("Start running function delete news");
            try
            {
                await _newServices.DeleteNewsAsync(userId, newsId);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPut("update-significant")]
        public async Task<IActionResult> UpdateSignificantNewsAsync([Required] int newsId, [Required]bool significant)
        {
            _logger.LogInformation("Start running function update significant");
            try
            {
                await _newServices.ChangeSignificant(userId, newsId, significant);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPost("news-censor")]
        public async Task<IActionResult> CensorNewsAsync([Required] int newsId, [Required] bool censor, [FromBody] string? message)
        {
            _logger.LogInformation("Start running function censor news");
            try
            {
                var result = await _newServices.CensorAsync(userId,newsId,censor,message);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPut("news-active")]
        public async Task<IActionResult> ChangeActiveNewsAsync([Required] int newsId, [Required] bool active)
        {
            _logger.LogInformation("Start running function change active");
            try
            {
                var result = await _newServices.ChangeActiveNewsAsync(newsId,active);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        //[HttpGet("news-pagination")]
        //public async Task<IActionResult> GetNewsDescriptionByUserIdWithPaginationAsync()
        //{
        //    _logger.LogInformation("Start running function get news description by user");
        //    try
        //    {
        //        var result = await _newServices.SearchNewsDescriptionWithPaginationAsync(null,userId);
        //        return ResponseMessage.Success(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation($"Errors: {ex.Message}");
        //        return ResponseMessage.Warning(ex.Message);
        //    }
        //}
        [HttpGet("news-for-user")]
        public async Task<IActionResult> GetNewsDescriptionByUserId()
        {
            _logger.LogInformation("Start running function get news description by user");
            try
            {
                var search = SearchQueryStringUntil.ConvertQueryStringToOSearch(HttpContext);
                var result = await _newServices.SearchAllNewsDescriptionAsync(Status.Success,search,userId,false,false);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        //[HttpGet("news-censor-pagination")]
        //public async Task<IActionResult> GetNewsHasCensorDescriptionWithPaginationAsync()
        //{
        //    _logger.LogInformation("Start running function get news description has active");
        //    try
        //    {
        //        var result = await _newServices.SearchNewsDescriptionWithPaginationAsync(null, null,false,true);
        //        return ResponseMessage.Success(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation($"Errors: {ex.Message}");
        //        return ResponseMessage.Warning(ex.Message);
        //    }
        //}
        [HttpGet("news-has-censor")]
        public async Task<IActionResult> GetNewsHasCensorDescriptionAsync()
        {
            _logger.LogInformation("Start running function get news description by user");
            try
            {
                var search = SearchQueryStringUntil.ConvertQueryStringToOSearch(HttpContext);
                var result = await _newServices.SearchAllNewsDescriptionAsync(Status.Success,search,null,false,true);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpGet("news-has-send")]
        public async Task<IActionResult> GetNewsSendStatusAsync()
        {
            _logger.LogInformation("Start running function get news send by user");
            try
            {
                var search = SearchQueryStringUntil.ConvertQueryStringToOSearch(HttpContext);
                var result = await _newServices.SearchAllNewsDescriptionAsync(Status.Send,search,null,false,true);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPut("send-news")]
        public async Task<IActionResult> SendNewsHasIdAsync([Required]int newsId, [FromBody] string? message)
        {
            _logger.LogInformation("Start running function send news by user");
            try
            {
                var result = await _newServices.SendNewsAsync(userId,newsId,message);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpGet("statistical/{startDay}/{endDay}")]
        public async Task<IActionResult> GetStatisticalNewsAsync(DateTime startDay, DateTime endDay)
        {
            _logger.LogInformation("Start running function send news by user");
            try
            {
                var search = SearchQueryStringUntil.ConvertQueryStringToOSearch(HttpContext);
                var result = await _newServices.GetStatisticalNewsAsync(startDay,endDay, search);
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
