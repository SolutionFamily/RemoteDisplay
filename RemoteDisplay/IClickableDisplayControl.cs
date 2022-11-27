// See https://aka.ms/new-console-template for more information
public interface IClickableDisplayControl : IDisplayControl
{
    public event EventHandler Clicked;

    public bool Pressed { get; set; }
}
