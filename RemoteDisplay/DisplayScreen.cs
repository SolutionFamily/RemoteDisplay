// See https://aka.ms/new-console-template for more information
using Meadow;
using Meadow.Foundation;
using Meadow.Foundation.Graphics;
using Meadow.Graphics;

public class DisplayScreen
{
    public List<IDisplayControl> Controls { get; set; } = new List<IDisplayControl>();

    public Color BackgrounColor { get; set; }

    private IGraphicsDisplay _display;
    private MicroGraphics _graphics;
    private ITouchScreen? _touchScreen;

    public DisplayScreen(IGraphicsDisplay physicalDisplay, ITouchScreen? touchScreen, DisplayTheme? theme)
    {
        _display = physicalDisplay;
        _graphics = new MicroGraphics(_display);
        _touchScreen = touchScreen;

        if (_touchScreen != null)
        {
            _touchScreen.TouchDown += _touchScreen_TouchDown;
            _touchScreen.TouchUp += _touchScreen_TouchUp;
            _touchScreen.TouchClick += _touchScreen_TouchClick;
        }

        if (theme?.Font != null)
        {
            _graphics.CurrentFont = theme.Font;
        }

        BackgrounColor = theme?.BackgroundColor ?? Color.Black;

        new Thread(DrawLoop).Start();
    }

    private void _touchScreen_TouchUp(int x, int y)
    {
        foreach (var control in Controls)
        {
            if (control is IClickableDisplayControl c)
            {
                if (control.Contains(x, y))
                {
                    c.Pressed = false;
                }
            }
        }
    }

    private void _touchScreen_TouchDown(int x, int y)
    {
        foreach (var control in Controls)
        {
            if (control is IClickableDisplayControl c)
            {
                if (control.Contains(x, y))
                {
                    c.Pressed = true;
                }
            }
        }
    }

    private void _touchScreen_TouchClick(int x, int y)
    {

    }

    private void DrawLoop()
    {
        while (true)
        {
            Resolver.App.InvokeOnMainThread((_) =>
            {
                _graphics.Clear(BackgrounColor);

                foreach (var control in Controls)
                {
                    control.Draw(_graphics);
                }

                _graphics.Show();
            });

            Thread.Sleep(50);
        }
    }
}
