namespace BeatPoppin.ViewModels
{
    public class TriangleViewModel : ShapeBaseViewModel
    {
        private double median;

        public TriangleViewModel()
        {
        }

        public double Median
        {
            get
            {
                return this.median;
            }
            set
            {
                this.median = value;
                this.OnPropertyChanged("Median");
            }
        }
    }
}
