using System;
using System.Linq;
using System.Net.Http;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using Dapper;
using MySql.Data.MySqlClient;
using HtmlAgilityPack;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

namespace AutoSkelbiuScraper
{
    public class LINKS
    {
        public int LINK_ID;
        public string LINK_URL;
        public int AUTO_ID;
    }
    public class Auto
    {
        public int? AUTO_ID { get; set; }
        public long LINK_ID { get; set; }
        public string AUTO_MAKE { get; set; }
        public string AUTO_MODEL { get; set; }
        public int AUTO_PRICE { get; set; }
        public DateTime AUTO_MADE_IN { get; set; }
        public int AUTO_MILEAGE { get; set; }
        public string AUTO_ENGINE { get; set; }
        public string AUTO_FUEL_TYPE { get; set; }
        public string AUTO_TYPE { get; set; }
        public string AUTO_DOOR_NUMBER { get; set; }
        public string AUTO_GEARBOX { get; set; }
        public string AUTO_CLIMATE_CONTROL { get; set; }
        public string AUTO_COLOR { get; set; }
        public string AUTO_STEERING_WHEEL_SIDE { get; set; }
        public string AUTO_WHEEL_SIZE { get; set; }
        public string AUTO_SEATS { get; set; }
        public string AUTO_EMISSIONS { get; set; }
        public string AUTO_LENGTH_TYPE { get; set; }
        public string AUTO_HEIGHT_TYPE { get; set; }
        public string AUTO_WHEEL_DRIVE { get; set; }
        public string AUTO_DEFECT { get; set; }
        public string AUTO_SDK { get; set; }
        public string AUTO_TA { get; set; }
        public bool IS_SOLD { get; set; } = false;


    }

    public class Image
    {
        public string TYPE { get; set; }
        public string URL { get; set; }
        public string THUMBNAIL { get; set; }
    }
    public class ImagePath
    {
        public string IMAGE_PATH { get; set; }
        public int LINK_ID { get; set; }
        public bool IS_THUMBNAIL { get; set; }
        public bool IS_MAIN_IMAGE { get; set; } = false;
    }
    class Program
    {
        private static List<LINKS> l = new List<LINKS>();
        private static int userAgentRotation = 4;

        static void Main(string[] args)
        {
            while (true)
            {
                GetUrlList();
                DateTime time = DateTime.Now;
                DateTime timeFinal;
                TimeSpan timeDifference;
                for (int i = 0; i < l.Count; i++)
                {
                    Task.Run(() => Get(l[i])).Wait();

                    timeFinal = DateTime.Now;
                    timeDifference = timeFinal.Subtract(time);

                    Console.WriteLine("Requests made: " + i.ToString());
                    Console.WriteLine("Time elapsed: " + timeDifference.TotalSeconds);
                    int delay = 9000 - Convert.ToInt32(timeDifference.TotalMilliseconds);
                    Thread.Sleep(delay < 0 ? 0 : delay);

                    timeFinal = DateTime.Now;
                    timeDifference = timeFinal.Subtract(time);

                    Console.WriteLine("Time elapsed: " + timeDifference.TotalSeconds);
                    time = DateTime.Now;
                }
            }
        }
        private static void GetUrlList()
        {
            using (MySqlConnection connection = new MySqlConnection("server=localhost;user=scraperis;password=useris3;database=AutoSkelbiu;"))
            {
                connection.Open();
                l = new List<LINKS>();
                l = connection.Query<LINKS>("SELECT * FROM LINKS WHERE IS_SOLD=0 AND SOURCE='autoplius' ORDER BY CREATED_AT DESC").ToList();
            }
        }

        private static void MarkLinkAsSold(long linkId)
        {
            using (MySqlConnection connection = new MySqlConnection("server=localhost;user=scraperis;password=useris3;database=AutoSkelbiu;"))
            {
                connection.Open();
                string sql = "UPDATE LINKS SET IS_SOLD=1, UPDATED_AT=CURRENT_TIMESTAMP() WHERE LINK_ID=@LINK_ID;";
                var affectedRows = connection.Execute(sql, new { LINK_ID = linkId });
                Console.WriteLine(affectedRows);
                sql = "UPDATE AUTO SET IS_SOLD=1 WHERE LINK_ID=@LINK_ID;";
                affectedRows = connection.Execute(sql, new { LINK_ID = linkId });
                Console.WriteLine(affectedRows);
            }
        }

        private static void InsertAuto(Auto auto)
        {
            using (MySqlConnection connection = new MySqlConnection("server=localhost;user=scraperis;password=useris3;database=AutoSkelbiu;"))
            {
                connection.Open();
                string sql = @"INSERT INTO AUTO (AUTO_ID,AUTO_TA,LINK_ID,AUTO_MAKE,AUTO_MODEL,AUTO_PRICE,AUTO_MADE_IN,AUTO_MILEAGE,AUTO_ENGINE,AUTO_FUEL_TYPE,AUTO_TYPE, AUTO_DOOR_NUMBER,AUTO_GEARBOX,AUTO_CLIMATE_CONTROL,AUTO_COLOR,AUTO_STEERING_WHEEL_SIDE,AUTO_WHEEL_SIZE,AUTO_SEATS,AUTO_EMISSIONS,AUTO_LENGTH_TYPE,AUTO_HEIGHT_TYPE, AUTO_WHEEL_DRIVE, AUTO_DEFECT, AUTO_SDK, IS_SOLD)
                 Values 
                 (@AUTO_ID,@AUTO_TA,@LINK_ID,@AUTO_MAKE,@AUTO_MODEL,@AUTO_PRICE,@AUTO_MADE_IN,@AUTO_MILEAGE,@AUTO_ENGINE,@AUTO_FUEL_TYPE,@AUTO_TYPE,@AUTO_DOOR_NUMBER,@AUTO_GEARBOX,@AUTO_CLIMATE_CONTROL,@AUTO_COLOR,@AUTO_STEERING_WHEEL_SIDE,@AUTO_WHEEL_SIZE,@AUTO_SEATS,@AUTO_EMISSIONS,@AUTO_LENGTH_TYPE,@AUTO_HEIGHT_TYPE,@AUTO_WHEEL_DRIVE,@AUTO_DEFECT,@AUTO_SDK,@IS_SOLD);";
                var affectedRows = connection.Execute(sql, auto);
                Console.WriteLine(affectedRows);
            }
        }

        private static void InsertImages(List<ImagePath> images)
        {
            using (MySqlConnection connection = new MySqlConnection("server=localhost;user=scraperis;password=useris3;database=AutoSkelbiu;"))
            {
                connection.Open();
                for (int i = 0; i < images.Count; i++)
                {
                    string sql = "INSERT INTO IMAGES (LINK_ID, IMAGE_PATH, IS_THUMBNAIL, IS_MAIN_IMAGE) Values (@LINK_ID, @IMAGE_PATH, @IS_THUMBNAIL, @IS_MAIN_IMAGE);";
                    var affectedRows = connection.Execute(sql, images[i]);
                    Console.WriteLine(affectedRows);
                }
            }
        }

        static async Task Get(LINKS l)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.

            try
            {

                HttpClient client = new HttpClient();

                switch (userAgentRotation)
                {
                    case 0:
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:86.0) Gecko/20100101 Firefox/86.0");
                        userAgentRotation = 0;
                        break;
                    case 1:
                        client.DefaultRequestHeaders.Add("User-Agent", "AppleWebKit/537.36 (KHTML, like Gecko)");
                        userAgentRotation += 1;
                        break;
                    case 2:
                        client.DefaultRequestHeaders.Add("User-Agent", "Gecko/20100101");
                        userAgentRotation += 1;
                        break;
                    case 3:
                        client.DefaultRequestHeaders.Add("User-Agent", "Chrome/42.0.2311.135");
                        userAgentRotation += 1;
                        break;
                    case 4:
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:95.0) Gecko/20100101 Firefox/95.0");
                        userAgentRotation = 0;
                        break;
                }

                client.DefaultRequestHeaders.Add("Accept", "test/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                client.DefaultRequestHeaders.Add("Host", "autoplius.lt");
                client.DefaultRequestHeaders.Add("Referer", "http://www.google.com/");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, l.LINK_URL))
                {
                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        MarkLinkAsSold(l.LINK_ID);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseBody);
                    GetParameters(responseBody, l.LINK_ID);
                }
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                if (e.Message.Contains("Too Many Requests"))
                    Thread.Sleep(180000);

            }
        }

        static private string FixHtml(string html)
        { return html.Replace("    ", "").Replace("\n", "").Replace("parameter-value ", "parameter-value"); }

        static Auto GetCarMakeAndPrice(Auto auto, string html)
        {

            try
            {
                if (html.Contains("Skelbimas neaktyvus!") || html.Contains("Skelbimas neegzistuoja"))
                {
                    auto.IS_SOLD = true;
                    MarkLinkAsSold(auto.LINK_ID);
                }
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                HtmlNodeCollection nodeCollection = doc.DocumentNode.SelectNodes("//li[@class='crumb']");

                auto.AUTO_MAKE = nodeCollection[2].InnerText;
                auto.AUTO_MODEL = nodeCollection[3].InnerText;

                HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@class='price']");

                auto.AUTO_PRICE = int.Parse(node.InnerText.Split('€')[0].Replace(" ", ""));


                if (html.Contains("Skelbimas neaktyvus!") && html.Contains("Skelbimas neegzistuoja"))
                    auto.IS_SOLD = true;
            }
            catch
            { }

            return auto;
        }

        static private void GetImageGallery(int linkId, string html)
        {
            try
            {
                string json = html.Split(new string[] { "var mediaGalleryItems = " }, StringSplitOptions.None)[1]
                .Split(']')[0] + "]";
                List<Image> imagesFromWeb = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Image>>(json);
                List<ImagePath> imageList = new List<ImagePath>();
                ImagePath iP;
                if (imagesFromWeb.Count <= 0)
                    return;

                for (int i = 0; i < imagesFromWeb.Count; i++)
                {
                    string path = "/var/www/html/AutoSkelbiu/images/" + linkId + "/";
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                    string fileName = string.Format("{0}image-{1}", linkId, i);
                    path += fileName;
                    if (System.IO.File.Exists(path + ".jpeg") && i < (imagesFromWeb.Count))
                        continue;

                    iP = new ImagePath();
                    iP.LINK_ID = linkId;
                    iP.IMAGE_PATH = "AutoSkelbiu/images/" + linkId + "/" + fileName + ".jpeg";
                    iP.IS_THUMBNAIL = false;
                    if (i == 0)
                        iP.IS_MAIN_IMAGE = true;
                    imageList.Add(iP);

                    Task.Run(() => DownloadImage(path, imagesFromWeb[i].URL)).Wait();
                    //Task.Run(() => DownloadImage(path + "thumbnail", imagesFromWeb[i].THUMBNAIL)).Wait();
                }

                InsertImages(imageList);
            }
            catch
            { }

        }

        public async static Task DownloadImage(string name, string url)
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (var response = await client.GetAsync(url))
                    {
                        response.EnsureSuccessStatusCode();

                        byte[] file = await response.Content.ReadAsByteArrayAsync();
                        System.IO.File.WriteAllBytes(name + ".jpeg", file);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load the image: {0}", ex.Message);
            }
        }

        static private string GetParameters(string html, int linkId)
        {
            Console.WriteLine("");
            string parameters = "";
            Auto auto = new Auto();

            auto.AUTO_ID = null;
            auto.LINK_ID = linkId;

            string[] arr = new string[] { "Pagaminimo data", "Rida", "Variklis",
            "Kuro tipas", "Kėbulo tipas", "Durų skaičius", "Pavarų dėžė",
            "Klimato valdymas","Spalva",
            "Vairo padėtis", "Ratlankių skersmuo",
            "Sėdimų vietų skaičius", "CO₂ emisija, g/km",
            "Ilgis", "Aukštis", "Varantieji ratai", "Defektai",
             "SDK", "Tech. apžiūra iki" };

            html = FixHtml(html);

            GetCarMakeAndPrice(auto, html);
            if (!auto.IS_SOLD)
                GetImageGallery(linkId, html);

            for (int i = 0; i < arr.Length; i++)
            {
                try
                {

                    string delimeter = "</div><div class=\"parameter-value\">";


                    parameters = html.Split(new string[] { arr[i] + delimeter }, StringSplitOptions.None)[1]
                    .Split('<')[0];

                    Console.WriteLine(arr[i] + ":  " + parameters);

                    switch (arr[i])
                    {
                        case "Pagaminimo data":
                            if (parameters.Length <= 4)
                                parameters += "-01-01";
                            auto.AUTO_MADE_IN = DateTime.Parse(parameters);
                            break;
                        case "Rida":
                            auto.AUTO_MILEAGE = int.Parse(parameters.Substring(0, parameters.Length - 2).Replace(" ", ""));
                            break;
                        case "Variklis":
                            auto.AUTO_ENGINE = parameters;
                            break;
                        case "Tech. apžiūra iki":
                            auto.AUTO_TA = parameters;
                            break;
                        case "Kuro tipas":
                            auto.AUTO_FUEL_TYPE = parameters;
                            break;
                        case "Kėbulo tipas":
                            auto.AUTO_TYPE = parameters;
                            break;
                        case "Durų skaičius":
                            auto.AUTO_DOOR_NUMBER = parameters;
                            break;
                        case "Pavarų dėžė":
                            auto.AUTO_GEARBOX = parameters;
                            break;
                        case "Klimato valdymas":
                            auto.AUTO_CLIMATE_CONTROL = parameters;
                            break;
                        case "Spalva":
                            auto.AUTO_COLOR = parameters;
                            break;
                        case "Vairo padėtis":
                            auto.AUTO_STEERING_WHEEL_SIDE = parameters;
                            break;
                        case "Ratlankių skersmuo":
                            auto.AUTO_WHEEL_SIZE = parameters;
                            break;
                        case "Sėdimų vietų skaičius":
                            auto.AUTO_SEATS = parameters;
                            break;
                        case "CO₂ emisija, g/km":
                            auto.AUTO_EMISSIONS = parameters;
                            break;
                        case "Ilgis":
                            auto.AUTO_LENGTH_TYPE = parameters;
                            break;
                        case "Aukštis":
                            auto.AUTO_HEIGHT_TYPE = parameters;
                            break;
                        case "Varantieji ratai":
                            auto.AUTO_WHEEL_DRIVE = parameters;
                            break;
                        case "Defektai":
                            auto.AUTO_DEFECT = parameters;
                            break;
                        case "SDK":
                            auto.AUTO_SDK = parameters;
                            break;

                    }
                }
                catch
                {
                    //Console.WriteLine("Klaida!");
                }
            }

            InsertAuto(auto);

            return parameters;
        }

    }
}
