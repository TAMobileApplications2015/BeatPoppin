namespace BeatPoppin.ViewModels
{
    public class RectangleViewModel : ShapeBaseViewModel
    {
        private const int SValue = 100;

        public RectangleViewModel()
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
