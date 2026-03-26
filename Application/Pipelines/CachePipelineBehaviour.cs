using Application.Pipelines.Contracts;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Application.Pipelines;
public class CachePipelineBehaviour<TRequest, TResponse>(IDistributedCache cache, IOptions<CacheSettings> cacheSettingsOptions) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICacheable
{
    private readonly IDistributedCache _cache = cache;
    private readonly CacheSettings _cacheSettings = cacheSettingsOptions.Value;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.BypassCache)
            return await next(cancellationToken);

        TResponse response;

        string cacheKey = $"{_cacheSettings.ApplicationName}:{request.CacheKey}";

        var cachedResponse = await _cache.GetAsync(cacheKey, cancellationToken);

        if (cachedResponse != null)
        {
            response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
        }
        else
        {
            response = await GetResponseAndWriteToCacheAsync();
        }

        return response;

        async Task<TResponse> GetResponseAndWriteToCacheAsync()
        {
            response = await next(cancellationToken);

            if (response is not null)
            {
                var slidingExpiration = request.SlidingExpiration == null
                    ? TimeSpan.FromMinutes(_cacheSettings.SlidingExpirationInMinutes)
                    : request.SlidingExpiration;

                var cacheOptions = new DistributedCacheEntryOptions
                {
                    SlidingExpiration = slidingExpiration,
                    AbsoluteExpiration = DateTime.Now.AddMinutes(_cacheSettings.AbsoluteExpirationInMinutes)
                };

                var serializedResponse = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response,
                                                                                               Formatting.Indented,
                                                                                               new JsonSerializerSettings()
                                                                                               {
                                                                                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                                                               }));
                await _cache.SetAsync(cacheKey, serializedResponse, cacheOptions, cancellationToken);
            }

            return response;
        }
    }
}
