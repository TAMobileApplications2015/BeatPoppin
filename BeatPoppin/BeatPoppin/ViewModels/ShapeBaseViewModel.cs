namespace BeatPoppin.ViewModels
{
    public abstract class ShapeBaseViewModel : BaseViewModel
    {
        private double left;
        private double top;
        
        public double Left
        {
            get
            {
                return this.left;
            }
            set
            {
                if (this.left == value)
                {
                    return;
                }

                this.left = value;
                this.OnPropertyChanged("Left");
            }
        }

        public double Top
        {
            get
            {
                return this.top;
            }
            set
            {
                if (this.top == value)
                {
                    return;
                }

                this.top = value;
                this.OnPropertyChanged("Top");
            }
        }
    }
}
