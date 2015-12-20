namespace BeatPoppin.Data.Models
{
    using Parse;

    [ParseClassName("HighScore")]
    public class HighScore : ParseObject
    {
        [ParseFieldName("value")]
        public long Value
        {
            get { return GetProperty<long>(); }
            set { SetProperty<long>(value); }
        }
    }
}
