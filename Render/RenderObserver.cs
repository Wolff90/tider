namespace tider.Render;
public class RenderObserver : IObserver<TimerProgress>
{
    private readonly IRenderer<TimerProgress> _renderer;

    public RenderObserver(IRenderer<TimerProgress> renderer)
    {
        this._renderer = renderer;
    }
    public void OnCompleted()
    {
        return;
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(TimerProgress progress)
    {
        _renderer.Render(progress);
    }

}
