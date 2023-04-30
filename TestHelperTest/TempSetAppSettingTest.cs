using System.Configuration;
using TestHelper;

namespace TestHelperTest;

[TestFixture]
public class TempSetAppSettingTest
{
    [Test]
    public void GenerateRandomIntTest_ValueDoesNotExist()
    {
        const string key = "TestEntry";
        const string value = "TestValue";
        Assert.IsNull(ConfigurationManager.AppSettings[key], $"{key} was already set");

        using (new TempSetAppSetting(key, value))
        {
            Assert.IsTrue(ConfigurationManager.AppSettings.AllKeys.Contains(key), $"{key} was not set");
        }

        Assert.IsNull(ConfigurationManager.AppSettings[key], $"{key} was not set to null");
    }

    [Test]
    public void GenerateRandomIntTest_ValueIsOverridden()
    {
        const string key = "TestEntry";
        const string initValue = "TestValue";
        const string value = "TestValue";
        ConfigurationManager.AppSettings[key] = initValue;

        Assert.AreEqual(initValue, ConfigurationManager.AppSettings[key]);

        using (new TempSetAppSetting(key, value))
        {
            Assert.AreEqual(value, ConfigurationManager.AppSettings[key]);
        }

        Assert.AreEqual(initValue, ConfigurationManager.AppSettings[key]);
    }
}