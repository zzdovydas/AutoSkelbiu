using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using AutoSkelbiu.Helpers;
using AutoSkelbiu.Models;
using System.Runtime;
using System.Linq;
using Dapper;

namespace AutoSkelbiu.DAL
{
    public class AutoDAL
    {

        private readonly ILogger<AutoDAL> _logger;
        private readonly string dbConnString;

        public AutoDAL()
        {
            dbConnString = "server=localhost;user=scraperis;password=useris3;database=AutoSkelbiu;";
        }

        public List<Auto> GetAutoList()
        {

            List<Auto> l = new List<Auto>();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                l = connection.Query<Auto>(@"SELECT auto1.*, img.IMAGE_PATH AS 'THUMBNAIL' FROM (SELECT LINK_ID, MAX(CREATED_AT) AS CREATED_AT_MAX FROM `AUTO` GROUP BY LINK_ID) AS auto INNER JOIN `AUTO` AS auto1 ON auto.LINK_ID = auto1.LINK_ID AND
                 auto1.CREATED_AT = auto.CREATED_AT_MAX INNER JOIN IMAGES img ON auto1.LINK_ID = img.LINK_ID AND auto1.IS_SOLD=0 AND img.IS_MAIN_IMAGE=true GROUP BY img.LINK_ID LIMIT 20").ToList();
            }

            return l;
        }

        public long? GeneratePageNumber(long? pageNumber)
        {
            if (pageNumber != null)
            {
                return (pageNumber - 1) * 20;
            }

            return 0;
        }

        public string GenerateFilterQuery(FilterData data)
        {
            string resultQuery = "";

            data.PageNumber = GeneratePageNumber(data.PageNumber);

            string filterQuery = "WHERE 1=1";
            
            filterQuery += FilterQueryGenerator.GenerateMake(data.FuelTypeId);
            filterQuery += FilterQueryGenerator.GenerateFuelTypeName(data.FuelTypeId);
            filterQuery += FilterQueryGenerator.GenerateModel(data.Model);
            filterQuery += FilterQueryGenerator.GeneratePriceFrom(data.PriceFrom);
            filterQuery += FilterQueryGenerator.GenerateHasVin(data.HasVin);
            filterQuery += FilterQueryGenerator.GeneratePriceTo(data.PriceTo);
            filterQuery += FilterQueryGenerator.GenerateGearbox(data.Gearbox);
            filterQuery += FilterQueryGenerator.GenerateYearFrom(data.YearFrom);
            filterQuery += FilterQueryGenerator.GenerateYearTo(data.YearTo);

            if (filterQuery.Length < 10)
                filterQuery = "";

            List<Auto> l = new List<Auto>();

            string queryStart = @"SELECT auto1.*, img.IMAGE_PATH AS 'THUMBNAIL' FROM (SELECT LINK_ID, MAX(CREATED_AT) AS CREATED_AT_MAX FROM `AUTO` GROUP BY LINK_ID) AS auto INNER JOIN `AUTO` AS auto1 ON auto.LINK_ID = auto1.LINK_ID AND
                 auto1.CREATED_AT = auto.CREATED_AT_MAX INNER JOIN IMAGES img ON auto1.LINK_ID = img.LINK_ID AND auto1.IS_SOLD=0 AND img.IS_MAIN_IMAGE=true ";

            string queryEnd = " GROUP BY img.LINK_ID LIMIT " + data.PageNumber.ToString() + " , 20;";

            resultQuery = queryStart + filterQuery + queryEnd;

            return resultQuery;
        }

        public List<Auto> GetFilteredAutoList(FilterData data)
        {
            List<Auto> l = new List<Auto>();

            string query = GenerateFilterQuery(data);
            
            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                l = connection.Query<Auto>(query).ToList();
            }

            return l;
        }

        public Auto GetAutoById(long autoId)
        {
            Auto l = new Auto();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                l = connection.Query<Auto>(@"SELECT auto.*, links.LINK_URL, links.AUTO_VIN FROM `AUTO` auto INNER JOIN LINKS links ON links.LINK_ID = auto.LINK_ID WHERE auto.AUTO_ID=" + autoId).First();

            }

            return l;
        }
        public List<Image> GetImagesById(long linkId)
        {
            List<Image> l = new List<Image>();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                l = connection.Query<Image>(@"SELECT * FROM `IMAGES` WHERE LINK_ID=" + linkId).ToList();

            }

            return l;
        }
        //SELECT DISTINCT AUTO_MAKE FROM `AUTO`
        public List<AutoMake> GetAutoMakes(long? makeId)
        {
            List<AutoMake> l = new List<AutoMake>();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                if (makeId == null)
                    l = connection.Query<AutoMake>(@"SELECT make.*, COUNT(DISTINCT(auto.LINK_ID)) AS AUTO_COUNT FROM AUTO_MAKES AS make RIGHT JOIN AUTO AS auto ON make.AUTO_MAKE = auto.AUTO_MAKE WHERE auto.IS_SOLD=0 GROUP BY make.AUTO_MAKE").ToList();
                else
                    l = connection.Query<AutoMake>(@"SELECT * FROM AUTO_MAKES WHERE AUTO_MAKE_ID=" + makeId).ToList();

            }

            return l;
        }

        public List<Model> GetAutoModels(long makeId)
        {
            List<Model> l = new List<Model>();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                if (makeId != null)
                    l = connection.Query<Model>(@"SELECT DISTINCT a.AUTO_MODEL FROM AUTO a 
                                                    INNER JOIN AUTO_MAKES am ON am.AUTO_MAKE_ID=" + makeId + " WHERE am.AUTO_MAKE=a.AUTO_MAKE").ToList();

            }

            return l;
        }

        public List<AutoFuelTypes> GetFuelTypes(long? fuelTypeId)
        {
            List<AutoFuelTypes> l = new List<AutoFuelTypes>();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                if (fuelTypeId == null)
                    l = connection.Query<AutoFuelTypes>(@"SELECT * FROM AUTO_FUEL_TYPES").ToList();
                else
                    l = connection.Query<AutoFuelTypes>(@"SELECT * FROM AUTO_FUEL_TYPES WHERE AUTO_FUEL_TYPE_ID=" + fuelTypeId).ToList();
            }

            return l;
        }

        public List<string> GetGearBox()
        {
            List<string> l = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                l = connection.Query<string>(@"SELECT DISTINCT AUTO_GEARBOX FROM AUTO").ToList();

            }

            return l;
        }

        public List<string> GetClimateControl()
        {
            List<string> l = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                l = connection.Query<string>(@"SELECT DISTINCT AUTO_CLIMATE_CONTROL FROM AUTO").ToList();

            }

            return l;
        }

        public List<string> GetAutoColor()
        {
            List<string> l = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                l = connection.Query<string>(@"SELECT DISTINCT AUTO_COLOR FROM AUTO").ToList();

            }

            return l;
        }

        public List<string> GetAutoSteering()
        {
            List<string> l = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                l = connection.Query<string>(@"SELECT DISTINCT AUTO_STEERING_WHEEL_SIDE FROM AUTO").ToList();

            }

            return l;
        }

        public List<string> GetAutoWheelDrive()
        {
            List<string> l = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                l = connection.Query<string>(@"SELECT DISTINCT AUTO_WHEEL_DRIVE FROM AUTO").ToList();

            }

            return l;
        }

        public List<string> GetAutoDefects()
        {
            List<string> l = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                l = connection.Query<string>(@"SELECT DISTINCT AUTO_DEFECT FROM AUTO").ToList();

            }

            return l;
        }

    }
}
