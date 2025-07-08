using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace MauiUiChallenge2025_SkiaSharpSlider.Components;

public class HueColorSlider : SKCanvasView
{
    public const double ComponentHeight = 24;
    private const int SliderBarHeight = 4;
    private const float ThumbRadius = 12f;
    private const int ThumbBorderThickness = 4;
    
    private SKCanvas? _canvas;
    private int _actualWidth, _actualHeight;

    public HueColorSlider()
    {
        HeightRequest = ComponentHeight;
    }

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        base.OnPaintSurface(e);

        _canvas = e.Surface.Canvas;
        _actualWidth = e.Info.Width;
        _actualHeight = e.Info.Height;

        DrawComponent();
    }

    private void DrawComponent()
    {
        _canvas.Clear(SKColors.Transparent);

        DrawSliderBar();
        DrawThumb();
    }

    private void DrawSliderBar()
    {
    }

    private void DrawThumb()
    {
    }
}