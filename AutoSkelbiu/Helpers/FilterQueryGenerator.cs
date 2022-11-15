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

namespace AutoSkelbiu.Helpers
{
    public static class FilterQueryGenerator
    {

        public static string GeneratePriceFrom(long? priceFrom)
        {
            if (priceFrom != null)
            {
                return " AND auto1.AUTO_PRICE>=" + priceFrom;
            }

            return "";
        }

        public static string GeneratePriceTo(long? priceTo)
        {
            if (priceTo != null)
            {
                return " AND auto1.AUTO_PRICE<=" + priceTo;
            }
            
            return "";
        }

        public static string GenerateYearFrom(long? yearFrom)
        {
            if (yearFrom != null)
            {
                return " AND auto1.AUTO_MADE_IN>=" + string.Format("'{0}-01-01'", yearFrom) + "";
            }

            return "";
        }

        public static string GenerateYearTo(long? yearTo)
        {
            if (yearTo != null)
            {
                return " AND auto1.AUTO_MADE_IN<=" + string.Format("'{0}-12-30'", yearTo) + "";
            }
            
            return "";
        }

        public static string GenerateGearbox(string gearbox)
        {
            if (gearbox != null && gearbox != string.Empty)
            {
                return " AND auto1.AUTO_GEARBOX='" + gearbox + "'";
            }

            return "";
        }

        public static string GenerateHasVin(int? hasVin)
        {
            if (hasVin != null)
            {
                return hasVin == 1 ? " AND auto1.HAS_VIN=" + hasVin : "";
            }
            
            return "";
        }

        public static string GenerateModel(string model)
        {
            if (model != null && model != "")
            {
                return " AND REPLACE(auto1.AUTO_MODEL, ' ', '')='" + model + "'";
            }
            
            return "";
        }

        public static string GenerateFuelTypeName(long? fuelTypeId)
        {
            AutoDAL dal = new AutoDAL();

            if (fuelTypeId != null)
            {
                string fuelTypeName = dal.GetFuelTypes(fuelTypeId).First().AUTO_FUEL_TYPE_NAME;

                return " AND auto1.AUTO_FUEL_TYPE='" + fuelTypeName + "'";
            }
            
            return "";
        }

        public static string GenerateMake(long? makeId)
        {
            AutoDAL dal = new AutoDAL();

            if (makeId != null)
            {
                string autoMakeName = dal.GetAutoMakes(makeId).First().AUTO_MAKE;

                return " AND auto1.AUTO_MAKE='" + autoMakeName + "'";
            }
            
            return "";
        }

    }
}
