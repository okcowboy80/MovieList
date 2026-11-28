
using System.Text.Json;

namespace MovieList;

public class MovieStorage
{
    private readonly string _filePath;
    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { WriteIndented = true };

    public MovieStorage(string filePath)
    {
        
        if(string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path can't be blank.", nameof(filePath));
        }

        _filePath = filePath;

    }

    public static string GetDefaultFilePath()
    {
        string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string filePath = Path.Combine(appDataFolder, "MovieList", "movies.json");

        return filePath;
    }

    public void Save(IEnumerable<Movie> movies)
    {
        if(movies == null)
        {
            throw new ArgumentNullException(nameof(movies));
        }
        
        string? directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string moviesJson = JsonSerializer.Serialize(movies, _jsonOptions);
        File.WriteAllText(_filePath, moviesJson);
        
    }

    public IReadOnlyList<Movie> Load()
    {
        if(!File.Exists(_filePath))
        {
            return new List<Movie>().AsReadOnly(); 
        }

        string movieText = File.ReadAllText(_filePath);
        List<Movie>? movies = JsonSerializer.Deserialize<List<Movie>>(movieText, _jsonOptions);

        return (movies ?? new List<Movie>()).AsReadOnly();
    }

}
