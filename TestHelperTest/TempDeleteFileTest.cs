
using ClassExtensions;
using TestHelper;

namespace TestHelperTest;

[TestFixture]
public class TempDeleteFileTests
{
    [Test]
    public void TempDeleteFileTests_DeletesFileAndRestoresIt()
    {
        var expectedText = Guid.NewGuid().ToString();
        const string path = "%temp%/Temp.txt";
        using var _ = new TempSetEnvVar("temp", ".");

        File.WriteAllText(path.ExpandEnv(), expectedText);

        using (new TempDeleteFile(path))
        {
            FileAssert.DoesNotExist(path.ExpandEnv(), expectedText);
        }
        FileAssert.Exists(path.ExpandEnv());
        Assert.AreEqual(expectedText, File.ReadAllText(path.ExpandEnv()));
    }
}
