namespace BeatPoppin.ViewModels
{
    public class RectangleViewModel : ShapeBaseViewModel
    {
        private double size;

        public RectangleViewModel()
        {
        }

        public double Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
                this.OnPropertyChanged("Size");
            }
        }
    }
}
