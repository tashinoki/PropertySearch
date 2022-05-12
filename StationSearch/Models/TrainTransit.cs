using StationSearch.Services.TrainTransitApi;

namespace StationSearch.Models
{
    public class TrainTransit
    {
        private readonly IReadOnlyList<TrainTransitWay> _trainTransits;
        public int TotalTime { get; init; }
        public string StartStationName { get; init; }
        public string DestinationStationName { get; init; }
        
        public TrainTransit(IReadOnlyList<TrainTransitWay> trainTransits)
        {
            _trainTransits = trainTransits;

            TotalTime = trainTransits.Select(t => t.Min).Sum();
            StartStationName = trainTransits.First().SrcStation.StationName;
            DestinationStationName = trainTransits.Last().DstStation.StationName;
        }
    }
}
