using System.Net.Http.Headers;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace StationSearch.Services
{
    public interface ITrainTransitService
    {
        public Task<string> SearchTrainTransitAsync(string startStation, string destinationStation);
    }

    public class TrainTransitService : ITrainTransitService
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.trip2.jp/ex/tokyo/v1.0/json"),
            Timeout = TimeSpan.FromSeconds(300)
        };

        public async Task<string> SearchTrainTransitAsync(string startStation, string destinationStation)
        {
            //var parameters = new Dictionary<string, string>
            //{
            //    { "src", startStation },
            //    { "dst", destinationStation },
            //    { "key", "aaaaaaaaaabbbbbbbbbbccccccccccddddddddddeeeeeeeeeeffffffffff" }
            //};

            //var requestMessage = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Get,
            //    RequestUri = new Uri($"https://api.trip2.jp/ex/tokyo/v1.0/json?{ await new FormUrlEncodedContent(parameters).ReadAsStringAsync() }")
            //};
            //requestMessage.SetBrowserRequestMode(BrowserRequestMode.NoCors);

            //var response = await _httpClient.SendAsync(requestMessage);
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());

            var queryDocument = await context.OpenAsync("https://api.trip2.jp");
            var form = queryDocument.QuerySelector<IHtmlFormElement>("form");
            var src = queryDocument.GetElementById("src_value");
            src.InnerHtml = "三軒茶屋";
            var dest = queryDocument.GetElementById("dst_value");
            dest.InnerHtml = "渋谷";
            var resultDocument = await form.SubmitAsync();

            return "";
        }


        private async Task<string> Scraip()
        {
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());

            var queryDocument = await context.OpenAsync("https://api.trip2.jp");
            var form = queryDocument.QuerySelector<IHtmlFormElement>("form");
            var resultDocument = await form.SubmitAsync(new { q = "anglesharp" });
            return "";
        }
    }
}
