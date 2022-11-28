using Meadow.Foundation;
using Meadow.Foundation.Graphics;

namespace MicroLayout;

public class DisplayLabel : DisplayControl
{
    public Color ForeColor { get; set; }
    public Color BackColor { get; set; } = Color.Transparent;
    public string Text { get; set; }

    public DisplayLabel(int left, int top, int width, int height, DisplayTheme? theme = null)
        : base(left, top, width, height)
    {
        if (theme != null)
        {
            this.ForeColor = theme.ForeColor;
        }
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
