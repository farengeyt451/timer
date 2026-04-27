namespace Tik;

internal enum TimeUnit { Seconds, Minutes, Hours }

internal static class TimeConverter
{
  public static int ToSeconds(int value, TimeUnit unit) =>
      unit switch
      {
        TimeUnit.Seconds => value,
        TimeUnit.Minutes => value * 60,
        TimeUnit.Hours => value * 3600,
        _ => throw new ArgumentOutOfRangeException(nameof(unit))
      };
}
