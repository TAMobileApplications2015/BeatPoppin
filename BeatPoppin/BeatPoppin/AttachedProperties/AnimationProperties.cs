﻿namespace BeatPoppin.AttachedProperties
{
    using System;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Shapes;

    public class AnimationProperties
    {
        private const double StepAnimation = 50;

        public static bool GetShapeIsExpiring(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShapeIsExpiringProperty);
        }

        public static void SetShapeIsExpiring(DependencyObject obj, bool value)
        {
            obj.SetValue(ShapeIsExpiringProperty, value);
        }
        
        public static readonly DependencyProperty ShapeIsExpiringProperty =
            DependencyProperty.RegisterAttached("ShapeIsExpiring", typeof(bool), typeof(Shape), new PropertyMetadata(false, new PropertyChangedCallback(HandleShapeIsExpiring)));

        private static void HandleShapeIsExpiring(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: Better animation for ExpiringShape
            var obj = d as Shape;
            obj.Stroke = new SolidColorBrush(Colors.Red);
            var addStroke = false;
            var step = 2;

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(25);
            timer.Tick += (snd, args) =>
            {
                if (ShapeProperties.GetIsDestroyed(obj))
                {
                    timer.Stop();
                }

                if (addStroke)
                {
                    obj.StrokeThickness += 1;
                }
                else
                {
                    obj.StrokeThickness -= 1;
                }

                if (obj.StrokeThickness > 5)
                {
                    addStroke = false;
                }
                else if (obj.StrokeThickness <= 0)
                {
                    addStroke = true;
                }
            };

            timer.Start();
        }

        public static bool GetCircleIsDestroyed(DependencyObject obj)
        {
            return (bool)obj.GetValue(CircleIsDestroyedProperty);
        }

        public static void SetCircleIsDestroyed(DependencyObject obj, bool value)
        {
            obj.SetValue(CircleIsDestroyedProperty, value);
        }

        public static readonly DependencyProperty CircleIsDestroyedProperty =
            DependencyProperty.RegisterAttached("CircleIsDestroyed", typeof(bool), typeof(Shape), new PropertyMetadata(false, new PropertyChangedCallback(HandleCircleIsDestroyed)));

        private static void HandleCircleIsDestroyed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: BETTER ANIMATE CIRCLE
            var obj = d as Ellipse;
            var step = 2;

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += (snd, args) =>
            {
                if (step > StepAnimation || obj.Width < step)
                {
                    ShapeProperties.SetIsDestroyed(obj, true);
                    timer.Stop();
                }
                else
                {
                    obj.Width += step / 2;
                    obj.Height += step / 2;
                    obj.Opacity -= 0.1;
                }
            };

            timer.Start();
        }

        public static bool GetRectIsDestroyed(DependencyObject obj)
        {
            return (bool)obj.GetValue(RectIsDestroyedProperty);
        }

        public static void SetRectIsDestroyed(DependencyObject obj, bool value)
        {
            obj.SetValue(RectIsDestroyedProperty, value);
        }
        
        public static readonly DependencyProperty RectIsDestroyedProperty =

            DependencyProperty.RegisterAttached("RectIsDestroyed", typeof(bool), typeof(Shape), new PropertyMetadata(false, new PropertyChangedCallback(HandleRectIsDestroyed)));

        private static void HandleRectIsDestroyed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: BETTER ANIMATE RECTANGLE
            var step = 0;
            var obj = d as Rectangle;

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += (snd, args) =>
            {
                if (step > StepAnimation || obj.Width < step)
                {
                    ShapeProperties.SetIsDestroyed(obj, true);
                    timer.Stop();
                }
                else
                {
                    obj.Opacity -= 0.1;
                }

                step++;
            };
            timer.Start();
        }

        public static bool GetTriangleIsDestroyed(DependencyObject obj)
        {
            return (bool)obj.GetValue(TriangleIsDestroyedProperty);
        }

        public static void SetTriangleIsDestroyed(DependencyObject obj, bool value)
        {
            obj.SetValue(TriangleIsDestroyedProperty, value);
        }

        public static readonly DependencyProperty TriangleIsDestroyedProperty =
            DependencyProperty.RegisterAttached("TriangleIsDestroyed", typeof(bool), typeof(Shape), new PropertyMetadata(false, new PropertyChangedCallback(HandleTriangleIsDestroyed)));

        private static void HandleTriangleIsDestroyed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: BETTER ANIMATE TRIANGLE
            var step = 0;

            var obj = d as Polygon;

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(30);
            timer.Tick += (snd, args) =>
            {
                if (step > StepAnimation || obj.Width < step)
                {
                    ShapeProperties.SetIsDestroyed(obj, true);
                    timer.Stop();
                }
                else
                {
                    obj.Width -= step;
                    obj.Height -= step;
                    obj.Visibility = obj.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
                    obj.Opacity -= 0.1;
                }

                step++;
            };

            timer.Start();
        }
    }
}
