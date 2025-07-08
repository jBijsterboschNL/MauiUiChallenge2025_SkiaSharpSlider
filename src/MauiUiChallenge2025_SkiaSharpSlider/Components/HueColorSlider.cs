using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace MauiUiChallenge2025_SkiaSharpSlider.Components;

public class HueColorSlider : SKCanvasView
{
    public const double ComponentHeight = 24;
    private const int SliderBarHeight = 4;
    private const int SliderBarRadius = SliderBarHeight / 2;
    private const float ThumbRadius = 12f;
    private const int ThumbBorderThickness = 4;
    
    private readonly SKColor[] _colors = new SKColor[8];
    
    private SKCanvas? _canvas;
    private int _actualWidth, _actualHeight;

    public HueColorSlider()
    {
        IgnorePixelScaling = true;
        HeightRequest = ComponentHeight;
        
        for (var i = 0; i < _colors.Length; i++)
        {
            _colors[i] = SKColor.FromHsl(i * 360f / (_colors.Length - 1), 100, 50);
        }
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
        if (_canvas is null)
            return;
        
        _canvas.Clear(SKColors.Transparent);

        DrawSliderBar();
        DrawThumb();
    }

    private void DrawSliderBar()
    {
        // calculate the size and position of our slider bar
        var barMarginY = (_actualHeight - SliderBarHeight) / 2;
        var barRect = new SKRect(
            left: ThumbRadius,
            top: barMarginY,
            right: _actualWidth - ThumbRadius,
            bottom: barMarginY + SliderBarHeight);
        
        // create painting for our slider bar
        using SKPaint paint = new()
        {
            Style = SKPaintStyle.Fill,
            IsStroke = false,
            IsAntialias = true,
            Shader = SKShader.CreateLinearGradient(
                new SKPoint(barRect.Left, 0),
                new SKPoint(barRect.Right, 0),
                _colors,
                SKShaderTileMode.Repeat)
        };
        
        // draw the slider bar
        _canvas.DrawRoundRect(barRect, SliderBarRadius, SliderBarRadius, paint);
    }

    private void DrawThumb()
    {
    }
}