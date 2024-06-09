namespace Podroze.Controllers.API;

using Podroze.Models.HotelAPI;
using System.Net.Http.Headers;
using System.Security.Policy;

public static class HotelsAPIController
{
    private static string APIEndPoint = "https://test.api.amadeus.com";
    private static string API_KEY = "bc7QRn33nSAxsSrnG7UwPQ5Z605R";

    public static async Task<List<HotelDetailsModel>?> SearchHotelsByGeoCode(double lat, double lng, int adults, DateTime CheckInDate, DateTime CheckOutDate, string BoardType, List<string>? amenities = null, int rating = 0)
    {
        string URL = $"{APIEndPoint}/v1/reference-data/locations/hotels/by-geocode";
        string URLParameters = $"?latitude={lat}&longitude={lng}&radius=35&radiusUnit=KM";

        if (amenities != null)
        {
            string AmenitiesParams = string.Join("&amenities=", amenities);
            URLParameters += AmenitiesParams;
        }

        if (rating != 0)
        {
            URLParameters += $"&ratings={rating}";
        }

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(URL);
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", API_KEY);

        HttpResponseMessage response = client.GetAsync(URLParameters).Result;
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            Dictionary<string, object> ObjResponse = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(result);

            if (ObjResponse.ContainsKey("data"))
            {
                string data = ObjResponse["data"].ToString();
                List<HotelDetailsModel> HotelDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HotelDetailsModel>?>
                    (data);

                if (HotelDetails != null)
                {
                    int position = 0;
                    foreach (var hotel in HotelDetails)
                    {
                        List<HotelOffersModel>? hotelOffers = await GetHotelOffers(hotel.hotelId, adults, CheckInDate, CheckOutDate, BoardType);

                        if (hotelOffers == null)
                        {
                            continue;
                        }

                        position++;
                        
                        hotel.offers = hotelOffers;

                        HotelSentimentModel hotelSentiment = await GetHotelSentiments(hotel.hotelId);

                        if (hotelSentiment != null)
                            hotel.sentiment = hotelSentiment;

                    }
                }

                return HotelDetails;
            }
            else
            {
                return null;
            }

        }

        return null;
    }

    private static async Task<List<HotelOffersModel>?> GetHotelOffers(string hotelId, int adults, DateTime CheckInDate, DateTime CheckOutDate, string BoardType)
    {
        string ChckInDate = CheckInDate.ToString("yyyy-MM-dd");
        string ChckOutDate = CheckOutDate.ToString("yyyy-MM-dd");

        HttpClient client = new HttpClient();
        string URL = $"{APIEndPoint}/v3/shopping/hotel-offers";
        string URLParameters = $"?hotelIds={hotelId}&adults={adults}&checkInDate={ChckInDate}&checkOutDate={ChckOutDate}&boardType={BoardType}&includeClosed={false}";

        client.BaseAddress = new Uri(URL);
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", API_KEY);

        HttpResponseMessage response = client.GetAsync(URLParameters).Result;
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            Dictionary<string, object> ObjResponse =
                System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(result);

            if (ObjResponse.ContainsKey("data"))
            {
                string data = ObjResponse["data"].ToString();
                List<HotelOffersModel> HotelOffers =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<List<HotelOffersModel>>
                        (data);

                return HotelOffers;
            }
            else
            {
                return null;
            }
        }

        return null;
    }

    private static async Task<HotelSentimentModel?> GetHotelSentiments(string hotelId)
    {

        HttpClient client = new HttpClient();
        string URL = $"{APIEndPoint}/v2/e-reputation/hotel-sentiments";
        string URLParameters = $"?hotelIds={hotelId}";

        client.BaseAddress = new Uri(URL);
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", API_KEY);

        HttpResponseMessage response = client.GetAsync(URLParameters).Result;
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            Dictionary<string, object> ObjResponse =
                System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(result);

            if (ObjResponse.ContainsKey("data"))
            {
                string data = ObjResponse["data"].ToString();
                List<HotelSentimentModel> HotelSentiment =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<List<HotelSentimentModel>>
                        (data);

                return HotelSentiment.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        return null;
    }
}