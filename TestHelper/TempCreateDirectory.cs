using ClassExtensions;

namespace TestHelper;

public class TempCreateDirectory : IDisposable
{
    private readonly string _path;

    public TempCreateDirectory(string path)
    {
        _path = path.ExpandEnv();
        Directory.CreateDirectory(_path);
    }

    public void Dispose() => Directory.Delete(_path, recursive: true);
}