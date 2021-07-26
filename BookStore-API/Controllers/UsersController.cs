﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore_API.Contracts;
using BookStore_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public UsersController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILoggerService logger,
            IMapper mapper
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// User Login Endpoint
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                var username = userDTO.Username;
                var password = userDTO.Password;
                _logger.LogInfo($"{location}: Attempted Call: {username}");
                var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

                if (result.Succeeded)
                {
                    _logger.LogInfo($"{location}: Successful Call: {username}");
                    var user = await _userManager.FindByNameAsync(username);
                    return Ok(user);
                }
                _logger.LogWarn($"{location}: Login username not found: {username}");
                return Unauthorized(userDTO);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }
;
        }
        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }
        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong contact Mark");
        }
    }
}
