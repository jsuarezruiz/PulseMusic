using PulseMusic.ViewModels;
using PulseMusic.ViewModels.Base;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PulseMusic.Views
{
    public partial class PlayerView : ContentPage
    {
        private CancellationTokenSource animateTimerCancellationTokenSource;

        public PlayerView()
        {
            InitializeComponent();

            BindingContext = new PlayerViewModel();

            MessagingCenter.Subscribe<string, bool>(MessengerKeys.App, MessengerKeys.Play, OnPlay);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as BaseViewModel;
            await vm?.LoadAsync();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var vm = BindingContext as BaseViewModel;
            await vm?.UnloadAsync();
        }

        private void OnPlay(string app, bool isPlaying)
        {
            if (app.Equals(MessengerKeys.App))
            {
                RotateCover(isPlaying);
            }
        }

        private void RotateCover(bool isPlaying)
        {
            if(isPlaying)
            {
                StartCoverAnimation(new CancellationTokenSource());
            }
            else
            {
                ViewExtensions.CancelAnimations(CoverImage);

                if (animateTimerCancellationTokenSource != null)
                {
                    animateTimerCancellationTokenSource.Cancel();
                }
            }
        }

        void StartCoverAnimation(CancellationTokenSource tokenSource)
        {
            try
            {
                animateTimerCancellationTokenSource = tokenSource;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (!animateTimerCancellationTokenSource.IsCancellationRequested)
                    {
                        await CoverImage.RelRotateTo(360, AppSettings.CoverAnimationDuration, Easing.Linear);

                        StartCoverAnimation(animateTimerCancellationTokenSource);
                    }
                });
            }
            catch (TaskCanceledException ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}