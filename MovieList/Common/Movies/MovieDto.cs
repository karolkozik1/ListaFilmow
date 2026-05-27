

namespace Common.Movies;
public class MovieDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Director { get; set; } = string.Empty;

        public int ReleaseYear { get; set; }
}
    