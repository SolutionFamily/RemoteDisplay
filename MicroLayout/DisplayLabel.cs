using Meadow.Foundation;
using Meadow.Foundation.Graphics;

namespace MicroLayout;

public class DisplayLabel : DisplayControl
{
    private string _text;
    private Color _foreColor = Color.White;
    private Color _backColor = Color.Transparent;

    public DisplayLabel(int left, int top, int width, int height, DisplayTheme? theme = null)
        : base(left, top, width, height)
    {
        if (theme != null)
        {
            this.ForeColor = theme.ForeColor;
        }
    }

    public Color ForeColor
    {
        get => _foreColor;
        set => SetInvalidatingProperty(ref _foreColor, value);
    }

    public Color BackColor
    {
        get => _backColor;
        set => SetInvalidatingProperty(ref _backColor, value);
    }

    public string Text
    {
        get => _text;
        set => SetInvalidatingProperty(ref _text, value);
    }

    protected override void OnDraw(MicroGraphics graphics)
    {
        if (BackColor != Color.Transparent)
        {
            graphics.DrawRectangle(Left, Top, Width, Height, BackColor, true);
        }

        graphics.DrawText(Left + this.Width / 2, Top + this.Height / 2, Text, ForeColor, alignmentH: HorizontalAlignment.Center, alignmentV: VerticalAlignment.Center);
    }
}
