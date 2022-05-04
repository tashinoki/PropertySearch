using StationSearch.Services;
using StationSearch.Models;
using StationSearch.Infrastructure;
using StationSearch.Services.TrainTransit;

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
                return new TrainTransitDto(new List<TrainTransitResponse>(0), $"{name} という駅は存在しません。", false);
            }

            var transits = await _trainTrainsitService.SearchTrainTransitAsync(name);

            return new TrainTransitDto(transits, "", true);
        }
    }

    public record TrainTransitDto(IReadOnlyList<TrainTransitResponse> TrainTransits, string ErrorMessage, bool IsValid)
    {
    }
}
