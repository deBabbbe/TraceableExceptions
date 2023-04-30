using ClassExtensions;

namespace TestHelper;

public class TempDeleteFile : IDisposable
{
    private readonly byte[] _content;
    private readonly string _filePath;

    public TempDeleteFile(string filePath)
    {
        _filePath = filePath.ExpandEnv();
        _content = File.ReadAllBytes(_filePath);
        File.Delete(_filePath);
    }

    public void Dispose() => File.WriteAllBytes(_filePath, _content);
}