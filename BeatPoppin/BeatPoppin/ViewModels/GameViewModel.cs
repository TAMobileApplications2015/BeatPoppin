namespace BeatPoppin.ViewModels
{
    using Commands;
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.UI.Xaml.Controls;

    public class GameViewModel : BaseViewModel
    {
        private LocalData localData;
        private RemoteData remoteData;

        public GameViewModel()
        {
            this.localData = new LocalData(new LocalDb());
            this.remoteData = new RemoteData();
        }
    }
}
