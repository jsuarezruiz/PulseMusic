using PulseMusic.Models;
using PulseMusic.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PulseMusic.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {    
        private Song _song;
        private string _title;
        private Countdown _countdown;
        private TimeSpan _startTime;
        private TimeSpan _remainTime;
        private bool _isPlaying;
        private double _progress;
        private string _icon;

        public PlayerViewModel()
        {
            _countdown = new Countdown();
            IsPlaying = true;
            Icon = "pause";
        }

        public ICommand PlayCommand => new Command(Play);
        public ICommand RewindCommand => new Command(Rewind);
        public ICommand PreviousCommand => new Command(Previous);
        public ICommand NextCommand => new Command(Next);
        public ICommand ForwardCommand => new Command(Forward);

        public Song Song
        {
            get => _song;
            set => SetProperty(ref _song, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public TimeSpan StartTime
        {
            get => _startTime;
            set => SetProperty(ref _startTime, value);
        }

        public TimeSpan RemainTime
        {
            get => _remainTime;
            set => SetProperty(ref _remainTime, value);
        }

        public bool IsPlaying
        {
            get => _isPlaying;
            set => SetProperty(ref _isPlaying, value);
        }

        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public override Task LoadAsync()
        {
            LoadSong();

            _countdown.StartTime = TimeSpan.Zero;
            _countdown.EndTime = Song.Duration;
            _countdown.IsRunning = true;

            _countdown.Start();

            _countdown.Ticked += OnCountdownTicked;
            _countdown.Completed += OnCountdownCompleted;

            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.Play, IsPlaying);

            return base.LoadAsync();
        }

        public override Task UnloadAsync()
        {
            _countdown.Ticked -= OnCountdownTicked;
            _countdown.Completed -= OnCountdownCompleted;

            return base.UnloadAsync();
        }

        void LoadSong()
        {
            Song = new Song
            {
                Title = "Imagine Dragons",
                Artist = "Radioactive",
                Cover = "imagine_dragons",
                Duration = new TimeSpan(0, 3, 5)
            };

            Title = string.Format("{0} - {1}", Song.Artist, Song.Title);
        }

        void OnCountdownTicked()
        {
            StartTime = _countdown.StartTime;
            RemainTime = _countdown.RemainTime;

            var totalSeconds = Song.Duration.TotalSeconds;
            var remainSeconds = _countdown.RemainTime.TotalSeconds;
            Progress = remainSeconds / totalSeconds;
        }

        void OnCountdownCompleted()
        {
            Progress = 0;
            IsPlaying = false;

            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.Play, IsPlaying);
        }

        void Play()
        {
            _countdown.IsRunning = !_countdown.IsRunning;
            IsPlaying = _countdown.IsRunning;

            if (_countdown.IsRunning)
            {
                Icon = "pause";
            }
            else
            {
                Icon = "play";
            }

            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.Play, IsPlaying);
        }

        void Rewind()
        {
            Debug.WriteLine("Rewind");
        }

        void Previous()
        {
            Debug.WriteLine("Previous");
        }

        void Next()
        {
            Debug.WriteLine("Next");
        }

        void Forward()
        {
            Debug.WriteLine("Rewind");
        }
    }
}