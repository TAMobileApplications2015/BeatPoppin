namespace BeatPoppin.AttachedProperties
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Shapes;

    public class ShapeProperties
    {
        public static int GetScoreValue(DependencyObject obj)
        {
            return (int)obj.GetValue(ScoreValueProperty);
        }

        public static void SetScoreValue(DependencyObject obj, int value)
        {
            obj.SetValue(ScoreValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for ScoreValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScoreValueProperty =
            DependencyProperty.RegisterAttached("ScoreValue", typeof(int), typeof(Shape), new PropertyMetadata(0));
    }
}
