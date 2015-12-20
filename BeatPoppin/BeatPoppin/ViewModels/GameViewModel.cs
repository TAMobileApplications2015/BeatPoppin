namespace BeatPoppin.ViewModels
{
    using Commands;
    using Data;
    using Data.Models;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.Foundation;
    using Windows.UI.Xaml.Controls;

    public class GameViewModel : BaseViewModel
    {
        private LocalData localData;
        private RemoteData remoteData;

        private const int InitialShapesCount = 10;
        private const int DefaultShapeSize = 100;
        private const double DefaultShapeExpirationTime = 3000;
        private ulong currentGameScore;
        private ICollection<ShapeBaseViewModel> shapes;
        private ObjectPool<RectangleViewModel> rectPool;
        private ObjectPool<TriangleViewModel> trianglePool;
        private ObjectPool<CircleViewModel> circlePool;
        private Random random;
        private double canvasHeight;
        private double canvasWidth;

        public GameViewModel()
        {
            this.localData = new LocalData(new LocalDb());
            this.remoteData = new RemoteData();

            this.InitGame();
        }

        public ulong CurrentGameScore
        {
            get
            {
                return this.currentGameScore;
            }
            set
            {
                this.currentGameScore = value;
                this.OnPropertyChanged("CurrentGameScore");
            }
        }

        public IEnumerable<ShapeBaseViewModel> StartGame(double canvasHeight, double canvasWidth)
        {
            this.canvasHeight = canvasHeight;
            this.canvasWidth = canvasWidth;
            this.InitGame();
            this.GetFirstShapes();
            return this.shapes;
        }

        public void InitGame()
        {
            this.rectPool = new ObjectPool<RectangleViewModel>(() => new RectangleViewModel());
            this.trianglePool = new ObjectPool<TriangleViewModel>(() => new TriangleViewModel());
            this.circlePool = new ObjectPool<CircleViewModel>(() => new CircleViewModel());
            this.shapes = new List<ShapeBaseViewModel>();

            this.random = new Random();
        }

        private IEnumerable<ShapeBaseViewModel> GetFirstShapes()
        {
            for (int i = 0; i < InitialShapesCount; i++)
            {
                this.GetShapeAtRandomPosition();
            }

            return this.shapes;
        }

        public void UpdateShapesTime(double currentIntervalStepTime)
        {
            var shapesToRemove = new List<ShapeBaseViewModel>();
            foreach (var shape in this.shapes)
            {
                if (shape.ExpirationTime - currentIntervalStepTime <= 0)
                {
                    shapesToRemove.Add(shape);
                }
            }

            foreach (var shape in shapesToRemove)
            {
                this.shapes.Remove(shape);
            }

            foreach (var shape in this.shapes)
            {
                shape.ExpirationTime -= currentIntervalStepTime;
            }
        }

        public ShapeBaseViewModel GetShapeAtRandomPosition()
        {
            var isShapeValid = false;
            ShapeBaseViewModel randomShape = null;
            int randomTop = 0;
            int randomLeft = 0;
            while (!isShapeValid)
            {
                randomTop = this.random.Next((int)this.canvasHeight - DefaultShapeSize + 1);
                randomLeft = this.random.Next((int)this.canvasWidth - DefaultShapeSize + 1);
                if (this.shapes.Count == 0)
                {
                    isShapeValid = true;
                }

                // TODO: fix this silly algorithm
                foreach (var shape in this.shapes)
                {
                    if (randomTop != shape.Top && randomLeft != shape.Left)
                    {
                        isShapeValid = true;
                        var top = shape.Top;
                        var left = shape.Left;
                        var size = shape.Size;

                        P x1 = new P { x = randomTop, y = randomLeft };
                        P y1 = new P { x = randomTop + DefaultShapeSize, y = randomLeft + DefaultShapeSize };
                        P x2 = new P { x = top, y = left };
                        P y2 = new P { x = top + DefaultShapeSize, y = left + DefaultShapeSize };
                        if (doOverlap(x1, y1, x2, y2))
                        {
                            isShapeValid = false;
                        }
                    }
                    else
                    {
                        isShapeValid = false;
                        break;
                    }
                }

                var randomNumber = this.random.Next(3);
                switch (randomNumber)
                {
                    case 0:
                        randomShape = this.rectPool.GetObject();
                        break;
                    case 1:
                        randomShape = this.trianglePool.GetObject();
                        break;
                    case 2:
                        randomShape = this.circlePool.GetObject();
                        break;
                    default:
                        break;
                };
            }

            randomShape.Top = randomTop;
            randomShape.Left = randomLeft;
            randomShape.Size = DefaultShapeSize;
            randomShape.ExpirationTime = DefaultShapeExpirationTime;

            this.shapes.Add(randomShape);
            return randomShape;
        }

        bool doOverlap(P l1, P r1, P l2, P r2)
        {
            if ((l1.x < r2.x) && (r1.x > l2.x) && (l1.y < r2.y) && (r1.y > l2.y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private class P
        {
            public double x, y;
        }

        public void EndGame()
        {

        }
    }
}
