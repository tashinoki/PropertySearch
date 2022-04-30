using Azure.Data.Tables;
using PropertySearch.Infrastructure;
using PropertySearch.Models;

namespace PropertySearch.UseCases
{
    interface IStationUseCase
    {
        public Task SearchStationAsync(string name);
    }

    public class StationUseCase: IStationUseCase
    {
        private readonly IStationApiClient _stationApiClient;

        public StationUseCase(
            IStationApiClient stationApiClient)
        {
            _stationApiClient = stationApiClient;
        }

        public async Task SearchStationAsync(string name)
        {
            await _stationApiClient.GetStationAsync(name, PrefectureCode.Tokyo);
            return;
        }
    }
}
