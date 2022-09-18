using BurgerBackend.Domain.Config;
using BurgerBackend.Domain.Entities.Cosmos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BurgerBackend.Domain.Repositories.Cosmos
{
    public class BurgerPlacesRepository : CosmosRepositoryBase<BurgerPlace>, IBurgerPlacesRepository
    {
        private readonly Container _container;

        private readonly CosmosConfiguration _cosmosConfiguration;

        private readonly ILogger<BurgerPlacesRepository> _logger;

        public BurgerPlacesRepository(CosmosClient client, IOptions<CosmosConfiguration> cosmosConfiguration, ILogger<BurgerPlacesRepository> logger) 
            : base(client, cosmosConfiguration.Value.Database, cosmosConfiguration.Value.BurgerPlacesContainer, logger)
        {
            _cosmosConfiguration = cosmosConfiguration.Value ?? throw new ArgumentNullException(nameof(CosmosConfiguration));
        }
    }
}
