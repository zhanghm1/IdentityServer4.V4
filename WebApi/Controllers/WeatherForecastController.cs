using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IUserService _userService;

        private readonly IMemoryCache _memoryCache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
            , IUserService userService
            , IMemoryCache memoryCache
            )
        {
            _logger = logger;
            _userService = userService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Authorize]
        public FilterRule Get()
        {
            var obj1 = User;
            int id = 2;
            string keyName = "xxxxx";
            string value = (string)_memoryCache.Get(keyName);
            if (value == null)
            {
                value = _userService.GetName(id);
                _memoryCache.Set(keyName, value);
            }

            return new FilterRule { key = id.ToString(), value = value };
        }
    }

    public class UserRequest
    {
        public string FilterRules { get; set; }
    }

    public class FilterRule
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}