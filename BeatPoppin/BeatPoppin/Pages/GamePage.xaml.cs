using BeatPoppin.AttachedProperties;
using BeatPoppin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BeatPoppin.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private double currentGameStage = 3000;
        private IEnumerable<UIElement> currentElements = new List<UIElement>();

        public GamePage()
        {
            this.InitializeComponent();
            this.ViewModel = new GameViewModel();
            this.LoadPreviewAsBackground();
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

        private void StartGame()
        {
            // TODO: check if those params are correct
            var firstShapes = this.ViewModel.StartGame(this.Canvas.Height, this.Canvas.ActualWidth);
            foreach (var shape in firstShapes)
            {
                this.AddShapeToCanvas(shape);
            }

            //var timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromMilliseconds(this.currentGameStage);
            //timer.Tick += (snd, args) =>
            //{
            //    if (enemyIndex >= enemiesCount - 1)
            //    {
            //        timer.Stop();
            //    }
            //    var enemy = this.AddEnemyAtRandomPosition();
            //    this.enemies.Add(enemy);
            //    this.gameObjects.Add(enemy);
            //    enemyIndex++;
            //};
            //var test = this.Canvas.Children;
            //var anotherTest = test[1];
            //var top = Canvas.GetTop(anotherTest)+ 100;
            //var left = Canvas.GetLeft(anotherTest) + 100;
            //Canvas.SetTop(anotherTest, top);
            //Canvas.SetTop(anotherTest, left);

            //var testt = new Rectangle();
            //var t = AnimationProperties.GetShapeIsExpiring(testt);
            //AnimationProperties.SetShapeIsExpiring(testt, true);
            //AnimationProperties.SetTest(testt, )
            //testt.Height = 50;
            //testt.Width = 50;
            //testt.Fill = new SolidColorBrush(Colors.Blue);
            //Canvas.SetTop(testt, 200);
            //Canvas.SetLeft(testt, 200);
            //this.Canvas.Children.Add(testt);
        }

        private void AddShapeToCanvas(ShapeBaseViewModel shape)
        {
            Shape shapeToAdd = null;
            if (shape is RectangleViewModel)
            {
                shapeToAdd = new Rectangle();
                shapeToAdd.ManipulationStarted += this.RectangleManipulationStarted;
                shapeToAdd.ManipulationDelta += this.RectangleManipulationDelta;
                shapeToAdd.ManipulationCompleted += this.RectangleManipulationCompleted;
                shapeToAdd.ManipulationInertiaStarting += this.RectangleManipulationInertiaStarting;
            }
            else if (shape is CircleViewModel)
            {
                shapeToAdd = new Ellipse();
                shapeToAdd.ManipulationStarted += this.CircleManipulationStarted;
                shapeToAdd.ManipulationDelta += this.CircleManipulationDelta;
                shapeToAdd.ManipulationCompleted += this.CircleManipulationCompleted;
                shapeToAdd.ManipulationInertiaStarting += this.CircleManipulationInertiaStarting;
            }
            else
            {
                shapeToAdd = new Polygon()
                {
                    Points = new PointCollection()
                    {
                        new Point(0, shape.Size),
                        new Point(shape.Size/2,0),
                        new Point(shape.Size, shape.Size)
                    }
                };

                shapeToAdd.ManipulationStarted += this.TriangleManipulationStarted;
                shapeToAdd.ManipulationDelta += this.TriangleManipulationDelta;
                shapeToAdd.ManipulationCompleted += this.TriangleManipulationCompleted;
                shapeToAdd.ManipulationInertiaStarting += this.TriangleManipulationInertiaStarting;
            }

            shapeToAdd.Height = shape.Size;
            shapeToAdd.Width = shape.Size;
            shapeToAdd.Fill = new SolidColorBrush(Colors.AliceBlue);
            this.Canvas.Children.Add(shapeToAdd);
            Canvas.SetTop(shapeToAdd, shape.Top);
            Canvas.SetLeft(shapeToAdd, shape.Left);
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
            this.StartGame();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

        }

        // CIRCLE PINCH GESTURE
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

        // RECT ROTATE GESTURE
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

        // TRIANGLE SWIPE GESTURE
        private UIElement testTriangles;
        #region TriangleAnimation
        private void TriangleManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            if (this.testTriangles == null)
            {
                this.testTriangles = sender as UIElement;
            }
            else
            {
                bool areEqual = this.testTriangles.Equals(sender as UIElement);
            }
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
