namespace BeatPoppin.Data.Models
{
    using SQLite.Net.Attributes;

    [Table("Game")]
    public class Game
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public long Value { get; set; }
        
        public string PlayerName { get; set; }

        public byte[] PlayerPhoto { get; set; }
    }
}
