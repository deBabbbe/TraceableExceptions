using System.Security.Cryptography;

namespace TestHelper;

public static class Helper
{
    public static int GenerateRandomInt(int min = 1, int max = 10) =>
        RandomNumberGenerator.GetInt32(min, max + 1);

    public static string GenerateRandomString(int numberOfCharacters = 10) =>
        string.Join("", Enumerable.Range(0, numberOfCharacters)
            .Select(_ => (char)RandomNumberGenerator.GetInt32('A', 'z')));

    public static List<T> GenerateRandomList<T>(Func<T> Generator, int numberOfElements) =>
        Enumerable.Range(0, numberOfElements)
            .Select(_ => Generator())
            .ToList();

    public static bool GenerateRandomBool() => RandomNumberGenerator.GetInt32(2) == 0;

    public static DateTime GenerateRandomDateTime() =>
        new DateTime(
            GenerateRandomYear(),
            GenerateRandomMonth(),
            GenerateRandomDay(),
            GenerateRandomHour(),
            GenerateRandomMinuteOrSecond(),
            GenerateRandomMinuteOrSecond());

    public static string GenerateRandomStringGuidWithPrefix(string prefix) =>
        $"{prefix}{Guid.NewGuid().ToString()}";

    public static string ToRandomCase(this string text) =>
        string.Join("", text.Select(ConvertCharToRandomCase));

    public static bool IsInBetween(this int value, int min, int max) =>
        IsValueBiggerMinValue(value, min) && IsValueSmallerMaxValue(value, max);

    public static bool HasAttribute<T>(this T _, Type attribute) where T : class =>
        Attribute.GetCustomAttribute(typeof(T), attribute) != null;

    public static bool HasPropertyWithAttribute<T>(this T _, string propertyName, Type attribute) where T : class
    {
        var property = typeof(T).GetProperty(propertyName);
        return property != null && Attribute.IsDefined(property, attribute);
    }

    private static string ConvertCharToRandomCase(char charText) =>
        RandomNumberGenerator.GetInt32(0, 2) == 1 ?
            charText.ToString().ToUpper() :
            charText.ToString().ToLower();

    private static bool IsValueSmallerMaxValue(int value, int max) =>
        LogOnCondition(value > max, $"{max} is smaller than {value}");

    private static bool IsValueBiggerMinValue(int value, int min) =>
        LogOnCondition(value < min, $"{min} is bigger than {value}");


    private static bool LogOnCondition(bool condition, string logText)
    {
        if (condition)
        {
            Console.WriteLine(logText);
        }
        return !condition;
    }

    private static int GenerateRandomYear() => RandomNumberGenerator.GetInt32(1870, 2301);
    private static int GenerateRandomMonth() => RandomNumberGenerator.GetInt32(1, 13);
    private static int GenerateRandomDay() => RandomNumberGenerator.GetInt32(1, 27);
    private static int GenerateRandomHour() => RandomNumberGenerator.GetInt32(0, 24);
    private static int GenerateRandomMinuteOrSecond() => RandomNumberGenerator.GetInt32(0, 60);
}
