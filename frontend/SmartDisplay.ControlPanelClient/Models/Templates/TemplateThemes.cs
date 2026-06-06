namespace SmartDisplay.ControlPanelClient.Models.Templates;

public static class TemplateThemes
{
    public const string Light = "light";
    public const string Dark = "dark";

    public static IReadOnlyList<string> All { get; } = new[]
    {
        Light,
        Dark
    };

    public static string GetDisplayName(string theme)
    {
        return theme switch
        {
            Light => "Светлая",
            Dark => "Тёмная",
            _ => "Светлая"
        };
    }
}
