namespace BeatPoppin.ViewModels
{
    public class CircleViewModel : ShapeBaseViewModel
    {
        private double radius;

        public CircleViewModel()
        {

        }

        public double Radius
        {
            get
            {
                return this.radius;
            }
            set
            {
                this.radius = value;
                this.OnPropertyChanged("Radius");
            }
        }
    }
}
