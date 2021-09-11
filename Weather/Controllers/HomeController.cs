using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Weather.Models;

namespace Weather.Controllers
{
    public class HomeController : Controller
    {
        private Models.WeatherEntities db = new WeatherEntities();
        JordanCity jordan = new JordanCity();


        public ActionResult Index()
        {
            Index1();
            var last = db.JordanCities.OrderByDescending(x => x.Id).ToList().Take(9);
            return View(last);
        }
        public void Index1()
        {
            string Url;

            List<string> CititsofJordan = new List<string>()
            {

                "Amman","Irbid","Madaba","Jerash","Mafraq","Zarqa","Ramtha","al-Karak","Aqaba"


            };

            JObject json = new JObject();

            List<object> result = new List<object>();
            foreach (var item2 in CititsofJordan)
            {
                Url = "http://api.weatherapi.com/v1/current.json?key=e7a34422478a4ed7a25200215210909&q=" + item2 + "&aqi=no";
                var client = new WebClient();
                var content = client.DownloadString(Url);// as string inside json 
                JordanWeather record = JsonConvert.DeserializeObject<JordanWeather>(content); //  JSON.Net

                double Temp_c = (double)record.current["temp_c"];
                DateTime Date = DateTime.Parse((string)record.current["last_updated"]);
                string CityName = (string)record.location["name"];
                var Check = db.JordanCities.Where(x => x.Lastupdated == Date && x.CitieName == CityName).FirstOrDefault();

                if (Check == null)
                {
                    jordan.Temp_c = (double)record.current["temp_c"];
                    jordan.Temp_f = (double)record.current["temp_f"];
                    jordan.CitieName = (string)record.location["name"];
                    jordan.Lastupdated = DateTime.Parse((string)record.current["last_updated"]);
                    jordan.Localtime = DateTime.Parse((string)record.location["localtime"]);
                    db.JordanCities.Add(jordan);
                    db.SaveChanges();
                }


            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}