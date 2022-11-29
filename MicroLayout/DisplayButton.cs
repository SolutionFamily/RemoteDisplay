﻿using Meadow.Foundation;
using Meadow.Foundation.Graphics;

namespace MicroLayout;

public class DisplayButton : ClickableDisplayControl
{
    private const int ButtonDepth = 3; // TODO: make this settable?
    private string _text;
    private Image? _image;
    private Color _foreColor = Color.Gray;
    private Color _pressedColor;
    private Color _highlightColor;
    private Color _shadowColor;
    private Color _textColor;

    public DisplayButton(int left, int top, int width, int height, DisplayTheme? theme = null)
        : base(left, top, width, height)
    {
        if (theme != null)
        {
            this.ForeColor = theme.ForeColor;
            this.PressedColor = theme.PressedColor;
            this.HighlightColor = theme.HighlightColor;
            this.ShadowColor = theme.ShadowColor;
            this.TextColor = theme.TextColor;
        }
    }

    public Color ForeColor
    {
        get => _foreColor;
        set => SetInvalidatingProperty(ref _foreColor, value);
    }

    public Color PressedColor
    {
        get => _pressedColor;
        set => SetInvalidatingProperty(ref _pressedColor, value);
    }

    public Color HighlightColor
    {
        get => _highlightColor;
        set => SetInvalidatingProperty(ref _highlightColor, value);
    }

    public Color ShadowColor
    {
        get => _shadowColor;
        set => SetInvalidatingProperty(ref _shadowColor, value);
    }

    public Color TextColor
    {
        get => _textColor;
        set => SetInvalidatingProperty(ref _textColor, value);
    }

    public string Text
    {
        get => _text;
        set => SetInvalidatingProperty(ref _text, value);
    }

    public Image? Image
    {
        get => _image;
        set => SetInvalidatingProperty(ref _image, value);
    }

    protected override void OnDraw(MicroGraphics graphics)
    {
        graphics.Stroke = ButtonDepth;

        if (Pressed)
        {
            graphics.DrawRectangle(Left, Top, Width, Height, PressedColor, true);

            graphics.DrawHorizontalLine(Left, Top, Width, ShadowColor);
            graphics.DrawVerticalLine(Left, Top, Height, ShadowColor);

            graphics.DrawHorizontalLine(Left, Bottom, Width, HighlightColor);
            graphics.DrawVerticalLine(Right, Top, Height, HighlightColor);

            if (Image != null) // image always wins over text
            {
                graphics.DrawImage(Left + (this.Width - Image.Width) / 2 + ButtonDepth, Top + (this.Height - Image.Height) / 2 + ButtonDepth, Image);
            }
            else if (!string.IsNullOrEmpty(Text))
            {
                graphics.DrawText(Left + ButtonDepth + this.Width / 2, Top + ButtonDepth + this.Height / 2, Text, TextColor, alignmentH: HorizontalAlignment.Center, alignmentV: VerticalAlignment.Center);
            }
        }
        else
        {
            graphics.DrawRectangle(Left, Top, Width, Height, ForeColor, true);

            graphics.DrawHorizontalLine(Left, Top, Width, HighlightColor);
            graphics.DrawVerticalLine(Left, Top, Height, HighlightColor);

            graphics.DrawHorizontalLine(Left, Bottom, Width, ShadowColor);
            graphics.DrawVerticalLine(Right, Top, Height, ShadowColor);

            if (Image != null) // image always wins over text
            {
                graphics.DrawImage(Left + (this.Width - Image.Width) / 2, Top + (this.Height - Image.Height) / 2, Image);
            }
            else if (!string.IsNullOrEmpty(Text))
            {
                graphics.DrawText(Left + this.Width / 2, Top + this.Height / 2, Text, TextColor, alignmentH: HorizontalAlignment.Center, alignmentV: VerticalAlignment.Center);
            }
        }
    }
}
