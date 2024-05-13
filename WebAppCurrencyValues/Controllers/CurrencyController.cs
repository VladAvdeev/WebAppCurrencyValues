using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Runtime.CompilerServices;
using System.Text.Json;
using WebAppCurrencyValues.Models;

namespace WebAppCurrencyValues.Controllers
{
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IMapper _mapper;
        
        [HttpGet("currencies")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var client = new RestClient("https://www.cbr-xml-daily.ru/daily_json.js");
            var request = new RestRequest(Method.Get.ToString());
            RestResponse response = client.Execute(request);
            var json = response.Content;
            var data = JsonConvert.DeserializeObject<CurrencyModel>(json);

            var currencies = data.Valute;
            var model = _mapper.Map<CurrencyModel>(currencies);
            return Ok(model);
            
        }
    }
}
