using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;

namespace PulseMusic.Controls
{
    public class CircleProgress : SKCanvasView
    {
        public static readonly BindableProperty StrokeWidthProperty =
            BindableProperty.Create(nameof(StrokeWidth), typeof(float), typeof(CircleProgress), 10f, propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(nameof(Progress), typeof(float), typeof(CircleProgress), 0f, propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty LineBackgroundColorProperty =
            BindableProperty.Create(nameof(LineBackgroundColor), typeof(Color), typeof(CircleProgress), Color.Default, propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty ProgressColorProperty =
            BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(CircleProgress), Color.Blue, propertyChanged: OnPropertyChanged);

        private const float StartAngle = 15;
        private const float SweepAngle = 270;

        public float StrokeWidth
        {
            get { return (float)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }

        public float Progress
        {
            get { return (float)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public Color LineBackgroundColor
        {
            get { return (Color)GetValue(LineBackgroundColorProperty); }
            set { SetValue(LineBackgroundColorProperty, value); }
        }

        public Color ProgressColor
        {
            get { return (Color)GetValue(ProgressColorProperty); }
            set { SetValue(ProgressColorProperty, value); }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            int size = Math.Min(info.Width, info.Height);
            int max = Math.Max(info.Width, info.Height);

            canvas.Translate((max - size) / 2, 0);

            canvas.Clear();
            canvas.Save();
            canvas.RotateDegrees(120, size / 2, size / 2);
            DrawBackgroundCircle(info, canvas);
            DrawProgressCircle(info, canvas);

            canvas.Restore();
        }

        private static void OnPropertyChanged(BindableObject bindable, object oldVal, object newVal)
        {
            var circleProgress = bindable as CircleProgress;
            circleProgress?.InvalidateSurface();
        }

        private void DrawBackgroundCircle(SKImageInfo info, SKCanvas canvas)
        {
            var paint = new SKPaint
            {
                Color = LineBackgroundColor.ToSKColor(),
                StrokeWidth = StrokeWidth,
                IsStroke = true,
                IsAntialias = true,
                StrokeCap = SKStrokeCap.Round
            };

            DrawCircle(info, canvas, paint, SweepAngle);
        }

        private void DrawProgressCircle(SKImageInfo info, SKCanvas canvas)
        {
            float progressAngle = SweepAngle * Progress;

            var paint = new SKPaint
            {
                Color = ProgressColor.ToSKColor(),
                StrokeWidth = StrokeWidth,
                IsStroke = true,
                IsAntialias = true,
                StrokeCap = SKStrokeCap.Round
            };

            DrawCircle(info, canvas, paint, progressAngle);
        }

        private void DrawCircle(SKImageInfo info, SKCanvas canvas, SKPaint paint, float angle)
        {
            int size = Math.Min(info.Width, info.Height);
            float halfWidth = size / 2;
            float halfHeight = size / 2;

            using (SKPath path = new SKPath())
            {
                SKRect rect = new SKRect(
                    StrokeWidth,
                    StrokeWidth,
                    size - StrokeWidth,
                    size - StrokeWidth);
                path.AddArc(rect, StartAngle, angle);

                canvas.DrawPath(path, paint);
            }
        }
    }
}