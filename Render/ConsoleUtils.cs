

namespace tider.Render;
public class ConsoleUtils : IConsoleUtils
{
    private bool FirstRenderDone { get; set; }
    private int WindowWidth { get; set; }
    public int MaxWriteLength
    {
        get
        {
            return WindowWidth - 3;
        }
    }
    public int CenterOfWriteArea
    {
        get
        {
            return (int)Math.Floor(MaxWriteLength / 2.0);
        }
    }

    public ConsoleUtils()
    {
        WindowWidth = Console.WindowWidth;
    }

    public string Centered(string text, char backgroundChar = ' ')
    {
        var centerOfText = (int)Math.Floor(text.Length / 2.0);
        var printStart = CenterOfWriteArea - centerOfText;
        var preFill = new string(backgroundChar, Math.Max(0, printStart - 1));
        return $"{preFill}{text}";
    }

    public string FilledConsoleLine(string text, FillConsoleLineOptions? options = null)
    {
        var optionsToUse = options ?? new FillConsoleLineOptions(' ', MaxWriteLength);
        var backgroundLength = Math.Max(0, (optionsToUse.Length ?? MaxWriteLength) - text.Length);
        return $"{text}{new string(optionsToUse.Background, backgroundLength)}";
    }

    public string ProgressBar(double portionComplete, ProgressBarOptions? options = null)
    {
        var optionToUse = options ?? new ProgressBarOptions("", "");

        var maxProgressBarLength = MaxWriteLength - optionToUse.PreBarLabel.Length - optionToUse.PostBarLabel.Length - 2;
        var fillLength = (int)Math.Round(portionComplete % 1.0 * maxProgressBarLength);
        var progressBar = new string(optionToUse.Fill, Math.Min(fillLength, maxProgressBarLength));
        return $"{optionToUse.PreBarLabel} {FilledConsoleLine(progressBar, new FillConsoleLineOptions(optionToUse.Background, maxProgressBarLength))} {optionToUse.PostBarLabel}";
    }

    public void Print(IEnumerable<string> lines, ConsoleAlignment align = ConsoleAlignment.Left)
    {
        if (!FirstRenderDone)
        {
            Console.Clear();
            WindowWidth = Console.WindowWidth;
            FirstRenderDone = true;
        }
        else
        {
            var (_, currentLine) = Console.GetCursorPosition();
            Console.SetCursorPosition(0, Math.Max(0, currentLine - (lines.Count() * 2)));
        }

        if (WindowWidth != Console.WindowWidth)
        {
            WindowWidth = Console.WindowWidth;
            Console.Clear();
        }
        foreach (var line in lines)
        {
            switch (align)
            {
                case ConsoleAlignment.Center:
                    Console.WriteLine($" {Centered(line)}");
                    break;
                default:
                    Console.WriteLine($" {FilledConsoleLine(line)}");
                    break;
            }

        }
    }
}
