namespace BeatPoppin.Pages
{
    using BeatPoppin.AttachedProperties;
    using BeatPoppin.ViewModels;
    using System;
    using System.Collections.Generic;
    using Windows.Foundation;
    using Windows.Media.Capture;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Navigation;
    using Windows.UI.Xaml.Shapes;
    using Windows.Media.Capture;
    using Windows.Storage;
    using Windows.Storage.Streams;
    public sealed partial class GamePage : Page
    {
        private MediaCapture _mediaCaptureMgr;

        private const double ShapeStartsToExpireAtMilliSeconds = 1000;
        private const int MaxShapesOnCanvas = 15;
        private const string PlayerHighScoredMessage = "Yayy!! You are too good ;p";
        private const string PlayerDidNotHighScoredMessage = "Sorry!! Not this time :(";
        private double currentGameStage = 3000;
        private Dictionary<Shape, double> shapesExpirationTime = new Dictionary<Shape, double>();
        private HashSet<Shape> shapesCurrentlyBeingRemovedWithAnimation = new HashSet<Shape>();
        private Random random = new Random();

        public GamePage()
        {
            this.InitializeComponent();

            this.ViewModel = new GameViewModel();
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

        public bool GameOver { get; private set; }

        private void StartGame()
        {
            // TODO: check if those params are correct
            var firstShapes = this.ViewModel.StartGame(this.Canvas.Height, this.Canvas.Width);
            foreach (var shape in firstShapes)
            {
                this.AddShapeToCanvas(shape);
            }

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(this.currentGameStage);
            timer.Tick += (snd, args) =>
            {
                if (this.GameOver)
                {
                    this.PrepareScoreScreen();
                    timer.Stop();
                }
                
                if (this.currentGameStage <= 0)
                {
                    this.currentGameStage = this.random.Next(500,2000);
                }

                this.ClearCanvasFromDestroyedShapes();

                if (this.shapesExpirationTime.Count < MaxShapesOnCanvas)
                {
                    var newShape = this.ViewModel.GetShapeAtRandomPosition();
                    this.AddShapeToCanvas(newShape);
                }

                var randomExpirationTimeStep = this.random.Next(1, (int)Math.Ceiling((this.currentGameStage / 5)));
                this.currentGameStage -= randomExpirationTimeStep;
                this.UpdateShapes(randomExpirationTimeStep);
                timer.Interval = TimeSpan.FromMilliseconds(this.currentGameStage);
            };

            timer.Start();
        }

        private async void PrepareScoreScreen()
        {
            this.StopPreviewAsBackground();
            this.ScoreContainer.Visibility = Visibility.Visible;
            this.GameContainer.Visibility = Visibility.Collapsed;
            var playerHighScored = await this.ViewModel.PlayerHighScored();
            if (playerHighScored)
            {
                this.GameScoreMessage.Text = PlayerHighScoredMessage;
                this.ccHighScored.IsEnabled = true;
            }
            else
            {
                this.GameScoreMessage.Text = PlayerDidNotHighScoredMessage;
            }
        }

        private void AddShapeToCanvas(ShapeBaseViewModel shape)
        {
            Shape shapeToAdd = null;
            if (shape is RectangleViewModel)
            {
                shapeToAdd = new Rectangle();

                shapeToAdd.Tapped += this.RectangleTapped;
                shapeToAdd.ManipulationStarted += this.RectangleManipulationStarted;
                shapeToAdd.ManipulationDelta += this.RectangleManipulationDelta;
                shapeToAdd.ManipulationCompleted += this.RectangleManipulationCompleted;
                shapeToAdd.ManipulationInertiaStarting += this.RectangleManipulationInertiaStarting;
                shapeToAdd.ManipulationMode = ManipulationModes.Rotate;
            }
            else if (shape is CircleViewModel)
            {
                shapeToAdd = new Ellipse();

                shapeToAdd.Tapped += this.CircleTapped;
                shapeToAdd.ManipulationStarted += this.CircleManipulationStarted;
                shapeToAdd.ManipulationDelta += this.CircleManipulationDelta;
                shapeToAdd.ManipulationCompleted += this.CircleManipulationCompleted;
                shapeToAdd.ManipulationInertiaStarting += this.CircleManipulationInertiaStarting;
                shapeToAdd.ManipulationMode = ManipulationModes.Scale;
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


                shapeToAdd.Tapped += this.TriangleTapped;
                shapeToAdd.ManipulationStarted += this.TriangleManipulationStarted;
                shapeToAdd.ManipulationDelta += this.TriangleManipulationDelta;
                shapeToAdd.ManipulationCompleted += this.TriangleManipulationCompleted;
                shapeToAdd.ManipulationInertiaStarting += this.TriangleManipulationInertiaStarting;
                shapeToAdd.ManipulationMode = ManipulationModes.TranslateX;
            }

            shapeToAdd.Height = shape.Size;
            shapeToAdd.Width = shape.Size;
            shapeToAdd.Fill = new SolidColorBrush(Colors.Black);

            shapesExpirationTime.Add(shapeToAdd, shape.ExpirationTime);
            var randomLastScoreDigit = this.random.Next(0, 10);
            ShapeProperties.SetScoreValue(shapeToAdd, shape.ScoreValue + randomLastScoreDigit);

            this.Canvas.Children.Add(shapeToAdd);
            Canvas.SetTop(shapeToAdd, shape.Top);
            Canvas.SetLeft(shapeToAdd, shape.Left);
        }

        private void UpdateShapes(double currentIntervalStepTime)
        {
            var shapes = this.Canvas.Children;
            foreach (var s in shapes)
            {
                var shape = s as Shape;
                if (shapesCurrentlyBeingRemovedWithAnimation.Contains(shape))
                {
                    continue;
                }

                var shapeExpirationTime = shapesExpirationTime[shape];
                shapesExpirationTime[shape] -= currentIntervalStepTime;
                if (shapesExpirationTime[shape] <= ShapeStartsToExpireAtMilliSeconds)
                {
                    if (shapesExpirationTime[shape] <= 0)
                    {
                        this.GameOver = true;
                    }
                    else
                    {
                        AnimationProperties.SetShapeIsExpiring(shape, true);
                    }
                }
            }
        }

        private void ClearCanvasFromDestroyedShapes()
        {
            var shapesToRemove = new List<Shape>();
            foreach (var element in this.Canvas.Children)
            {
                if (ShapeProperties.GetIsDestroyed(element))
                {
                    shapesToRemove.Add(element as Shape);
                }
            }

            foreach (var shape in shapesToRemove)
            {
                this.RemoveShapeFromCanvas(shape as Shape);
            }
        }

        private void RemoveShapeFromCanvas(Shape shape)
        {
            var top = Canvas.GetTop(shape);
            var left = Canvas.GetLeft(shape);
            this.Canvas.Children.Remove(shape);
            this.ViewModel.RemoveShape(top, left);
            this.shapesExpirationTime.Remove(shape);
        }

        private async void StopPreviewAsBackground()
        {
            if (_mediaCaptureMgr != null)
            {
                await _mediaCaptureMgr.StopPreviewAsync();
                _mediaCaptureMgr.Dispose();
                _mediaCaptureMgr = null;
            }
        }

        private async void LoadPreviewAsBackground()
        {
            // Using Windows.Media.Capture.MediaCapture APIs to stream from webcam
            _mediaCaptureMgr = new MediaCapture();
            await _mediaCaptureMgr.InitializeAsync();

            // Start capture preview.                
            this.myCaptureElement.Source = _mediaCaptureMgr;

            await _mediaCaptureMgr.StartPreviewAsync();
        }

        private void TakeAPhoto(object sender, TappedRoutedEventArgs e)
        {
            this.InitCamera();
        }

        private async void InitCamera()
        {
            var camera = new CameraCaptureUI();
            camera.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);
            var photo = await camera.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (photo == null)
            {
                return;
            }

            var file = await StorageFile.GetFileFromPathAsync(photo.Path);
            byte[] photoAsArr = null;
            using (IRandomAccessStream stream = await file.OpenReadAsync())
            {
                using (DataReader reader = new DataReader(stream.GetInputStreamAt(0)))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    byte[] Bytes = new byte[stream.Size];
                    reader.ReadBytes(Bytes);
                    photoAsArr = Bytes;
                }
            }

            this.ViewModel.SavePlayerPicture(photoAsArr);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.LoadPreviewAsBackground();
            this.StartGame();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            this.StopPreviewAsBackground();
        }

        private void PrepareShapeForRemoval(Shape shape)
        {
            shapesCurrentlyBeingRemovedWithAnimation.Add(shape);

            var shapeValue = ShapeProperties.GetScoreValue(shape);
            this.ViewModel.CurrentGameScore += shapeValue;
        }

        // FOR TEST PURPOSES ALL HAVE TAPPED EVENT ~~~~~~~~~~
        // CIRCLE PINCH GESTURE
        #region CircleGestures
        private void CircleTapped(object sender, TappedRoutedEventArgs e)
        {
            var shape = sender as Ellipse;
            this.PrepareShapeForRemoval(shape);
            AnimationProperties.SetCircleIsDestroyed(shape, true);
        }

        private void CircleManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            
        }

        private void CircleManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (e.Delta.Scale > 0)
            {
                var shape = sender as Ellipse;
                this.PrepareShapeForRemoval(shape);
                AnimationProperties.SetCircleIsDestroyed(shape, true);
            }
        }

        private void CircleManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {

        }

        private void CircleManipulationInertiaStarting(object sender, ManipulationInertiaStartingRoutedEventArgs e)
        {

        }
        #endregion

        // RECT ROTATE GESTURE
        #region RectangleGestures
        private void RectangleTapped(object sender, TappedRoutedEventArgs e)
        {
            var shape = sender as Rectangle;
            this.PrepareShapeForRemoval(shape);
            AnimationProperties.SetRectIsDestroyed(shape, true);
        }

        private void RectangleManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {

        }

        private void RectangleManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (e.Delta.Rotation > 1)
            {
                var shape = sender as Rectangle;
                this.PrepareShapeForRemoval(shape);
                AnimationProperties.SetRectIsDestroyed(shape, true);
            }
        }

        private void RectangleManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {

        }

        private void RectangleManipulationInertiaStarting(object sender, ManipulationInertiaStartingRoutedEventArgs e)
        {

        }
        #endregion

        // TRIANGLE SWIPE GESTURE
        #region TriangleGestures
        private void TriangleTapped(object sender, TappedRoutedEventArgs e)
        {
            var shape = sender as Polygon;
            this.PrepareShapeForRemoval(shape);
            AnimationProperties.SetTriangleIsDestroyed(shape, true);
        }

        private void TriangleManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {

        }

        private void TriangleManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (e.Delta.Translation.X > 5 || e.Delta.Translation.Y > 5)
            {
                var shape = sender as Polygon;
                this.PrepareShapeForRemoval(shape);
                AnimationProperties.SetTriangleIsDestroyed(shape, true);
            }
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
