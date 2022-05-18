using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoSkelbiu.Models;
using AutoSkelbiu.DAL;
using Dapper;

namespace AutoSkelbiu.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ValidateUserData(string email, string password)
        {
            UserDAL dal = new UserDAL();
            return Ok(dal.ValidateUserData(email, password));
        }
        [HttpGet]
        public IActionResult AddRememberedItem(int userId, int linkId)
        {
            UserDAL dal = new UserDAL();
            return Ok(dal.AddRememberedItem(userId, linkId));
        }
        [HttpGet]
        public IActionResult GetRememberedList(int userId)
        {
            UserDAL dal = new UserDAL();
            return Ok(dal.GetRememberedList(userId));
        }
        [HttpGet]
        public IActionResult RemoveRememberedItem(int userId, int linkId)
        {
            UserDAL dal = new UserDAL();
            return Ok(dal.RemoveRememberedItem(userId, linkId));
        }

    }
}
