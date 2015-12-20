namespace BeatPoppin.ViewModels
{
    public abstract class ShapeBaseViewModel : BaseViewModel
    {
        private double left;
        private double top;
        private double size;
        private double expirationTime;

        public abstract int ScoreValue { get; }

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

        public double ExpirationTime
        {
            get
            {
                return this.expirationTime;
            }
            set
            {
                this.expirationTime = value;
            }
        }
    }
}
