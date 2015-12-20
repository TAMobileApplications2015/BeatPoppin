namespace BeatPoppin.Data.Models
{
    using SQLite.Net.Attributes;

    [Table("GameScore")]
    public class GameScore
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public long Value { get; set; }
        
        public string PlayerName { get; set; }
    }
}
