namespace BeatPoppin.Controls
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;
    using BeatPoppin.Pages;

    public sealed partial class NavigationView : UserControl
    {
        private const string ShowMoreButtonHideIconPath = "ms-appx:///Icons/appbar.navigate.previous.png";
        private const string ShowMoreButtonShowIconPath = "ms-appx:///Icons/appbar.navigate.next.png";

        public NavigationView()
        {
            this.InitializeComponent();
        }

        private void ShowMore(object sender, TappedRoutedEventArgs e)
        {
            var button = sender as Button;
            var buttonImageBrush = button.Background as ImageBrush;
            var buttonNewImage = new Image();

            var splitViewContainer = button.Tag as SplitView;
            splitViewContainer.OpenPaneLength = splitViewContainer.ActualWidth;
            splitViewContainer.IsPaneOpen = !splitViewContainer.IsPaneOpen;
            if (splitViewContainer.IsPaneOpen)
            {
                buttonNewImage.Source = new BitmapImage(new Uri(ShowMoreButtonHideIconPath));
            }
            else
            {
                buttonNewImage.Source = new BitmapImage(new Uri(ShowMoreButtonShowIconPath));
            }

            buttonImageBrush.ImageSource = buttonNewImage.Source;
        }

        private void Play(object sender, TappedRoutedEventArgs e)
        {
            (Window.Current.Content as AppShell).Frame.Navigate(typeof(GamePage));
        }

        private void AboutUs(object sender, TappedRoutedEventArgs e)
        {
            (Window.Current.Content as AppShell).Frame.Navigate(typeof(AboutUsPage));
        }

        private void Home(object sender, TappedRoutedEventArgs e)
        {
            (Window.Current.Content as AppShell).Frame.Navigate(typeof(HomePage));
        }

        private void Help(object sender, TappedRoutedEventArgs e)
        {
            (Window.Current.Content as AppShell).Frame.Navigate(typeof(HelpPage));
        }
    }
}
