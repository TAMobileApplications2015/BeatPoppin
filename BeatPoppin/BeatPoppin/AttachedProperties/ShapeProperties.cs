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

        public static bool GetIsDestroyed(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDestroyedProperty);
        }

        public static void SetIsDestroyed(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDestroyedProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsDestroyed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDestroyedProperty =
            DependencyProperty.RegisterAttached("IsDestroyed", typeof(bool), typeof(Shape), new PropertyMetadata(false));
    }
}
