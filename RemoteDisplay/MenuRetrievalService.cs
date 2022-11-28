public class MenuRetrievalService
{
    private int _top = 1;

    public MenuState GetCurrentMenu()
    {
        return new MenuState
        {
            Title = "Downtime Reason",
            Button1 = new MenuButton
            {
                Enabled = true,
                ID = _top,
                Text = $"Reason {_top}"
            },
            Button2 = new MenuButton
            {
                Enabled = true,
                ID = _top + 1,
                Text = $"Reason {_top + 1}"
            },
            Button3 = new MenuButton
            {
                Enabled = true,
                ID = _top + 2,
                Text = $"Reason {_top + 2}"
            },
            Up = new MenuButton
            {
                Enabled = _top > 1,
                ID = -1
            },
            Down = new MenuButton
            {
                Enabled = true,
                ID = -2
            },
        };
    }

    public void SendClick(int buttonID)
    {
        switch (buttonID)
        {
            case -1: // up
                if (_top > 1)
                {
                    _top--;
                }
                break;
            case -2: // down
                _top++;
                break;
        }
    }
}
