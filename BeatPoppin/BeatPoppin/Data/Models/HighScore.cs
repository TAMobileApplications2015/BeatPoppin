namespace BeatPoppin.Data.Models
{
    using Parse;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [ParseClassName("HighScore")]
    public class HighScore : ParseObject
    {
        [ParseFieldName("value")]
        public int Value
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
    }
}
