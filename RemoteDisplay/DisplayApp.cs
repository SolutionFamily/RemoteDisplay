using Meadow;
using Meadow.Foundation;
using Meadow.Foundation.Graphics;
using Meadow.Graphics;
using Meadow.Simulation;
using MicroLayout;

public record MenuButton
{
    public string Text { get; set; }
    public int ID { get; set; }
    public bool Enabled { get; set; }
}

public record MenuState
{
    public MenuButton Up { get; set; }
    public MenuButton Down { get; set; }
    public MenuButton Button1 { get; set; }
    public MenuButton Button2 { get; set; }
    public MenuButton Button3 { get; set; }
}

public class MenuRetrievalService
{
    public MenuState GetCurrentMenu()
    {
        return null;
    }
}

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

        var title = new DisplayLabel(0, 0, _display.Width - 67, 30);
        title.Text = "Downtime Reason";
        title.ForeColor = Color.White;

        var b1 = new DisplayButton(5, 36, _display.Width - 67, 90, theme);
        b1.Text = "Button 1";
        var b2 = new DisplayButton(5, 131, _display.Width - 67, 90, theme);
        b2.Text = "Button 2";
        var b3 = new DisplayButton(5, 226, _display.Width - 67, 90, theme);
        b3.Text = "Button 3";

        var up = new DisplayButton(423, 5, 52, 80, theme);
        up.Image = Image.LoadFromResource("up.bmp");
        up.Clicked += Up_Clicked;

        var down = new DisplayButton(423, 238, 52, 77, theme);
        down.Image = Image.LoadFromResource("down.bmp");
        down.Clicked += Down_Clicked;

        b1.Clicked += OnButtonClicked;
        b2.Clicked += OnButtonClicked;

        _screen = new DisplayScreen(_display, _display, theme);

        _screen.Controls.Add(title);
        _screen.Controls.Add(b1);
        _screen.Controls.Add(b2);
        _screen.Controls.Add(b3);
        _screen.Controls.Add(up);
        _screen.Controls.Add(down);

        return Task.CompletedTask;
    }

    private int top = 1;

    private void Down_Clicked(object? sender, EventArgs e)
    {
        top++;
    }

    private void Up_Clicked(object? sender, EventArgs e)
    {
        if (top > 1)
        {
            top--;
        }
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