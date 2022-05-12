using Newtonsoft.Json;
using StationSearch.Models;
using StationSearch.Services.TrainTransitApi;
using System.Net;

namespace StationSearch.Services
{
    public interface ITrainTransitService
    {
        public Task<IReadOnlyList<TrainTransit>> SearchTrainTransitAsync(string srctStation);
    }

    public class TrainTransitService : ITrainTransitService
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.trip2.jp/ex/tokyo/v1.0/json"),
            Timeout = TimeSpan.FromSeconds(300)
        };

        public async Task<IReadOnlyList<TrainTransit>> SearchTrainTransitAsync(string srctStation)
        {
            IReadOnlyList<TrainTransitResponse> traintransit = await Task.WhenAll(
                SearchTransitToDestination(srctStation, DestinationStations.ShibuyaStation),
                SearchTransitToDestination(srctStation, DestinationStations.OmoteSandoStation),
                SearchTransitToDestination(srctStation, DestinationStations.YokohamaStation),
                SearchTransitToDestination(srctStation, DestinationStations.AirportStation),
                SearchTransitToDestination(srctStation, DestinationStations.ShinagawaStawtion)
                );

            return traintransit.Select(t => new TrainTransit(t.Ways)).ToList().AsReadOnly();
        }

        private async Task<TrainTransitResponse> SearchTransitToDestination(string srcStation, string dstStation)
        {
            var parameters = new Dictionary<string, string>
            {
                { "src", srcStation },
                { "dst", dstStation },
                { "key", "aaaaaaaaaabbbbbbbbbbccccccccccddddddddddeeeeeeeeeeffffffffff" }
            };

            var response = await _httpClient.GetAsync($"?{ await new FormUrlEncodedContent(parameters).ReadAsStringAsync() }");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return default(TrainTransitResponse);
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TrainTransitResponse>(content);
        }
    }
}
