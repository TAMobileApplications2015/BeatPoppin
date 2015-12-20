namespace BeatPoppin.AttachedProperties
{
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Shapes;

    public class AnimationProperties
    {
        private static DispatcherTimer timer = new DispatcherTimer();

        public static bool GetShapeIsExpiring(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShapeIsExpiringProperty);
        }

        public static void SetShapeIsExpiring(DependencyObject obj, bool value)
        {
            obj.SetValue(ShapeIsExpiringProperty, value);
        }

        // Using a DependencyProperty as the backing store for ShapeIsExpiring.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShapeIsExpiringProperty =
            DependencyProperty.RegisterAttached("ShapeIsExpiring", typeof(bool), typeof(Shape), new PropertyMetadata(false, new PropertyChangedCallback(HandleShapeIsExpiring)));

        private static void HandleShapeIsExpiring(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: Better animation for ExpiringShape
            var shape = d as Shape;
            shape.Fill = new SolidColorBrush(Colors.Red);
        }

        public static bool GetCircleIsDestroyed(DependencyObject obj)
        {
            return (bool)obj.GetValue(CircleIsDestroyedProperty);
        }

        public static void SetCircleIsDestroyed(DependencyObject obj, bool value)
        {
            obj.SetValue(CircleIsDestroyedProperty, value);
        }

        // Using a DependencyProperty as the backing store for CircleIsDestroyed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CircleIsDestroyedProperty =
            DependencyProperty.RegisterAttached("CircleIsDestroyed", typeof(bool), typeof(Shape), new PropertyMetadata(false, new PropertyChangedCallback(HandleCircleIsDestroyed)));

        private static void HandleCircleIsDestroyed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: ANIMATE CIRCLE
        }
        
        public static bool GetRectIsDestroyed(DependencyObject obj)
        {
            return (bool)obj.GetValue(RectIsDestroyedProperty);
        }

        public static void SetRectIsDestroyed(DependencyObject obj, bool value)
        {
            obj.SetValue(RectIsDestroyedProperty, value);
        }

        // Using a DependencyProperty as the backing store for RectIsDestroyed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RectIsDestroyedProperty =
            DependencyProperty.RegisterAttached("RectIsDestroyed", typeof(bool), typeof(Shape), new PropertyMetadata(false, new PropertyChangedCallback(HandleRectIsDestroyed)));

        private static void HandleRectIsDestroyed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: ANIMATE RECTANGLE
        }

        public static bool GetTriangleIsDestroyed(DependencyObject obj)
        {
            return (bool)obj.GetValue(TriangleIsDestroyedProperty);
        }

        public static void SetTriangleIsDestroyed(DependencyObject obj, bool value)
        {
            obj.SetValue(TriangleIsDestroyedProperty, value);
        }

        // Using a DependencyProperty as the backing store for TriangleIsDestroyed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TriangleIsDestroyedProperty =
            DependencyProperty.RegisterAttached("TriangleIsDestroyed", typeof(bool), typeof(Shape), new PropertyMetadata(false, new PropertyChangedCallback(HandleTriangleIsDestroyed)));

        private static void HandleTriangleIsDestroyed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: ANIMATE TRIANGLE
        }
    }
}
