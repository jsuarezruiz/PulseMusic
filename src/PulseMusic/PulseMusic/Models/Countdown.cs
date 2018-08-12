using System;
using Xamarin.Forms;

namespace PulseMusic.Models
{
    public class Countdown : BindableObject
    {
        TimeSpan _remainTime;

        public event Action Completed;
        public event Action Ticked;

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsRunning { get; set; }

        public TimeSpan RemainTime
        {
            get { return _remainTime; }

            private set
            {
                _remainTime = value;
                OnPropertyChanged();
            }
        }

        public void Start(int seconds = 1)
        {
            Device.StartTimer(TimeSpan.FromSeconds(seconds), () =>
            {
                if (IsRunning)
                {
                    StartTime += TimeSpan.FromSeconds(1);
                    RemainTime = (EndTime - StartTime);

                    var ticked = RemainTime.TotalSeconds >= 0;

                    if (ticked)
                    {
                        Ticked?.Invoke();
                    }
                    else
                    {
                        Completed?.Invoke();
                    }

                    return ticked;
                }

                return true;
            });
        }
    }
}