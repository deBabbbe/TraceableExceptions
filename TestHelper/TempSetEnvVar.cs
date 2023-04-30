namespace TestHelper;

public class TempSetEnvVar : IDisposable
{
    private readonly string _envVarName;
    private readonly string? _envVarValue;

    public TempSetEnvVar(string var, string value)
    {
        _envVarName = var;
        _envVarValue = Environment.GetEnvironmentVariable(var);
        Environment.SetEnvironmentVariable(var, value);
    }

    public void Dispose() =>
        Environment.SetEnvironmentVariable(_envVarName, _envVarValue);
}
