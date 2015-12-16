namespace BeatPoppin.ViewModels
{
    using System.ComponentModel;

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged == null)
            {
                return;
            }

            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
