using System;

namespace BeatPoppin.ViewModels
{
    public class TriangleViewModel : ShapeBaseViewModel
    {
        private const int SValue = 250;

        public TriangleViewModel()
        {
        }

        public override int ScoreValue
        {
            get
            {
                return SValue;
            }
        }
    }
}
