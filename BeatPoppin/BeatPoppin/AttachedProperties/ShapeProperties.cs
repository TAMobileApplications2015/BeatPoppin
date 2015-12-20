namespace BeatPoppin.AttachedProperties
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Shapes;

    public class ShapeProperties
    {
        public static double GetExpirationTime(DependencyObject obj)
        {
            return (double)obj.GetValue(ExpirationTimeProperty);
        }

        public static void SetExpirationTime(DependencyObject obj, double value)
        {
            obj.SetValue(ExpirationTimeProperty, value);
        }

        // Using a DependencyProperty as the backing store for ExpirationTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpirationTimeProperty =
            DependencyProperty.RegisterAttached("ExpirationTime", typeof(double), typeof(Shape), new PropertyMetadata(3000));
    }
}
