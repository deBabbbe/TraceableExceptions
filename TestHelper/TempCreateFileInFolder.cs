namespace TestHelper;

public class TempCreateFileInFolder : IDisposable
{
    private readonly List<IDisposable> _toDispose = new List<IDisposable>();

    public TempCreateFileInFolder(string path, string content)
    {
        _toDispose.Add(new TempCreateDirectory(Path.GetDirectoryName(path)));
        _toDispose.Add(new TempCreateFile(path, content));
    }

    public void Dispose()
    {
        _toDispose.Reverse();
        _toDispose.ForEach(d => d.Dispose());
    }
}