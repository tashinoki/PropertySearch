using StationSearch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Azure.Data.Tables;
using Azure;

namespace StationSearch.Infrastructure
{
    public interface IStationRepository
    {
        public Task<Station?> GetStationAsync(string name, PrefectureCode code);
    }

    public class StationRepository : IStationRepository
    {
        private readonly static HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://express.heartrails.com"),
            Timeout = TimeSpan.FromSeconds(300)
        };

        public async Task<Station?> GetStationAsync(string name, PrefectureCode code)

        {
            var param = new Dictionary<string, string>()
            {
                { "method", "getStations" },
                { "name", name },
                { "prefecture", PrefectureCodeMapper.GetPrefecture(code) }
            };

            var response = await _httpClient.GetAsync($"/api/json?{ await new FormUrlEncodedContent(param).ReadAsStringAsync() }");

            var content = JsonConvert.DeserializeObject<StationApiResponse>(await response.Content.ReadAsStringAsync());

            if (content is null || !String.IsNullOrEmpty(content.Response.Error))
            {
                return null;
            }

            var station = content.Response.Station.FirstOrDefault();
            
            if (station is null)
            {
                return null;
            }

            return new Station(station.Name, station.Prefecture, code);
        }
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    record StationApiResponse(Response Response)
    { }


    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    record Response(IReadOnlyList<StationInformation> Station, string Error)
    { }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    record StationInformation(string Name, string Prefecture, string Line)
    { }


    class StationEntity : ITableEntity
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }
    }
}