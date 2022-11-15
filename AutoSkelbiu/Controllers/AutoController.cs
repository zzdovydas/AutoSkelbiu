using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoSkelbiu.Helpers;
using AutoSkelbiu.Models;
using AutoSkelbiu.DAL;
using System.Linq;
using Dapper;

namespace AutoSkelbiu.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AutoController : Controller
    {

        private readonly AutoDAL _autoDal;

        public AutoController(AutoDAL autoDAL)
        {
            _autoDal = autoDAL;
        }

        [HttpGet]
        public IActionResult GetAutoList()
        {
            return Ok(_autoDal.GetAutoList());
        }

        [HttpGet]
        public IActionResult GetAutoById(long autoId)
        {
            return Ok(_autoDal.GetAutoById(autoId));
        }

        [NonAction]
        public FilterData CleanFilterInput(FilterData data)
        {
            data.Gearbox = InputValidationHelper.SanitizeIllegalCharacters(data.Gearbox);
            data.Model = InputValidationHelper.SanitizeIllegalCharacters(data.Model);
            
            return data;
        }

        [HttpGet]
        public IActionResult GetFilteredAutoList(FilterData input)
        {
            AutoDAL _autoDal = new AutoDAL();

            input = CleanFilterInput(input);
            List<Auto> filteredList = _autoDal.GetFilteredAutoList(input);

            return Ok(filteredList);
        }

        [HttpGet]
        public IActionResult GetImagesById(long linkId)
        {
            return Ok(_autoDal.GetImagesById(linkId));
        }

        [HttpGet]
        public IActionResult GetAutoMakes()
        {
            return Ok(_autoDal.GetAutoMakes(null));
        }

        [HttpGet]
        public IActionResult GetAutoModels(long makeId)
        {
            return Ok(_autoDal.GetAutoModels(makeId));
        }

        [HttpGet]
        public IActionResult GetFuelTypes()
        {
            return Ok(_autoDal.GetFuelTypes(null));
        }
    }
}
