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
    public class UserDAL
    {
        private readonly string dbConnString;
        public UserDAL()
        {
            
        }

        //SELECT DISTINCT AUTO_MAKE FROM `AUTO`
        public User ValidateUserData(string email, string password)
        {
            User user = new User();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                user = connection.Query<User>(@"SELECT * FROM USERS WHERE USER_EMAIL='" + email + "' AND USER_PASSWORD='" + password + "'").FirstOrDefault();
            }

            return user;
        }

        public List<Auto> GetRememberedList(int userId)
        {

            List<Auto> l = new List<Auto>();

            using (MySqlConnection connection = new MySqlConnection(dbConnString))
            {
                connection.Open();
                l = connection.Query<Auto>(@"SELECT auto1.*, img.IMAGE_PATH AS 'THUMBNAIL' FROM (SELECT LINK_ID, MAX(CREATED_AT) AS CREATED_AT_MAX FROM `AUTO` GROUP BY LINK_ID) AS auto INNER JOIN `AUTO` AS auto1 ON auto.LINK_ID = auto1.LINK_ID AND
                 auto1.CREATED_AT = auto.CREATED_AT_MAX INNER JOIN IMAGES img ON auto1.LINK_ID = img.LINK_ID AND auto1.IS_SOLD=0 AND img.IS_MAIN_IMAGE=true INNER JOIN REMEMBERED_LIST rL ON rL.USER_ID=" + userId + " AND rL.LINK_ID=auto1.LINK_ID GROUP BY img.LINK_ID").ToList();
                //SELECT auto1.* FROM (SELECT LINK_ID, MAX(CREATED_AT) AS CREATED_AT_MAX FROM `AUTO` GROUP BY LINK_ID) AS auto INNER JOIN `AUTO` AS auto1 ON auto.LINK_ID = auto1.LINK_ID AND auto1.CREATED_AT = auto.CREATED_AT_MAX 

            }

            return l;
        }

        public string AddRememberedItem(int userId, int linkId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(dbConnString))
                {
                    connection.Execute("INSERT INTO REMEMBERED_LIST (USER_ID, LINK_ID) VALUES (" + userId + ", " + linkId + ")");
                }

                return "success";
            }
            catch
            {
                return "error";
            }
        }

        public string RemoveRememberedItem(int userId, int linkId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(dbConnString))
                {
                    connection.Execute("DELETE FROM REMEMBERED_LIST WHERE USER_ID=" + userId + " AND LINK_ID=" + linkId);
                }

                return "success";
            }
            catch
            {
                return "error";
            }
        }



    }
}
