using System.Threading.Tasks;
using StackExchange.Redis;

namespace SFA.DAS.Experiment.Application.Cms.Services
{
    public interface ICacheService 
    {
        Task ClearKeysStartingWith(string prefix);
        Task<string> Get(string key);
        Task Set(string key, string value);

        Task<bool> KeyExists(string key);
        Task Delete(string key);
    }

    public class CacheService : ICacheService
    {
        private readonly IDatabase _redisDatabase;
        private readonly ConnectionMultiplexer _redisConnection;

        public CacheService(IDatabase redisDatabase, ConnectionMultiplexer redisConnection)
        {
            _redisDatabase = redisDatabase;
            _redisConnection = redisConnection;
        }

        public async Task ClearKeysStartingWith(string prefix)
        {
            var endpoint = _redisConnection.GetEndPoints()[0];
            var server = _redisConnection.GetServer(endpoint);

            foreach (var key in server.Keys(pattern: $"{prefix}_*"))
            {
                await _redisDatabase.KeyDeleteAsync(key);
            }
        }
    
        public async Task Delete(string key)
        {
            await _redisDatabase.KeyDeleteAsync(key);
        }

        public async Task<string> Get(string key)
        {
            return await _redisDatabase.StringGetAsync(key);
        }

        public async Task<bool> KeyExists(string key)
        {
            return await _redisDatabase.KeyExistsAsync(key);
        }

        public async Task Set(string key, string value)
        {
            await _redisDatabase.StringSetAsync(key, value);
        }
    }
}