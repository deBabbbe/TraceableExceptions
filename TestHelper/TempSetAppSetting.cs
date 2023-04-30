using System.Configuration;

namespace TestHelper;

public class TempSetAppSetting : IDisposable
{
    private readonly string _key;
    private readonly string? _initialValue;

    public TempSetAppSetting(string key, string value)
    {
        _key = key;
        _initialValue = ConfigurationManager.AppSettings[key];
        ConfigurationManager.AppSettings[key] = value;
    }

    public void Dispose() => ConfigurationManager.AppSettings[_key] = _initialValue;
}