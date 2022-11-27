using Meadow.Foundation.Graphics;

namespace MicroLayout;

public abstract class DisplayControl : IDisplayControl
{
    public int Left { get; set; }
    public int Top { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Bottom => Top + Height;
    public int Right => Left + Width;

    public DisplayControl()
        : this(0, 0, 10, 10)
    {
    }

    public DisplayControl(int left, int top, int width, int height)
    {
        this.Left = left;
        this.Top = top;
        this.Width = width;
        this.Height = height;
    }

    public abstract void Draw(MicroGraphics graphics);
}
