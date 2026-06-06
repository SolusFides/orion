namespace SmartDisplay.ControlPanelClient.Models.Templates;

public static class TemplateLayouts
{
    public const string Grid = "grid";
    public const string Split = "split";
    public const string Columns3 = "columns_3";
    public const string Columns4 = "columns_4";
    public const string Columns6 = "columns_6";

    public static IReadOnlyList<string> All { get; } = new[]
    {
        Grid,
        Split,
        Columns3,
        Columns4,
        Columns6
    };

    public static string GetDisplayName(string layout)
    {
        return layout switch
        {
            Grid => "Сетка",
            Split => "Разделённый экран",
            Columns3 => "3 колонки",
            Columns4 => "4 колонки",
            Columns6 => "6 колонок",
            _ => "Сетка"
        };
    }
}
