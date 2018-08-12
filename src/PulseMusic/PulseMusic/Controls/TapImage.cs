using System.Windows.Input;
using Xamarin.Forms;

namespace PulseMusic.Controls
{
    public class TapImage : Image
    {
        public static readonly BindableProperty TapCommandProperty =
           BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(TapImage), default(ICommand), propertyChanged: OnCommandChanged);

        public ICommand TapCommand
        {
            get { return (ICommand)GetValue(TapCommandProperty); }
            set { SetValue(TapCommandProperty, value); }
        }

        private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var image = bindable as TapImage;

            image?.GestureRecognizers.Clear();
            image?.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = image?.TapCommand
            });
        }
    }
}