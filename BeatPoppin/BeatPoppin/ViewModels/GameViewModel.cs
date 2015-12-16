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
        public GameViewModel()
        {
            this.ToastMessage("Error", "Content");
        }
    }
}
