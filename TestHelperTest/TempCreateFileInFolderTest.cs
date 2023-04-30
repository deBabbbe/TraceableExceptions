
using ClassExtensions;
using TestHelper;

namespace TestHelperTest;

[TestFixture]
public class TempCreateFileInFolderTests
{
    [Test]
    public void TempCreateFileInFolderTests_CreatesAndDeletesFileInFolder()
    {
        const string folder = "%temp%/Test/Dings";
        const string path = $"{folder}/Temp.txt";
        FileAssert.DoesNotExist(path.ExpandEnv());
        var expectedText = Guid.NewGuid().ToString();
        using (new TempSetEnvVar("temp", "."))
        using (new TempCreateFileInFolder(path, expectedText))
        {
            FileAssert.Exists(path.ExpandEnv(), expectedText);
            Assert.AreEqual(expectedText, File.ReadAllText(path.ExpandEnv()));
        }
        FileAssert.DoesNotExist(path.ExpandEnv());
        DirectoryAssert.DoesNotExist(folder.ExpandEnv());
    }

    [Test]
    public void TempCreateFileInFolderTests_PassedPathIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new TempCreateFileInFolder(null!, "irrelevant"));
    }

    [Test]
    public void TempCreateFileInFolderTests_PassedPathCannotBeExtracted()
    {
        Assert.Throws<ArgumentException>(() => new TempCreateFileInFolder(Guid.NewGuid().ToString(), "irrelevant"));
    }
}
