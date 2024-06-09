namespace Podroze.Models.HotelAPI;

public class HotelDetailsModel
{
    public string hotelId { get; set; }
    public string name { get; set; }
    public HotelGeoCodeModel geoCode { get; set; }
    public List<HotelOffersModel> offers { get; set; }
    public HotelSentimentModel sentiment { get; set; }

}

public class HotelGeoCodeModel
{
    public float latitude { get; set; }
    public float longitude { get; set; }
}

public class HotelOffersModel
{
    public string id { get; set; }
    public DateTime checkInDate { get; set; }
    public DateTime checkOutDate { get; set;}
    public HotelOfferRoomModel room { get; set; }
    public HotelOfferDescriptionModel description { get; set; }
    public string boardType { get; set; }
    public HotelOfferGuestsModel guests { get; set; }
    public HotelOfferPriceModel price { get; set; }
    public HotelOfferPoliciesModel policies { get; set; }
}

public class HotelOfferDescriptionModel
{
    public string text { get; set; }
    public string lang { get; set; }
}

public class HotelOfferGuestsModel
{
    public int adults { get; set; }
    public List<int> childAges { get; set; }
}

public class HotelOfferPriceModel
{
    public string currency { get; set; }
    public string sellingTotal { get; set; }
    public string total { get; set; }
}

public class HotelOfferPoliciesModel
{
    public string paymentType { get; set; }

}

public class HotelOfferRoomModel
{
    public HotelOfferRoomEstimatedModel typeEstimated { get; set; }
}

public class HotelOfferRoomEstimatedModel
{
    public string category { get; set; }
    public int beds { get; set; }
    public string bedType { get; set; }
}

public class HotelSentimentModel
{
    public int numberOfReviews { get; set; }
    public int numberOfRatings { get; set; }
    public int overallRating { get; set; }
    public HotelSentimentDetailsModel sentiments { get; set; }
}

public class HotelSentimentDetailsModel
{
    public int sleepQuality { get; set; }
    public int service { get; set; }
    public int facilities { get; set; }
    public int roomComforts { get; set; }
    public int valueForMoney { get; set; }
    public int catering { get; set; }
    public int location { get; set; }
    public int pointsOfInterest { get; set; }
    public int staff { get; set; }
}
