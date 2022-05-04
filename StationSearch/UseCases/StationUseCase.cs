using StationSearch.Services;

namespace StationSearch.UseCases
{
    interface IStationUseCase
    {
        public Task SearchStationAsync(string name);
    }

    public class StationUseCase : IStationUseCase
    {
        private readonly ITrainTransitService _trainTrainsitService;

        public StationUseCase(
            ITrainTransitService trainTransitService)
        {
            _trainTrainsitService = trainTransitService;
        }

        public async Task SearchStationAsync(string name)
        {
            await _trainTrainsitService.SearchTrainTransitAsync(name);
            return;
        }
    }
}
