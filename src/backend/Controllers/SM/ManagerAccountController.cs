﻿using backend.DTOs.SM.Request;
using backend.Helper.Untils;
using backend.Services.SM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace backend.Controllers.SM
{
    [Route("api/manager-account")]
    [ApiController]
    public class ManagerAccountController : ControllerBase
    {
        private readonly ILogger<ManagerAccountController> _logger;
        private readonly IAccountServices _accountServices;
        private readonly int userId = 2;
        // claim contain user name
        public ManagerAccountController(ILogger<ManagerAccountController> logger, IAccountServices accountServices)
        {
            _logger = logger;
            _accountServices = accountServices;
        }
        [HttpPost("create-account")]
        public async Task<IActionResult> CreateNewAccountAsync([FromBody] CreateAccountRequest request)
        {
            _logger.LogInformation("Start running function login");
            try
            {
                var result = await _accountServices.CreateNewAccountAsync(request);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors :{ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UpdateProfileAccountRequest request)
        {
            _logger.LogInformation("Running update profile account");
            try
            {
                var jwt = await _accountServices.UpdateProfileAsync(userId, request);
                return ResponseMessage.Success(jwt);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}", ex);
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([Required] int userId)
        {
            _logger.LogInformation("Running reset password account");
            try
            {
                var jwt = await _accountServices.ResetPasswordUserHasIdAsync(userId);
                return ResponseMessage.Success(jwt);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}", ex);
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpGet("accounts-pagination/{page}")]
        public async Task<IActionResult> GetAccountWithPaginationAsync([Required]int page)
        {
            _logger.LogInformation("Running get account with pagination");
            try
            {
                var search = SearchQueryStringUntil.ConvertQueryStringToOSearch(HttpContext);
                var result = await _accountServices.GetAllAccountDescriptionWithPaginationAsync(search,page);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}", ex);
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPost("add-role-user")]
        public async Task<IActionResult> AddRoleIntoUserAsync([Required] int userId, [FromBody] IEnumerable<int> roleIds)
        {
            _logger.LogInformation("Start running function add role into account");
            try
            {
                var result = await _accountServices.AddRoleToAccountAsync(roleIds, userId);
                return ResponseMessage.Success(result);
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Error {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpDelete("remove-role-user")]
        public async Task<IActionResult> RemoveRoleIntoUserAsync([Required] int userId, IEnumerable<int> roleId)
        {
            _logger.LogInformation("Start running function remove role into account");
            try
            {
                var result = await _accountServices.RemoveRoleForAccountAsync(userId, roleId);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
    }
}
