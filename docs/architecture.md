# Architecture

## Overview

Single-project .NET 10 console application. No external NuGet dependencies — only the .NET standard library.

## Layers

```
┌─────────────────────────────┐
│         Entry Point         │  Program.cs — wires everything together
├─────────────────────────────┤
│       Argument Parser       │  Parses and validates -t / -u CLI args
├─────────────────────────────┤
│     Time Unit Converter     │  Converts input value + unit → total seconds
├─────────────────────────────┤
│       Timer Engine          │  Countdown loop, 1-second tick
├─────────────────────────────┤
│      Console Renderer       │  Progress bar, time display, colors, spinner
└─────────────────────────────┘
```

## Components

### `ArgParser`
- Accepts `string[] args`
- Returns a validated `(int value, TimeUnit unit)` tuple or prints error + usage, then exits
- Supported flags: `-t`, `-u`
- Supported units: `s`, `sec`, `seconds`, `m`, `min`, `minutes`, `h`, `hr`, `hours` (case-insensitive)

### `TimeConverter`
- Converts `(value, unit)` → `totalSeconds` (int)
- Validates: value must be > 0

### `TimerEngine`
- Accepts `totalSeconds`
- Runs a `while` loop with `Thread.Sleep(1000)`
- On each tick: calls `ConsoleRenderer.Update(elapsed, total)`
- On completion: calls `ConsoleRenderer.PlayFinish()`

### `ConsoleRenderer`
- **Progress bar:** `[████░░░░░░] HH:MM:SS remaining`
- Uses `Console.SetCursorPosition` to update in-place (no scroll)
- **Colors:** safe ANSI via `Console.ForegroundColor` (no escape codes — cross-platform safe)
- **Finish animation:** ASCII spinner frames (`|`, `/`, `-`, `\`) looped for ~2 seconds, then success message

## File Structure

```
tik/
├── docs/                     ← documentation
│   ├── architecture.md
│   ├── todo.md
│   └── delegation.md
├── src/
│   ├── ArgParser.cs
│   ├── TimeConverter.cs
│   ├── TimerEngine.cs
│   └── ConsoleRenderer.cs
├── Program.cs
├── tik.csproj
└── tik.sln
```

## CLI Usage

```
tik -t 5 -u minutes
tik -t 30 -u s
tik -t 1 -u hour
```

## Error Handling

- Unknown flag → print error + usage hint, exit code 1
- Non-numeric value for `-t` → friendly message, exit code 1
- Value ≤ 0 → friendly message, exit code 1
- Unknown unit → list accepted units, exit code 1
- Missing `-t` or `-u` → show full usage, exit code 1
