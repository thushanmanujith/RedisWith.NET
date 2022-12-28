using Core.DTO;
using Core.Enum;
using Microsoft.AspNetCore.Mvc;
using Redis.Persistence;

namespace RedisWith.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IRedisPersistence _redisPersistence;
        public RedisController(IRedisPersistence redisPersistence)
        {
            _redisPersistence = redisPersistence;
        }

        [HttpPost("redis_string/{key}/{value}")]
        public async Task<IActionResult> WriteData(string key, string value)
        {
            var result = await _redisPersistence.WriteStringData(key, value);
            return Ok(result);
        }

        [HttpGet("redis_string/{key}")]
        public async Task<IActionResult> ReadData(string key)
        {
            var result = await _redisPersistence.ReadStringData(key);
            return Ok(result);
        }

        [HttpPost("redis_hash/{key}")]
        public async Task<IActionResult> WriteHashData(string key, UserDto user)
        {
            await _redisPersistence.WriteHashData(key, user);
            return Ok();
        }

        [HttpGet("redis_hash/{key}/{hashField}")]
        public async Task<IActionResult> GetHashData(string key, UserEnum hashField)
        {
            var result = await _redisPersistence.ReadHashKeyValue(key, hashField);
            //var result = await _redisPersistence.ReadAllHashEntries(key);
            //var result = await _redisPersistence.ReadAllHashValues(key);
            //var result = await _redisPersistence.ReadAllHashKeys(key);
            //var result = await _redisPersistence.GetHashLength(key);
            //var result = await _redisPersistence.GetHashDecrement(key);
            //var result = await _redisPersistence.GetHashIncrement(key);
            return Ok(result);
        }

        [HttpGet("redis_hash/{key}")]
        public async Task<IActionResult> GetHashData(string key)
        {
            var result = await _redisPersistence.ReadAllHashEntries(key);
            //var result = await _redisPersistence.ReadAllHashValues(key);
            //var result = await _redisPersistence.ReadAllHashKeys(key);
            //var result = await _redisPersistence.GetHashLength(key);
            //var result = await _redisPersistence.GetHashDecrement(key);
            //var result = await _redisPersistence.GetHashIncrement(key);
            return Ok(result);
        }
    }
}
