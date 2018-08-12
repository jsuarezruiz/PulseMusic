using System;

namespace PulseMusic.Models
{
    public class Song
    {
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Artist { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }
        public string ReleaseYear { get; set; }
    }
}