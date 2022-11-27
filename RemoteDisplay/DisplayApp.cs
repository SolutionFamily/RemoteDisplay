using Meadow;
using Meadow.Foundation;
using Meadow.Foundation.Graphics;
using Meadow.Graphics;
using Meadow.Simulation;

public class DisplayApp : App<SimulatedMeadow<SimulatedPinout>>
{
    private DisplayScreen _screen = default!;
    private GtkDisplay _display = default!;

    public override Task Initialize()
    {
        // simulating the Adafruit display
        _display = new GtkDisplay(480, 320, ColorType.Format16bppRgb565);

        var theme = new DisplayTheme
        {
            ForeColor = Color.DarkGray,
            HighlightColor = Color.LightGray,
            ShadowColor = Color.Gray,
            TextColor = Color.Black,
            PressedColor = Color.DimGray,
            Font = new Font12x20()
        };

        var b1 = new DisplayButton(5, 5, _display.Width - 10, 150, theme);
        b1.Text = "Button 1";
        var b2 = new DisplayButton(5, 160, _display.Width - 10, 150, theme);
        b2.Text = "Button 2";

        b1.Clicked += OnButtonClicked;
        b2.Clicked += OnButtonClicked;

        _screen = new DisplayScreen(_display, _display, theme);

        _screen.Controls.Add(b1);
        _screen.Controls.Add(b2);

        return Task.CompletedTask;
    }

    private void OnButtonClicked(object? sender, EventArgs e)
    {
        Resolver.Log.Info($"Button clicked!");
    }

    public override Task Run()
    {
        _display.Run();

        return Task.CompletedTask;
    }
}