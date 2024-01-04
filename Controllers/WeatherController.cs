using api1.DTO;
using api1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Xml.Serialization;
using weatherForm.DTO;

using static System.Net.WebRequestMethods;

namespace api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private void saveToXml(Weateher weateher)
        {

            xmlmodel xmlmodel;
            var serializer = new XmlSerializer(typeof(xmlmodel));
           

            using (Stream mystream = System.IO.File.OpenRead("D:\\Practicies\\api1\\cashing\\lastcountery.xml"))
            {
                xmlmodel = (xmlmodel) serializer.Deserialize(mystream);
            }

           var flag =   xmlmodel.Weatehers.SingleOrDefault(x=>x.Location.name ==  weateher.Location.name);
            if(flag is null)
            {
                xmlmodel.Weatehers.Add(weateher);
            }
           

            using (Stream stream = System.IO.File.Open("D:\\Practicies\\api1\\cashing\\lastcountery.xml", FileMode.Create))
            {
                serializer.Serialize(stream, xmlmodel);
            }
        }

       
        [HttpGet]
        public IActionResult get(string name)
        {
            HttpClient client = new HttpClient();
            string url = $" http://api.weatherapi.com/v1/current.json?key=cf816923d55440db88580453232712&q={name}";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                Weateher data = response.Content.ReadAsAsync<Weateher>().Result;
                saveToXml(data);
                return Ok(data);
            }
            else
            {
                return BadRequest("error");
            }
            
        }

        
        [HttpGet()]
        [Route("/getCountery")]
        public IActionResult GetByLocation(string lat ,string lon)
        {
            string path = $"https://api.geoapify.com/v1/geocode/reverse?lat={lat}&lon={lon}&apiKey=7069deeedfd64c95a6f483d762bd0c6b";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {

                var data = response.Content.ReadAsAsync<CountryName>().Result;
                string x = data.features[0].properties.state.ToString();
               return get(x);
            }
            else
            {
                return BadRequest("error");
            }
        }


        [HttpGet]
        [Route("GetSunTime")]
        public async Task<IActionResult> GetSunTime(string lat, string lon)
        {
            using (var client = new HttpClient())
            {
                string apiuRL = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lon}";
                HttpResponseMessage response = await client.GetAsync(apiuRL);
                if (true)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var resualtToJSON = JObject.Parse(result);
                    string sunSetTime = resualtToJSON["results"]?["sunrise"]?.ToString();
                    string sunRaiseTime = resualtToJSON["results"]?["sunset"]?.ToString();
                    return Ok(new
                    {
                        sunsetTime = sunSetTime,
                        sunraiseTime = sunRaiseTime
                    });
                }

            }
        }


    }
}
