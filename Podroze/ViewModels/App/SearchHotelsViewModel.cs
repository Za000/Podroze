using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Podroze.Controllers.App;
using Podroze.Models;

namespace Podroze.ViewModels.App;

public partial class SearchHotelsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

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

    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ICommand PerformSearch => new Command<string>((string query) =>
    {
        var resultLits = SearchAutocomplete.GetSearchResults(query).Result.results;
        SearchResults = resultLits;
        searchResults = resultLits;
    });
}