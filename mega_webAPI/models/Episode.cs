using System.ComponentModel.DataAnnotations.Schema;

namespace mega_webAPI.models
{
    public class Episode
    {
        [Column("episode_id")]
        public int EpisodeId { get; set; }

        [Column("tvshow_id")]
        public int TvshowId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Column("release_date")]
        public DateTime ReleaseDate { get; set; }

        [Column("duration")]
        public int Duration { get; set; }

        [Column("episode_number")]
        public int EpisodeNumber { get; set; }

        [Column("season_number")]
        public int SeasonNumber { get; set; }

        [Column("video_url")]
        public string VideoUrl { get; set; }

        //needs to be add to de appdbcontext in modelbuilder

    }
}
