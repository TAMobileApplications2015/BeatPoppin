namespace BeatPoppin.Data.Models
{
    using SQLite.Net.Attributes;

    [Table("Highscore")]
    public class GameScore
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public long Value { get; set; }
    }
}
