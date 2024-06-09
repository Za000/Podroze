using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Podroze.Controllers.App;
using Podroze.Models;
using Podroze.Controllers.API;
using Podroze.Models.HotelAPI;


namespace Podroze.ViewModels.App;

public partial class SearchHotelsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public List<HotelAmenities> hotelAmenities { get; set; }
    public Dictionary<string,string> boardType { get; set; }
    public int MinimumRooms { get; set; }
    public int MaximumRooms { get; set; }
    public List<HotelDetailsModel> Hotels { get; set; }
    public bool SearchResultsVisibility {get; set; }
    public DateTime CheckInMinDate { get; set; }
    public DateTime CheckOutMinDate { get; set; }

    public int GuestsCount { get; set; }
    public int RoomsCount { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate {get; set; }
    public Place SelectedPlace {get; set; }
    public List<HotelAmenities>? SelectedAmenities { get; set; }
    public int HotelRating { get; set; }

    public int ObMinimumRooms
    {
        get
        {
            return MinimumRooms;
        }
        set
        {
            value = Convert.ToInt32(Math.Ceiling((double)(value / 9.0)));
            MinimumRooms = value;
            NotifyPropertyChanged();
        }
    }

    public int ObMaximumRooms
    {
        get
        {
            return MaximumRooms;
        }
        set
        {
            MaximumRooms = value;
            NotifyPropertyChanged();
        }
    }

    public int ObGuestsCount
    {
        get
        {
            return GuestsCount;
        }

        set
        {
            GuestsCount = value;
            ObMaximumRooms = value;
            ObMinimumRooms = value;
            NotifyPropertyChanged();
        }
    }

    public int ObRoomsCount
    {
        get
        {
            return RoomsCount;
        }

        set
        {
            RoomsCount = value;
            NotifyPropertyChanged();
        }
    }

    public List<HotelDetailsModel> ObHotels
    {
        get
        {
            return Hotels;
        }
        set
        {
            Hotels = value;
            NotifyPropertyChanged();
        }
    }

    public bool ObSearchResultsVisibility
    {
        get
        {
            return SearchResultsVisibility;
        }
        set
        {
            SearchResultsVisibility = value;
            NotifyPropertyChanged();
        }
    }

    public DateTime ObCheckInMinDate
    {
        get
        {
            return CheckInMinDate;
        }

        set
        {
            CheckInMinDate = value;
            NotifyPropertyChanged();
        }
    }

    public DateTime ObCheckOutMinDate
    {
        get
        {
            return CheckOutMinDate;
        }

        set
        {
            CheckOutMinDate = value;
            NotifyPropertyChanged();
        }
    }

    public DateTime? ObCheckInDate
    {
        get
        {
            return CheckInDate;
        }
        set
        {
            CheckInDate = value;
            ObCheckInMinDate = DateTime.Now;
            ObCheckOutMinDate = value.Value.AddDays(1);
            NotifyPropertyChanged();
        }
    }


    public List<Place> searchResults;
    public List<Place> SearchResults
    {
        get
        {
            return searchResults;
        }
        set
        {
            searchResults = value;
            NotifyPropertyChanged();
        }
    }

    public ICommand SearchHotels { get; private set; }


    public SearchHotelsViewModel()
    {
        ObSearchResultsVisibility = false;
        GuestsCount = 1;
        ObCheckInMinDate = DateTime.Now;
        ObCheckOutMinDate = CheckInMinDate.AddDays(1);
        hotelAmenities = new List<HotelAmenities>
        {
            new HotelAmenities{amenity = "SWIMMING_POOL", name = "Basen"},
            new HotelAmenities{amenity = "SPA", name = "Spa"},
            new HotelAmenities{amenity = "FITNESS_CENTER", name = "Centrum rekreacyjne"},
            new HotelAmenities{amenity = "AIR_CONDITIONING", name = "Klimatyzacja"},
            new HotelAmenities{amenity = "RESTAURANT", name = "Restauracja"},
            new HotelAmenities{amenity = "PARKING", name = "Parking"},
            new HotelAmenities{amenity = "PETS_ALLOWED", name = "Zwierzęta dozwolone"},
            new HotelAmenities{amenity = "AIRPORT_SHUTTLE", name = "Transfer na lotnisko"},
            new HotelAmenities{amenity = "BUSINESS_CENTER", name = "Centrum biznesowe"},
            new HotelAmenities{amenity = "DISABLED_FACILITIES", name = "Dla niepełnosprawnych"},
            new HotelAmenities{amenity = "WIFI", name = "WIFI"},
            new HotelAmenities{amenity = "MEETING_ROOMS", name = "Sale konferencyjne"},
            new HotelAmenities{amenity = "NO_KID_ALLOWED", name = "Bez dzieci"},
            new HotelAmenities{amenity = "TENNIS", name = "Kort tennisowy"},
            new HotelAmenities{amenity = "GOLF", name = "Pole golfowe"},
            new HotelAmenities{amenity = "KITCHEN", name = "Kuchnia"},
            new HotelAmenities{amenity = "ANIMAL_WATCHING", name = "Nadzór nad zwierzętami"},
            new HotelAmenities{amenity = "BABY-SITTING", name = "Opieka nad dzieckiem"},
            new HotelAmenities{amenity = "BEACH", name = "Plaża"},
            new HotelAmenities{amenity = "CASINO", name = "Kasyno"},
            new HotelAmenities{amenity = "JACUZZI", name = "Jacuzzi"},
            new HotelAmenities{amenity = "SAUNA", name = "Sauna"},
            new HotelAmenities{amenity = "SOLARIUM", name = "Solarium"},
            new HotelAmenities{amenity = "MASSAGE", name = "Masaż"},
            new HotelAmenities{amenity = "VALET_PARKING", name = "Obsługa parkingu"},
            new HotelAmenities{amenity = "BAR ", name = "Bar"},
            new HotelAmenities{amenity = "KIDS_WELCOME", name = "Dla dzieci"},
            new HotelAmenities{amenity = "TELEVISION", name = "Telewizja"},
            new HotelAmenities{amenity = "WI-FI_IN_ROOM", name = "WIFI w pokoju"},
            new HotelAmenities{amenity = "ROOM_SERVICE", name = "Room service"},
            new HotelAmenities{amenity = "GUARDED_PARKG", name = "Strzeżony parking"},
        };

        boardType = new Dictionary<string, string>
        {
            { "ROOM_ONLY", "Pokój" },
            { "BREAKFAST", "Ze śniadaniem" },
            { "HALF_BOARD", "Obiad oraz kolacja" },
            { "FULL_BOARD", "Pełne wyżywienie" },
            { "ALL_INCLUSIVE", "All Inclusive" }
        };

        SearchHotels = new Command(SearchHotelExecute);

    }

    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ICommand PerformSearch => new Command<string>(async (string query) =>
    {
        var resultLits = SearchAutocomplete.GetSearchResults(query).Result.results;
        SearchResults = resultLits;
        if (resultLits.Any())
        {
            ObSearchResultsVisibility = true;
        }
        else
        {
            ObSearchResultsVisibility = false;
        }
    });

    public async void SearchHotelExecute()
    {
        DateTime ChckInDate = DateTime.Now;
        DateTime ChckOutDate = DateTime.Now.AddDays(1);

        List<string> amenities = new List<string>();

        if (CheckInDate != null)
            ChckInDate = CheckInDate.Value;

        if(CheckOutDate != null)
            ChckOutDate = CheckOutDate.Value;

        if (SelectedAmenities != null)
        {
            foreach (var amenity in SelectedAmenities)
            {
                amenities.Add(amenity.amenity);
            }
        }

        List<HotelDetailsModel>? HotelDetails = HotelsAPIController.SearchHotelsByGeoCode(SelectedPlace.position["lat"], SelectedPlace.position["lon"], GuestsCount, ChckInDate, ChckOutDate, "ALL_INCLUSIVE", amenities, HotelRating).Result;

        if (HotelDetails != null)
        {
            ObHotels = HotelDetails.Where(r => r.offers != null).ToList();
        }
    }



}