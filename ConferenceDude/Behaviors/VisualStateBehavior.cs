using System.Windows;

namespace ConferenceDude.Behaviors
{
    public class VisualStateBehavior
    {
        public static readonly DependencyProperty VisualStateProperty =
            DependencyProperty.RegisterAttached("VisualState", typeof(string), typeof(VisualStateBehavior),
            new PropertyMetadata("ReadOnly", OnVisualStatePropertyChanged));

        private static void OnVisualStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var state = e.NewValue as string;
            var element = d as FrameworkElement;
            VisualStateManager.GoToElementState(element, state, false);
        }

        public static string GetVisualState(UIElement element)
        {
            return (string)element.GetValue(VisualStateProperty);
        }

        public static void SetVisualState(UIElement element, string value)
        {
            element.SetValue(VisualStateProperty, value);
        }
    }
}
