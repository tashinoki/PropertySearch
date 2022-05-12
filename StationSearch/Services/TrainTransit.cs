using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StationSearch.Services.TrainTransitApi
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public record TrainTransitResponse(IReadOnlyList<TrainTransitWay> Ways)
    {
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public record TrainTransitWay(Station SrcStation, Station DstStation, Line Line, int Min)
    {
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public record Station(string StationName, string StationNameHira, string StationNameRoma, string StationNumber)
    {
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public record Line(string LineName, string StationCount, string LinePrefix)
    {
    }
}
