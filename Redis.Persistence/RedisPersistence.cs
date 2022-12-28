using Core.DTO;
using Core.Enum;
using StackExchange.Redis;

namespace Redis.Persistence
{
    public class RedisPersistence : IRedisPersistence
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisPersistence(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<bool> WriteStringData(string key, string value)
        {
            var db = _redis.GetDatabase();
            var result = await db.StringSetAsync(key, value);
            Console.WriteLine("write to Redis");
            return result;
        }

        public async Task<string> ReadStringData(string key)
        {
            var db = _redis.GetDatabase();
            var value = await db.StringGetAsync(key);
            Console.WriteLine($"Read from Redis: {value}");
            return value.ToString();
        }

        public async Task WriteHashData(string key, UserDto user)
        {
            var db = _redis.GetDatabase();

            HashEntry[] redisHashDetails =
            {
                new HashEntry(UserEnum.Name.ToString(), user.Name),
                new HashEntry(UserEnum.Age.ToString(), user.Age),
                new HashEntry(UserEnum.Profession.ToString(), user.Profession)
            };

            await db.HashSetAsync(key, redisHashDetails);
            Console.WriteLine("write to Redis hash");
        }

        public async Task<RedisValue> ReadHashKeyValue(string hashKey, UserEnum valueEnum)
        {
            var db = _redis.GetDatabase();

            if (!await db.HashExistsAsync(hashKey, valueEnum.ToString())) throw new Exception("No data found");

            var value = await db.HashGetAsync(hashKey, valueEnum.ToString());

            Console.WriteLine($"Read from Redis Hash: {value}");

            return value;

        }

        public async Task<HashEntry[]> ReadAllHashEntries(string hashKey)
        {
            var db = _redis.GetDatabase();
            var allHash = await db.HashGetAllAsync(hashKey);

            //get all the items
            foreach (var item in allHash)
            {
                //output example:
                //key: Name, value: Ravindra Naik
                //key: Age , value: 26
                //key: Profession, value: Software Engineer
                Console.WriteLine($"key:{item.Name}, value: {item.Value}");
            }

            return allHash;
        }

        public async Task<RedisValue[]> ReadAllHashValues(string hashKey)
        {
            var db = _redis.GetDatabase();

            //get all the values
            var values = await db.HashValuesAsync(hashKey);
            foreach (var val in values)
            {
                Console.WriteLine(val); //result example = Ravindra Naik, 26, Software Engineer
            }

            return values;
        }

        public async Task<RedisValue[]> ReadAllHashKeys(string hashKey)
        {
            var db = _redis.GetDatabase();

            //get all the keys
            var keys = await db.HashKeysAsync(hashKey);
            foreach (var k in keys)
            {
                Console.WriteLine(k); //result example = Name, Age, Profession
            }

            return keys;
        }

        public async Task<long> GetHashLength(string hashKey)
        {
            var db = _redis.GetDatabase();

            var len = await db.HashLengthAsync(hashKey); //result of len is 3
            Console.WriteLine($"Length: {len}");

            return len;
        }

        public async Task<long> GetHashDecrement(string hashKey)
        {
            var db = _redis.GetDatabase();

            var age = await db.HashDecrementAsync(hashKey, "Age", 1); //year now becomes 26
            Console.WriteLine($"HashDecrement: {age}");

            return age;
        }

        public async Task<long> GetHashIncrement(string hashKey)
        {
            var db = _redis.GetDatabase();

            var age = await db.HashIncrementAsync(hashKey, "Age", 1); //year now becomes 27
            Console.WriteLine($"HashIncrement: {age}");

            return age;
        }
    }
}
