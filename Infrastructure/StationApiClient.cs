using PropertySearch.Models;

namespace PropertySearch.Infrastructure
{
    public interface IStationApiClient
    {
        public Task<string> GetStationAsync(string name, PrefectureCode code);
    }

    public class StationApiClient: IStationApiClient
    {
        private readonly static HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://express.heartrails.com"),
            Timeout = TimeSpan.FromSeconds(300)
        };

        public async Task<string> GetStationAsync(string name, PrefectureCode code)
        
        {
            var param = new Dictionary<string, string>()
            {
                { "method", "getStations" },
                { "name", name },
                { "prefecture", "“Œ‹ž“s" }
            };

            var response = await _httpClient.GetAsync($"/api/json?{ await new FormUrlEncodedContent(param).ReadAsStringAsync() }");
            var content = await response.Content.ReadAsStringAsync();
            return string.Empty;
        }
    }
}