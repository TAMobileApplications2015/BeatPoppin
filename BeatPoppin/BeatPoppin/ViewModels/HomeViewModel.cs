namespace BeatPoppin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.UI.Xaml.Controls;
    using BeatPoppin.Commands;
    using BeatPoppin.Pages;
    using Data;

    public class HomeViewModel : BaseViewModel
    {
        public int PlayerCurrentHighscore { get; set; }

        public HomeViewModel()
        {
            this.PlayerCurrentHighscore = 5;
        }
    }
}
