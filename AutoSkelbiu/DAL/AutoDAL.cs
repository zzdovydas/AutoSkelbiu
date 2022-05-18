using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Linq;
using System.Threading.Tasks;
using AutoSkelbiu.Models;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Dapper;

namespace AutoSkelbiu.DAL
{
    public class AutoDAL
    {

        private readonly ILogger<AutoDAL> _logger;

        public AutoDAL()
        {

        }

        public List<Auto> GetAutoList()
        {

            List<Auto> l = new List<Auto>();

            using (MySqlConnection connection = new MySqlConnection(""))
            {
                connection.Open();
                l = connection.Query<Auto>(@"SELECT auto1.*, img.IMAGE_PATH AS 'THUMBNAIL' FROM (SELECT LINK_ID, MAX(CREATED_AT) AS CREATED_AT_MAX FROM `AUTO` GROUP BY LINK_ID) AS auto INNER JOIN `AUTO` AS auto1 ON auto.LINK_ID = auto1.LINK_ID AND
                 auto1.CREATED_AT = auto.CREATED_AT_MAX INNER JOIN IMAGES img ON auto1.LINK_ID = img.LINK_ID AND auto1.IS_SOLD=0 AND img.IS_MAIN_IMAGE=true GROUP BY img.LINK_ID LIMIT 20").ToList();
                //SELECT auto1.* FROM (SELECT LINK_ID, MAX(CREATED_AT) AS CREATED_AT_MAX FROM `AUTO` GROUP BY LINK_ID) AS auto INNER JOIN `AUTO` AS auto1 ON auto.LINK_ID = auto1.LINK_ID AND auto1.CREATED_AT = auto.CREATED_AT_MAX 

            }

            return l;
        }

        public List<Auto> GetAutoListFiltered(long? makeId, long? pageNumber, long? fuelTypeId, long? priceFrom, long? priceTo, int? yearFrom, int? yearTo,
                                                int? hasVin, string gearbox, string autoModel)
        {
            if (pageNumber == null)
                pageNumber = 0;
            else
                pageNumber = (pageNumber - 1) * 20;

            string filterQuery = "WHERE 1=1";

            if (makeId != null)
            {
                string autoMakeName = GetAutoMakes(makeId).First().AUTO_MAKE;
                filterQuery += " AND auto1.AUTO_MAKE='" + autoMakeName + "'";
            }

            if (fuelTypeId != null)
            {
                string fuelTypeName = GetFuelTypes(fuelTypeId).First().AUTO_FUEL_TYPE_NAME;
                filterQuery += " AND auto1.AUTO_FUEL_TYPE='" + fuelTypeName + "'";
            }

            if (autoModel != null && autoModel != "")
            {
                filterQuery += " AND REPLACE(auto1.AUTO_MODEL, ' ', '')='" + autoModel + "'";
            }

            if (priceFrom != null)
            {
                filterQuery += " AND auto1.AUTO_PRICE>=" + priceFrom;
            }

            if (hasVin != null)
            {
                filterQuery += hasVin == 1 ? " AND auto1.HAS_VIN=" + hasVin : "";
            }

            if (priceTo != null)
            {
                filterQuery += " AND auto1.AUTO_PRICE<=" + priceTo;
            }

            if (gearbox != null && gearbox != string.Empty)
            {
                filterQuery += " AND auto1.AUTO_GEARBOX='" + gearbox + "'";
            }

            if (yearFrom != null)
            {
                filterQuery += " AND auto1.AUTO_MADE_IN>=" + string.Format("'{0}-01-01'", yearFrom) + "";
            }

            if (yearTo != null)
            {
                filterQuery += " AND auto1.AUTO_MADE_IN<=" + string.Format("'{0}-12-30'", yearTo) + "";
            }

            if (filterQuery.Length < 10)
                filterQuery = "";

            List<Auto> l = new List<Auto>();

            string queryStart = @"SELECT auto1.*, img.IMAGE_PATH AS 'THUMBNAIL' FROM (SELECT LINK_ID, MAX(CREATED_AT) AS CREATED_AT_MAX FROM `AUTO` GROUP BY LINK_ID) AS auto INNER JOIN `AUTO` AS auto1 ON auto.LINK_ID = auto1.LINK_ID AND
                 auto1.CREATED_AT = auto.CREATED_AT_MAX INNER JOIN IMAGES img ON auto1.LINK_ID = img.LINK_ID AND auto1.IS_SOLD=0 AND img.IS_MAIN_IMAGE=true ";

            string queryEnd = " GROUP BY img.LINK_ID LIMIT " + pageNumber.ToString() + " , 20;";

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.Open();
                l = connection.Query<Auto>(queryStart + filterQuery + queryEnd).ToList();
            }

            return l;
        }

        public Auto GetAutoById(long autoId)
        {
            Auto l = new Auto();

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.Open();
                l = connection.Query<Auto>(@"SELECT auto.*, links.LINK_URL, links.AUTO_VIN FROM `AUTO` auto INNER JOIN LINKS links ON links.LINK_ID = auto.LINK_ID WHERE auto.AUTO_ID=" + autoId).First();

            }

            return l;
        }
        public List<Image> GetImagesById(long linkId)
        {
            List<Image> l = new List<Image>();

            using (MySqlConnection connection = new MySqlConnection())
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

            using (MySqlConnection connection = new MySqlConnection())
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

            using (MySqlConnection connection = new MySqlConnection())
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

            using (MySqlConnection connection = new MySqlConnection())
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

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.Open();
                l = connection.Query<string>(@"SELECT DISTINCT AUTO_GEARBOX FROM AUTO").ToList();

            }

            return l;
        }

        public List<string> GetClimateControl()
        {
            List<string> l = new List<string>();

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.Open();
                l = connection.Query<string>(@"SELECT DISTINCT AUTO_CLIMATE_CONTROL FROM AUTO").ToList();

            }

            return l;
        }

        public List<string> GetAutoColor()
        {
            List<string> l = new List<string>();

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.Open();
                l = connection.Query<string>(@"SELECT DISTINCT AUTO_COLOR FROM AUTO").ToList();

            }

            return l;
        }

        public List<string> GetAutoSteering()
        {
            List<string> l = new List<string>();

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.Open();
                l = connection.Query<string>(@"SELECT DISTINCT AUTO_STEERING_WHEEL_SIDE FROM AUTO").ToList();

            }

            return l;
        }

        public List<string> GetAutoWheelDrive()
        {
            List<string> l = new List<string>();

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.Open();
                l = connection.Query<string>(@"SELECT DISTINCT AUTO_WHEEL_DRIVE FROM AUTO").ToList();

            }

            return l;
        }

        public List<string> GetAutoDefects()
        {
            List<string> l = new List<string>();

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.Open();
                l = connection.Query<string>(@"SELECT DISTINCT AUTO_DEFECT FROM AUTO").ToList();

            }

            return l;
        }

    }
}
