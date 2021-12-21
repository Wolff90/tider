DisplayNameAndVersion();

var renderer = new ProgressRenderer(new ConsoleUtils());
var renderObserver = new RenderObserver(renderer);

var startTime = StartTime();
var expectedDuration = ExpectedDuration();


renderer.Render(new TimerProgress(
    startTime, expectedDuration, TimeSpan.Zero));

var clock = Observable.ToObservable(OngoingProgress())
            .Delay(UpdateDelay())
            .Repeat();

clock.Subscribe(renderObserver);

var exit = "";
while (exit != "exit" && exit != "q" && exit != "quit")
{
    exit = Console.ReadLine()?.Trim().ToLowerInvariant() ?? "";
}

return;

IEnumerable<TimerProgress> OngoingProgress()
{
    yield return (new TimerProgress(
    startTime, expectedDuration, TimeSpan.Zero)) with { Elapsed = DateTime.Now - startTime };
}

DateTime StartTime()
{
    var start = default(DateTime?);
    while (start == null)
    {
        System.Console.WriteLine("Start time in HH:mm (or Press Enter for current time):");
        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            start = DateTime.Now;
            continue;
        }
        if (DateTime.TryParseExact(input, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var parsedDate))
        {
            start = parsedDate;
        }
    }
    System.Console.WriteLine($"Start: {start:yyyy.MM.dd HH:mm}");
    return start.Value;
}

TimeSpan ExpectedDuration()
{
    var results = default(TimeSpan?);
    while (results == null)
    {
        System.Console.WriteLine("Expected duration in h:mm (or Press Enter for 8:00 - 8 hours):");
        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            results = TimeSpan.FromHours(8);
            continue;
        }
        if (TimeSpan.TryParseExact(input, "h\\:mm", CultureInfo.InvariantCulture, TimeSpanStyles.None, out var parsedTimeSpan))
        {
            results = parsedTimeSpan;
        }
    }
    System.Console.WriteLine($"Expected duration: {results:c}");
    return results.Value;
}

TimeSpan UpdateDelay()
{
    if (expectedDuration >= TimeSpan.FromMinutes(2)) return TimeSpan.FromSeconds(5);
    return TimeSpan.FromMilliseconds(300);
}

void DisplayNameAndVersion()
{
    var assembly = Assembly.GetExecutingAssembly().GetName();
    var version = assembly.Version;
    Console.Title = $"{assembly.Name}";
    Console.WriteLine($"{assembly.Name} v{version?.Major}.{version?.MajorRevision}");
    Console.WriteLine("");
}