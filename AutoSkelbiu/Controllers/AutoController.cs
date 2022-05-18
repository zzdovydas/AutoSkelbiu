using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using AutoSkelbiu.Models;
using AutoSkelbiu.DAL;
using Dapper;

namespace AutoSkelbiu.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AutoController : Controller
    {

        private readonly ILogger<AutoController> _logger;

        public AutoController(ILogger<AutoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAutoList()
        {
            AutoDAL dal = new AutoDAL();
            return Ok(dal.GetAutoList());
        }

        [HttpGet]
        public IActionResult GetAutoById(long autoId)
        {
            AutoDAL dal = new AutoDAL();
            return Ok(dal.GetAutoById(autoId));
        }

        static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters,
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        [HttpGet]
        public IActionResult GetAutoListFiltered(long? makeId, long? pageNumber, long? fuelTypeId, long? priceFrom, long? priceTo, int? yearFrom, int? yearTo,
                                                int? hasVin, string gearbox, string autoModel)
        {
            AutoDAL dal = new AutoDAL();
            gearbox = gearbox == null ? "" : CleanInput(gearbox);
            autoModel = autoModel == null ? "" : CleanInput(autoModel);
            return Ok(dal.GetAutoListFiltered(makeId, pageNumber, fuelTypeId, priceFrom, priceTo, yearFrom, yearTo, hasVin, gearbox, autoModel));
        }

        [HttpGet]
        public IActionResult GetImagesById(long linkId)
        {
            AutoDAL dal = new AutoDAL();
            return Ok(dal.GetImagesById(linkId));
        }

        [HttpGet]
        public IActionResult GetAutoMakes()
        {
            AutoDAL dal = new AutoDAL();
            return Ok(dal.GetAutoMakes(null));
        }

        [HttpGet]
        public IActionResult GetAutoModels(long makeId)
        {
            AutoDAL dal = new AutoDAL();
            return Ok(dal.GetAutoModels(makeId));
        }

        [HttpGet]
        public IActionResult GetFuelTypes()
        {
            AutoDAL dal = new AutoDAL();
            return Ok(dal.GetFuelTypes(null));
        }
    }
}
