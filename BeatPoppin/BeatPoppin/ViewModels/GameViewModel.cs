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
    using Windows.UI.Xaml.Controls;

    public class GameViewModel : BaseViewModel
    {
        private LocalData localData;
        private RemoteData remoteData;

        private ulong currentGameScore;
        private ObjectPool<RectangleViewModel> rectPool;
        private ObjectPool<TriangleViewModel> trianglePool;
        private ObjectPool<CircleViewModel> circlePool;
        private ShapeFactory<RectangleViewModel> rectFactory;
        private ShapeFactory<TriangleViewModel> triangleFactory;
        private ShapeFactory<CircleViewModel> circleFactory;

        public GameViewModel()
        {
            this.localData = new LocalData(new LocalDb());
            this.remoteData = new RemoteData();

            this.InitGame();
            this.StartGame();
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

        public void EndGame()
        {

        }

        public void StartGame()
        {

        }

        public void InitGame()
        {
            this.rectPool = new ObjectPool<RectangleViewModel>(() => new RectangleViewModel());
            this.trianglePool = new ObjectPool<TriangleViewModel>(() => new TriangleViewModel());
            this.circlePool = new ObjectPool<CircleViewModel>(() => new CircleViewModel());

            this.rectFactory = new ShapeFactory<RectangleViewModel>();
            this.triangleFactory = new ShapeFactory<TriangleViewModel>();
            this.circleFactory = new ShapeFactory<CircleViewModel>();
        }

        public Task<ShapeBaseViewModel> GetShapeAtRandomPosition()
        {
            throw new NotImplementedException();
        }
    }
}
