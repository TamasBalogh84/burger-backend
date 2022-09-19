﻿using BurgerBackend.Domain.Config;
using BurgerBackend.Domain.Entities.Cosmos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BurgerBackend.Domain.Repositories.Cosmos
{
    public class BurgerPlacesRepository : CosmosRepositoryBase<BurgerPlace>, IBurgerPlacesRepository
    {
        public BurgerPlacesRepository(CosmosClient client, IOptions<CosmosConfiguration> cosmosConfiguration, ILogger<BurgerPlacesRepository> logger) 
            : base(client, cosmosConfiguration.Value.Database, cosmosConfiguration.Value.BurgerPlacesContainer, logger)
        {}

        public async Task<IEnumerable<Review>> GetReviewsByPlaceIdAsync(Guid placeId, CancellationToken cancellationToken)
        {
            var result = await GetByIdAsync(placeId, cancellationToken: cancellationToken);

            return result?.Reviews ?? Enumerable.Empty<Review>();
        }

        public async Task<Review?> GetReviewByIdAsync(Guid reviewId, CancellationToken cancellationToken)
        {
            var places = await GetAllAsync(cancellationToken: cancellationToken);

           return places.Select(p => p.Reviews.FirstOrDefault(r => r.Id == reviewId)).FirstOrDefault();
        }
    }
}