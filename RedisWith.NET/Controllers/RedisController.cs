using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace RedisWith.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisController(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        [HttpPost("redis_string/{key}/{value}")]
        public async Task<IActionResult> WriteData(string key, string value)
        {
            var db = _redis.GetDatabase();
            var result = await db.StringSetAsync(key, value);
            return Ok(result);
        }

        [HttpGet("redis_string/{key}")]
        public async Task<IActionResult> ReadData(string key)
        {
            var db = _redis.GetDatabase();
            var result = await db.StringGetAsync(key);
            return Ok(result.ToString());
        }
    }
}
