using StationSearch.Services;
using StationSearch.Models;
using StationSearch.Infrastructure;

namespace StationSearch.UseCases
{
    interface IStationUseCase
    {
        public Task<string> SearchStationAsync(string name);
    }

    public class StationUseCase : IStationUseCase
    {
        private readonly ITrainTransitService _trainTrainsitService;
        private readonly IStationRepository _stationRepository;

        public StationUseCase(
            ITrainTransitService trainTransitService,
            IStationRepository stationRepository)
        {
            _trainTrainsitService = trainTransitService;
            _stationRepository = stationRepository;
        }

        public async Task<string> SearchStationAsync(string name)
        {
            var station = await _stationRepository.GetStationAsync(name, PrefectureCode.Tokyo);

            if (station is null)
            {
                return "存在しない";
            }

            var transits = await _trainTrainsitService.SearchTrainTransitAsync(name);
            return "";
        }
    }
}
