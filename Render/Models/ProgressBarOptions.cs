namespace tider.Render.Models;
public record ProgressBarOptions(
    string PreBarLabel,
    string PostBarLabel,
    char Fill = '#',
    char Background = ' ');
