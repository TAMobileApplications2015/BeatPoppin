namespace BeatPoppin.Data.Models
{
    using Parse;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
    }
}
