using Core.DTO;
using Core.Enum;
using StackExchange.Redis;

namespace Redis.Persistence
{
    public interface IRedisPersistence
    {
        Task<bool> WriteStringData(string key, string value);
        Task<string> ReadStringData(string key);
        Task WriteHashData(string key, UserDto user);
        Task<RedisValue> ReadHashKeyValue(string hashKey, UserEnum valueEnum);
        Task<HashEntry[]> ReadAllHashEntries(string hashKey);
        Task<RedisValue[]> ReadAllHashValues(string hashKey);
        Task<RedisValue[]> ReadAllHashKeys(string hashKey);
        Task<long> GetHashLength(string hashKey);
        Task<long> GetHashDecrement(string hashKey);
        Task<long> GetHashIncrement(string hashKey);
    }
}
