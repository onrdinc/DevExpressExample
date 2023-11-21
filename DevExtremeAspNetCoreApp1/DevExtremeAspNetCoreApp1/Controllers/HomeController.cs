using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DevExtremeAspNetCoreApp1.ApiServices.Interfaces;
using DevExtremeAspNetCoreApp1.Models;
using DevExtremeAspNetCoreApp1.Models.CountryDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using static DevExpress.Data.Helpers.FindSearchRichParser;

namespace DevExtremeAspNetCoreApp1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IEnumerable<Country> _countries;
        private readonly IHttpApiService _httpApiService;
        public HomeController(IHttpClientFactory httpClientFactory,IHttpApiService httpApiService)
        {
            _httpClientFactory = httpClientFactory;
            _httpApiService = httpApiService;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var uri = "https://restcountries.com/v3.1/all";

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers = { { HeaderNames.Accept, "application/json" } }
            };
            var responseMessage = await client.SendAsync(requestMessage);
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<List<Country>>(jsonResponse);
            _countries = response;

            return View(response);

        }
        public IActionResult GetMostCrowdedCountries()
        {
            //var client = _httpClientFactory.CreateClient();
            //var uri = "https://restcountries.com/v3.1/all";

            //var requestMessage = new HttpRequestMessage()
            //{
            //    Method = HttpMethod.Get,
            //    RequestUri = new Uri(uri),
            //    Headers = { { HeaderNames.Accept, "application/json" } }
            //};

            //var responseMessage = await client.SendAsync(requestMessage);
            //var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            //var response = JsonSerializer.Deserialize<List<Country>>(jsonResponse);

            ////var asia = response.GroupBy(x => x.region).Select(y => new
            ////{
            ////    Region = y.Key,
            ////    TotalPopulation = y.Sum(k => k.population)

            ////});

            return View();
        }
        public IActionResult GetCountryList()
        {
            return View();
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            var client = _httpClientFactory.CreateClient();
            var uri = "https://restcountries.com/v3.1/all";

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers = { { HeaderNames.Accept, "application/json" } }
            };
            var responseMessage = client.Send(requestMessage);
            var jsonResponse = responseMessage.Content.ReadAsStream();
            var response = JsonSerializer.Deserialize<List<Country>>(jsonResponse);
            var asia = response.GroupBy(x => x.region).Select(y => new
            {
                Region = y.Key,
                TotalPopulation = y.Sum(k => k.population)

            }).ToList();
            return DataSourceLoader.Load(asia, loadOptions);
        }

        [HttpPost]
        public async Task<IActionResult> CountrySave(string values)
        {
            //var jsonValues = JsonSerializer.Serialize(values); // string deðeri JSON'a dönüþtür

            var response = await _httpApiService.PostDataAsync<ResponseBody<CountryGetDto>>("/country/add", values);

            //return View(response);
            //return RedirectToAction("GetMostCrowdedCountries", "Home");
            return Ok();


        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCountry(int key)
        {
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/country/{key}");

            return Ok();    
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountry(int key, string values)
        {
            var jsonObject = JObject.Parse(values);

            jsonObject["Id"] = key;

            string updatedValues = jsonObject.ToString();

            var response = await _httpApiService.PutDataAsync<ResponseBody<CountryGetDto>>("/Country", updatedValues);

            if(response.StatusCode == 200)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public object GetCountry(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var uri = "https://localhost:7117/api/Country/GetAll";

                var requestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(uri),
                    Headers = { { HeaderNames.Accept, "application/json" } }
                };
                var responseMessage = client.Send(requestMessage);
                var jsonResponse = responseMessage.Content.ReadAsStream();
                //var response = JsonSerializer.Deserialize<List<CountryList>>(jsonResponse);
                var response = JsonSerializer.Deserialize < List <CountryGetDto>> (jsonResponse,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return DataSourceLoader.Load(response, loadOptions);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        [HttpGet]
        public object GetData(DataSourceLoadOptions loadOptions)
        {
            var client = _httpClientFactory.CreateClient();
            var uri = "https://restcountries.com/v3.1/all";

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers = { { HeaderNames.Accept, "application/json" } }
            };
            var responseMessage = client.Send(requestMessage);
            var jsonResponse = responseMessage.Content.ReadAsStream();
            var response = JsonSerializer.Deserialize<List<Country>>(jsonResponse);
            return DataSourceLoader.Load(response, loadOptions);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
