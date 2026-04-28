namespace Tik;

internal static class ArgParser
{
  private const string Usage =
      "Usage: tik -t <value> -u <unit> [-c <color>]\n" +
      "       tik -v | --version\n" +
      "Flags:\n" +
      "  -t, --time <value>    Time value (positive integer)\n" +
      "  -u, --unit <unit>     Time unit\n" +
      "  -c, --color <color>   Progress bar color (optional, default: cyan)\n" +
      "  -v, --version         Display version information\n" +
      "Units:  s, sec, seconds, m, min, minutes, h, hr, hours\n" +
      "Colors: red, green, yellow, blue, cyan, magenta, white";

  public static (int value, TimeUnit unit, ConsoleColor color) Parse(string[] args)
  {
    if (args.Length == 1 && (args[0] == "-v" || args[0] == "--version"))
    {
      var version = typeof(ArgParser).Assembly.GetName().Version;
      Console.WriteLine($"tik version {version?.Major}.{version?.Minor}.{version?.Build}");
      Environment.Exit(0);
    }

    string? rawValue = null;
    string? rawUnit = null;
    string? rawColor = null;

    for (int i = 0; i < args.Length; i++)
    {
      switch (args[i])
      {
        case "-t":
        case "--time":
          if (i + 1 >= args.Length) Fail("Missing value after -t/--time.");
          rawValue = args[++i];
          break;
        case "-u":
        case "--unit":
          if (i + 1 >= args.Length) Fail("Missing value after -u/--unit.");
          rawUnit = args[++i];
          break;
        case "-c":
        case "--color":
          if (i + 1 >= args.Length) Fail("Missing value after -c/--color.");
          rawColor = args[++i];
          break;
        default:
          Fail($"Unknown flag: {args[i]}");
          break;
      }
    }

    if (rawValue is null) Fail("Missing required flag: -t/--time");
    if (rawUnit is null) Fail("Missing required flag: -u/--unit");

    if (!int.TryParse(rawValue, out int value))
      Fail($"Invalid value for -t/--time: '{rawValue}' is not a whole number.");

    if (value <= 0)
      Fail($"Value for -t/--time must be greater than 0.");

    if (!TryParseUnit(rawUnit!, out TimeUnit unit))
      Fail($"Unknown unit: '{rawUnit}'. Accepted: s, sec, seconds, m, min, minutes, h, hr, hours");

    ConsoleColor color = ConsoleColor.Cyan;
    if (rawColor is not null && !TryParseColor(rawColor, out color))
      Fail($"Unknown color: '{rawColor}'. Accepted: red, green, yellow, blue, cyan, magenta, white");

    return (value, unit, color);
  }

  private static bool TryParseUnit(string raw, out TimeUnit unit)
  {
    unit = raw.ToLowerInvariant() switch
    {
      "s" or "sec" or "seconds" => TimeUnit.Seconds,
      "m" or "min" or "minutes" => TimeUnit.Minutes,
      "h" or "hr" or "hours" => TimeUnit.Hours,
      _ => (TimeUnit)(-1)
    };
    return (int)unit >= 0;
  }

  private static bool TryParseColor(string raw, out ConsoleColor color)
  {
    color = raw.ToLowerInvariant() switch
    {
      "red" => ConsoleColor.Red,
      "green" => ConsoleColor.Green,
      "yellow" => ConsoleColor.Yellow,
      "blue" => ConsoleColor.Blue,
      "cyan" => ConsoleColor.Cyan,
      "magenta" => ConsoleColor.Magenta,
      "white" => ConsoleColor.White,
      _ => (ConsoleColor)(-1)
    };
    return (int)color >= 0;
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
