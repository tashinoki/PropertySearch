using StationSearch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StationSearch.Infrastructure
{
    public interface IStationRepository
    {
        public Task<StationApiResponse?> GetStationAsync(string name, PrefectureCode code);
    }

    public class StationRepository : IStationRepository
    {
        private readonly static HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://express.heartrails.com"),
            Timeout = TimeSpan.FromSeconds(300)
        };

        public async Task<StationApiResponse?> GetStationAsync(string name, PrefectureCode code)

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
            return content;
        }
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public record StationApiResponse(Response Response)
    { }


    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public record Response(IReadOnlyList<StationInformation> Station, string Error)
    { }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public record StationInformation(string Name, string Prefecture, string Line)
    { }
}