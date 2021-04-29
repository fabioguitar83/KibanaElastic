using Microsoft.Extensions.Configuration;
using Nest;
using System;

namespace KibanaSerilog.Infrastructure.Elastic
{
    public abstract class BaseRepository
    {

        protected readonly ElasticClient _client;

        public BaseRepository(string index, IConfiguration config)
        {
            var elasticUri = config["ElasticConfiguration:Uri"];

            var settings = new ConnectionSettings(new Uri(elasticUri)).DefaultIndex(index);

            _client = new ElasticClient(settings);
        }

    }
}
