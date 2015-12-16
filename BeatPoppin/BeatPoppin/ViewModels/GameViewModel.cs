namespace BeatPoppin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Controls;

    public class GameViewModel : BaseViewModel
    {
        private TextBlock notificationBox;

        public GameViewModel(TextBlock notificationBox)
        {
            this.notificationBox = notificationBox;
            this.notificationBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
