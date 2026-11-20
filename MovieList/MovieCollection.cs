
namespace MovieList;

public class MovieCollection
{
    private readonly List<Movie> _movies;

    public MovieCollection(IEnumerable<Movie> movies)
    {
        if (movies == null)
        {
            throw new ArgumentNullException(nameof(movies));
        }
        _movies = new List<Movie>(movies);
    }
     
    public Movie Add(string title, int year, string? genre)
    {

        int id = _movies.Any() ? _movies.Max(m => m.Id) + 1 : 1;                            
        Movie movie = new Movie(id, title, year, genre);

        bool isDuplicate = _movies.Exists(m => m.Title.Equals(movie.Title, StringComparison.OrdinalIgnoreCase) && m.Year == movie.Year);
        if (isDuplicate)
        {
            throw new InvalidOperationException("Movie already exists in the list.");
        }

        _movies.Add(movie);

        return movie;
    }

    public IReadOnlyList<Movie> GetAll()
    {
        return _movies.OrderBy(m => m.Title).ThenBy(m => m.Year).ToList().AsReadOnly();
    }

    public IReadOnlyList<Movie> Search(string searchTerm)
    {
        
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            throw new ArgumentException("Search term must contain text.", nameof(searchTerm));
        }
        string searchText = searchTerm.Trim();

        List<Movie> filteredMovies = GetAll().Where(m => m.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

        return filteredMovies.AsReadOnly();
                        
    }
}

