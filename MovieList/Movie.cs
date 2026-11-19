
namespace MovieList;

public class Movie
{
    public int Id { get; }
    public string Title { get; }
    public int Year { get; }
    public string? Genre { get; }
    public bool IsWatched { get; private set; }


    public Movie(int id, string title, int year, string? genre, bool isWatched = false)
    {
        int maxYear = DateTime.Today.Year + 5;

        if (id < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(id), id, "Id must be 1 or greater.");
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title can't be blank.", nameof(title));
        }

        if (year < 1888 || year > maxYear)
        {
            throw new ArgumentOutOfRangeException(nameof(year), year, $"Year must be between 1888 and {maxYear}.");
        }

        Id = id;
        Title = title.Trim();
        Year = year;
        Genre = !string.IsNullOrWhiteSpace(genre) ? genre.Trim() : null;
        IsWatched = isWatched;
    }

    public void MarkAsWatched()
    {
        if (IsWatched)
        {
            throw new InvalidOperationException("This movie is already marked as watched.");
        } 
        
        IsWatched = true;
        
    }

}

