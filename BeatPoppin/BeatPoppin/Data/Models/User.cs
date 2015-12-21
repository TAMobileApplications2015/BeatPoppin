namespace BeatPoppin.Data.Models
{
    using Parse;

    [ParseClassName("User")]
    public class User : ParseObject
    {
        [ParseFieldName("displayName")]
        public string DisplayName
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("highScores")]
        public ParseRelation<HighScore> HighScores
        {
            get { return GetRelationProperty<HighScore>(); }
        }

        [ParseFieldName("Image")]
        public ParseFile Image
        {
            get { return GetProperty<ParseFile>(); }
            set { SetProperty<ParseFile>(value); }
        }
    }
}
