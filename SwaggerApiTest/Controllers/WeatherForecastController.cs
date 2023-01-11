using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerApiTest.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 건물 네모 매물 검색
        /// </summary>
        /// <param name="BuildingManagementSerialNumber"></param>
        /// <param name="Type"></param>
        /// <returns>number</returns>
        /// <returns>articleType</returns>
        /// <returns>monthlyRent</returns>
        /// <remarks>
        /// 
        /// Parameters:
        ///     
        ///     (string) BuildingManagementSerialNumber : 건물 고유 번호
        ///     (int?) Type : 타입(1: 상가, 2: 사무실, 3: 상가+사무실)
        ///
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        //[Authorize(Roles = "점포거래관리자, 콘텐츠관리자, 매물화요원, 중개요원, 온라인매물화, 온라인제휴서비스관리")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get(string BuildingManagementSerialNumber, int Type)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
