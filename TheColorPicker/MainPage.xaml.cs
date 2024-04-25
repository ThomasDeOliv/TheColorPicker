using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace TheColorPicker;

public partial class MainPage : ContentPage
{
    private bool _isRandom;
    private string _colorHexCode;

    public MainPage()
    {
        InitializeComponent();
        _colorHexCode = string.Empty;
        _isRandom = false;
        SetColor(new Color(0, 0, 0));
    }

    protected virtual void SetColor(Color color)
    {
        Container.BackgroundColor = color;
        _colorHexCode = color.ToHex();
        lblHex.Text = $"HEX Value : {_colorHexCode}";
    }

    private void ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (!_isRandom)
        {
            int redValue = (int)RedSlider.Value;
            int greenValue = (int)GreenSlider.Value;
            int blueValue = (int)BlueSlider.Value;

            Color displayedColor = new(redValue, greenValue, blueValue);

            SetColor(displayedColor);
        }
    }

    private void GenerateRandomColor(object sender, EventArgs e)
    {
        _isRandom = true;

        Random rnd = new();

        int redValue = rnd.Next(0, 256);
        int greenValue = rnd.Next(0, 256);
        int blueValue = rnd.Next(0, maxValue: 256);

        Color displayedColor = new(redValue, greenValue, blueValue);

        SetColor(displayedColor);

        RedSlider.Value = redValue;
        GreenSlider.Value = greenValue;
        this.BlueSlider.Value = blueValue;

        _isRandom = false;
    }

    private async void CopyColorString(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(_colorHexCode);
        IToast toast = Toast.Make("Color Copied !", CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
        await toast.Show();
    }
}

