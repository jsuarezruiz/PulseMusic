using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace PulseMusic.Controls
{
    public class CircleGradientBackground : ContentView
    {
        public static readonly BindableProperty StartColorProperty =
         BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(CircleGradientBackground), Color.Transparent);

        public Color StartColor
        {
            get { return (Color)GetValue(StartColorProperty); }
            set { SetValue(StartColorProperty, value); }
        }

        public static readonly BindableProperty EndColorProperty =
            BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(CircleGradientBackground), Color.Transparent);

        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }

        public static readonly BindableProperty RadiusProperty =
            BindableProperty.Create(nameof(EndColor), typeof(int), typeof(CircleGradientBackground), 1000);

        public int Radius
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public CircleGradientBackground()
        {
            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            canvas.Save();

            var colors = new SKColor[] { StartColor.ToSKColor(), EndColor.ToSKColor() };
            SKPoint startPoint = new SKPoint(info.Width / 2, info.Height / 2);
            var shader = SKShader.CreateRadialGradient(startPoint, Radius, colors, null, SKShaderTileMode.Clamp);

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Shader = shader
            };

            canvas.DrawRect(new SKRect(0, 0, info.Width, info.Height), paint);
        }
    }
}