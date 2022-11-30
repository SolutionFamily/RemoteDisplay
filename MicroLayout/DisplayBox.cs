using Meadow.Foundation;
using Meadow.Foundation.Graphics;

namespace MicroLayout;

public class DisplayBox : DisplayControl
{
    private Color _foreColor;

    public DisplayBox(int left, int top, int width, int height, DisplayTheme? theme = null)
        : base(left, top, width, height)
    {
    }

    public Color ForeColor
    {
        get => _foreColor;
        set => SetInvalidatingProperty(ref _foreColor, value);
    }

    protected override void OnDraw(MicroGraphics graphics)
    {
        if (ForeColor != Color.Transparent)
        {
            graphics.DrawRectangle(Left, Top, Width, Height, ForeColor, true);
        }
    }
}
