using PropertySearch.Infrastructure;
using PropertySearch.Models;
using PropertySearch.Services;

namespace PropertySearch.UseCases
{
    interface IStationUseCase
    {
        public Task SearchStationAsync(string name);
    }

    public class StationUseCase: IStationUseCase
    {
        private readonly IStationApiClient _stationApiClient;
        private readonly ITrainTransitService _trainTrainsitService;

        public StationUseCase(
            IStationApiClient stationApiClient,
            ITrainTransitService trainTransitService)
        {
            _stationApiClient = stationApiClient;
            _trainTrainsitService = trainTransitService;
        }

        public async Task SearchStationAsync(string name)
        {
            await _trainTrainsitService.SearchTrainTransitAsync(name, "渋谷");
            return;
        }
    }
}
