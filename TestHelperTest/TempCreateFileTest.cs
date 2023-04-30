
using ClassExtensions;
using TestHelper;

namespace TestHelperTest;

[TestFixture]
public class TempCreateFileTests
{
    [Test]
    public void TempCreateFileTests_CreatesAndDeletesFile()
    {
        const string path = "%temp%/Temp.txt";
        FileAssert.DoesNotExist(path.ExpandEnv());
        var expectedText = Guid.NewGuid().ToString();
        using (new TempSetEnvVar("temp", "."))
        using (new TempCreateFile(path, expectedText))
        {
            FileAssert.Exists(path.ExpandEnv(), expectedText);
            Assert.AreEqual(expectedText, File.ReadAllText(path.ExpandEnv()));
        }
        FileAssert.DoesNotExist(path.ExpandEnv());
    }
}
