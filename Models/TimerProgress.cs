namespace tider.Models;

public record TimerProgress(DateTime Start, TimeSpan ExpectedDuration, TimeSpan Elapsed){
    public double PrecentComplete()
    {
        return Math.Round(ProportionComplete(),2);
    }

    public double ProportionComplete()
    {
        return Elapsed / ExpectedDuration;
    }

    public DateTime End()
    {
        return Start.Add(ExpectedDuration);
    }
}