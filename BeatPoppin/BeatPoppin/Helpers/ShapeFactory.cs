namespace BeatPoppin.Helpers
{
    using ViewModels;

    public class ShapeFactory<T> where T : ShapeBaseViewModel, new()
    {
        public T Create(double top, double left)
        {
            var newShape = new T()
            {
                Top = top,
                Left = left
            };

            return newShape;
        }
    }
}
