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
    private float _thumbCenterX, _thumbCenterY;
    private bool _isHoldingThumb;

    public HueColorSlider()
    {
        IgnorePixelScaling = true;
        EnableTouchEvents = true;
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
        
        // calculate the center position of our thumb
        if (_thumbCenterX is 0 || _thumbCenterY is 0)
        {
            _thumbCenterX = _actualWidth / 2;
            _thumbCenterY = _actualHeight / 2;
        }

        DrawComponent();
    }

    protected override void OnTouch(SKTouchEventArgs e)
    {
        base.OnTouch(e);

        switch (e.ActionType)
        {
            case SKTouchAction.Pressed:
                HandlePressedTouchAction(e.Location);
                break;
            case SKTouchAction.Moved:
                HandleMovedTouchAction(e.Location);
                break;
            case SKTouchAction.Released:
            case SKTouchAction.Cancelled:
                HandleReleasedTouchAction();
                break;
        }

        e.Handled = true;
    }

    #region Drawing Methods

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
        // create painting for our thumb inner circle and the border
        using SKPaint fillPaint = new()
        {
            Style = SKPaintStyle.Fill,
            IsStroke = false,
            IsAntialias = true,
            Color = SKColors.White
        };
        using SKPaint borderPaint = new()
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = ThumbBorderThickness,
            IsAntialias = true,
            Color = SKColors.Black
        };

        // draw the thumb's inner circle and border
        _canvas.DrawCircle(_thumbCenterX, _thumbCenterY, ThumbRadius, fillPaint);
        _canvas.DrawCircle(_thumbCenterX, _thumbCenterY, ThumbRadius - ThumbBorderThickness / 2, borderPaint);
    }
    
    #endregion Drawing Methods
    
    #region Touch Events

    private void HandlePressedTouchAction(SKPoint location)
    {
        // check if the touch action is 'on' the thumb (touch location is with the thumb radius of it's center x and y)
        if (Math.Abs(location.X - _thumbCenterX) > ThumbRadius ||
            Math.Abs(location.Y - _thumbCenterY) > ThumbRadius)
            return;

        _isHoldingThumb = true;
    }

    private void HandleMovedTouchAction(SKPoint location)
    {
        // we only want to move the thumb when it's being hold
        if (!_isHoldingThumb)
            return;
        
        // make sure the thumb center x is not outside the slider bar
        var thumbCenterX = Math.Min(location.X, _actualWidth - ThumbRadius);
        _thumbCenterX = Math.Max(thumbCenterX, ThumbRadius);
        
        InvalidateSurface();
    }

    private void HandleReleasedTouchAction()
    {
        _isHoldingThumb = false;
    }
    
    #endregion Touch Events
}