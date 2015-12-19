using BeatPoppin.Commands;
using BeatPoppin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BeatPoppin.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private UISettings uiSettings;

        public HomePage()
        {
            this.InitializeComponent();
            
            this.ViewModel = new HomeViewModel();

            uiSettings = new UISettings();
        }

        public HomeViewModel ViewModel
        {
            get
            {
                return this.DataContext as HomeViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.PrepareScreen();
        }

        private async Task RefreshData()
        {
            await this.ViewModel.RefreshLocalScoreAsync();
            await this.ViewModel.RefreshRemotScoreAsync();
        }

        private async void PrepareScreen()
        {
            VisualStateManager.GoToState(this, "ContentNotLoadedState", false);

            try
            {
                await this.RefreshData();
            }
            catch (System.Net.WebException)
            {
                this.ViewModel.ToastMessage("No Connection with the server!", "Please try again later.", ToastMessageIconsEnum.Frown);
            }

            VisualStateManager.GoToState(this, "ContentLoadedState", uiSettings.AnimationsEnabled);
        }

        private void RefreshPage(object sender, TappedRoutedEventArgs e)
        {
            this.PrepareScreen();
        }
    }
}
