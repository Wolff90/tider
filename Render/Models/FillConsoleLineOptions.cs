namespace tider.Render.Models;
public record FillConsoleLineOptions(
    char Background = ' ',
    int? Length = default(int?));
