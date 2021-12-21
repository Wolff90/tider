namespace tider.Render;
public class ProgressRenderer : IRenderer<TimerProgress>
{
    private readonly IConsoleUtils ConsoleUtils;

    public ProgressRenderer(IConsoleUtils utils)
    {
        this.ConsoleUtils = utils;
    }

    public void Render(TimerProgress progress)
    {
        ConsoleUtils.Print(OutputLinesFrom(progress), ConsoleAlignment.Center);
    }

    private IEnumerable<string> OutputLinesFrom(TimerProgress progress)
    {
        var percent = progress.Elapsed / progress.ExpectedDuration;
        return new[]{
            ProgressBarFrom(progress),
            PercentCompleteFrom(progress),
            ElapsedFrom(progress),
            RelationToExpectedDuration(progress),
        };
    }

    private string RelationToExpectedDuration(TimerProgress progress)
    {
        var expectedDurationString = $"{progress.ExpectedDuration:h\\:mm}";
        var end = progress.Start.Add(progress.ExpectedDuration);
        if (progress.ExpectedDuration - progress.Elapsed > TimeSpan.Zero)
        {
            return $"Remaining: {progress.ExpectedDuration - progress.Elapsed:h\\:mm}";
        }

        var over = progress.Elapsed - progress.ExpectedDuration;
        return $"Over: {over:h\\:mm}";
    }

    private string ProgressBarFrom(TimerProgress progress)
    {
        var options = new ProgressBarOptions(
                progress.Start.ToString("HH:mm"),
                progress.End().ToString("HH:mm")
            );
        switch (progress.ProportionComplete())
        {
            case > 2.0:
                return ConsoleUtils.ProgressBar(
                    progress.ProportionComplete(), options with
                    {
                        Fill = 'X',
                        Background = '%'
                    });
            case > 1.0:
                return ConsoleUtils.ProgressBar(
                    progress.ProportionComplete(), options with
                    {
                        Fill = '%',
                        Background = '#'
                    });
            default:
                return ConsoleUtils.ProgressBar(
                    progress.ProportionComplete(), options with
                    {
                        Fill = '#',
                        Background = '-'
                    });
        }
    }

    private string PercentCompleteFrom(TimerProgress progress)
    {
        return $"{(int)(progress.PrecentComplete() * 100)} %";
    }

    private string ElapsedFrom(TimerProgress progress)
    {
        return $"Elapsed: {progress.Elapsed:h\\:mm}";
    }
}
