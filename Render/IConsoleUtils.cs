namespace tider.Render;
public interface IConsoleUtils
{
    string Centered(string text, char backgroundChar = ' ');
    string FilledConsoleLine(string text, FillConsoleLineOptions? options = null);
    void Print(IEnumerable<string> lines, ConsoleAlignment align = ConsoleAlignment.Left);
    string ProgressBar(double portionComplete, ProgressBarOptions? options = null);
}
