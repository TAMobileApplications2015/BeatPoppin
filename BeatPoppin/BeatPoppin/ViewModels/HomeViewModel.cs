using BeatPoppin.Commands;
using BeatPoppin.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace BeatPoppin.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private TextBlock notificationBox;

        public HomeViewModel(TextBlock notificationBox)
        {
            this.notificationBox = notificationBox;
            this.notificationBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
    }
}
