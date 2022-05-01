namespace StationSearch.Services.TrainTransit
{
    public class TrainTransitResponse
    {
        public IReadOnlyList<TrainTransitWay> Ways { get; set; }
    }

    public class TrainTransitWay
    {
        public Station SrcStation { get; set; }

        public Station DstStation { get; set; }

        public Line Line { get; set; }

        public int Min { get; set; }
    }

    public class Station
    {
        public string StationName { get; set; }

        public string StationNameHira { get; set; }

        public string StationNameRoma { get; set; }

        public string StationNumber { get; set; }
    }

    public class Line
    {
        public string LineName { get; set; }

        public string StationCount { get; set; }

        public string LinePrefix { get; set; }
    }
}
