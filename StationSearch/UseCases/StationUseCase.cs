using StationSearch.Services;
using StationSearch.Models;
using StationSearch.Infrastructure;

namespace StationSearch.UseCases
{
    interface IStationUseCase
    {
        public Task<TrainTransitDto> SearchStationAsync(string name);
    }

    public class StationUseCase : IStationUseCase
    {
        private readonly ITrainTransitService _trainTrainsitService;
        private readonly IStationRepository _stationRepository;

        private readonly IReadOnlyDictionary<string, string> _trainNameServiceMapper = new Dictionary<string, string>
        {
            { "押上", "押上〈スカイツリー前〉" }
        };

        public StationUseCase(
            ITrainTransitService trainTransitService,
            IStationRepository stationRepository)
        {
            _trainTrainsitService = trainTransitService;
            _stationRepository = stationRepository;
        }

        public async Task<TrainTransitDto> SearchStationAsync(string name)
        {
            var station = await _stationRepository.GetStationAsync(name, PrefectureCode.Tokyo);

            if (station is null)
            {
                return new TrainTransitDto(new List<TrainTransit>(0), $"{name} という駅は存在しません。", false);
            }

            // Some stations have different names for each service
            // TODO: change the definition of Entity
            if (_trainNameServiceMapper.TryGetValue(station.Name, out var mappedStation))
            {
                name = mappedStation;
            }
            var transits = await _trainTrainsitService.SearchTrainTransitAsync(name);

            return new TrainTransitDto(transits, "", true);
        }
    }

    public record TrainTransitDto(IReadOnlyList<TrainTransit> TrainTransits, string ErrorMessage, bool IsValid)
    {
    }
}
