using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Podroze.Models;
using System.Text.Json.Nodes;

namespace Podroze.Controllers.App;

public static class SearchAutocomplete
{
    private static string APIKey = (new AppSettings()).api_search;

    public static async Task<PlacesList?> GetSearchResults(string query)
    {
        string? results = await MakeAutocompleteCall(query);

        if(string.IsNullOrEmpty(results))
            return null;

        if (results.Contains("detailedError"))
            return null;
        JsonSerializer serializer = new JsonSerializer();
        PlacesList? SearchResult = Newtonsoft.Json.JsonConvert.DeserializeObject<PlacesList?>
            (results);

        return SearchResult;
    }

    private static async Task<string> MakeAutocompleteCall(string query)
    {
        string URL =
            $"https://api.tomtom.com/search/2/geocode/{query}.json?";
        string urlParameters = $"?language=pl-PL&limit=15&entityTypeSet=Country,CountrySubdivision,CountrySecondarySubdivision,CountryTertiarySubdivision,Municipality,MunicipalitySubdivision&key={APIKey}";

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(URL);
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = client.GetAsync(urlParameters).Result;
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return result;

        }

        return null;
    }
}