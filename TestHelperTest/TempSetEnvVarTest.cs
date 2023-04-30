
using ClassExtensions;
using TestHelper;

namespace TestHelperTest;

[TestFixture]
public class TempSetEnvVarTest
{
    [Test]
    public void TempSetEnvVarTest_SetsEnvVar()
    {
        const string name = "Schorsch";
        var value = Guid.NewGuid().ToString();

        Assert.AreEqual($"%{name}%".ExpandEnv(), $"%{name}%");
        using (new TempSetEnvVar(name, value))
        {
            Assert.AreEqual($"%{name}%".ExpandEnv(), value);
        }
        Assert.AreEqual($"%{name}%".ExpandEnv(), $"%{name}%");
    }

    [Test]
    public void TempSetEnvVarTest_ResetsExisting()
    {
        const string name = "Schorsch";
        var valueBefore = Guid.NewGuid().ToString();
        var value = Guid.NewGuid().ToString();

        Assert.AreEqual($"%{name}%".ExpandEnv(), $"%{name}%");
        using (new TempSetEnvVar(name, valueBefore))
        {
            Assert.AreEqual($"%{name}%".ExpandEnv(), valueBefore);

            using (new TempSetEnvVar(name, value))
            {
                Assert.AreEqual($"%{name}%".ExpandEnv(), value);
            }

            Assert.AreEqual($"%{name}%".ExpandEnv(), valueBefore);
        }
        Assert.AreEqual($"%{name}%".ExpandEnv(), $"%{name}%");
    }
}
