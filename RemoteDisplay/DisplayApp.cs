using Meadow;
using Meadow.Foundation;
using Meadow.Foundation.Graphics;
using Meadow.Graphics;
using Meadow.Simulation;
using MicroLayout;

public class DisplayApp : App<SimulatedMeadow<SimulatedPinout>>
{
    private DisplayScreen _screen = default!;
    private GtkDisplay _display = default!;

    private DisplayLabel title;
    private DisplayButton b1;
    private DisplayButton b2;
    private DisplayButton b3;
    private DisplayButton up;
    private DisplayButton down;

    public override Task Initialize()
    {
        Resolver.Services.Create<MenuRetrievalService>();

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

        title = new DisplayLabel(0, 0, _display.Width - 67, 30);
        title.Text = "[starting]";
        title.ForeColor = Color.White;

        b1 = new DisplayButton(5, 36, _display.Width - 67, 90, theme);
        b1.Text = "[starting]";
        b2 = new DisplayButton(5, 131, _display.Width - 67, 90, theme);
        b2.Text = "[starting]";
        b3 = new DisplayButton(5, 226, _display.Width - 67, 90, theme);
        b3.Text = "[starting]";

        up = new DisplayButton(423, 5, 52, 80, theme);
        up.Image = Image.LoadFromResource("up.bmp");

        down = new DisplayButton(423, 238, 52, 77, theme);
        down.Image = Image.LoadFromResource("down.bmp");

        b1.Clicked += OnButtonClicked;
        b2.Clicked += OnButtonClicked;
        b3.Clicked += OnButtonClicked;
        up.Clicked += OnButtonClicked;
        down.Clicked += OnButtonClicked;

        _screen = new DisplayScreen(_display, _display, theme);

        _screen.Controls.Add(title);
        _screen.Controls.Add(b1);
        _screen.Controls.Add(b2);
        _screen.Controls.Add(b3);
        _screen.Controls.Add(up);
        _screen.Controls.Add(down);

        RefreshMenu();

        return Task.CompletedTask;
    }

    private void RefreshMenu()
    {
        var menu = Resolver.Services.Get<MenuRetrievalService>()?.GetCurrentMenu();
        if (menu != null)
        {
            title.Text = menu.Title;

            b1.Text = menu.Button1.Text;
            b1.Context = menu.Button1.ID;

            b2.Text = menu.Button2.Text;
            b2.Context = menu.Button2.ID;

            b3.Text = menu.Button3.Text;
            b3.Context = menu.Button3.ID;

            up.Context = menu.Up.ID;
            up.Visible = menu.Up.Enabled;

            down.Context = menu.Down.ID;
            down.Visible = menu.Down.Enabled;
        }
    }

    private void OnButtonClicked(object? sender, EventArgs e)
    {
        var b = sender as DisplayButton;
        var id = (int)(b?.Context ?? 0);

        if (id > 0)
        {
            Resolver.Log.Info($"Button {id} clicked!");
        }

        Resolver.Services.Get<MenuRetrievalService>()?.SendClick((int)(b?.Context ?? 0));

        RefreshMenu();
    }

    public override Task Run()
    {
        _display.Run();

        return Task.CompletedTask;
    }
}