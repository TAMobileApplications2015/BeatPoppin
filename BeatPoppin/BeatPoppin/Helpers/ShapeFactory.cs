namespace BeatPoppin.Helpers
{
    using ViewModels;

    public class ShapeFactory<T> where T : ShapeBaseViewModel, new()
    {
        public T Create(double top, double left, double size)
        {
            var newShape = new T()
            {
                Top = top,
                Left = left,
                Size = size
            };

            return newShape;
        }
    }
}
