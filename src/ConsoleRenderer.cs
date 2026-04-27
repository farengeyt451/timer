namespace Tik;

internal static class ConsoleRenderer
{
  private const int BarWidth = 20;
  private static int _row;

  internal static void Init()
  {
    Console.CursorVisible = false;
    _row = Console.CursorTop;
    Console.WriteLine();  // reserve progress bar line
    Console.WriteLine();  // reserve finish animation line
  }

  internal static void Render(int remaining, int total)
  {
    double progress = total > 0 ? 1.0 - (double)remaining / total : 1.0;
    int filled = (int)(progress * BarWidth);
    int empty = BarWidth - filled;

    string bar = new string('█', filled) + new string('░', empty);
    string time = FormatTime(remaining);

    Console.SetCursorPosition(0, _row);
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write($"[{bar}] ");
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write($"{time} remaining   ");
    Console.ResetColor();
  }

  internal static void PlayFinish()
  {
    char[] frames = ['|', '/', '-', '\\'];
    int iterations = 16; // 16 × 125 ms = 2 seconds

    for (int i = 0; i < iterations; i++)
    {
      Console.SetCursorPosition(0, _row + 1);
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.Write($" {frames[i % frames.Length]} ");
      Console.ResetColor();
      Thread.Sleep(125);
    }

    Console.SetCursorPosition(0, _row + 1);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(" Done! Time is up.   ");
    Console.ResetColor();
    Console.CursorVisible = true;
  }

  private static string FormatTime(int totalSeconds)
  {
    int h = totalSeconds / 3600;
    int m = (totalSeconds % 3600) / 60;
    int s = totalSeconds % 60;
    return $"{h:D2}:{m:D2}:{s:D2}";
  }
}
