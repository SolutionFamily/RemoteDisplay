// See https://aka.ms/new-console-template for more information
using Meadow.Foundation;
using Meadow.Foundation.Graphics;

public class DisplayButton : ClickableDisplayControl
{
    public Color ForeColor { get; set; }
    public Color PressedColor { get; set; }
    public Color HighlightColor { get; set; }
    public Color ShadowColor { get; set; }
    public Color TextColor { get; set; }

    public string Text { get; set; }

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

    public override void Draw(MicroGraphics graphics)
    {
        graphics.Stroke = 3;

        if (Pressed)
        {
            graphics.DrawRectangle(Left, Top, Width, Height, PressedColor, true);

            graphics.DrawHorizontalLine(Left, Top, Width, ShadowColor);
            graphics.DrawVerticalLine(Left, Top, Height, ShadowColor);

            graphics.DrawHorizontalLine(Left, Bottom, Width, HighlightColor);
            graphics.DrawVerticalLine(Right, Top, Height, HighlightColor);

            if (!string.IsNullOrEmpty(Text))
            {
                graphics.DrawText(Left + 2 + this.Width / 2, Top + 2 + this.Height / 2, Text, TextColor, alignmentH: HorizontalAlignment.Center, alignmentV: VerticalAlignment.Center);
            }
        }
        else
        {
            graphics.DrawRectangle(Left, Top, Width, Height, ForeColor, true);

            graphics.DrawHorizontalLine(Left, Top, Width, HighlightColor);
            graphics.DrawVerticalLine(Left, Top, Height, HighlightColor);

            graphics.DrawHorizontalLine(Left, Bottom, Width, ShadowColor);
            graphics.DrawVerticalLine(Right, Top, Height, ShadowColor);

            if (!string.IsNullOrEmpty(Text))
            {
                graphics.DrawText(Left + this.Width / 2, Top + this.Height / 2, Text, TextColor, alignmentH: HorizontalAlignment.Center, alignmentV: VerticalAlignment.Center);
            }
        }
    }
}
