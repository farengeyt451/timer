namespace Tik;

internal static class TimerEngine
{
    public static void Run(int totalSeconds)
    {
        ConsoleRenderer.Init();

        for (int remaining = totalSeconds; remaining >= 0; remaining--)
        {
            ConsoleRenderer.Render(remaining, totalSeconds);

            if (remaining > 0)
                Thread.Sleep(1000);
        }

        ConsoleRenderer.PlayFinish();
    }
}
