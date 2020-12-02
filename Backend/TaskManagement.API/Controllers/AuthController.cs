﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Dtos;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
             _userService = userService;
        }
        [HttpGet]
        public String showCookie() {

            var userName = User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.Name).Value;
            Console.WriteLine(userName);
            return "Cookie";
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task <IActionResult> Login(LoginModel loginModel)
        {

      
           
            var valid = await _userService.Authenticate(loginModel.UserName, loginModel.Password);

            if (!valid)
            {
                return new JsonResult(new {message ="Invalid UserName And Password", status="fail" });
            }

            var token= _userService.GenerateToken();

            var isAdmin = _userService.isAdmin();

            return new JsonResult(new { status="success", token,  access = isAdmin });


        }
    }
}