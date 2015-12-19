using BeatPoppin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
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
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            this.InitializeComponent();
            this.ViewModel = new GameViewModel();
            this.LoadPreviewAsBackground();
            this.StartGame();
        }

        public GameViewModel ViewModel
        {
            get
            {
                return this.DataContext as GameViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        public ItemsControl Shapes
        {
            get
            {
                return this.icShapes as ItemsControl;
            }
            set
            {
                this.icShapes = value;
            }
        }

        private async void StartGame()
        {

        }

        private async void LoadPreviewAsBackground()
        {
            // Using Windows.Media.Capture.MediaCapture APIs to stream from webcam
            MediaCapture mediaCaptureMgr = new MediaCapture();
            await mediaCaptureMgr.InitializeAsync();

            // Start capture preview.                
            this.myCaptureElement.Source = mediaCaptureMgr;

            await mediaCaptureMgr.StartPreviewAsync();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

        }

        // CIRCLE PINCH ANIMATION
        #region CircleAnimation
        private void CircleManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {

        }

        private void CircleManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {

        }

        private void CircleManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {

        }

        private void CircleManipulationInertiaStarting(object sender, ManipulationInertiaStartingRoutedEventArgs e)
        {

        }
        #endregion

        // RECT ROTATE ANIMATION
        #region RectangleAnimation
        private void RectangleManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {

        }

        private void RectangleManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {

        }

        private void RectangleManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {

        }

        private void RectangleManipulationInertiaStarting(object sender, ManipulationInertiaStartingRoutedEventArgs e)
        {

        }
        #endregion

        // TRIANGLE SWIPE ANIMATION
        #region TriangleAnimation
        private void TriangleManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {

        }

        private void TriangleManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {

        }

        private void TriangleManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {

        }

        private void TriangleManipulationInertiaStarting(object sender, ManipulationInertiaStartingRoutedEventArgs e)
        {

        }
        #endregion
    }
}
