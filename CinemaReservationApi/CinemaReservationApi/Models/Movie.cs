using System.Text.Json.Serialization;

public class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Duration { get; set; }

    public string PosterUrl { get; set; } = string.Empty;

    [JsonIgnore] // zapobiega pętli przy serializacji
    public List<Screening>? Screenings { get; set; } // nullable, więc niewymagane
}
