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
    public static class InputValidationHelper
    {

        public static string SanitizeIllegalCharacters(string strIn = "")
        {
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

    }
}
