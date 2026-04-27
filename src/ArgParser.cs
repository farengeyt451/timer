namespace Tik;

internal static class ArgParser
{
    private const string Usage =
        "Usage: timer -t <value> -u <unit>\n" +
        "       Units: s, sec, seconds, m, min, minutes, h, hr, hours";

    public static (int value, TimeUnit unit) Parse(string[] args)
    {
        string? rawValue = null;
        string? rawUnit  = null;

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "-t":
                    if (i + 1 >= args.Length) Fail("Missing value after -t.");
                    rawValue = args[++i];
                    break;
                case "-u":
                    if (i + 1 >= args.Length) Fail("Missing value after -u.");
                    rawUnit = args[++i];
                    break;
                default:
                    Fail($"Unknown flag: {args[i]}");
                    break;
            }
        }

        if (rawValue is null) Fail("Missing required flag: -t");
        if (rawUnit  is null) Fail("Missing required flag: -u");

        if (!int.TryParse(rawValue, out int value))
            Fail($"Invalid value for -t: '{rawValue}' is not a whole number.");

        if (value <= 0)
            Fail($"Value for -t must be greater than 0.");

        if (!TryParseUnit(rawUnit!, out TimeUnit unit))
            Fail($"Unknown unit: '{rawUnit}'. Accepted: s, sec, seconds, m, min, minutes, h, hr, hours");

        return (value, unit);
    }

    private static bool TryParseUnit(string raw, out TimeUnit unit)
    {
        unit = raw.ToLowerInvariant() switch
        {
            "s" or "sec"  or "seconds" => TimeUnit.Seconds,
            "m" or "min"  or "minutes" => TimeUnit.Minutes,
            "h" or "hr"   or "hours"   => TimeUnit.Hours,
            _                          => (TimeUnit)(-1)
        };
        return (int)unit >= 0;
    }

    [System.Diagnostics.CodeAnalysis.DoesNotReturn]
    private static void Fail(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine($"Error: {message}");
        Console.ResetColor();
        Console.Error.WriteLine();
        Console.Error.WriteLine(Usage);
        Environment.Exit(1);
    }
}
