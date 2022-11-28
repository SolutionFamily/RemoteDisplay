using Meadow.Foundation.Graphics;

namespace MicroLayout;

public abstract class DisplayControl : IDisplayControl
{
    private int _left;

    // TODO: allow region invalidation?
    public bool IsInvalid { get; protected set; }

    public void Invalidate()
    {
        IsInvalid = true;
    }

    public int Left
    {
        get => _left;
        set
        {
            _left = value;
            Invalidate();
        }
    }

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

        IsInvalid = true;
    }

    public void Refresh(MicroGraphics graphics)
    {
        if (IsInvalid)
        {
            OnDraw(graphics);
            IsInvalid = false;
        }
    }

    protected abstract void OnDraw(MicroGraphics graphics);
}
