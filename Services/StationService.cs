using Azure.Data.Tables;
using PropertySearch.Infrastructure;
using PropertySearch.Models;

namespace PropertySearch.Services
{
    interface IStationService
    {
        public Task SearchStationAsync(string name);
    }

    public class StationService: IStationService
    {
        private readonly IStationApiClient _stationApiClient;

        public StationService(
            IStationApiClient stationApiClient)
        {
            _stationApiClient = stationApiClient;
            _yahooTransitApiClient = yahooTransitApiClient;
        }

        public async Task SearchStationAsync(string name)
        {
            await _stationApiClient.GetStationAsync(name, PrefectureCode.Tokyo);
            return;
        }
    }
}
