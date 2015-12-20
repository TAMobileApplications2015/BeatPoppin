namespace BeatPoppin.ViewModels
{
    public class CircleViewModel : ShapeBaseViewModel
    {
        private const int SValue = 350;

        public CircleViewModel()
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
