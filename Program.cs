using Tik;

var (value, unit, color) = ArgParser.Parse(args);
int totalSeconds = TimeConverter.ToSeconds(value, unit);
TimerEngine.Run(totalSeconds, color);
