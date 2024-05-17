using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Runtime.CompilerServices;
using System.Text.Json;
using WebAppCurrencyValues.Helpers;
using WebAppCurrencyValues.Models;
using WebAppCurrencyValues.Services;

namespace WebAppCurrencyValues.Controllers
{
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        
        [HttpGet("currencies")]
        public async Task<IActionResult> GetAllCurrencies([FromQuery] PaginationHelper pagination)
        {
            try
            {
                var currencies = await _currencyService.GetValuteModels();
                return Ok(currencies
                    .OrderBy(n => n.Name)
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                    .Take(pagination.PageSize)
                    .ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("currency")]
        public async Task<IActionResult> GetCurrencyById([FromQuery] string id)
        {
            try
            {
                var currency = await _currencyService.GetValuteById(id);
                return Ok(currency);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
