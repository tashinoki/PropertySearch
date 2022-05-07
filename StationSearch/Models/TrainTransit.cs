using StationSearch.Services.TrainTransit;

namespace StationSearch.Models
{
    public class TrainTransit
    {
        private readonly IReadOnlyList<TrainTransitWay> _trainTransits;
        
        public TrainTransit(IReadOnlyList<TrainTransitWay> trainTransits)
        {
            _trainTransits = trainTransits;
        }
        public Station Distination
            => _trainTransits.Last().DstStation;
    }
}
