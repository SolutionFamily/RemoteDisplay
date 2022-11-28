using Meadow;
using Meadow.Foundation;
using Meadow.Foundation.Graphics;
using Meadow.Hardware;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MicroLayout;

public class DisplayScreen
{
    public List<IDisplayControl> Controls { get; set; } = new List<IDisplayControl>();

    public Color BackgroundColor { get; set; }

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
        }

        if (theme?.Font != null)
        {
            _graphics.CurrentFont = theme.Font;
        }

        BackgroundColor = theme?.BackgroundColor ?? Color.Black;

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

    private void DrawLoop()
    {
        while (true)
        {
            Resolver.App.InvokeOnMainThread((_) =>
            {
                if (Controls.Any(c => c.IsInvalid))
                {
                    _graphics.Clear(BackgroundColor);

                    foreach (var control in Controls)
                    {
                        // until micrographics supports invalidating regions, we have to invalidate everything when one control needs updating
                        control.Invalidate();
                        control.Refresh(_graphics);
                    }

                    _graphics.Show();
                }
            });

            Thread.Sleep(50);
        }
    }
}
